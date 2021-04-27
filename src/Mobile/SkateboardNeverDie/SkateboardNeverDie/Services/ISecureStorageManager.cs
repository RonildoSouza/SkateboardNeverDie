using System.Threading.Tasks;

namespace SkateboardNeverDie.Services
{
    internal interface ISecureStorageManager
    {
        Task SetAsync(string key, object obj);
        Task<T> GetAsync<T>(string key);
    }
}
