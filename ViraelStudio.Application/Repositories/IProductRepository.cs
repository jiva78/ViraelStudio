using System;
using System.Collections.Generic;
using System.Text;
using ViraelStudio.Domain.Entities;

namespace ViraelStudio.Application.Repositories
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);

        Task<List<Product>> GetAllAsync();

        Task<Product?> GetByIdAsync(int id);

        Task<bool> DeleteAsync(int id);
    }
}
