using Backend.Erp.Skeleton.Application.Commands.Category;
using FluentValidation;

namespace Backend.Erp.Skeleton.Application.Validators.Category
{
    internal class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.Request)
                .NotNull();
        }
    }
}
