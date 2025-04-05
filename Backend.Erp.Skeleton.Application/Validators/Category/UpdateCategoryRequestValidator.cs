using Backend.Erp.Skeleton.Application.DTOs.Request.Category;
using Backend.Erp.Skeleton.Application.Extensions;
using FluentValidation;
using static Backend.Erp.Skeleton.Application.Helpers.ValidationsHelper;
using static Backend.Erp.Skeleton.Application.Validators.Category.CategoriesConstants;

namespace Backend.Erp.Skeleton.Application.Validators.Category
{
    public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
    {
        public UpdateCategoryRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(NotEmptyMessage(name))
                .NotNull()
                .WithMessage(NotNullMessage(name))
                .Must(x => x.IsValidStringWithLength(100, alphaNumeric: true))
                .WithMessage(StringLesserThanInput(name, 100, alphaNumeric: true));
        }
    }
}