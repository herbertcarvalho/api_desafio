using Backend.Erp.Skeleton.Application.DTOs.Request.Authorization;
using Backend.Erp.Skeleton.Application.Extensions;
using FluentValidation;
using System.Text.RegularExpressions;
using static Backend.Erp.Skeleton.Application.Helpers.ValidationsHelper;
using static Backend.Erp.Skeleton.Application.Validators.Authorization.AuthorizationConstants;

namespace Backend.Erp.Skeleton.Application.Validators.Authorization
{
    public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserRequestValidator()
        {
            RuleFor(x => x.email)
                .NotEmpty()
                .WithMessage(NotEmptyMessage(email))
                .NotNull()
                .WithMessage(NotNullMessage(email))
                .Must(x => x.IsEmail())
                .WithMessage(InvalidMessage(email));

            RuleFor(x => x.password)
                .NotEmpty()
                .WithMessage("A senha não pode ser vazia.");

            RuleFor(x => x.password)
                .MinimumLength(6)
                .WithMessage("A senha deve conter mais de 6 caracteres.");

            RuleFor(x => x.password)
                .Matches(new Regex(@"[A-Z]")).WithMessage("É necessário inserir uma letra maiúscula.");

            RuleFor(x => x.password)
                .Matches(new Regex(@"[0-9]")).WithMessage("É necessário inserir um número.");

            RuleFor(x => x.password)
                .Matches(new Regex(@"[a-zA-Z]")).WithMessage("É necessário inserir uma letra.");

            RuleFor(x => x.password)
                .Matches(new Regex(@"[!@#$%^&*(),.?""':{}|<>]")).WithMessage("É necessário inserir um símbolo.");

            RuleFor(x => x.cpf)
                .NotEmpty()
                .WithMessage(NotEmptyMessage(cpf))
                .NotNull()
                .WithMessage(NotNullMessage(cpf))
                .Must(x => x.IsValidCPF())
                .WithMessage(InvalidMessage(cpf));

            RuleFor(x => x.cnpj)
               .Must(x => x is null || x.IsValidCNPJ())
               .WithMessage(InvalidMessage(cnpj));

            RuleFor(x => x.companyName)
                .NotEmpty()
                .WithMessage(NotEmptyMessage(companyName))
                .NotNull()
                .WithMessage(NotNullMessage(companyName))
                .Must(x => x.IsValidStringWithLength(100))
                .WithMessage(InvalidMessage(companyName))
                .When(x => x.cnpj is not null);

            RuleFor(x => x.name)
                .NotEmpty()
                .WithMessage(NotEmptyMessage(name))
                .NotNull()
                .WithMessage(NotNullMessage(name))
                .Must(x => x.IsValidStringWithLength(100))
                .WithMessage(InvalidMessage(name));
        }
    }
}
