using ETradeBackend.Application.Consts;
using ETradeBackend.Application.CustomAttributes;
using ETradeBackend.Application.Enums;
using ETradeBackend.Application.Features.Commands.CompletedOrder;
using ETradeBackend.Application.Features.Commands.Orders.CreateOrder;
using ETradeBackend.Application.Features.Queries.Orders.GetAllOrder;
using ETradeBackend.Application.Features.Queries.Orders.GetByIdOrderQuery;
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
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Reading, Definition = "Get All Orders")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllOrderQueryRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("get-by-id")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Reading, Definition = "Get Order By Id")]
        public async Task<IActionResult> GetById([FromQuery] GetByIdOrderQueryRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("create")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Writing, Definition = "Create Order")]
        public async Task<IActionResult> Create(CreateOrderCommandRequest request)
        {
            return Ok(await mediator.Send(request));
        }

        [HttpGet("completed-order/{guid:Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Updating, Definition = "Completed Order")]
        public async Task<IActionResult> CompletedOrder([FromRoute] CompletedOrderCommandRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }
    }
}
