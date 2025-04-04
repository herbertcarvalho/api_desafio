using Backend.Erp.Skeleton.Application.Commands.Authorization;
using FluentValidation;

namespace Backend.Erp.Skeleton.Application.Validators.Authorization
{
    internal class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.Request)
                .NotNull();
        }
    }
}
