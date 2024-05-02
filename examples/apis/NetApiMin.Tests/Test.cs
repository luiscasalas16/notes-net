using System.Net;
using System.Net.Http.Json;
using Api.Tests.Common.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using NetApiMin.Tests.Abstractions;

namespace NetApiMin.Tests
{
    public class Test : BaseTests
    {
        public Test(WebApplicationFactory<Program> factory)
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
            var parameter = new TestRequestDto { InputMessage = $"{controller} - {action}" };

            var response = await HttpClient.PostAsJsonAsync($"{controller}/{action}", parameter);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<TestResponseDto>();

            AssertPostResult(parameter.InputMessage, result);
        }

        [Theory]
        [InlineData("Test2")]
        [InlineData("Test3")]
        public async Task TestGetAll(string controller)
        {
            var response = await HttpClient.GetAsync($"{controller}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<List<TestEntityDto>>();

            AssertGetResult(result);
        }

        [Theory]
        [InlineData("Test2")]
        [InlineData("Test3")]
        public async Task TestGetId(string controller)
        {
            var response = await HttpClient.GetAsync($"{controller}/1");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<TestEntityDto>();

            AssertGetResult(result);
        }

        [Theory]
        [InlineData("Test2")]
        [InlineData("Test3")]
        public async Task TestInsert(string controller)
        {
            var parameter = new TestEntityDto
            {
                FistName = "Hello",
                LastName = "World",
                Email = "hello@world.com"
            };

            var response = await HttpClient.PostAsJsonAsync($"{controller}", parameter);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<TestEntityDto>();

            result.Should().NotBeNull();
            result!.FistName.Should().Be(parameter.FistName);
            result!.LastName.Should().Be(parameter.LastName);
            result!.Email.Should().Be(parameter.Email);
            result!.Id.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("Test2")]
        [InlineData("Test3")]
        public async Task TestUpdate(string controller)
        {
            var parameter = new TestEntityDto
            {
                FistName = "Hello",
                LastName = "World",
                Email = "hello@world.com"
            };

            var response = await HttpClient.PutAsJsonAsync($"{controller}/1", parameter);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("Test2")]
        [InlineData("Test3")]
        public async Task TestDelete(string controller)
        {
            var response = await HttpClient.DeleteAsync($"{controller}/1");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("Test4")]
        public async Task TestError(string controller)
        {
            var response = await HttpClient.GetAsync($"{controller}/ErrorGet");

            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Theory]
        [InlineData("Test4")]
        public async Task TestValidationSuccess(string controller)
        {
            var parameter = new TestEntityDto
            {
                FistName = "Hello",
                LastName = "World",
                Email = "hello@world.com"
            };

            var response = await HttpClient.PostAsJsonAsync($"{controller}/ErrorValidation", parameter);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("Test4")]
        public async Task TestValidationFail(string controller)
        {
            var parameter = new TestEntityDto { Email = "hello@world.com" };

            var response = await HttpClient.PostAsJsonAsync($"{controller}/ErrorValidation", parameter);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("Test5", "Get1")]
        [InlineData("Test5", "Get2")]
        public async Task TestTypes(string controller, string action)
        {
            var response = await HttpClient.GetAsync($"{controller}/{action}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<TestTypesDto>();

            result.Should().NotBeNull();
        }
    }
}
