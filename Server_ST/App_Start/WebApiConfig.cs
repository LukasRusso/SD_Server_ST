using Server.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace Server_ST
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {            
            // Rotas da API da Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var formatters = GlobalConfiguration.Configuration.Formatters;
            formatters.Add(formatters.JsonFormatter);
        }
    }
}
