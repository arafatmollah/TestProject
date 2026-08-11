using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ProductManagement.Application.Common
{
    public static class SearchHelper
    {
        public static string Normalize(string value)
        {
            return Regex.Replace(
                value.ToLowerInvariant(),
                @"\s+",
                "");
        }
    }
}
