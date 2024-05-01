using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Api.Tests.Common.Models;
using FluentAssertions;

namespace NetFwApi.Tests.Abstractions
{
    public class BaseTests
    {
        private readonly HttpServer server;

        protected string URL => "http://localhost:5003";

        protected HttpMessageInvoker HttpClient
        {
            get { return new HttpMessageInvoker(server); }
        }

        internal BaseTests()
        {
            HttpConfiguration config = new HttpConfiguration();

            WebApiConfig.Register(config);

            server = new HttpServer(config);
        }

        public static void AssertGetResult(TestResponseDto result)
        {
            result.Should().NotBeNull();
            result.OutputMessage.Should().MatchRegex(@"\w+ - \d{4}-\d{2}-\d{2} \d{2}-\d{2}-\d{2}");
        }

        public static void AssertPostResult(string message, TestResponseDto result)
        {
            result.Should().NotBeNull();
            result
                .OutputMessage.Should()
                .MatchRegex(message + @" - \w+ - \d{4}-\d{2}-\d{2} \d{2}-\d{2}-\d{2}");
        }
    }
}
