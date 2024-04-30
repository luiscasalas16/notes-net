using System;
using System.Diagnostics;
using System.Web.Http;
using NetFwApi.Common.Extensions;
using NetFwApi.Common.Results;
using NetFwApi.Models;

namespace NetFwApi.Controllers
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
