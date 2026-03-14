using ETradeBackend.Application.Repositories.Products;
using ETradeBackend.Application.ViewModels.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETradeBackend.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository) : ControllerBase
    {
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var response = await productReadRepository.GetAll(false).ToListAsync();
            return Ok(response);
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
        
    }
}
