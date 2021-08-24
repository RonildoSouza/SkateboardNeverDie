using Microsoft.EntityFrameworkCore;
using SkateboardNeverDie.Core.Domain;
using SkateboardNeverDie.Core.Infrastructure;
using SkateboardNeverDie.Core.Infrastructure.Extensions;
using SkateboardNeverDie.Domain.Security;
using SkateboardNeverDie.Domain.Security.QueryData;
using SkateboardNeverDie.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Infrastructure.Domain.Security
{
    public sealed class UserRepository : Repository<User, ApplicationDbContext>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context)
            : base(context) { }

        public async Task<PagedResult<UserQueryData>> GetUsersByEmailAsync(string email, int page, int pageSize, CancellationToken cancelationToken = default)
        {
            return await Context.Users
                .AsNoTracking()
                .Where(_ => _.Email.StartsWith(email))
                .GetPagedResultAsync(
                page,
                pageSize,
                _ => new UserQueryData
                {
                    Id = _.Id,
                    Name = _.Name,
                    Email = _.Email
                },
                cancelationToken);
        }
    }
}
