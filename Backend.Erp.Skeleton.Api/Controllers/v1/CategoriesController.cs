using Backend.Erp.Skeleton.Application.Commands.Category;
using Backend.Erp.Skeleton.Application.DTOs.Request.Category;
using Backend.Erp.Skeleton.Application.Extensions;
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
    public class CategoriesController : BaseApiController<CategoriesController>
    {
        public CategoriesController(
            IMediator mediator,
            ILogger<CategoriesController> logger) : base(mediator, logger)
        {
        }

        [Authorize(Roles = $"{Role.Company}")]
        [HttpGet]
        public async Task<ActionResult> GetCategoriesFilteredCommand([FromQuery] GetCategoriesFilteredQuery model)
        {
            var sendRequest = new GetCategoriesFilteredCommand(model);
            var result = await Mediator.Send(sendRequest);
            return Ok(result);
        }

        [Authorize(Roles = $"{Role.Company}")]
        [HttpPost]
        public async Task<ActionResult> CreateCategoryCommand([FromBody] CreateCategoryRequest model)
        {
            var sendRequest = new CreateCategoryCommand(User.GetUser(), model);
            var result = await Mediator.Send(sendRequest);
            return Ok(result);
        }

        [Authorize(Roles = $"{Role.Company}")]
        [HttpPatch]
        public async Task<ActionResult> UpdateCategoryCommand([FromQuery] UpdateCategoryQuery query, [FromBody] UpdateCategoryRequest model)
        {
            var sendRequest = new UpdateCategoryCommand(User.GetUser(), query, model);
            var result = await Mediator.Send(sendRequest);
            return Ok(result);
        }

        [Authorize(Roles = $"{Role.Company}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteCategoryCommand([FromQuery] DeleteCategoryQuery query)
        {
            var sendRequest = new DeleteCategoryCommand(User.GetUser(), query);
            var result = await Mediator.Send(sendRequest);
            return Ok(result);
        }
    }
}
