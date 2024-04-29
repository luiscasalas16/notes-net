using Microsoft.AspNetCore.Mvc;
using net_api.Models;
using net_api_utils.Extensions;
using net_api_utils.Results;

namespace net_api.Controllers
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
