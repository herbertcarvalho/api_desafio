using Backend.Erp.Skeleton.Application.DTOs;
using Backend.Erp.Skeleton.Application.DTOs.Request.Category;
using Backend.Erp.Skeleton.Application.Exceptions;
using Backend.Erp.Skeleton.Application.Extensions;
using Backend.Erp.Skeleton.Domain.Entities;
using Backend.Erp.Skeleton.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Application.Commands.Category
{
    public record CreateCategoryCommand(UserClaim UserClaim, CreateCategoryRequest Request) : IRequest<Result<string>>;

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<string>>
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryCommandHandler(
            ICategoriesRepository categoriesRepository,
            IUnitOfWork unitOfWork)
        {
            _categoriesRepository = categoriesRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (await _categoriesRepository.Any(request.Request.name))
                throw new ApiException("Essa categoria já foi cadastrada.");

            var addCategory = new Categories()
            {
                name = request.Request.name,
                idCreatedBy = request.UserClaim.IdUser,
            };

            await _categoriesRepository.AddAsync(addCategory);
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success(null, "Nova categoria cadastrada com sucesso");
        }
    }
}
