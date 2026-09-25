using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ViraelStudio.Domain.Enums;

namespace ViraelStudio.Application.DTOs
{
    public class UpdateProductDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Range(typeof(decimal), "0", "1000000")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        public ProductType ProductType { get; set; }

        public string Description { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }
    }
}
