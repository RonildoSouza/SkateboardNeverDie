using Simple.Hateoas;
using Simple.Hateoas.Models;
using SkateboardNeverDie.Domain.Tricks.QueryData;

namespace SkateboardNeverDie.Services.Api.HateoasLinkBuilders.Tricks
{
    public class TrickQueryDataHateoasLinkBuilder : IHateoasLinkBuilder<TrickQueryData>
    {
        public HateoasResult<TrickQueryData> Build(HateoasResult<TrickQueryData> hateoasResult)
        {
            return hateoasResult
                .AddSelfLink(TrickRouteNames.GetTrick, _ => new { id = _.Id });
        }
    }
}
