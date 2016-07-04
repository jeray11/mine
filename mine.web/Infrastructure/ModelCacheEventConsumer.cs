using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mine.web.Infrastructure
{
    public class ModelCacheEventConsumer
    {
        /// <summary>
        /// Key for widget info
        /// </summary>
        /// <remarks>
        /// {0} : current store ID
        /// {1} : widget zone
        /// </remarks>
        public const string WIDGET_MODEL_KEY = "Nop.pres.widget-{0}-{1}";
        public const string WIDGET_PATTERN_KEY = "Nop.pres.widget";
    }
}