using Api.Tests.Common.Models;
using Microsoft.AspNetCore.Mvc;
using NetApi.Common.Results;

namespace NetApiCon.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class Test5Controller : ControllerBase
    {
        [HttpGet]
        public TestTypesDto Get1()
        {
            return TestTypesDto.Generate();
        }

        [HttpGet]
        public Result Get2()
        {
            return Result.Success(TestTypesDto.Generate());
        }
    }
}
