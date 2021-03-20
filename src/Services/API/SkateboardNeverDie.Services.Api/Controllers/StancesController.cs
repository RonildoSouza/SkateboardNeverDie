using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simple.Hateoas;
using Simple.Hateoas.Models;
using SkateboardNeverDie.Application.Stances;
using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Stances.QueryData;
using SkateboardNeverDie.Services.Api.HateoasLinkBuilders.Stances;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Services.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class StancesController : ControllerBase
    {
        private readonly IStanceAppService _stanceAppService;
        private readonly IHateoas _hateoas;

        public StancesController(IStanceAppService stanceAppService, IHateoas hateoas)
        {
            _stanceAppService = stanceAppService;
            _hateoas = hateoas;
        }

        [HttpGet(Name = StanceRouteNames.GetStances)]
        [ProducesResponseType(typeof(HateoasResult<PagedResult<StanceQueryData>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int page = 1, int pageSize = 10)
        {
            var stances = await _stanceAppService.GetAllAsync(page, pageSize);
            return Ok(_hateoas.Create(stances));
        }
    }
}
