using mine.core.Infrastructure;
using mine.web.framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace mine.web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //register custom routes (plugins, etc)
            var routePublisher = EngineContext.Current.Resolve<IRoutePublisher>();
            routePublisher.RegisterRoutes(routes);

            routes.MapRoute(
               name: "Default",
               url: "{lang}/{controller}/{action}/{id}",
               defaults: new { controller = "Boards", action = "Index", id = UrlParameter.Optional,lang=UrlParameter.Optional },
               namespaces: new[] { "mine.web.Controllers" }
           );

       
        }
    }
}
