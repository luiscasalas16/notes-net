using System;
using System.Collections.Generic;
using System.Web.Http;
using Bogus;
using netfw_api.Models;

namespace netfw_api.Controllers
{
    public class Test2Controller : ApiController
    {
        public IEnumerable<TestEntityDto> Get()
        {
            var result = new Faker<TestEntityDto>()
                .RuleFor(o => o.Id, f => f.Random.Int(1, 100))
                .RuleFor(o => o.FistName, f => f.Name.FirstName())
                .RuleFor(o => o.LastName, f => f.Name.LastName())
                .RuleFor(o => o.Email, f => f.Internet.Email());

            return result.Generate(2);
        }

        public TestEntityDto Get(int id)
        {
            var result = new Faker<TestEntityDto>()
                .RuleFor(o => o.Id, id)
                .RuleFor(o => o.FistName, f => f.Name.FirstName())
                .RuleFor(o => o.LastName, f => f.Name.LastName())
                .RuleFor(o => o.Email, f => f.Internet.Email());

            return result.Generate(1)[0];
        }

        public TestEntityDto Post(TestEntityDto value)
        {
            Assert(value.FistName == "Hello");
            Assert(value.LastName == "World");
            Assert(value.Email == "hello@world.com");

            value.Id = new Faker().Random.Int(1, 100);

            return value;
        }

        public IHttpActionResult Put(int id, TestEntityDto value)
        {
            Assert(id == 1);
            Assert(value.FistName == "Hello");
            Assert(value.LastName == "World");
            Assert(value.Email == "hello@world.com");

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            Assert(id == 1);

            return Ok();
        }

        private void Assert(bool expression)
        {
            if (!expression)
                throw new Exception("error");
        }
    }
}
