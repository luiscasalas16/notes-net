using System.Net;
using System.Net.Http.Json;
using Api.Tests.Common.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using NetApiCon.Tests.Abstractions;

namespace NetApiCon.Tests
{
    public class Test1 : BaseTests
    {
        public Test1(WebApplicationFactory<Program> factory)
            : base(factory) { }

        [Theory]
        [InlineData("Test1", "Get")]
        [InlineData("Test1", "GetA")]
        [InlineData("Test1", "GetB")]
        [InlineData("Test1", "CGet")]
        [InlineData("Test1", "DGet")]
        public async Task TestGet(string controller, string action)
        {
            var response = await HttpClient.GetAsync($"{controller}/{action}");

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
            var request = new TestRequestDto { InputMessage = $"{controller} - {action}" };

            var response = await HttpClient.PostAsJsonAsync($"{controller}/{action}", request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<TestResponseDto>();

            AssertPostResult(request.InputMessage, result);
        }
    }
}
