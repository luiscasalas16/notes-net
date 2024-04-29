using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using net_api.Models;
using net_api_utils.Extensions;
using net_api_utils.Results;

namespace net_api.Controllers
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
