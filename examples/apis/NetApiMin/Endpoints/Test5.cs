using Api.Tests.Common.Models;
using NetApi.Common.Results;

namespace NetApiMin.Endpoints
{
    public static class Test5
    {
        public static void MapEndpoints(WebApplication app)
        {
            app.MapGet("/Test5/Get1", () => Get1());
            app.MapGet("/Test5/Get2", () => Get2());
        }

        public static TestTypesDto Get1()
        {
            return TestTypesDto.Generate();
        }

        public static Result Get2()
        {
            return Result.Success(TestTypesDto.Generate());
        }
    }
}
