using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace mine.web.Models.Cms
{
    public class RenderWidgetModel
    {
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public RouteValueDictionary RouteValues { get; set; }
    }
}