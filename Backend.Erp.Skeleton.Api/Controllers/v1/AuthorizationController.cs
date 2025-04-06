using Backend.Erp.Skeleton.Application.Commands.Authorization;
using Backend.Erp.Skeleton.Application.DTOs.Request.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthorizationController : BaseApiController<AuthorizationController>
    {
        public AuthorizationController(
            IMediator mediator,
            ILogger<AuthorizationController> logger) : base(mediator, logger)
        {
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest model)
        {
            var sendRequest = new LoginUserCommand(model);
            var result = await Mediator.Send(sendRequest);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult> ResetPassword([FromBody] RegisterUserRequest model)
        {
            var sendRequest = new RegisterUserCommand(model);
            var result = await Mediator.Send(sendRequest);
            return Ok(result);
        }
    }
}