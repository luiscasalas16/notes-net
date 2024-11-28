using Api.Tests.Common.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace NetApiCon.Tests.Abstractions
{
    public class BaseTests : IClassFixture<WebApplicationFactory<Program>>
    {
        protected HttpClient HttpClient { get; }

        internal BaseTests(WebApplicationFactory<Program> factory)
        {
            HttpClient = factory.CreateClient();
        }

        public static void AssertGetResult(TestResponseDto? result)
        {
            result.Should().NotBeNull();
            result!.OutputMessage.Should().MatchRegex(@"\w+ - \d{4}-\d{2}-\d{2} \d{2}-\d{2}-\d{2}");
        }

        public static void AssertGetResult(TestEntityDto? result)
        {
            result.Should().NotBeNull();
            result!.FistName.Should().NotBeNullOrEmpty();
            result!.LastName.Should().NotBeNullOrEmpty();
            result!.Id.Should().BeGreaterThan(0);
            result!.Email.Should().NotBeNullOrEmpty().And.Match("*@*.*");
        }

        public static void AssertGetResult(List<TestEntityDto>? result)
        {
            result.Should().NotBeNull();
            result!.Count.Should().Be(2);
            foreach (var i in result)
                AssertGetResult(i);
        }

        public static void AssertPostResult(string message, TestResponseDto? result)
        {
            result.Should().NotBeNull();
            result!.OutputMessage.Should().MatchRegex(message + @" - \w+ - \d{4}-\d{2}-\d{2} \d{2}-\d{2}-\d{2}");
        }
    }
}
