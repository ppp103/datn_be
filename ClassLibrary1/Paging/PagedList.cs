using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace datn.Domain
{
    public class PagedList<T>
    {
        public PagedList(List<T> items, PaginationInfo paginationInfo)
        {
            Items = items;
            Paging = paginationInfo;
        }

        public PaginationInfo Paging { get; }
        public List<T> Items { get; }


        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> query, int page, int pageSize)
        {
            var totalCount = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var paginationInfo = new PaginationInfo(page, pageSize, totalCount);

            return new PagedList<T>(items, paginationInfo);
        }
    }

    public class PaginationInfo
    {
        public PaginationInfo(int page, int pageSize, int totalCount)
        {
            PageNumber = page;
            PageSize = pageSize;
            TotalItems = totalCount;
        }

        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalItems { get; }
        public bool HasNextPage => PageNumber * PageSize < TotalItems;
        public int TotalPages => (int) Math.Ceiling((double)TotalItems / PageSize);

        public bool HasPreviousPage => PageSize > 1;
    }
}
