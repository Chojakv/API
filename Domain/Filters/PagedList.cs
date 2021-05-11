using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Filters
{
    public class PagedList<T> : List<T>
    {
        public PaginationFilters Filters { get; set; }

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            Filters = new PaginationFilters
            {
                TotalCount = count,
                PageSize = pageSize,
                PageNumber = pageNumber,
                TotalPages = (int)Math.Ceiling(count /(double)pageSize)
            };
            
            AddRange(items);
        }

        public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}