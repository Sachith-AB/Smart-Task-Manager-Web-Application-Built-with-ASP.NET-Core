using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class QueryObject
    {
        public string? SortBy { get; set; }
        public bool IsDescending { get; set; } = false;
        public string? SearchTerm { get; set; }

        public QueryObject()
        {
        }

        public QueryObject(string? sortBy, bool isDescending, string? searchTerm = null)
        {
            SortBy = sortBy;
            IsDescending = isDescending;
            SearchTerm = searchTerm;
        }
    }
}
