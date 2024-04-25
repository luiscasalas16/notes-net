using System.Web.Mvc;

namespace NetFwBaseWebMvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Test()
        {
            return Json(new { datetime = Global.Helpers.GetDateTime() });
        }
    }
}
