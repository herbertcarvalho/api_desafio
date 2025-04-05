using Backend.Erp.Skeleton.Application.Commands.Category;
using FluentValidation;

namespace Backend.Erp.Skeleton.Application.Validators.Category
{
    internal class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Request)
                .NotNull();
        }
    }
}
