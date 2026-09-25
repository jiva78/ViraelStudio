using Microsoft.AspNetCore.Mvc;
using ViraelStudio.Application.DTOs;
using ViraelStudio.Application.Models;
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
        public async Task<ActionResult<PagedResultDto<ProductDTO>>> GetAll([FromQuery] ProductQuery query)
        {
            if (query.Page < 1) return BadRequest("Page must be greater than 0.");
            if (query.PageSize < 1 || query.PageSize > 100)
                return BadRequest("PageSize must be between 1 and 100.");

            var products = await _productService.GetAllAsync(query);
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

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDTO>> Update(int id, [FromBody] UpdateProductDTO dto)
        {
            var updatedProduct = await _productService.UpdateAsync(id, dto);
            if (updatedProduct == null) return NotFound();
            return Ok(updatedProduct);
        }
    }
}
