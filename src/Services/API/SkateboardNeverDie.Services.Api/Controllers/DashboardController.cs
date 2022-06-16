using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkateboardNeverDie.Application.Dashboard;
using SkateboardNeverDie.Domain.Skaters.QueryData;
using SkateboardNeverDie.Domain.Stances;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Services.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize("Read")]
#if DEBUG
    [ApiExplorerSettings(IgnoreApi = false)]
#else
    [ApiExplorerSettings(IgnoreApi = true)]
#endif
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardAppService _dashboardAppService;

        public DashboardController(IDashboardAppService skaterAppService)
        {
            _dashboardAppService = skaterAppService;
        }

        [HttpGet("tricks/count")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTricksCount(CancellationToken cancelationToken = default)
            => Ok(await _dashboardAppService.GetTricksCountAsync(cancelationToken));

        [HttpGet("skaters/count")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSkatersCount(CancellationToken cancelationToken = default)
            => Ok(await _dashboardAppService.GetSkatersCountAsync(cancelationToken));

        [HttpGet("skaters/goofy-vs-regular")]
        [ProducesResponseType(typeof(IDictionary<StanceType, int>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSkatersGoofyVsRegular(CancellationToken cancelationToken = default)
            => Ok(await _dashboardAppService.GetGoofyVsRegularAsync(cancelationToken));

        [HttpGet("skaters/count-per-age")]
        [ProducesResponseType(typeof(IList<SkaterCountPerAgeQueryData>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSkatersCountPerAge(CancellationToken cancelationToken = default)
            => Ok(await _dashboardAppService.GetSkatersCountPerAgeAsync(cancelationToken));
    }
}
