using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Domain.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PaginatedResult<T>> ToPaginatedList<T>(this IQueryable<T> query, int pageNumber, int pageSize) where T : class
        {
            if (pageNumber == 0)
                pageNumber = 1;

            if (pageSize == 0)
                pageSize = 10;

            long count = await query.CountAsync();

            List<T> items = await query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return PaginatedResult<T>.Success(items, count, pageNumber, pageSize);
        }
    }
}
