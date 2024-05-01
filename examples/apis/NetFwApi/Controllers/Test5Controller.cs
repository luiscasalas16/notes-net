using System.Web.Http;
using Api.Tests.Common.Models;
using NetFwApi.Common.Extensions;
using NetFwApi.Common.Results;

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
