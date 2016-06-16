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
            routes.MapRoute("HomePage",
                            "",
                            new { controller = "Home", action = "Index" },
                            new[] { "mine.web.Controllers" });
        }

        public int Priority
        {
            get { return 0; }
        }
    }
}