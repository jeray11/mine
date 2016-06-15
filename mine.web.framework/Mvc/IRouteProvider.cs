using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace mine.web.framework.Mvc
{
   public interface IRouteProvider
    {
        void RegisterRoutes(RouteCollection routes);

        int Priority { get; }
    }
}
