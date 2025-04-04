using Backend.Erp.Skeleton.Application.DTOs.Request.Authorization;
using Backend.Erp.Skeleton.Application.DTOs.Response.Authorization;
using Backend.Erp.Skeleton.Application.Exceptions;
using Backend.Erp.Skeleton.Application.Extensions;
using Backend.Erp.Skeleton.Application.Helpers.Interfaces;
using Backend.Erp.Skeleton.Domain.Enums;
using Backend.Erp.Skeleton.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Application.Commands
{
    public record RegisterUserCommand(RegisterUserRequest Model) : IRequest<Result<UsuarioToken>>;

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<UsuarioToken>>
    {
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly SignInManager<IdentityUser<int>> _signInManager;
        private readonly IAuthHelper _authHelper;
        //private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserCommandHandler(
            UserManager<IdentityUser<int>> userManager,
            SignInManager<IdentityUser<int>> signInManager,
            IAuthHelper authHelper,
            IUnitOfWork unitOfWork
           /* IPersonRepository personRepository*/)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authHelper = authHelper;
            _unitOfWork = unitOfWork;
            //_personRepository = personRepository;
        }

        public async Task<Result<UsuarioToken>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new IdentityUser<int>
            {
                UserName = request.Model.email,
                Email = request.Model.email,
                EmailConfirmed = true,
            };

            var identityEmail = await _userManager.FindByEmailAsync(request.Model.email);
            if (identityEmail is not null)
                throw new ApiException("Esse email já foi cadastrado.");

            /*var existsCpf = await _personRepository.AnyByCpf(request.Model.cpf);
            if (existsCpf)
                throw new ApiException("Esse cpf já foi cadastrado.");*/

            var resultCreateUser = await _userManager.CreateAsync(user, request.Model.password);
            if (!resultCreateUser.Succeeded)
                throw new Exception($"Não foi possível cadastrar o novo usuário. {String.Join(',', resultCreateUser.Errors.Select(x => x.Description))}");

            identityEmail = await _userManager.FindByEmailAsync(request.Model.email);

            var result = await _userManager.AddToRoleAsync(user, request.Model.idUserType.GetEnumDescription<UserTypeEnum>());
            if (!result.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                throw new Exception("Não foi possível cadastrar o tipo de usuário");
            }

            /*var person = _mapper.Map<Person>(request.Model);

            person.Address.IdCity = city.Id;
            person.IdUser = identityEmail.Id;
            person.IdUserType = request.Model.IdUserType.IntValue();

            await _personRepository.AddAsync(person);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _signInManager.SignInAsync(user, false);

            var response = await _authHelper.GenerateToken(person);
            */
            return Result<UsuarioToken>.Success(null, "Usuário criado com sucesso.");
        }
    }
}
