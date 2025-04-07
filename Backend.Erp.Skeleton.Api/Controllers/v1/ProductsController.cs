using Backend.Erp.Skeleton.Application.Commands.Product;
using Backend.Erp.Skeleton.Application.DTOs.Request.Product;
using Backend.Erp.Skeleton.Application.DTOs.Response.Product;
using Backend.Erp.Skeleton.Application.Extensions;
using Backend.Erp.Skeleton.Domain.Extensions;
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
        public async Task<ActionResult<PaginatedResult<GetProductsResponse>>> GetProductsFilteredCommand([FromQuery] GetProductsQuery query)
        {
            var sendRequest = new GetProductsFilteredCommand(query);
            var result = await Mediator.Send(sendRequest);
            return Ok(result);
        }

        [Authorize(Roles = $"{Role.Company}")]
        [HttpPost]
        public async Task<ActionResult<Result<string>>> CreateProductCommand([FromBody] CreateProductRequest query)
        {
            var sendRequest = new CreateProductCommand(User.GetUser(), query);
            var result = await Mediator.Send(sendRequest);
            return Ok(result);
        }

        [Authorize(Roles = $"{Role.Company}")]
        [HttpDelete]
        public async Task<ActionResult<Result<string>>> DeleteProductCommand([FromQuery] DeleteProductQuery query)
        {
            var sendRequest = new DeleteProductCommand(User.GetUser(), query);
            var result = await Mediator.Send(sendRequest);
            return Ok(result);
        }

        [Authorize(Roles = $"{Role.Company}")]
        [HttpPatch]
        public async Task<ActionResult<Result<string>>> UpdateProductCommand([FromQuery] UpdateProductQuery query, [FromBody] UpdateProductRequest request)
        {
            var sendRequest = new UpdateProductCommand(User.GetUser(), query, request);
            var result = await Mediator.Send(sendRequest);
            return Ok(result);
        }
    }
}
