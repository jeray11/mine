﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace mine.web.framework
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString Widget(this HtmlHelper helper, string widgetZone, object additionalData = null)
        {
            return helper.Action("WidgetsByZone", "Widget", new { widgetZone = widgetZone, additionalData = additionalData });
        }
    }
}
