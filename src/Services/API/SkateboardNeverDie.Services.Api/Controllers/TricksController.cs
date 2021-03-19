using Microsoft.AspNetCore.Mvc;
using Simple.Hateoas;
using Simple.Hateoas.Models;
using SkateboardNeverDie.Application.Tricks;
using SkateboardNeverDie.Application.Tricks.Dtos;
using SkateboardNeverDie.Core.Application;
using System.Net;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Services.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TricksController : ControllerBase
    {
        private readonly ITrickAppService _trickAppService;
        private readonly IHateoas _hateoas;

        public TricksController(ITrickAppService trickAppService, IHateoas hateoas)
        {
            _trickAppService = trickAppService;
            _hateoas = hateoas;
        }

        [HttpGet]
        [ProducesResponseType(typeof(HateoasResult<PagedResult<TrickDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(int page = 1, int pageSize = 10)
        {
            var trickDtos = await _trickAppService.GetAllAsync(page, pageSize);
            return Ok(_hateoas.Create(trickDtos));
        }
    }
}
