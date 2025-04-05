using Backend.Erp.Skeleton.Application.Commands.Product;
using FluentValidation;

namespace Backend.Erp.Skeleton.Application.Validators.Product
{
    internal class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Request)
                .NotNull();
        }
    }
}
