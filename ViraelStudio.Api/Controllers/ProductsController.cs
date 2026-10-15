using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ViraelStudio.Application.DTOs;
using ViraelStudio.Application.DTOs.Product;
using ViraelStudio.Application.Models;
using ViraelStudio.Application.Options;
using ViraelStudio.Application.Services;
using ViraelStudio.Domain.Enums;

namespace ViraelStudio.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController(
        IProductService productService,
        IOptions<ProductMediaStorageOptions> storageOptions) : ControllerBase
    {
        private readonly IProductService _productService = productService;
        private readonly long _maxFileSizeBytes = (long)storageOptions.Value.MaxFileSizeMb * 1024 * 1024;

        [HttpPost]
        [Authorize(Roles ="Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var res = await _productService.DeleteAsync(id);
            if(!res) return NotFound();
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductDTO>> Update(int id, [FromBody] UpdateProductDTO dto)
        {
            var updatedProduct = await _productService.UpdateAsync(id, dto);
            if (updatedProduct == null) return NotFound();
            return Ok(updatedProduct);
        }

        [HttpPost("{id}/media")]
        [Authorize(Roles = "Admin")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ProductMediaDTO>> AddMedia(
            int id,
            IFormFile file,
            [FromForm] ProductMediaType mediaType,
            [FromForm] bool isPrimary,
            [FromForm] int sortOrder,
            CancellationToken cancellationToken)
        {
            if (file is null || file.Length == 0)
                return BadRequest("A file is required.");

            if (file.Length > _maxFileSizeBytes)
                return BadRequest($"File size must not exceed {storageOptions.Value.MaxFileSizeMb} MB.");

            var extension = Path.GetExtension(file.FileName);
            var allowedExtensions = mediaType switch
            {
                ProductMediaType.Image => storageOptions.Value.AllowedImageExtensions,
                ProductMediaType.Video => storageOptions.Value.AllowedVideoExtensions,
                _ => []
            };

            if (!allowedExtensions.Contains(
                    extension,
                    StringComparer.OrdinalIgnoreCase))
            {
                return BadRequest("Unsupported file extension.");
            }

            await using var stream = file.OpenReadStream();

            var media = await _productService.AddMediaAsync(
                id,
                stream,
                file.FileName,
                mediaType,
                isPrimary,
                sortOrder,
                cancellationToken);

            if (media is null)  return NotFound();

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                media);
        }

        [HttpDelete("{id}/media/{mediaId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMedia(int id, int mediaId)
        {
            var deleted = await _productService.DeleteMediaAsync(id, mediaId);

            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
