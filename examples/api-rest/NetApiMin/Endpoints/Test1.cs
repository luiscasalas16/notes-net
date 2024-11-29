using Api.Tests.Common;
using Api.Tests.Common.Models;
using Microsoft.AspNetCore.Mvc;
using NetApi.Common.Results;

namespace NetApiMin.Endpoints
{
    public static class Test1
    {
        public static void MapEndpoints(WebApplication app)
        {
            app.MapGet("/Test1/Get", () => Get());
            app.MapGet("/Test1/GetA", () => GetA());
            app.MapGet("/Test1/GetB", () => GetB());
            app.MapGet("/Test1/CGet", () => GetC());
            app.MapGet("/Test1/DGet", () => GetD());

            app.MapPost("/Test1/Post", (TestRequestDto parameters) => Post(parameters));
            app.MapPost("/Test1/PostA", (TestRequestDto parameters) => PostA(parameters));
            app.MapPost("/Test1/PostB", (TestRequestDto parameters) => PostB(parameters));
            app.MapPost("/Test1/CPost", (TestRequestDto parameters) => PostC(parameters));
            app.MapPost("/Test1/DPost", (TestRequestDto parameters) => PostD(parameters));
        }

        public static Result Get()
        {
            return GetResult("0");
        }

        public static Result GetA()
        {
            return GetResult("A");
        }

        public static Result GetB()
        {
            return GetResult("B");
        }

        public static Result GetC()
        {
            return GetResult("C");
        }

        public static Result GetD()
        {
            return GetResult("D");
        }

        private static Result GetResult(string id)
        {
            return Result.Success(new TestResponseDto { OutputMessage = $"{id} - {Helpers.GetDateTime()}" });
        }

        public static Result Post(TestRequestDto parameters)
        {
            return PostResult(parameters, "0");
        }

        public static Result PostA(TestRequestDto parameters)
        {
            return PostResult(parameters, "A");
        }

        public static Result PostB(TestRequestDto parameters)
        {
            return PostResult(parameters, "B");
        }

        public static Result PostC(TestRequestDto parameters)
        {
            return PostResult(parameters, "C");
        }

        public static Result PostD(TestRequestDto parameters)
        {
            return PostResult(parameters, "D");
        }

        private static Result PostResult(TestRequestDto parameters, string id)
        {
            return Result.Success(new TestResponseDto() { OutputMessage = $"{parameters.InputMessage} - {id} - {Helpers.GetDateTime()}" });
        }
    }
}
