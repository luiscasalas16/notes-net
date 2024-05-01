using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NetFwApi.Tests.Extensions
{
    public static class HttpMessageInvokerExtensions
    {
        public static async Task<HttpResponseMessage> GetAsync(this HttpMessageInvoker client, string requestUri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            return await client.SendAsync(request, CancellationToken.None);
        }

        public static async Task<HttpResponseMessage> PostAsJsonAsync<TValue>(this HttpMessageInvoker client, string requestUri, TValue value)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, requestUri) { Content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json") };

            return await client.SendAsync(request, CancellationToken.None);
        }
    }
}
