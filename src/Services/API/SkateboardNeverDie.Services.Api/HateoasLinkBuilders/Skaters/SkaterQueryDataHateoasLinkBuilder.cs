using Simple.Hateoas;
using Simple.Hateoas.Models;
using SkateboardNeverDie.Domain.QueryData;

namespace SkateboardNeverDie.Services.Api.HateoasLinkBuilders.Skaters
{
    public class SkaterQueryDataHateoasLinkBuilder : IHateoasLinkBuilder<SkaterQueryData>
    {
        public HateoasResult<SkaterQueryData> Build(HateoasResult<SkaterQueryData> hateoasResult)
        {
            return hateoasResult
                .AddSelfLink(_ => new { id = _.Id }, SkaterRouteNames.GetSkater);
        }
    }
}
