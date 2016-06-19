using mine.web.framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mine.web.Infrastructure
{
    public class RouteProvider:IRouteProvider
    {

        public void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {
            routes.MapRoute(name:"HomePage",
                            url: "homepage/",
                           defaults: new { controller = "Home", action = "Index" },
                           namespaces: new[] { "mine.web.Controllers" });
        }

        public int Priority
        {
            get { return 0; }
        }
    }
}