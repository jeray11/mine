using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace mine.web.framework.Mvc
{
    public interface IRoutePublisher
    {
        /// <summary>
        /// Register routes
        /// </summary>
        /// <param name="routes">Routes</param>
        void RegisterRoutes(RouteCollection routes);
    }
}
