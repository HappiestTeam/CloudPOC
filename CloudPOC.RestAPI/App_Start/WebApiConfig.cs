using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CloudASPNETWebApi
{
    public static class WebApiConfig
    {
        // Adding the comments
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                 name: "DefaultApiWIthMsg",
                 routeTemplate: "api/{controller}/{msg}",
                 defaults: new { msg = RouteParameter.Optional }
            );

            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }
    }
}
