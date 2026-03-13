using ETradeBackend.Application.Repositories.Products;
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
            var response = await productReadRepository.GetAll().ToListAsync();
            return Ok(response);
        }
        
        [HttpGet("get-by-id/{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await productReadRepository.GetByIdAsync(id);
            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create()
        {
            return Ok();
        }
        
        [HttpPut("update")]
        public async Task<IActionResult> Update()
        {
            return Ok();
        }
        
        [HttpDelete("remove/{id:guid}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            productWriteRepository.Delete(id);
            await productWriteRepository.SaveChangesAsync();
            return Ok();
        }
        
    }
}
