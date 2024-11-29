using Bogus;
using NetApi.Common.Errors;
using NetApiMin.Models;

namespace NetApiMin.Endpoints
{
    public static class Test2
    {
        public static void MapEndpoints(WebApplication app)
        {
            app.MapGet("/Test2", () => Get());
            app.MapGet("/Test2/{id}", (int id) => Get(id));
            app.MapPost("/Test2", (TestEntityDto value) => Post(value)).Validate<TestEntityDto>();
            app.MapPut("/Test2/{id}", (int id, TestEntityDto value) => Put(id, value)).Validate<TestEntityDto>();
            app.MapDelete("/Test2/{id}", (int id) => Delete(id));
        }

        public static IEnumerable<TestEntityDto> Get()
        {
            var result = new Faker<TestEntityDto>()
                .RuleFor(o => o.Id, f => f.Random.Int(1, 100))
                .RuleFor(o => o.FistName, f => f.Name.FirstName())
                .RuleFor(o => o.LastName, f => f.Name.LastName())
                .RuleFor(o => o.Email, f => f.Internet.Email());

            return result.Generate(2);
        }

        public static TestEntityDto Get(int id)
        {
            var result = new Faker<TestEntityDto>()
                .RuleFor(o => o.Id, id)
                .RuleFor(o => o.FistName, f => f.Name.FirstName())
                .RuleFor(o => o.LastName, f => f.Name.LastName())
                .RuleFor(o => o.Email, f => f.Internet.Email());

            return result.Generate(1)[0];
        }

        public static TestEntityDto Post(TestEntityDto value)
        {
            Assert(value.FistName == "Hello");
            Assert(value.LastName == "World");
            Assert(value.Email == "hello@world.com");

            value.Id = new Faker().Random.Int(1, 100);

            return value;
        }

        public static void Put(int id, TestEntityDto value)
        {
            Assert(id == 1);
            Assert(value.FistName == "Hello");
            Assert(value.LastName == "World");
            Assert(value.Email == "hello@world.com");
        }

        public static void Delete(int id)
        {
            Assert(id == 1);
        }

        private static void Assert(bool expression)
        {
            if (!expression)
                throw new Exception("error");
        }
    }
}
