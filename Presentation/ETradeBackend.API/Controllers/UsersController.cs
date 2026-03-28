using ETradeBackend.Application.Features.Commands.AppUsers.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETradeBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        
        public async Task<IActionResult> Create(CreateUserCommandRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }
    }
}
