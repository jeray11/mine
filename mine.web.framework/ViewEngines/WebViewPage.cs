using mine.core.Infrastructure;
using mine.services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace mine.web.framework.ViewEngines
{
    public abstract class WebViewPage<TModel>:System.Web.Mvc.WebViewPage<TModel>
    {
        private ILocalizationService _localizationService;
        /// <summary>
        /// Get a localized resources
        /// </summary>
        public IHtmlString T(string format, params object[] args)
        {
            var resFormat = _localizationService.GetResource(format);
            if (string.IsNullOrEmpty(resFormat))
            {
                return new HtmlString(format);
            }
            return
                new HtmlString((args == null || args.Length == 0)
                                        ? resFormat
                                        : string.Format(resFormat, args));
        }

        public override void InitHelpers()
        {
            base.InitHelpers();

            _localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            
        }

    }

    public abstract class WebViewPage : WebViewPage<dynamic>
    {
    }
}
