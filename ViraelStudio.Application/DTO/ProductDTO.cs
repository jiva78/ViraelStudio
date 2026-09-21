using System;
using System.Collections.Generic;
using System.Text;
using ViraelStudio.Domain.Enums;

namespace ViraelStudio.Application.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public ProductType ProductType { get; set; }    
        public string? ImageUrl {  get; set; }
        public int Quantity {  get; set; }
    }
}
