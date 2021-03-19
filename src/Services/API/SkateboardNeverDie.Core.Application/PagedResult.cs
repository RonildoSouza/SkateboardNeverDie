using System;
using System.Collections.Generic;

namespace SkateboardNeverDie.Core.Application
{
    public class PagedResult<TResult> where TResult : class
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }
        public int FirstRowOnPage => (CurrentPage - 1) * PageSize + 1;
        public int LastRowOnPage => Math.Min(CurrentPage * PageSize, RowCount);
        public IReadOnlyList<TResult> Results { get; set; }

        public PagedResult()
        {
            Results = new List<TResult>();
        }
    }
}
