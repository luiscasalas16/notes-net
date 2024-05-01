using System;
using System.Web.Http;
using Api.Tests.Common;
using NetFwApi.Common.Extensions;
using NetFwApi.Common.Results;
using NetFwApi.Models;

namespace NetFwApi.Controllers
{
    public class Test1Controller : ApiController
    {
        [HttpGet]
        public Result Get()
        {
            return GetResult("0");
        }

        [HttpGet]
        public Result GetA()
        {
            return GetResult("A");
        }

        [HttpGet]
        public Result GetB()
        {
            return GetResult("B");
        }

        [HttpGet, ActionName("CGet")]
        public Result GetC()
        {
            return GetResult("C");
        }

        [HttpGet, ActionName("DGet")]
        public Result GetD()
        {
            return GetResult("D");
        }

        private Result GetResult(string id)
        {
            return this.ResultValid(new { OutputMessage = $"{id} - {Helpers.GetDateTime()}" });
        }

        [HttpPost]
        public Result Post(TestRequestDto parameters)
        {
            return PostResult(parameters, "0");
        }

        [HttpPost]
        public Result PostA(TestRequestDto parameters)
        {
            return PostResult(parameters, "A");
        }

        [HttpPost]
        public Result PostB(TestRequestDto parameters)
        {
            return PostResult(parameters, "B");
        }

        [HttpPost, ActionName("CPost")]
        public Result PostC(TestRequestDto parameters)
        {
            return PostResult(parameters, "C");
        }

        [HttpPost, ActionName("DPost")]
        public Result PostD(TestRequestDto parameters)
        {
            return PostResult(parameters, "D");
        }

        private Result PostResult(TestRequestDto parameters, string id)
        {
            if (this.Validate(parameters, out Result resultado))
                return resultado;

            return this.ResultValid(new TestResponseDto() { OutputMessage = $"{parameters.InputMessage ?? ""} - {id} - {Helpers.GetDateTime()}" });
        }
    }
}
