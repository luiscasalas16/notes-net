using System.Web.Http;

namespace NetFwBaseWebApi.Controllers
{
    public class HomeController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Json(new HomeResponse { Message = Global.Helpers.GetDateTime() });
        }
    }

    public class HomeResponse
    {
        public string Message { get; set; }
    }
}
