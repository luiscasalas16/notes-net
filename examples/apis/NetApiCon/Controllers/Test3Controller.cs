using Bogus;
using Microsoft.AspNetCore.Mvc;
using NetApi.Common.Results;
using NetApiCon.Models;

namespace NetApiCon.Controllers
{
    [ApiController, Route("[controller]")]
    public class Test3Controller : ControllerBase
    {
        [HttpGet]
        public Result Get()
        {
            var result = new Faker<TestEntityDto>()
                .RuleFor(o => o.Id, f => f.Random.Int(1, 100))
                .RuleFor(o => o.FistName, f => f.Name.FirstName())
                .RuleFor(o => o.LastName, f => f.Name.LastName())
                .RuleFor(o => o.Email, f => f.Internet.Email());

            return Result.Success(result.Generate(2));
        }

        [HttpGet("{id}")]
        public Result Get(int id)
        {
            var result = new Faker<TestEntityDto>()
                .RuleFor(o => o.Id, id)
                .RuleFor(o => o.FistName, f => f.Name.FirstName())
                .RuleFor(o => o.LastName, f => f.Name.LastName())
                .RuleFor(o => o.Email, f => f.Internet.Email());

            return Result.Success(result.Generate(1)[0]);
        }

        [HttpPost]
        public Result Post(TestEntityDto value)
        {
            Assert(value.FistName == "Hello");
            Assert(value.LastName == "World");
            Assert(value.Email == "hello@world.com");

            value.Id = new Faker().Random.Int(1, 100);

            return Result.Success(value);
        }

        [HttpPut("{id}")]
        public Result Put(int id, TestEntityDto value)
        {
            Assert(id == 1);
            Assert(value.FistName == "Hello");
            Assert(value.LastName == "World");
            Assert(value.Email == "hello@world.com");

            return Result.Success();
        }

        [HttpDelete("{id}")]
        public Result Delete(int id)
        {
            Assert(id == 1);

            return Result.Success();
        }

        private void Assert(bool expression)
        {
            if (!expression)
                throw new Exception("error");
        }
    }
}
