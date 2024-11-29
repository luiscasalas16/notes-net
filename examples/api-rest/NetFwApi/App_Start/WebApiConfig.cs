using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Routing;
using ApiMultiPartFormData;
using NetFwApi.Common.Errores;
using NetFwApi.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace NetFwApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Sets Newtonsoft as default serializer and sets the default serializer settings.

            var defaultSettings = new JsonSerializerSettings
            {
                //DateFormatString = "dd/MM/yyyy HH:mm:ss",
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter> { new StringEnumConverter() }
            };

            JsonConvert.DefaultSettings = () =>
            {
                return defaultSettings;
            };

            config.Formatters.JsonFormatter.SerializerSettings = defaultSettings;

            // Register http attributes routes.

            config.MapHttpAttributeRoutes();

            // Register custom routes to enable:
            // - the use of multiple methods with the same http verb.
            // - the use of methods with the same http verb name.

            config.Routes.MapHttpRoute("ApiId", "{controller}/{id}", new { id = RouteParameter.Optional }, new { id = @"\d+" });

            config.Routes.MapHttpRoute("ApiAction", "{controller}/{action}");

            config.Routes.MapHttpRoute("ApiGet", "{controller}", new { action = "Get" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });
            config.Routes.MapHttpRoute("ApiPost", "{controller}", new { action = "Post" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });
            config.Routes.MapHttpRoute("ApiPut", "{controller}", new { action = "Put" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) });
            config.Routes.MapHttpRoute("ApiDelete", "{controller}", new { action = "Delete" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Delete) });

            // Implements default exception handler.
            config.Services.Replace(typeof(IExceptionHandler), new DefaultExceptionHandler());

            // Register multipart/form-data formatter.
            //config.Formatters.Add(new MultipartFormDataFormatter());

            // Enables automatic validation of objects that have validation annotations.
            config.Filters.Add(new ValidateModelAttribute());
        }
    }
}
