using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Re
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    new string[] { "Re.Areas.Admin.Controllers" }
            //);

            //routes.MapRoute()
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
               new { controller = "Home", action = "Login", id = UrlParameter.Optional },
                //new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "Re.Controllers" }
            );
        }
    }
}