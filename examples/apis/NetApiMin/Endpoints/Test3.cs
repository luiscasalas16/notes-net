using Bogus;
using NetApi.Common.Results;
using NetApiMin.Models;

namespace NetApiMin.Endpoints
{
    public static class Test3
    {
        public static void MapEndpoints(WebApplication app)
        {
            app.MapGet("/Test3", () => Get());
            app.MapGet("/Test3/{id}", (int id) => Get(id));
            app.MapPost("/Test3", (TestEntityDto value) => Post(value));
            app.MapPut("/Test3/{id}", (int id, TestEntityDto value) => Put(id, value));
            app.MapDelete("/Test3/{id}", (int id) => Delete(id));
        }

        public static Result Get()
        {
            var result = new Faker<TestEntityDto>()
                .RuleFor(o => o.Id, f => f.Random.Int(1, 100))
                .RuleFor(o => o.FistName, f => f.Name.FirstName())
                .RuleFor(o => o.LastName, f => f.Name.LastName())
                .RuleFor(o => o.Email, f => f.Internet.Email());

            return Result.Success(result.Generate(2));
        }

        public static Result Get(int id)
        {
            var result = new Faker<TestEntityDto>()
                .RuleFor(o => o.Id, id)
                .RuleFor(o => o.FistName, f => f.Name.FirstName())
                .RuleFor(o => o.LastName, f => f.Name.LastName())
                .RuleFor(o => o.Email, f => f.Internet.Email());

            return Result.Success(result.Generate(1)[0]);
        }

        public static Result Post(TestEntityDto value)
        {
            Assert(value.FistName == "Hello");
            Assert(value.LastName == "World");
            Assert(value.Email == "hello@world.com");

            value.Id = new Faker().Random.Int(1, 100);

            return Result.Success(value);
        }

        public static Result Put(int id, TestEntityDto value)
        {
            Assert(id == 1);
            Assert(value.FistName == "Hello");
            Assert(value.LastName == "World");
            Assert(value.Email == "hello@world.com");

            return Result.Success();
        }

        public static Result Delete(int id)
        {
            Assert(id == 1);

            return Result.Success();
        }

        private static void Assert(bool expression)
        {
            if (!expression)
                throw new Exception("error");
        }
    }
}
