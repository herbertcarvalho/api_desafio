using Backend.Erp.Skeleton.Application.DTOs.Request.Authorization;
using Backend.Erp.Skeleton.Application.DTOs.Response.Authorization;
using Backend.Erp.Skeleton.Application.Exceptions;
using Backend.Erp.Skeleton.Application.Extensions;
using Backend.Erp.Skeleton.Application.Helpers.Interfaces;
using Backend.Erp.Skeleton.Domain.Entities;
using Backend.Erp.Skeleton.Domain.Enums;
using Backend.Erp.Skeleton.Domain.Extensions;
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
                UserName = request.Request.Email,
                Email = request.Request.Email,
                EmailConfirmed = true,
            };

            var identityEmail = await _userManager.FindByEmailAsync(request.Request.Email);
            if (identityEmail is not null)
                throw new ApiException("Esse email já foi cadastrado.");

            var existsCpf = await _personsRepository.Any(request.Request.Cpf);
            if (existsCpf)
                throw new ApiException("Esse cpf já foi cadastrado.");

            var existsCnpj = await _companyRepository.Any(request.Request.Cnpj);
            if (existsCnpj)
                throw new ApiException("Esse cnpj já foi cadastrado.");

            var resultCreateUser = await _userManager.CreateAsync(user, request.Request.Password);
            if (!resultCreateUser.Succeeded)
                throw new ApiException($"Não foi possível cadastrar o novo usuário. {string.Join(',', resultCreateUser.Errors.Select(x => x.Description))}");

            identityEmail = await _userManager.FindByEmailAsync(request.Request.Email);

            var addNewPerson = new Persons()
            {
                Cpf = request.Request.Cpf,
                IdUser = identityEmail.Id,
                Name = request.Request.Name,
                IdUserType = request.Request.Cnpj is null ? (int)UserTypeEnum.Client : (int)UserTypeEnum.Company
            };

            var result = await _userManager.AddToRoleAsync(user, addNewPerson.IdUserType.GetEnumDescription<UserTypeEnum>());
            if (!result.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                throw new ApiException("Não foi possível cadastrar o tipo de usuário");
            }

            var addNewCompany = new Company()
            {
                Cnpj = request.Request.Cnpj,
                Name = request.Request.CompanyName
            };

            using (var transaction = await _unitOfWork.BeginTransaction())
            {
                try
                {
                    if (addNewPerson.IdUserType == (int)UserTypeEnum.Company)
                    {
                        await _companyRepository.AddAsync(addNewCompany);
                        await _unitOfWork.SaveChangesAsync(default);
                        addNewPerson.IdCompany = addNewCompany.Id;
                    }

                    await _personsRepository.AddAsync(addNewPerson);
                    await _unitOfWork.SaveChangesAsync(default);

                    await transaction.CommitAsync(default);
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(default);
                    throw new ApiException("Não foi possível processar a requisição.");
                }
            }

            await _signInManager.SignInAsync(user, false);

            var response = await _authHelper.GenerateToken(addNewPerson);

            return Result<UsuarioToken>.Success(response, "Usuário criado com sucesso.");
        }
    }
}
