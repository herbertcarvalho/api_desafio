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

            RuleFor(x => x.Cpf)
                .NotEmpty()
                .WithMessage(NotEmptyMessage(cpf))
                .NotNull()
                .WithMessage(NotNullMessage(cpf))
                .Must(x => x.IsValidCPF())
                .WithMessage(InvalidMessage(cpf));

            RuleFor(x => x.Cnpj)
               .Must(x => x is null || x.IsValidCNPJ())
               .WithMessage(InvalidMessage(cnpj));

            RuleFor(x => x.CompanyName)
                .NotEmpty()
                .WithMessage(NotEmptyMessage(companyName))
                .NotNull()
                .WithMessage(NotNullMessage(companyName))
                .Must(x => x.IsValidStringWithLength(100))
                .WithMessage(StringLesserThanInput(companyName, 100))
                .When(x => x.Cnpj is not null);

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(NotEmptyMessage(name))
                .NotNull()
                .WithMessage(NotNullMessage(name))
                .Must(x => x.IsValidStringWithLength(100))
                .WithMessage(InvalidMessage(name));
        }
    }
}
