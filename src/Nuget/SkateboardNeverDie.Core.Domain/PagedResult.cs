using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SkateboardNeverDie.Core.Infrastructure")]

namespace SkateboardNeverDie.Core.Domain
{
    public class PagedResult<T> where T : IQueryData
    {
        internal PagedResult()
        {
            Results = new List<T>();
        }

        public int CurrentPage { get; internal set; }
        public int PageCount { get; internal set; }
        public int PageSize { get; internal set; }
        public int RowCount { get; internal set; }
        public int FirstRowOnPage => (CurrentPage - 1) * PageSize + 1;
        public int LastRowOnPage => Math.Min(CurrentPage * PageSize, RowCount);
        public IReadOnlyList<T> Results { get; internal set; }
    }
}
