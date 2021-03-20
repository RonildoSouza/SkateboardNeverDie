using System.Collections.Generic;

namespace SkateboardNeverDie.Models
{
    public sealed class PagedResult<T>
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }
        public IReadOnlyList<T> Results { get; set; }
    }
}
