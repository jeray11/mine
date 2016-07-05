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
        /// <summary>
        /// Key for TopicModel caching
        /// </summary>
        /// <remarks>
        /// {0} : topic system name
        /// {1} : language id
        /// {2} : store id
        /// </remarks>
        public const string TOPIC_MODEL_BY_SYSTEMNAME_KEY = "Nop.pres.topic.details.bysystemname-{0}-{1}-{2}";
    }
}