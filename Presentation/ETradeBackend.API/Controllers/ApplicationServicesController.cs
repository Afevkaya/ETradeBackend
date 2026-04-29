using ETradeBackend.Application.Abstractions.Services.Configurations;
using ETradeBackend.Application.CustomAttributes;
using ETradeBackend.Application.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETradeBackend.API.Controllers
{
    [Route("api/application-services")]
    [ApiController]
    [Authorize]
    public class ApplicationServicesController(IApplicationService applicationService) : ControllerBase
    {

        [HttpGet("get-all")]
        [AuthorizeDefinition(Menu = "Application Services", ActionType = ActionType.Reading, Definition = "Get Authorize Definition Endpoints")]
        public IActionResult GetAuthorizeDefinitionEndpoints()
        {
            return Ok(applicationService.GetAuthorizeDefinitionEndpoints(typeof(Program)));
        }
    }
}
