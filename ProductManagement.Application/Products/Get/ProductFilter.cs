using ProductManagement.Application.Common.Filtering;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Application.Products.Get
{
    public class ProductFilter : IFilter
    {
        public string? Search { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public string? ProductType { get; set; }

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
