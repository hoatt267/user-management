using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagementApp.Domain.Values
{
    public class PaginatedList<T>
    {
        public IReadOnlyCollection<T> Items { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalPages { get; }
        public int TotalCount { get; }
        public int Count => Items.Count;

        public PaginatedList(IReadOnlyCollection<T> items, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Items = items;
        }
    }
}