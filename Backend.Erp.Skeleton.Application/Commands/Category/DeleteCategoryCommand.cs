using Backend.Erp.Skeleton.Application.DTOs.Request.Category;
using Backend.Erp.Skeleton.Application.Exceptions;
using Backend.Erp.Skeleton.Domain.Extensions;
using Backend.Erp.Skeleton.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Application.Commands.Category
{
    public record DeleteCategoryCommand(DeleteCategoryQuery Query) : IRequest<Result<string>>;

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<string>>
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryCommandHandler(
            ICategoriesRepository categoriesRepository,
            IUnitOfWork unitOfWork,
            IProductsRepository productsRepository)
        {
            _categoriesRepository = categoriesRepository;
            _unitOfWork = unitOfWork;
            _productsRepository = productsRepository;
        }

        public async Task<Result<string>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoriesRepository.GetByIdAsync(request.Query.Id)
                ?? throw new ApiException("Não foi possível encontrar a categoria selecionada.");

            if (await _productsRepository.Any(category.Id))
                throw new ApiException("Não é possível apagar essa categoria pois a mesma se encontra vinculada à produtos.");

            await _categoriesRepository.DeleteAsync(category);
            await _unitOfWork.SaveChangesAsync(default);

            return Result<string>.Success(null, "Categoria removida com sucesso.");
        }
    }
}
