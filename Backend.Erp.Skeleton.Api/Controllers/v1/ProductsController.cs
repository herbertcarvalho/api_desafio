using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Backend.Erp.Skeleton.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductsController : BaseApiController<ProductsController>
    {
        public ProductsController(
            IMediator mediator,
            ILogger<ProductsController> logger) : base(mediator, logger)
        {
        }
    }
}
