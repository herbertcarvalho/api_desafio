using Backend.Erp.Skeleton.Application.DTOs;
using Backend.Erp.Skeleton.Application.DTOs.Request.Product;
using Backend.Erp.Skeleton.Application.Exceptions;
using Backend.Erp.Skeleton.Domain.Extensions;
using Backend.Erp.Skeleton.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Application.Commands.Product
{
    public record DeleteProductCommand(UserClaim UserClaim, DeleteProductQuery Query) : IRequest<Result<string>>;

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<string>>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IPersonsRepository _personsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(
            IProductsRepository productsRepository,
            IUnitOfWork unitOfWork,
            IPersonsRepository personsRepository)
        {
            _productsRepository = productsRepository;
            _unitOfWork = unitOfWork;
            _personsRepository = personsRepository;
        }

        public async Task<Result<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productsRepository.GetByIdAsync(request.Query.Id) ??
                throw new ApiException("Não foi possível encontrar o produto.");

            var person = await _personsRepository.GetByIdAsync(request.UserClaim.IdUser) ??
                throw new ApiException("Não foi possível encontrar o usuário.");

            if (product.IdCompany != person.IdCompany)
                throw new ApiException("Usuário não possui acesso a empresa.");

            await _productsRepository.DeleteAsync(product);
            await _unitOfWork.SaveChangesAsync(default);

            return Result<string>.Success(null, "Produto apagado com sucesso");
        }
    }
}
