using Microsoft.EntityFrameworkCore;
using SkateboardNeverDie.Core.Application;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Core.Infrastructure.Extensions
{
    public static class PagedResultExtension
    {
        public static async Task<PagedResult<TResult>> GetPagedResultAsync<TEntity, TResult>([NotNull] this IQueryable<TEntity> query, int page, int pageSize, Expression<Func<TEntity, TResult>> selector)
            where TEntity : class
            where TResult : class
        {
            var result = new PagedResult<TResult>
            {
                CurrentPage = page,
                PageSize = pageSize,
            };

            result.RowCount = await query.AsNoTracking().CountAsync();

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;

            result.Results = await query.AsNoTracking()
                                        .Select(selector)
                                        .Skip(skip)
                                        .Take(pageSize)
                                        .ToListAsync();

            return result;
        }
    }
}
