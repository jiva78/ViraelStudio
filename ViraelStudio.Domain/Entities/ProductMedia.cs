using System;
using System.Collections.Generic;
using System.Text;
using ViraelStudio.Domain.Enums;

namespace ViraelStudio.Domain.Entities
{
    public class ProductMedia
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public ProductMediaType MediaType { get; set; }

        public required string Url { get; set; }

        public bool IsPrimary { get; set; }

        public int SortOrder { get; set; }

        public Product Product { get; set; } = null!;
    }
}
