using Backend.Erp.Skeleton.Application.DTOs.Request.Product;
using Backend.Erp.Skeleton.Application.Extensions;
using FluentValidation;
using static Backend.Erp.Skeleton.Application.Helpers.ValidationsHelper;
using static Backend.Erp.Skeleton.Application.Validators.Product.ProductsConstants;

namespace Backend.Erp.Skeleton.Application.Validators.Product
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(x => x.IdCategory)
                .Must(x => x.GreaterThanZero())
                .WithMessage(GreaterThanZeroMessage(idCategory));

            RuleFor(x => x.Price)
                .Must(x => x.GreaterThanZero())
                .WithMessage(GreaterThanZeroMessage(price));

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(NotEmptyMessage(name))
                .NotNull()
                .WithMessage(NotNullMessage(name))
                .Must(x => x.IsValidStringWithLength(100, alphaNumeric: true))
                .WithMessage(StringLesserThanInput(name, 100, alphaNumeric: true));

            RuleFor(x => x.Img)
                .Must(x => x.IsBase64StringAndLengthValid())
                .WithMessage(Base64Invalid());
        }
    }
}