using Microsoft.EntityFrameworkCore;
using SkateboardNeverDie.Core.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Core.Infrastructure.Extensions
{
    public static class PagedResultExtension
    {
        public static async Task<PagedResult<TResult>> GetPagedResultAsync<TEntity, TResult>(
            [NotNull] this IQueryable<TEntity> query,
            int page,
            int pageSize,
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TResult, object>> orderBy,
            CancellationToken cancelationToken)
            where TEntity : class
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
                                        .OrderBy(orderBy)
                                        .Skip(skip)
                                        .Take(pageSize)
                                        .ToListAsync(cancelationToken);

            return result;
        }

        public static async Task<PagedResult<TResult>> GetPagedResultAsync<TEntity, TResult>(
            [NotNull] this IEnumerable<TEntity> query,
            int page,
            int pageSize,
            Func<TEntity, TResult> selector,
            Func<TResult, object> orderBy)
            where TEntity : class
            where TResult : IQueryData
        {
            var result = new PagedResult<TResult>
            {
                CurrentPage = page,
                PageSize = pageSize,
            };

            result.RowCount = query.Count();

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;

            result.Results = query.Select(selector)
                                  .OrderBy(orderBy)
                                  .Skip(skip)
                                  .Take(pageSize)
                                  .ToList();

            return await Task.FromResult(result);
        }
    }
}
