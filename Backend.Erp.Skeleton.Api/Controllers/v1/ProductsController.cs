using Backend.Erp.Skeleton.Application.Commands.Product;
using Backend.Erp.Skeleton.Application.DTOs.Request.Products;
using Backend.Erp.Skeleton.Infrastructure.Roles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

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

        [Authorize(Roles = $"{Role.Company},{Role.Client}")]
        [HttpGet]
        public async Task<ActionResult> GetProductsFilteredCommand([FromQuery] GetProductsQuery query)
        {
            var sendRequest = new GetProductsFilteredCommand(query);
            var result = await Mediator.Send(sendRequest);
            return Ok(result);
        }
    }
}
