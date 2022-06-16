using System.Collections.Generic;
using System.Linq;

namespace SkateboardNeverDie.Models
{
    public sealed class HateoasResult<T>
    {
        public T Data { get; set; }
        public IEnumerable<HateoasLink> Links { get; set; }

        public sealed class HateoasLink
        {
            public string Href { get; set; }
            public string Rel { get; set; }
            public string Method { get; set; }
        }

        public bool HasLink(string rel)
        {
            return !string.IsNullOrEmpty(rel)
                && Links.Any()
                && Links.Any(_ => _.Rel.ToLower() == rel.ToLower());
        }
    }
}
