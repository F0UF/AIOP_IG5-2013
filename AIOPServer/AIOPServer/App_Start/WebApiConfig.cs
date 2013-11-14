using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AIOPServer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
           
            config.Routes.MapHttpRoute(
                name: "AccountAPI",
                routeTemplate: "api/{controller}/{action}"
            );
        }
    }
}
