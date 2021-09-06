using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkateboardNeverDie.Application.Security;
using SkateboardNeverDie.Core.Security.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Services.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserAppService _userAppService;

        public UsersController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [Authorize]
        [HttpPost("authorize")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostAuthorize(CancellationToken cancelationToken = default)
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized();

            var identityUserId = User.GetIdentityUserId();

            if (identityUserId == null)
                return Forbid();

            await _userAppService.AuthorizeAsync(identityUserId.Value, cancelationToken);

            return Created(string.Empty, "User has been authorized!");
        }
    }
}
