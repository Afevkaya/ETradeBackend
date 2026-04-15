using ETradeBackend.Application.Features.Commands.Baskets.AddItemToBasket;
using ETradeBackend.Application.Features.Commands.Baskets.RemoveBasketItem;
using ETradeBackend.Application.Features.Commands.Baskets.UpdateQuantity;
using ETradeBackend.Application.Features.Queries.Baskets.GetBasketItems;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETradeBackend.API.Controllers
{
    [Route("api/baskets")]
    [ApiController]
    [Authorize]
    public class BasketsController(IMediator mediator) : ControllerBase
    {
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll([FromQuery] GetBasketItemsQueryRequest request) => Ok(await mediator.Send(request));

        [HttpPost("add")]
        public async Task<IActionResult> Add(AddItemToBasketCommandRequest request)
        {
            await mediator.Send(request);
            return Ok();
        }
        
        [HttpPut("update-quantity")]
        public async Task<IActionResult> UpdateQuantity(UpdateQuantityCommandRequest request)
        {
            await mediator.Send(request);
            return Ok();
        }

        [HttpDelete("remove-item/{BasketItemId:guid}")]
        public async Task<IActionResult> RemoveItem([FromRoute] RemoveBasketItemCommandRequest request)
        {
            await mediator.Send(request);
            return Ok();
        }
    }
}
