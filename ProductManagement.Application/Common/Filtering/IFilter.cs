using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Application.Common.Filtering
{
    public interface IFilter
    {
        public string? Search { get; set; }

        int Page { get; set; }

        int PageSize { get; set; }
    }
}
