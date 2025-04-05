using Backend.Erp.Skeleton.Application.DTOs;
using Backend.Erp.Skeleton.Application.DTOs.Request.Category;
using Backend.Erp.Skeleton.Domain.Extensions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Application.Commands.Products
{

    public record GetProductsCommand(UserClaim UserClaim, CreateCategoryRequest Request) : IRequest<Result<string>>;

    public class GetProductsCommandHandler : IRequestHandler<GetProductsCommand, Result<string>>
    {
        public Task<Result<string>> Handle(GetProductsCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
