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


            var jsonIDRemove = config.Formatters.JsonFormatter;
            jsonIDRemove.SerializerSettings.PreserveReferencesHandling =
                Newtonsoft.Json.PreserveReferencesHandling.None;
            //var json = config.Formatters.JsonFormatter;
            //json.SerializerSettings.PreserveReferencesHandling =
            //    Newtonsoft.Json.PreserveReferencesHandling.Objects;

            config.Formatters.Remove(config.Formatters.XmlFormatter);

        }
    }
}
