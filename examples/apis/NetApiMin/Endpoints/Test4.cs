using NetApi.Common.Errors;
using NetApi.Common.Results;
using NetApiMin.Models;

namespace NetApiMin.Endpoints
{
    public static class Test4
    {
        public static void MapEndpoints(WebApplication app)
        {
            app.MapGet("/Test4/ErrorGet", () => ErrorGet());
            app.MapPost("/Test4/ErrorValidation", (TestEntityDto value) => ErrorValidation(value)).Validate<TestEntityDto>();
        }

        public static Result ErrorGet()
        {
            throw new ApplicationException("application error");
        }

        public static Result ErrorValidation(TestEntityDto value)
        {
            Assert(value.FistName == "Hello");
            Assert(value.LastName == "World");
            Assert(value.Email == "hello@world.com");

            return Result.Success();
        }

        private static void Assert(bool expression)
        {
            if (!expression)
                throw new Exception("error");
        }
    }
}
