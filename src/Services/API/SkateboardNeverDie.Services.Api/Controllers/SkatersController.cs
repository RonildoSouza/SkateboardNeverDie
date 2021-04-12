using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using Simple.Hateoas;
using Simple.Hateoas.Models;
using SkateboardNeverDie.Application.Skaters;
using SkateboardNeverDie.Application.Skaters.Dtos;
using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Skaters.QueryData;
using SkateboardNeverDie.Services.Api.HateoasLinkBuilders.Skaters;
using System;
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

        [Authorize("Read")]
        [HttpGet(Name = SkaterRouteNames.GetSkaters)]
        [ProducesResponseType(typeof(HateoasResult<PagedResult<SkaterQueryData>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int page = 1, int pageSize = 10)
        {
            var skaters = await _skaterAppService.GetAllAsync(page, pageSize);
            return Ok(_hateoas.Create(skaters));
        }

        [Authorize("Read")]
        [HttpGet("{id}", Name = SkaterRouteNames.GetSkater)]
        [ProducesResponseType(typeof(HateoasResult<SkaterQueryData>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var skater = await _skaterAppService.GetByIdAsync(id);
            return skater != null ? Ok(_hateoas.Create(skater)) : NotFound("Skater is not found!");
        }

        [Authorize("Write")]
        [HttpPost(Name = SkaterRouteNames.CreateSkater)]
        [ProducesResponseType(typeof(HateoasResult<SkaterQueryData>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(CreateSkaterDto createSkaterDto)
        {
            var skater = await _skaterAppService.CreateAsync(createSkaterDto);
            return skater != null ? Created(string.Empty, _hateoas.Create(skater)) : BadRequest("Skater is not created!");
        }
    }
}
