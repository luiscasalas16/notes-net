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

        [Fact]
        public async Task Test1Get()
        {
            string controller = "Test1";
            string action = "Get";

            var response = await HttpClient.GetAsync($"{controller}/{action}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<TestDtoResult>();

            AssertDateTimeResult(result);
        }
    }
}
