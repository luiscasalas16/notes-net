using System;
using System.Diagnostics;
using System.Web.Http;
using netfw_api.Models;
using netfw_api_utils.Extensions;
using netfw_api_utils.Results;

namespace netfw_api.Controllers
{
    public class Test4Controller : ApiController
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
