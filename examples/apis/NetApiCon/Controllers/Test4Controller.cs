using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NetApi.Common.Extensions;
using NetApi.Common.Results;
using NetApiCon.Models;

namespace NetApiCon.Controllers
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

            return Result.Success();
        }

        private void Assert(bool expression)
        {
            if (!expression)
                throw new Exception("error");
        }
    }
}
