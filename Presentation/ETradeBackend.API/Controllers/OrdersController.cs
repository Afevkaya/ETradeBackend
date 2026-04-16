using ETradeBackend.Application.Features.Commands.Orders.CreateOrder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETradeBackend.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    [Authorize]
    public class OrdersController(IMediator mediator) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateOrderCommandRequest request)
        {
            return Ok(await mediator.Send(request));
        }
    }
}
