using SOMIOD.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SomiodApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Formatters.Insert(0, config.Formatters.XmlFormatter);
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "SomiodApi",
                routeTemplate: "api/somiod/{controller}/{name}",
                defaults: new { name = RouteParameter.Optional }
            );
        }
    }
}
