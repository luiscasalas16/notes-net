using Api.Tests.Common;
using Microsoft.AspNetCore.Mvc;
using NetApi.Common.Extensions;
using NetApi.Common.Results;
using NetApiCon.Models;

namespace NetApiCon.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class Test1Controller : ControllerBase
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
            return this.ResultValid(
                new TestDtoResult { OutputMessage = $"{id} - {Helpers.GetDateTime()}" }
            );
        }

        [HttpPost]
        public Result Post(TestDto parameters)
        {
            return PostResult(parameters, "0");
        }

        [HttpPost]
        public Result PostA(TestDto parameters)
        {
            return PostResult(parameters, "A");
        }

        [HttpPost]
        public Result PostB(TestDto parameters)
        {
            return PostResult(parameters, "B");
        }

        [HttpPost, ActionName("CPost")]
        public Result PostC(TestDto parameters)
        {
            return PostResult(parameters, "C");
        }

        [HttpPost, ActionName("DPost")]
        public Result PostD(TestDto parameters)
        {
            return PostResult(parameters, "D");
        }

        private Result PostResult(TestDto parameters, string id)
        {
            if (this.Validate(parameters, out Result resultado))
                return resultado;

            return this.ResultValid(
                new TestDtoResult()
                {
                    OutputMessage = $"{parameters.InputMessage} - {id} - {Helpers.GetDateTime()}"
                }
            );
        }
    }
}
