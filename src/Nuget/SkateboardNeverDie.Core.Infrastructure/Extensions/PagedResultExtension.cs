using Microsoft.EntityFrameworkCore;
using SkateboardNeverDie.Core.Domain;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Core.Infrastructure.Extensions
{
    public static class PagedResultExtension
    {
        public static async Task<PagedResult<TResult>> GetPagedResultAsync<TEntity, TResult>([NotNull] this IQueryable<TEntity> query, int page, int pageSize, Expression<Func<TEntity, TResult>> selector, CancellationToken cancelationToken)
            where TEntity : class, IEntity
            where TResult : IQueryData
        {
            var result = new PagedResult<TResult>
            {
                CurrentPage = page,
                PageSize = pageSize,
            };

            result.RowCount = await query.AsNoTrackingWithIdentityResolution().CountAsync(cancelationToken);

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;

            result.Results = await query.AsNoTrackingWithIdentityResolution()
                                        .Select(selector)
                                        .Skip(skip)
                                        .Take(pageSize)
                                        .ToListAsync(cancelationToken);

            return result;
        }
    }
}
