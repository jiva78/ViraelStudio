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
    }
}
