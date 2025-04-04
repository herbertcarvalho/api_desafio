using Backend.Erp.Skeleton.Application.DTOs.Request.Authorization;
using Backend.Erp.Skeleton.Application.DTOs.Response.Authorization;
using Backend.Erp.Skeleton.Application.Exceptions;
using Backend.Erp.Skeleton.Application.Extensions;
using Backend.Erp.Skeleton.Application.Helpers.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Application.Commands
{
    public record LoginUserCommand(LoginUserRequest Model) : IRequest<Result<UsuarioToken>>;

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<UsuarioToken>>
    {
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly SignInManager<IdentityUser<int>> _signInManager;
        private readonly IAuthHelper _authHelper;

        public LoginUserCommandHandler(
            SignInManager<IdentityUser<int>> signInManager,
            IAuthHelper authHelper,
            UserManager<IdentityUser<int>> userManager)
        {
            _signInManager = signInManager;
            _authHelper = authHelper;
            _userManager = userManager;
        }

        public async Task<Result<UsuarioToken>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Model.email,
                request.Model.password, isPersistent: false, lockoutOnFailure: true);

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                    throw new ApiException($"Login Bloqueado por excesso de tentativas.");

                if (result.IsNotAllowed)
                    throw new ApiException($"Login Bloqueado.");

                throw new ApiException($"Credenciais Inválidas.");
            }

            var identityUser = await _userManager.FindByEmailAsync(request.Model.email);

            /*var person = await _personRepository.GetByIdUser(identityUser.Id);
            if (person.IsDeleted)
                throw new ApiException($"Usuário bloqueado.");

            var response = await _authHelper.GenerateToken(person);
            */
            return Result<UsuarioToken>.Success(null, "Login realizado com sucesso.");
        }
    }
}
