using System;
using System.Collections.Generic;
using System.Text;
using ViraelStudio.Domain.Enums;

namespace ViraelStudio.Application.Models
{
    public class ProductQuery
    {
        public ProductType? ProductType { get; set; }
        public string? SortBy { get; set; }
        public string? SortDirection { get; set; }
        public string? Search {  get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
