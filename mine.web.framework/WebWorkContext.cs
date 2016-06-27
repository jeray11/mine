using mine.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mine.core.Domain.Localization;
using System.Web;

namespace mine.web.framework
{
    public class WebWorkContext : IWorkContext
    {
        private Language _cachedLanguage;
        private readonly HttpContextBase _httpContext;
        private readonly LocalizationSettings _localizationSettings;
        public WebWorkContext(HttpContextBase httpContext, LocalizationSettings localizationSettings)
        {
            this._httpContext = httpContext;
            this._localizationSettings = localizationSettings;
        }
        public Language WorkingLanguage
        {
            get
            {
                if (_cachedLanguage != null)
                    return _cachedLanguage;
                Language detectedLanguage = null;
                if (_localizationSettings.SeoFriendlyUrlsForLanguagesEnabled)
                {
                    detectedLanguage = GetLanguageFromUrl();
                }
            }
        }
        protected virtual Language GetLanguageFromUrl()
        {
            if (_httpContext == null || _httpContext.Request == null)
                return null;

            string virtualPath = _httpContext.Request.AppRelativeCurrentExecutionFilePath;
            string applicationPath = _httpContext.Request.ApplicationPath;
            if (!virtualPath.IsLocalizedUrl(applicationPath, false))
                return null;

            var seoCode = virtualPath.GetLanguageSeoCodeFromUrl(applicationPath, false);
            if (String.IsNullOrEmpty(seoCode))
                return null;

            var language = _languageService
                .GetAllLanguages()
                .FirstOrDefault(l => seoCode.Equals(l.UniqueSeoCode, StringComparison.InvariantCultureIgnoreCase));
            if (language != null && language.Published && _storeMappingService.Authorize(language))
            {
                return language;
            }

            return null;
        }
    }
}
