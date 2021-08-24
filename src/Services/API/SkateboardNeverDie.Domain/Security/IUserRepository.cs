using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Domain.Security.QueryData;
using System.Threading;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Domain.Security
{
    public interface IUserRepository : IRepository<User>
    {
        Task<PagedResult<UserQueryData>> GetUsersByEmailAsync(string email, int page, int pageSize, CancellationToken cancelationToken = default);
    }
}
