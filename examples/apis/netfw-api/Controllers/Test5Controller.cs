using System.Web.Http;
using netfw_api.Models;
using netfw_api_utils.Extensions;
using netfw_api_utils.Results;

namespace netfw_api.Controllers
{
    public class Test5Controller : ApiController
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
