using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simple.Hateoas;
using Simple.Hateoas.Models;
using SkateboardNeverDie.Application.Skaters;
using SkateboardNeverDie.Application.Skaters.Dtos;
using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Skaters.QueryData;
using SkateboardNeverDie.Services.Api.HateoasLinkBuilders.Skaters;
using System;
using System.Threading;
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
        public async Task<IActionResult> Get(int page = 1, int pageSize = 10, CancellationToken cancelationToken = default)
        {
            var skaters = await _skaterAppService.GetAllAsync(page, pageSize, cancelationToken);
            return Ok(_hateoas.Create(skaters));
        }

        [Authorize("Read")]
        [HttpGet("{id}", Name = SkaterRouteNames.GetSkater)]
        [ProducesResponseType(typeof(HateoasResult<SkaterQueryData>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancelationToken = default)
        {
            var skater = await _skaterAppService.GetByIdAsync(id, cancelationToken);
            return skater != null ? Ok(_hateoas.Create(skater)) : NotFound("Skater is not found!");
        }

        [Authorize("Skaters:Add")]
        [HttpPost(Name = SkaterRouteNames.CreateSkater)]
        [ProducesResponseType(typeof(HateoasResult<SkaterQueryData>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(CreateSkaterDto createSkaterDto, CancellationToken cancelationToken = default)
        {
            var skater = await _skaterAppService.CreateAsync(createSkaterDto, cancelationToken);
            return skater != null ? Created(string.Empty, _hateoas.Create(skater)) : BadRequest("Skater is not created!");
        }

        [Authorize("Skaters:Remove")]
        [HttpDelete("{id}", Name = SkaterRouteNames.DeleteSkater)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancelationToken = default)
        {
            throw new NotImplementedException();
        }

        [Authorize("Read")]
        [HttpGet("{id}/tricks", Name = SkaterRouteNames.GetSkaterTricks)]
        [ProducesResponseType(typeof(HateoasResult<SkaterTrickQueryData>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSkaterTricks(Guid id, int page = 1, int pageSize = 10, CancellationToken cancelationToken = default)
        {
            var skaterTricks = await _skaterAppService.GetSkaterTricksAsync(id, page, pageSize, cancelationToken);
            return Ok(_hateoas.Create(skaterTricks));
        }
    }
}
