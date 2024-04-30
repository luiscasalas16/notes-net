using System.Web.Http;
using Swashbuckle.Application;

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

            // Enable Swagger
            GlobalConfiguration
                .Configuration.EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "NetFwBaseWebApi");
                })
                .EnableSwaggerUi();

            GlobalConfiguration.Configuration.EnsureInitialized();
        }
    }
}
