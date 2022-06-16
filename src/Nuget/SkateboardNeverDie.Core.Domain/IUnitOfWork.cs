using System.Threading;
using System.Threading.Tasks;

namespace SkateboardNeverDie.Core.Domain
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync(CancellationToken cancellationToken = default);
    }
}
