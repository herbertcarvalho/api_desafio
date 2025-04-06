using Backend.Erp.Skeleton.Application.DTOs.Request.Authorization;
using Backend.Erp.Skeleton.Application.DTOs.Response.Authorization;
using Backend.Erp.Skeleton.Application.Exceptions;
using Backend.Erp.Skeleton.Application.Helpers.Interfaces;
using Backend.Erp.Skeleton.Domain.Extensions;
using Backend.Erp.Skeleton.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Application.Commands.Authorization
{
    public record LoginCommand(LoginRequest Request) : IRequest<Result<UsuarioToken>>;

    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<UsuarioToken>>
    {
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly SignInManager<IdentityUser<int>> _signInManager;
        private readonly IAuthHelper _authHelper;
        private readonly IPersonsRepository _personsRepository;

        public LoginCommandHandler(
            SignInManager<IdentityUser<int>> signInManager,
            IAuthHelper authHelper,
            UserManager<IdentityUser<int>> userManager,
            IPersonsRepository personsRepository)
        {
            _signInManager = signInManager;
            _authHelper = authHelper;
            _userManager = userManager;
            _personsRepository = personsRepository;
        }

        public async Task<Result<UsuarioToken>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Request.Email,
                request.Request.Password, isPersistent: false, lockoutOnFailure: true);

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                    throw new ApiException($"Login Bloqueado por excesso de tentativas.");

                if (result.IsNotAllowed)
                    throw new ApiException($"Login Bloqueado.");

                throw new ApiException($"Credenciais Inválidas.");
            }

            var identityUser = await _userManager.FindByEmailAsync(request.Request.Email);

            var person = await _personsRepository.Get(identityUser.Id)
                ?? throw new ApiException("Usuário não identificado.");

            var response = await _authHelper.GenerateToken(person);

            return Result<UsuarioToken>.Success(response, "Login realizado com sucesso.");
        }
    }
}
