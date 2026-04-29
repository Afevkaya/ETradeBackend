using ETradeBackend.Application.Consts;
using ETradeBackend.Application.CustomAttributes;
using ETradeBackend.Application.Enums;
using ETradeBackend.Application.Features.Commands.ProductImageFiles.ChangeShowcaseImage;
using ETradeBackend.Application.Features.Commands.ProductImageFiles.DeleteProductImage;
using ETradeBackend.Application.Features.Commands.ProductImageFiles.UploadImageFile;
using ETradeBackend.Application.Features.Commands.Products.CreateProduct;
using ETradeBackend.Application.Features.Commands.Products.DeleteProduct;
using ETradeBackend.Application.Features.Commands.Products.UpdateProduct;
using ETradeBackend.Application.Features.Queries.Products.GetAllProduct;
using ETradeBackend.Application.Features.Queries.Products.GetByIdProduct;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETradeBackend.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllProductQueryRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }
        
        [HttpGet("get-by-id/{Id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdProductQueryRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }
        
        [HttpPost("create")]
        [Authorize]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Writing, Definition = "Create Product")]
        public async Task<IActionResult> Create([FromBody] CreateProductCommandRequest request)
        {
            await mediator.Send(request);
            return Ok();
        }
        
        [HttpPut("update")]
        [Authorize]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Updating, Definition = "Update Product")]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommandRequest request)
        {
            await mediator.Send(request);
            return Ok();
        }
        
        [HttpDelete("delete/{Id:guid}")]
        [Authorize]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Deleting, Definition = "Delete Product")]
        public async Task<IActionResult> Delete([FromRoute] DeleteProductCommandRequest request)
        {
            await mediator.Send(request);
            return Ok();
        }

        [HttpPost("upload-image")]
        [Authorize]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Writing, Definition = "Upload Product Image")]
        public async Task<IActionResult> UploadImage([FromQuery]UploadImageFileCommandRequest request)
        {
            request.FormFileCollection = Request.Form.Files;
            await mediator.Send(request);
            return Ok();
        }

        [HttpGet("get-product-images/{productId:guid}")]
        [Authorize]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Reading, Definition = "Get Product Images")]
        public async Task<IActionResult> GetImages([FromRoute]GetByIdProductQueryRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("delete-product-image/{productId:guid}")]
        [Authorize]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Deleting, Definition = "Delete Product Image")]
        public async Task<IActionResult> DeleteImage([FromRoute] DeleteProductImageCommandRequest request, [FromQuery] Guid imageId)
        {
            request.ImageId = imageId;
            await mediator.Send(request);
            return Ok();
        }

        [HttpPut("change-showcase-image/{productId:guid}/{imageId:guid}")]
        [Authorize]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Updating, Definition = "Change Showcase Image")]
        public async Task<IActionResult> ChangeShowcaseImage([FromRoute] ChangeShowcaseImageCommandRequest request)
        {
            await mediator.Send(request);
            return Ok();
        }

    }
}
