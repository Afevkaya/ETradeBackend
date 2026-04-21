using ETradeBackend.Application.DTOs.Tokens;
using ETradeBackend.Application.Features.Commands.AppUsers.GoogleLogin;
using ETradeBackend.Application.Features.Commands.AppUsers.LoginUser;
using ETradeBackend.Application.Features.Commands.AppUsers.RefreshTokenLogin;
using ETradeBackend.Application.Features.Commands.AppUsers.ResetPassword;
using ETradeBackend.Application.Features.Commands.AppUsers.VerifyResetPassword;
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

        [HttpPost("refresh-token-login")]
        public async Task<IActionResult> RefreshTokenLogin([FromBody] RefreshTokenRequest request)
        {
            var response = await mediator.Send(new RefreshTokenLoginCommandRequest(request.RefreshToken));
            return Ok(response);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommandRequest request)
        {
            await mediator.Send(request);
            return Ok();
        }

        [HttpPost("verify-reset-password-token")]
        public async Task<IActionResult> VerifyResetPasswordToken([FromBody] VerifyResetPasswordTokenCommandRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }
    }
}
