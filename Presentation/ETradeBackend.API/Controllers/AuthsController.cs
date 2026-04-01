using ETradeBackend.Application.Features.Commands.AppUsers.GoogleLogin;
using ETradeBackend.Application.Features.Commands.AppUsers.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETradeBackend.API.Controllers
{
    [Route("api/auths")]
    [ApiController]
    public class AuthsController(IMediator mediator) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserCommandRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }
        
        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }
    }
}
