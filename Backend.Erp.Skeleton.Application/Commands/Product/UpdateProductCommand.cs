using Backend.Erp.Skeleton.Application.DTOs;
using Backend.Erp.Skeleton.Application.DTOs.Request.Product;
using Backend.Erp.Skeleton.Application.Exceptions;
using Backend.Erp.Skeleton.Domain.Extensions;
using Backend.Erp.Skeleton.Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Application.Commands.Product
{
    public record UpdateProductCommand(UserClaim UserClaim, UpdateProductQuery Query, UpdateProductRequest Request) : IRequest<Result<string>>;

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<string>>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IPersonsRepository _personsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(
            IProductsRepository productsRepository,
            IPersonsRepository personsRepository,
            IUnitOfWork unitOfWork)
        {
            _productsRepository = productsRepository;
            _personsRepository = personsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productsRepository.GetByIdAsync(request.Query.Id) ??
                throw new ApiException("Não foi possível encontrar o produto.");

            var person = await _personsRepository.GetByIdAsync(request.UserClaim.IdUser) ??
                throw new ApiException("Não foi possível encontrar o usuário.");

            if (product.IdCompany != person.IdCompany)
                throw new ApiException("Usuário não possui acesso a empresa.");

            if (await _productsRepository.Any(request.Request.Name))
                throw new ApiException("Esse nome já foi utilizado por outro produto.");

            product.Status = request.Request.Status;
            product.Name = request.Request.Name;
            product.Price = request.Request.Price;
            product.UpdatedAt = DateTime.UtcNow;
            product.IdLastModifiedBy = request.UserClaim.IdUser;

            await _productsRepository.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync(default);

            return Result<string>.Success(null, "Produto alterado com sucesso");
        }
    }
}
