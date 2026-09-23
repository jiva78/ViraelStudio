using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using ViraelStudio.Application.DTOs;

namespace ViraelStudio.Application.Services
{
    public interface IProductService
    {
        Task<ProductDTO> CreateAsync(CreateProductDTO dto);

        Task<List<ProductDTO>> GetAllAsync();

        Task<ProductDTO?> GetByIdAsync(int  id);

        Task<bool> DeleteAsync(int id);

        Task<ProductDTO?> UpdateAsync(ProductDTO dto);
    }
}
