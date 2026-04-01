using ETradeBackend.Application.Features.Commands.AppUsers.CreateUser;
using MediatR; 
using Microsoft.AspNetCore.Mvc;

namespace ETradeBackend.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateUserCommandRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }
    }
}
