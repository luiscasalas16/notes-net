using Microsoft.AspNetCore.Mvc;
using NetApi.Common.Extensions;
using NetApi.Common.Results;
using NetApiCon.Models;

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
            return this.ResultValid(TestTypesDto.Generate());
        }
    }
}
