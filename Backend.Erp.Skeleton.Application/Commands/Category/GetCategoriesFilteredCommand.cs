using Backend.Erp.Skeleton.Application.DTOs.Request.Category;
using Backend.Erp.Skeleton.Application.DTOs.Response.Authorization;
using Backend.Erp.Skeleton.Application.Extensions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Application.Commands.Category
{
    public record GetCategoriesFilteredCommand(GetCategoriesFilteredQuery Query) : IRequest<Result<UsuarioToken>>;

    public class GetCategoriesFilteredCommandHandler : IRequestHandler<GetCategoriesFilteredCommand, Result<UsuarioToken>>
    {
        public Task<Result<UsuarioToken>> Handle(GetCategoriesFilteredCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
