using Microsoft.AspNetCore.Mvc;
using NetApi.Common.Extensions;
using NetApi.Common.Results;
using NetApi.Models;

namespace NetApi.Controllers
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
            return this.ResultValid(TestTypesDto.Generate());
        }
    }
}
