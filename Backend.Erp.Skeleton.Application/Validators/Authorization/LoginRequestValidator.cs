using Backend.Erp.Skeleton.Application.DTOs.Request.Authorization;
using Backend.Erp.Skeleton.Application.Extensions;
using FluentValidation;
using System.Text.RegularExpressions;
using static Backend.Erp.Skeleton.Application.Helpers.ValidationsHelper;
using static Backend.Erp.Skeleton.Application.Validators.Authorization.AuthorizationConstants;

namespace Backend.Erp.Skeleton.Application.Validators.Authorization
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(NotEmptyMessage(email))
                .NotNull()
                .WithMessage(NotNullMessage(email))
                .Must(x => x.IsEmail())
                .WithMessage(InvalidMessage(email));

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("A senha não pode ser vazia.");

            RuleFor(x => x.Password)
                .MinimumLength(6)
                .WithMessage("A senha deve conter mais de 6 caracteres.");

            RuleFor(x => x.Password)
                .Matches(new Regex(@"[A-Z]")).WithMessage("É necessário inserir uma letra maiúscula.");

            RuleFor(x => x.Password)
                .Matches(new Regex(@"[0-9]")).WithMessage("É necessário inserir um número.");

            RuleFor(x => x.Password)
                .Matches(new Regex(@"[a-zA-Z]")).WithMessage("É necessário inserir uma letra.");

            RuleFor(x => x.Password)
                .Matches(new Regex(@"[!@#$%^&*(),.?""':{}|<>]")).WithMessage("É necessário inserir um símbolo.");
        }
    }
}
