using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using ViraelStudio.Application.DTOs;
using ViraelStudio.Application.Models;

namespace ViraelStudio.Application.Services
{
    public interface IProductService
    {
        Task<ProductDTO> CreateAsync(CreateProductDTO dto);

        Task<PagedResultDto<ProductDTO>> GetAllAsync(ProductQuery query);

        Task<ProductDTO?> GetByIdAsync(int  id);

        Task<bool> DeleteAsync(int id);

        Task<ProductDTO?> UpdateAsync(ProductDTO dto);
    }
}
