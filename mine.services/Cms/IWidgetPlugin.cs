using mine.core.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace mine.services.Cms
{
    public interface IWidgetPlugin:IPlugin
    {
        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        IList<string> GetWidgetZones();

        void GetDisplayWidgetRoute(string widgetZone, out string actionName, out string controllerName, out RouteValueDictionary routeValues);
    }
}
