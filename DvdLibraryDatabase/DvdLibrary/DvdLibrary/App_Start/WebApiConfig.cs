using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DvdLibrary
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //Need a better explination into why var is needed. (Used to enable full use of CORS in entire API)
            var corsSettings = new EnableCorsAttribute(origins: "*", headers: "*", methods: "*");
            config.EnableCors(corsSettings);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}"
            );
        }
    }
}
