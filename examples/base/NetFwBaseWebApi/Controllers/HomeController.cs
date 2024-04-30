using System.Web.Http;

namespace NetFwBaseWebApi2.Controllers
{
    public class HomeController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Json(new { message = "Hello World!" });
        }
    }
}
