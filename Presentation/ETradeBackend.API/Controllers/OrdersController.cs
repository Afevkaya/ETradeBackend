using ETradeBackend.Application.Features.Commands.Orders.CreateOrder;
using ETradeBackend.Application.Features.Queries.Orders.GetAllOrder;
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
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllOrderQueryRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }
        
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateOrderCommandRequest request)
        {
            return Ok(await mediator.Send(request));
        }
    }
}
