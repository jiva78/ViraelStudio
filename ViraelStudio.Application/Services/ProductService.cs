using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using ViraelStudio.Application.DTOs;
using ViraelStudio.Application.Mappings;
using ViraelStudio.Application.Models;
using ViraelStudio.Application.Repositories;
using ViraelStudio.Domain.Entities;

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

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);           
        }

      
        public async Task<PagedResultDto<ProductDTO>> GetAllAsync(ProductQuery query)
        {
            var result = await _repository.GetAllAsync(query);
            return new PagedResultDto<ProductDTO>
            {
                TotalCount = result.TotalCount,
                Items = result.Items.ConvertAll(p => p.ToDto()),
                Page = query.Page,
                PageSize = query.PageSize,
                TotalPages = (int)Math.Ceiling((double)result.TotalCount / query.PageSize)
            };
        }

        public async Task<ProductDTO?> GetByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            return product?.ToDto();
            
        }

        public async Task<ProductDTO?> UpdateAsync(ProductDTO dto)
        {
            var product = dto.ToEntity();
            var updatedProduct = await _repository.UpdateAsync(product);
            return updatedProduct?.ToDto();
        }
    }
}
