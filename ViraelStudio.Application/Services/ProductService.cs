using System;
using System.Collections.Generic;
using System.Text;
using ViraelStudio.Application.DTOs;
using ViraelStudio.Application.Mappings;

namespace ViraelStudio.Application.Services
{
    public class ProductService : IProductService
    {
        public Task<ProductDTO> CreateAsync(CreateProductDTO dto)
        {
            var product = dto.ToEntity();
            //safe to DB
            var result = product.ToDto();
            return Task.FromResult(result);
        }
    }
}
