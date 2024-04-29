using System;
using System.Web.Http;
using Bogus;
using netfw_api.Models;
using netfw_api_utils.Extensions;
using netfw_api_utils.Results;

namespace netfw_api.Controllers
{
    public class Test3Controller : ApiController
    {
        public Result Get()
        {
            var result = new Faker<TestEntityDto>()
                .RuleFor(o => o.Id, f => f.Random.Int(1, 100))
                .RuleFor(o => o.FistName, f => f.Name.FirstName())
                .RuleFor(o => o.LastName, f => f.Name.LastName())
                .RuleFor(o => o.Email, f => f.Internet.Email());

            return this.ResultValid(result.Generate(2));
        }

        public Result Get(int id)
        {
            var result = new Faker<TestEntityDto>()
                .RuleFor(o => o.Id, id)
                .RuleFor(o => o.FistName, f => f.Name.FirstName())
                .RuleFor(o => o.LastName, f => f.Name.LastName())
                .RuleFor(o => o.Email, f => f.Internet.Email());

            return this.ResultValid(result.Generate(1)[0]);
        }

        public Result Post(TestEntityDto value)
        {
            Assert(value.FistName == "Hello");
            Assert(value.LastName == "World");
            Assert(value.Email == "hello@world.com");

            value.Id = new Faker().Random.Int(1, 100);

            return this.ResultValid(value);
        }

        public Result Put(int id, TestEntityDto value)
        {
            Assert(id == 1);
            Assert(value.FistName == "Hello");
            Assert(value.LastName == "World");
            Assert(value.Email == "hello@world.com");

            return this.ResultValid();
        }

        public Result Delete(int id)
        {
            Assert(id == 1);

            return this.ResultValid();
        }

        private void Assert(bool expression)
        {
            if (!expression)
                throw new Exception("error");
        }
    }
}
