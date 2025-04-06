using Backend.Erp.Skeleton.Application.DTOs.Request.Product;
using Backend.Erp.Skeleton.Application.DTOs.Response.Product;
using Backend.Erp.Skeleton.Application.Services.Interfaces;
using Backend.Erp.Skeleton.Domain.Extensions;
using Backend.Erp.Skeleton.Domain.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Application.Commands.Product
{
    public record GetProductsFilteredCommand(GetProductsQuery Query) : IRequest<PaginatedResult<GetProductsResponse>>;

    public class GetProductsFilteredCommandHandler : IRequestHandler<GetProductsFilteredCommand, PaginatedResult<GetProductsResponse>>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMinIoServices _minIoServices;

        public GetProductsFilteredCommandHandler(
            IProductsRepository productsRepository,
            IMinIoServices minIoServices)
        {
            _productsRepository = productsRepository;
            _minIoServices = minIoServices;
        }

        public async Task<PaginatedResult<GetProductsResponse>> Handle(GetProductsFilteredCommand request, CancellationToken cancellationToken)
        {
            var result = await _productsRepository
                .GetFiltered(request.Query.IdCompany, request.Query.IdCategory, request.Query.MinPrice, request.Query.MaxPrice, new PageOption() { Page = request.Query.Page, PageSize = request.Query.PageSize }, active: request.Query.Active);

            var response = result.Data.Select(x =>
            {
                var url = _minIoServices.GetDownloadLinkAsync(x.Image);
                return new GetProductsResponse(x, url);
            }).ToList();

            return PaginatedResult<GetProductsResponse>.Success("Comando executado com sucesso.", response, result.TotalCount, result.Page, result.Data.Count);
        }
    }
}
