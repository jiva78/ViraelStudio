using System;
using System.Collections.Generic;
using System.Text;
using ViraelStudio.Application.Models;
using ViraelStudio.Domain.Entities;

namespace ViraelStudio.Application.Repositories
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);

        Task<PagedResult<Product>> GetAllAsync(ProductQuery query);

        Task<Product?> GetByIdAsync(int id);

        Task<bool> DeleteAsync(int id);

        Task<Product?> UpdateAsync(Product product);


    }
}
