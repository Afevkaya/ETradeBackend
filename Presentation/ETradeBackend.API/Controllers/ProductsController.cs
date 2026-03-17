using ETradeBackend.Application.Repositories.Products;
using ETradeBackend.Application.RequestParameters;
using ETradeBackend.Application.ViewModels.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETradeBackend.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController(
        IProductReadRepository productReadRepository, 
        IProductWriteRepository productWriteRepository,
        IWebHostEnvironment webHostEnvironment) : ControllerBase
    {
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll([FromQuery] Pagination pagination)
        {
            var totalCount = productReadRepository.GetAll(false).Count();
            var response = await productReadRepository
                .GetAll(false)
                .Select(p=> new {p.Id, p.Name, p.Price, p.Stock, p.CreatedAt, p.UpdatedAt})
                .Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync();
            return Ok(new
            {
                TotalCount = totalCount,
                Products = response
            });
        }
        
        [HttpGet("get-by-id/{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await productReadRepository.GetByIdAsync(id,false);
            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(VMCreateProduct model)
        {
            await productWriteRepository.AddAsync(new()
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock
            });
            await productWriteRepository.SaveChangesAsync();
            return Ok();
        }
        
        [HttpPut("update")]
        public async Task<IActionResult> Update(VMUpdateProduct model)
        {
            var product = await productReadRepository.GetSingleWhereAsync(x => x.Id == model.Id);
            if(product == null) return NotFound();
            product.Name = model.Name;
            product.Price = model.Price;
            product.Stock = model.Stock;
            await productWriteRepository.SaveChangesAsync();
            return Ok();
        }
        
        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            productWriteRepository.Delete(id);
            await productWriteRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage()
        {
            var uploadPath = Path.Combine(webHostEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var random = new Random();
            foreach (var file in Request.Form.Files)
            {
                var fullPath = Path.Combine(uploadPath, $"{random.Next()}{Path.GetExtension(file.FileName)}");
                using var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
            }
            
            return Ok();
        }
    }
}
