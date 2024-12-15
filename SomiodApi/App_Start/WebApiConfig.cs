using SOMIOD.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace SomiodApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //Clear formatters
            config.Formatters.Clear();
            
            //Xml first to be default
            config.Formatters.Add(new XmlMediaTypeFormatter());
            //Json after to be compatible with both formats
            config.Formatters.Add(new JsonMediaTypeFormatter());

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
