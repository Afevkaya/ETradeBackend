using ETradeBackend.Application.Consts;
using ETradeBackend.Application.CustomAttributes;
using ETradeBackend.Application.Enums;
using ETradeBackend.Application.Features.Commands.Roles.CreateRole;
using ETradeBackend.Application.Features.Commands.Roles.DeleteRole;
using ETradeBackend.Application.Features.Commands.Roles.UpdateRole;
using ETradeBackend.Application.Features.Queries.Roles.GetRoleById;
using ETradeBackend.Application.Features.Queries.Roles.GetRoles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETradeBackend.API.Controllers
{
    [Route("api/roles")]
    [ApiController]
    [Authorize]
    public class RolesController(IMediator mediator) : ControllerBase
    {
        [HttpGet("get-all")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Roles, ActionType = ActionType.Reading, Definition = "Get Roles")]
        public async Task<IActionResult> GetAll([FromQuery] GetRolesQueryRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("get-by-id/{Id:guid}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Roles, ActionType = ActionType.Reading, Definition = "Get Role By Id")]
        public async Task<IActionResult> GetById([FromRoute] GetRoleByIdQueryRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("create")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Roles, ActionType = ActionType.Writing, Definition = "Create Role")]
        public async Task<IActionResult> Create([FromBody] CreateRoleCommandRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("update/{Id:guid}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Roles, ActionType = ActionType.Updating, Definition = "Update Role")]
        public async Task<IActionResult> Update([FromBody, FromRoute] UpdateRoleCommandRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("delete/{Id:guid}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Roles, ActionType = ActionType.Deleting, Definition = "Delete Role")]
        public async Task<IActionResult> Delete([FromRoute] DeleteRoleCommandRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }
    }
}
