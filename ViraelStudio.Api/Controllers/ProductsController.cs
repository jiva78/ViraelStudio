using Microsoft.AspNetCore.Mvc;
using ViraelStudio.Application.DTOs;
using ViraelStudio.Application.Services;

namespace ViraelStudio.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        { 
            _productService = productService;
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Create(CreateProductDTO dto)
        {
            var product = await _productService.CreateAsync(dto);
            return Ok(product);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetById(int id)
        { 
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);           
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var res = await _productService.DeleteAsync(id);
            if(!res) return NotFound();
            return NoContent();
        }
    }
}
