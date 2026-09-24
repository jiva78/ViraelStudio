using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ViraelStudio.Application.Models;
using ViraelStudio.Application.Repositories;
using ViraelStudio.Domain.Entities;
using ViraelStudio.Infrastructure.Data;

namespace ViraelStudio.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if(product == null) return false;
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PagedResult<Product>> GetAllAsync(int page, int pageSize)
        {
            var totalCount = await _context.Products.CountAsync();

            var products = await _context.Products
                .OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Product>
            {
                TotalCount = totalCount,
                Items = products
            };
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id==id);
            
        }

        public async Task<Product?> UpdateAsync(Product product)
        {
            var existingProduct = await GetByIdAsync(product.Id);

            if (existingProduct == null) return null;

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Quantity = product.Quantity;
            existingProduct.ProductType = product.ProductType;

            await _context.SaveChangesAsync();

            return existingProduct;
        }
    }
}
