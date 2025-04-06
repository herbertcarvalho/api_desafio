using Backend.Erp.Skeleton.Application.DTOs;
using Backend.Erp.Skeleton.Application.DTOs.Request.Product;
using Backend.Erp.Skeleton.Application.Exceptions;
using Backend.Erp.Skeleton.Application.Services.Interfaces;
using Backend.Erp.Skeleton.Domain.Entities;
using Backend.Erp.Skeleton.Domain.Extensions;
using Backend.Erp.Skeleton.Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Application.Commands.Product
{
    public record CreateProductCommand(UserClaim UserClaim, CreateProductRequest Request) : IRequest<Result<string>>;

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<string>>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IPersonsRepository _personsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMinIoServices _minIoServices;

        public CreateProductCommandHandler(
            IProductsRepository productsRepository,
            IUnitOfWork unitOfWork,
            ICompanyRepository companyRepository,
            ICategoriesRepository categoriesRepository,
            IPersonsRepository personsRepository,
            IMinIoServices minIoServices)
        {
            _productsRepository = productsRepository;
            _unitOfWork = unitOfWork;
            _companyRepository = companyRepository;
            _categoriesRepository = categoriesRepository;
            _personsRepository = personsRepository;
            _minIoServices = minIoServices;
        }

        public async Task<Result<string>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var user = await _personsRepository.GetByIdAsync(request.UserClaim.IdUser)
                ?? throw new ApiException("Não foi possível encontrar o usuário.");

            if (!await _companyRepository.Any(user.IdCompany.Value))
                throw new ApiException("A empresa selecionada não existe.");

            if (!await _categoriesRepository.Any(request.Request.IdCategory))
                throw new ApiException("A categoria selecionada não existe.");

            if (await _productsRepository.Any(request.Request.Name))
                throw new ApiException("Esse nome já foi utilizado por outro produto.");

            var identifier = Guid.NewGuid().ToString();
            while (await _productsRepository.AnyImage(identifier))
                identifier = Guid.NewGuid().ToString();

            await _minIoServices.UploadFileAsync(identifier, request.Request.Img);

            var addProduct = new Products()
            {
                IdCompany = user.IdCompany.Value,
                IdCategory = request.Request.IdCategory,
                Status = request.Request.Status,
                Image = identifier,
                Name = request.Request.Name,
                Price = request.Request.Price,
            };

            await _productsRepository.AddAsync(addProduct);
            await _unitOfWork.SaveChangesAsync(default);

            return Result<string>.Success(null, "Novo produto cadastrado com sucesso");
        }
    }
}
