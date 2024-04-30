using System.Web.Http;

namespace NetFwBaseWebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.MapHttpAttributeRoutes();

            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional, controller = "Home" }
            );

            GlobalConfiguration.Configuration.EnsureInitialized();
        }
    }
}
