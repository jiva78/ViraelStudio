using System;
using System.Collections.Generic;
using System.Text;
using ViraelStudio.Domain.Enums;

namespace ViraelStudio.Application.DTOs.Product
{
    public class ProductMediaDTO
    {
        public int Id { get; set; }

        public ProductMediaType MediaType { get; set; }

        public required string Url { get; set; }

        public bool IsPrimary { get; set; }

        public int SortOrder { get; set; }
    }
}
