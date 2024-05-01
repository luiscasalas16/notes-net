using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Tests.Common.Models;
using FluentAssertions;
using NetFwApi.Tests.Abstractions;
using NetFwApi.Tests.Extensions;
using Xunit;

namespace NetFwApi.Tests
{
    public class Test1 : BaseTests
    {
        public Test1()
            : base() { }

        [Theory]
        [InlineData("Test1", "Get")]
        [InlineData("Test1", "GetA")]
        [InlineData("Test1", "GetB")]
        [InlineData("Test1", "CGet")]
        [InlineData("Test1", "DGet")]
        public async Task TestGet(string controller, string action)
        {
            var response = await HttpClient.GetAsync($"{URL}/{controller}/{action}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<TestResponseDto>();

            AssertGetResult(result);
        }

        [Theory]
        [InlineData("Test1", "Post")]
        [InlineData("Test1", "PostA")]
        [InlineData("Test1", "PostB")]
        [InlineData("Test1", "CPost")]
        [InlineData("Test1", "DPost")]
        public async Task TestPost(string controller, string action)
        {
            var parameter = new TestRequestDto { InputMessage = $"{controller} - {action}" };

            var response = await HttpClient.PostAsJsonAsync($"{URL}/{controller}/{action}", parameter);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<TestResponseDto>();

            AssertPostResult(parameter.InputMessage, result);
        }
    }
}
