using Microsoft.AspNetCore.Mvc;
using Simple.Hateoas;
using Simple.Hateoas.Models;
using SkateboardNeverDie.Application.Skaters;
using SkateboardNeverDie.Application.Skaters.Dtos;
using SkateboardNeverDie.Core.Application;
using SkateboardNeverDie.Services.Api.HateoasLinkBuilders.Skaters;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Services.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SkatersController : ControllerBase
    {
        private readonly ISkaterAppService _skaterAppService;
        private readonly IHateoas _hateoas;

        public SkatersController(ISkaterAppService skaterAppService, IHateoas hateoas)
        {
            _skaterAppService = skaterAppService;
            _hateoas = hateoas;
        }

        [HttpGet(Name = SkaterRouteNames.GetSkaters)]
        [ProducesResponseType(typeof(HateoasResult<PagedResult<SkaterDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(int page = 1, int pageSize = 10)
        {
            var skaterDtos = await _skaterAppService.GetAllAsync(page, pageSize);
            return Ok(_hateoas.Create(skaterDtos));
        }

        [HttpGet("{id}", Name = SkaterRouteNames.GetSkater)]
        [ProducesResponseType(typeof(HateoasResult<SkaterDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var skaterDto = await _skaterAppService.GetByIdAsync(id);
            return skaterDto != null ? Ok(_hateoas.Create(skaterDto)) : NotFound("Skater is not found!");
        }

        [HttpPost(Name = SkaterRouteNames.CreateSkater)]
        [ProducesResponseType(typeof(HateoasResult<SkaterDto>), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(CreateSkaterDto createSkaterDto)
        {
            var skaterDto = await _skaterAppService.CreateSkaterAsync(createSkaterDto);
            return skaterDto != null ? Created(string.Empty, _hateoas.Create(skaterDto)) : BadRequest("Skater is not created!");
        }
    }
}
