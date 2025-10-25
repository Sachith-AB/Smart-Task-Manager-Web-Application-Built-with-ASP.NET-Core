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
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public QueryObject()
        {
        }

        public QueryObject(string? sortBy, bool isDescending, string? searchTerm = null, int pageNumber = 1, int pageSize = 10)
        {
            SortBy = sortBy;
            IsDescending = isDescending;
            SearchTerm = searchTerm;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
