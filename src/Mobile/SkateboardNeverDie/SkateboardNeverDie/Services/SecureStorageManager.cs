using Newtonsoft.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SkateboardNeverDie.Services
{
    internal sealed class SecureStorageManager : ISecureStorageManager
    {
        public async Task SetAsync(string key, object obj)
        {
            var value = JsonConvert.SerializeObject(obj);
            SecureStorage.Remove(key);
            await SecureStorage.SetAsync(key, value);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var value = await SecureStorage.GetAsync(key);

            if (string.IsNullOrEmpty(value))
                return default;

            return JsonConvert.DeserializeObject<T>(value);
        }

        public bool Remove(string key) => SecureStorage.Remove(key);
    }
}
