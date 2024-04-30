using System.Web.Http;
using NetFwApi.Common.Extensions;
using NetFwApi.Common.Results;
using NetFwApi.Models;

namespace NetFwApi.Controllers
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
