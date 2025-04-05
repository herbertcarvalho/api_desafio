using Backend.Erp.Skeleton.Application.DTOs.Request.Category;
using Backend.Erp.Skeleton.Application.DTOs.Response.Category;
using Backend.Erp.Skeleton.Domain.Extensions;
using Backend.Erp.Skeleton.Domain.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Application.Commands.Category
{
    public record GetCategoriesFilteredCommand(GetCategoriesFilteredQuery Query) : IRequest<PaginatedResult<GetCategoriesFilteredResponse>>;

    public class GetCategoriesFilteredCommandHandler : IRequestHandler<GetCategoriesFilteredCommand, PaginatedResult<GetCategoriesFilteredResponse>>
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public GetCategoriesFilteredCommandHandler(
            ICategoriesRepository categoriesRepository
            )
        {
            _categoriesRepository = categoriesRepository;
        }

        public async Task<PaginatedResult<GetCategoriesFilteredResponse>> Handle(GetCategoriesFilteredCommand request, CancellationToken cancellationToken)
        {
            var result = await _categoriesRepository
                .GetFiltered(request.Query.Name, new PageOption() { Page = request.Query.Page, PageSize = request.Query.PageSize });

            return PaginatedResult<GetCategoriesFilteredResponse>
                .Success("Comando executado com sucesso.", [.. result.Data.Select(x => new GetCategoriesFilteredResponse(x))], result.TotalCount, result.Page, result.Data.Count);
        }
    }
}
