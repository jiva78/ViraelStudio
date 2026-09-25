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

        public async Task<PagedResult<Product>> GetAllAsync(ProductQuery query)
        {
            var productsQuery = _context.Products.AsQueryable();

            if (query.ProductType.HasValue) productsQuery = productsQuery.Where(p => p.ProductType == query.ProductType);

            if(!string.IsNullOrEmpty(query.Search)) productsQuery = productsQuery.Where(p => p.Name.Contains(query.Search) || p.Description.Contains(query.Search));

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var sortBy = query.SortBy.ToLower();
                var descending = query.SortDirection?.ToLower() == "desc";

                productsQuery = sortBy switch
                {
                    "name" => descending
                    ? productsQuery.OrderByDescending(p => p.Name)
                    : productsQuery.OrderBy(p => p.Name),

                    "price" => descending
                    ? productsQuery.OrderByDescending(p => p.Price)
                    : productsQuery.OrderBy(p => p.Price),

                    "quantity" => descending
                    ? productsQuery.OrderByDescending(p => p.Quantity)
                    : productsQuery.OrderBy(p => p.Quantity),

                    _ => productsQuery.OrderBy(p => p.Id)
                };
            }

            var totalCount = await productsQuery.CountAsync();

            var products = await productsQuery
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
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

        public async Task<Product?> UpdateAsync(int id, Product product)
        {
            var existingProduct = await GetByIdAsync(id);

            if (existingProduct == null) return null;

            _context.Entry(existingProduct)
            .Property(p => p.RowVersion)
            .OriginalValue = product.RowVersion;

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
