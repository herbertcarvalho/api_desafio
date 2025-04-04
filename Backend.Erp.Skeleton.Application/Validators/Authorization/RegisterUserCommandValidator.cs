using Backend.Erp.Skeleton.Application.Commands.Authorization;
using FluentValidation;

namespace Backend.Erp.Skeleton.Application.Validators.Authorization
{
    internal class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Request)
                .NotNull();
        }
    }
}
