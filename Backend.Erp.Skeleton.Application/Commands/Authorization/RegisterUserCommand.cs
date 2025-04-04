using Backend.Erp.Skeleton.Application.DTOs.Request.Authorization;
using Backend.Erp.Skeleton.Application.DTOs.Response.Authorization;
using Backend.Erp.Skeleton.Application.Exceptions;
using Backend.Erp.Skeleton.Application.Extensions;
using Backend.Erp.Skeleton.Application.Helpers.Interfaces;
using Backend.Erp.Skeleton.Domain.Entities;
using Backend.Erp.Skeleton.Domain.Enums;
using Backend.Erp.Skeleton.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Application.Commands.Authorization
{
    public record RegisterUserCommand(RegisterUserRequest Request) : IRequest<Result<UsuarioToken>>;

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<UsuarioToken>>
    {
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly SignInManager<IdentityUser<int>> _signInManager;
        private readonly IAuthHelper _authHelper;
        private readonly IPersonsRepository _personsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyRepository _companyRepository;

        public RegisterUserCommandHandler(
            UserManager<IdentityUser<int>> userManager,
            SignInManager<IdentityUser<int>> signInManager,
            IAuthHelper authHelper,
            IUnitOfWork unitOfWork,
            IPersonsRepository personsRepository,
            ICompanyRepository companyRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authHelper = authHelper;
            _unitOfWork = unitOfWork;
            _personsRepository = personsRepository;
            _companyRepository = companyRepository;
        }

        public async Task<Result<UsuarioToken>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new IdentityUser<int>
            {
                UserName = request.Request.email,
                Email = request.Request.email,
                EmailConfirmed = true,
            };

            var identityEmail = await _userManager.FindByEmailAsync(request.Request.email);
            if (identityEmail is not null)
                throw new ApiException("Esse email já foi cadastrado.");

            var existsCpf = await _personsRepository.Any(request.Request.cpf);
            if (existsCpf)
                throw new ApiException("Esse cpf já foi cadastrado.");

            var existsCnpj = await _companyRepository.Any(request.Request.cnpj);
            if (existsCnpj)
                throw new ApiException("Esse cnpj já foi cadastrado.");

            var resultCreateUser = await _userManager.CreateAsync(user, request.Request.password);
            if (!resultCreateUser.Succeeded)
                throw new ApiException($"Não foi possível cadastrar o novo usuário. {string.Join(',', resultCreateUser.Errors.Select(x => x.Description))}");

            identityEmail = await _userManager.FindByEmailAsync(request.Request.email);

            var addNewPerson = new Persons()
            {
                cpf = request.Request.cpf,
                idUser = identityEmail.Id,
                name = request.Request.name,
                IdUserType = request.Request.cnpj is null ? (int)UserTypeEnum.Client : (int)UserTypeEnum.Company
            };

            var result = await _userManager.AddToRoleAsync(user, addNewPerson.IdUserType.GetEnumDescription<UserTypeEnum>());
            if (!result.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                throw new ApiException("Não foi possível cadastrar o tipo de usuário");
            }

            var addNewCompany = new Company()
            {
                cnpj = request.Request.cnpj,
                createdAt = DateTime.UtcNow,
                name = request.Request.companyName
            };

            using (var transaction = await _unitOfWork.BeginTransaction())
            {
                try
                {
                    if (addNewPerson.IdUserType == (int)UserTypeEnum.Company)
                    {
                        await _companyRepository.AddAsync(addNewCompany);
                        await _unitOfWork.SaveChangesAsync();
                        addNewPerson.idCompany = addNewCompany.id;
                    }

                    await _personsRepository.AddAsync(addNewPerson);
                    await _unitOfWork.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw new ApiException("Não foi possível processar a requisição.");
                }
            }

            await _signInManager.SignInAsync(user, false);

            var response = await _authHelper.GenerateToken(addNewPerson);

            return Result<UsuarioToken>.Success(response, "Usuário criado com sucesso.");
        }
    }
}
