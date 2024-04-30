using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NetApi.Common.Extensions;
using NetApi.Common.Results;
using NetApi.Models;

namespace NetApi.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class Test4Controller : ControllerBase
    {
        [HttpGet]
        [DebuggerStepThrough]
        public Result ErrorGet()
        {
            throw new ApplicationException("application error");
        }

        [HttpPost]
        public Result ErrorValidation(TestEntityDto value)
        {
            Assert(value.FistName == "Hello");
            Assert(value.LastName == "World");
            Assert(value.Email == "hello@world.com");

            return this.ResultValid();
        }

        private void Assert(bool expression)
        {
            if (!expression)
                throw new Exception("error");
        }
    }
}
