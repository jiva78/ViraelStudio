using System;
using System.Collections.Generic;
using System.Text;
using ViraelStudio.Application.DTOs;
using ViraelStudio.Domain.Entities;

namespace ViraelStudio.Application.Mappings
{
    public static class ProductMapping
    {
        public static Product ToEntity(this CreateProductDTO dto) 
        {
            return new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                ProductType = dto.ProductType,
                ImageUrl = dto.ImageUrl,
                Quantity = dto.Quantity
            };
        }

   
        public static Product ToEntity(this UpdateProductDTO dto)
        {
            return new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                ProductType = dto.ProductType,
                ImageUrl = dto.ImageUrl,
                Quantity = dto.Quantity,
                RowVersion = dto.RowVersion
            };
        }


        public static ProductDTO ToDto(this Product product)
        {
            return new ProductDTO
            { 
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ProductType = product.ProductType,
                ImageUrl = product.ImageUrl,
                Quantity = product.Quantity,
                RowVersion = product.RowVersion
            };
        }

    }
}
