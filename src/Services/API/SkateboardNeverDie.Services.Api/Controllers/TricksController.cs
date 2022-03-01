using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simple.Hateoas;
using Simple.Hateoas.Models;
using SkateboardNeverDie.Application.Tricks;
using SkateboardNeverDie.Application.Tricks.Dtos;
using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Tricks.QueryData;
using SkateboardNeverDie.Services.Api.HateoasLinkBuilders.Tricks;
using System;
using System.Threading;
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

        [Authorize("Read")]
        [HttpGet(Name = TrickRouteNames.GetTricks)]
        [ProducesResponseType(typeof(HateoasResult<PagedResult<TrickQueryData>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int page = 1, int pageSize = 10, CancellationToken cancelationToken = default)
        {
            var tricks = await _trickAppService.GetAllAsync(page, pageSize, cancelationToken);
            return Ok(_hateoas.Create(tricks));
        }

        [Authorize("Read")]
        [HttpGet("{id}", Name = TrickRouteNames.GetTrick)]
        [ProducesResponseType(typeof(HateoasResult<TrickQueryData>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancelationToken = default)
        {
            var skater = await _trickAppService.GetByIdAsync(id, cancelationToken);
            return skater != null ? Ok(_hateoas.Create(skater)) : NotFound("Trick not found!");
        }

        [Authorize("Tricks:Add")]
        [HttpPost(Name = TrickRouteNames.CreateTrick)]
        [ProducesResponseType(typeof(HateoasResult<TrickQueryData>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(CreateTrickDto createTrickDto, CancellationToken cancelationToken = default)
        {
            var trick = await _trickAppService.CreateAsync(createTrickDto, cancelationToken);
            return trick != null ? Created(string.Empty, _hateoas.Create(trick)) : BadRequest("Trick not created!");
        }

        [Authorize("Tricks:Remove")]
        [HttpDelete("{id}", Name = TrickRouteNames.DeleteTrick)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancelationToken)
        {
            var result = await _trickAppService.DeleteAsync(id, cancelationToken);
            return result ? Ok() : BadRequest("Trick not deleted!");
        }
    }
}
