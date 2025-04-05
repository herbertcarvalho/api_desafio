using Backend.Erp.Skeleton.Application.DTOs;
using Backend.Erp.Skeleton.Application.DTOs.Request.Category;
using Backend.Erp.Skeleton.Application.Exceptions;
using Backend.Erp.Skeleton.Domain.Extensions;
using Backend.Erp.Skeleton.Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Application.Commands.Category
{
    public record UpdateCategoryCommand(UserClaim UserClaim, UpdateCategoryQuery Query, UpdateCategoryRequest Request) : IRequest<Result<string>>;

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<string>>
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryCommandHandler(
            ICategoriesRepository categoriesRepository,
            IUnitOfWork unitOfWork)
        {
            _categoriesRepository = categoriesRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoriesRepository.GetByIdAsync(request.Query.Id)
                ?? throw new ApiException("Não foi possível encontrar a categoria selecionada.");

            if (await _categoriesRepository.Any(request.Request.Name))
                throw new ApiException("Essa categoria já foi cadastrada.");

            category.Name = request.Request.Name;
            category.IdLastModifiedBy = request.UserClaim.IdUser;
            category.UpdatedAt = DateTime.UtcNow;

            await _categoriesRepository.UpdateAsync(category);
            await _unitOfWork.SaveChangesAsync(default);

            return Result<string>.Success(null, "Categoria atualizada com sucesso");
        }
    }
}
