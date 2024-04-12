using System.Configuration;
using System.Web.Mvc;

namespace NetFwBaseWebMvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}