﻿using Api.Tests.Common;
using Api.Tests.Common.Models;
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
    }
}
