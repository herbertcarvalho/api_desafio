using Backend.Erp.Skeleton.Application.Commands.Product;
using FluentValidation;

namespace Backend.Erp.Skeleton.Application.Validators.Product
{
    internal class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Request)
                .NotNull();
        }
    }
}
