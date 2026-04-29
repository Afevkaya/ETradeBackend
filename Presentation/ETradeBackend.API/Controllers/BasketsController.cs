using ETradeBackend.Application.Consts;
using ETradeBackend.Application.CustomAttributes;
using ETradeBackend.Application.Enums;
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
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Baskets, ActionType = ActionType.Reading, Definition = "Get Basket Items")]
        public async Task<IActionResult> GetAll([FromQuery] GetBasketItemsQueryRequest request) => Ok(await mediator.Send(request));

        [HttpPost("add")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Baskets, ActionType = ActionType.Writing, Definition = "Add Item To Basket")]
        public async Task<IActionResult> Add(AddItemToBasketCommandRequest request)
        {
            await mediator.Send(request);
            return Ok();
        }
        
        [HttpPut("update-quantity")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Baskets, ActionType = ActionType.Updating, Definition = "Update Quantity Of Basket Item")]
        public async Task<IActionResult> UpdateQuantity(UpdateQuantityCommandRequest request)
        {
            await mediator.Send(request);
            return Ok();
        }

        [HttpDelete("remove-item/{BasketItemId:guid}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Baskets, ActionType = ActionType.Deleting, Definition = "Remove Basket Item")]
        public async Task<IActionResult> RemoveItem([FromRoute] RemoveBasketItemCommandRequest request)
        {
            await mediator.Send(request);
            return Ok();
        }
    }
}
