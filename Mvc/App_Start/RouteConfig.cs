using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("404", "404", new { controller = "Home", action = "NotFound" });
            routes.MapRoute(
                name: "UploadImg",
                url: "Profile/Upload",
                defaults: new { controller = "Profile", action = "Upload"}
                );
            routes.MapRoute(
                name: "Profile",
                url: "Profile/{id}",
                defaults: new { controller = "Profile", action = "Index", id = UrlParameter.Optional });
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
            routes.MapRoute(
               name: "catchall-404",
               url: "{*catchall}",
               defaults: new { Controller = "Home", action = "NotFound" });
        }
    }
}
