using MyComicList.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyComicList.Application.Helpers
{
    public static class IQueryableExtensions
    {
        public static PagedResponse<T> Paginate<T>( this IQueryable<T> query, int perPage, int currentPage)
        {
            var result = query.Skip((currentPage - 1) * perPage).Take(perPage);
            var totalCount = query.Count();
            var pagesCount = (int)Math.Ceiling((double)totalCount / perPage);

            return new PagedResponse<T>()
            {
                CurrentPage = currentPage,
                PagesCount = pagesCount,
                PerPage = perPage,
                TotalCount = totalCount,
                Data = result.ToList()
            };
        }
    }
}
