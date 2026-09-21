using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using ViraelStudio.Application.DTOs;
using ViraelStudio.Application.Mappings;
using ViraelStudio.Application.Repositories;

namespace ViraelStudio.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductDTO> CreateAsync(CreateProductDTO dto)
        {
            var product = dto.ToEntity();

            await _repository.AddAsync(product);
            
            return product.ToDto();
        }
    }
}
