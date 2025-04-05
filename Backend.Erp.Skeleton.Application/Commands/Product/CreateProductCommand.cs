using Backend.Erp.Skeleton.Application.DTOs;
using Backend.Erp.Skeleton.Application.DTOs.Request.Category;
using Backend.Erp.Skeleton.Domain.Extensions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Application.Commands.Product
{
    public record CreateProductCommand(UserClaim UserClaim, CreateCategoryRequest Request) : IRequest<Result<string>>;

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<string>>
    {
        public Task<Result<string>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
