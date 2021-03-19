using Simple.Hateoas;
using Simple.Hateoas.Models;
using SkateboardNeverDie.Application.Skaters.Dtos;

namespace SkateboardNeverDie.Services.Api.HateoasLinkBuilders.Skaters
{
    public class SkaterDtoHateoasLinkBuilder : IHateoasLinkBuilder<SkaterDto>
    {
        public HateoasResult<SkaterDto> Build(HateoasResult<SkaterDto> hateoasResult)
        {
            return hateoasResult
                .AddSelfLink(_ => new { id = _.Id }, SkaterRouteNames.GetSkater);
        }
    }
}
