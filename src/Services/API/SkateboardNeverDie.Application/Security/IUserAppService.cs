using System;
using System.Threading;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Application.Security
{
    public interface IUserAppService
    {
        Task AuthorizeAsync(Guid identityUserId, CancellationToken cancelationToken = default);
    }
}
