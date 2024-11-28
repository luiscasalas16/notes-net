using Api.Tests.Common;
using Api.Tests.Common.Models;
using Microsoft.AspNetCore.Mvc;
using NetApi.Common.Results;

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
            return Result.Success(new TestResponseDto { OutputMessage = $"{id} - {Helpers.GetDateTime()}" });
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
            return Result.Success(new TestResponseDto() { OutputMessage = $"{parameters.InputMessage} - {id} - {Helpers.GetDateTime()}" });
        }
    }
}
