using mine.core.Domain.Localization;
using mine.core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace mine.web.framework.Localization
{
    public class LocalizedRoute:Route
    {
        #region Fields

        private bool? _seoFriendlyUrlsForLanguagesEnabled;

        #endregion
        #region Properties
        protected bool SeoFriendlyUrlsForLanguagesEnabled
        {
            get
            {
                if (!_seoFriendlyUrlsForLanguagesEnabled.HasValue)
                    _seoFriendlyUrlsForLanguagesEnabled = EngineContext.Current.Resolve<LocalizationSettings>().SeoFriendlyUrlsForLanguagesEnabled;

                return _seoFriendlyUrlsForLanguagesEnabled.Value;
            }
        }
        #endregion
        public LocalizedRoute(string url, IRouteHandler routeHandler) 
            : base(url,routeHandler) { }
        public LocalizedRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler){}
        public LocalizedRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
            : base(url, defaults, constraints, routeHandler){}
        public LocalizedRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler){}

    }
}
