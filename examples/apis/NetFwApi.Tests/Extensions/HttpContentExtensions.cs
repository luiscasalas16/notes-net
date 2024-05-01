using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NetFwApi.Tests.Extensions
{
    public static class HttpContentExtensions
    {
        public static async Task<T> ReadFromJsonAsync<T>(this HttpContent content)
        {
            return JsonConvert.DeserializeObject<T>(await content.ReadAsStringAsync());
        }
    }
}
