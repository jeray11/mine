using mine.core;
using mine.core.Caching;
using mine.services.Directory;
using mine.web.Infrastructure;
using mine.web.Models.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mine.services.Localization;

namespace mine.web.Controllers
{
    public class CommonController : Controller
    {
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly ICurrencyService _currencyService;
        private readonly ILanguageService _languageService;
        public CommonController(ICacheManager cacheManager, IWorkContext workContext, IStoreContext storeContext, ICurrencyService currencyService, ILanguageService languageService)
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._currencyService = currencyService;
            this._languageService = languageService;
        }
        //
        // GET: /Common/
        [ChildActionOnly]
        public ActionResult CurrencySelector()
        {
            var availableCurrencies = _cacheManager.Get(string.Format(ModelCacheEventConsumer.AVAILABLE_CURRENCIES_MODEL_KEY, _workContext.WorkingLanguage.Id, _storeContext.CurrentStore.Id), () =>
            {
                var query = _currencyService.GetAllCurrencies(storeId: _storeContext.CurrentStore.Id)
                    .Select(c =>
                    {
                        var currencySymbol = "";
                        if (!string.IsNullOrEmpty(c.DisplayLocale))
                            currencySymbol = new RegionInfo(c.DisplayLocale).CurrencySymbol;
                        else
                            currencySymbol = c.CurrencyCode;
                        //model
                        var currencyModel = new CurrencyModel
                        {
                            Id = c.Id,
                            Name = c.GetLocalized(y => y.Name),
                            CurrencySymbol = currencySymbol
                        };
                        return currencyModel;
                    });

                return query;
            });
            return View(availableCurrencies);
        }
        [ChildActionOnly]
        public ActionResult LanguageSelector()
        {
            var avaliableLanguages = _cacheManager.Get(string.Format(ModelCacheEventConsumer.AVAILABLE_LANGUAGES_MODEL_KEY, _storeContext.CurrentStore.Id), () =>
            {
                var query = _languageService.GetAllLanguages(storeId: _storeContext.CurrentStore.Id)
                    .Select(l => new LanguageModel
                    {
                        Id=l.Id,
                        Name = l.Name,
                        FlagImageFileName = l.FlagImageFileName
                    }).ToList();
                return query;
            });

            return PartialView();
        }
        public ActionResult SetCurrency(int customerCurrency, string returnUrl = "")
        {
            var currency = _currencyService.GetCurrencyById(customerCurrency);
            if (currency != null)
                _workContext.WorkingCurrency = currency;

            //home page
            if (String.IsNullOrEmpty(returnUrl))
                returnUrl = Url.RouteUrl("HomePage");

            //prevent open redirection attack
            if (!Url.IsLocalUrl(returnUrl))
                returnUrl = Url.RouteUrl("HomePage");

            return Redirect(returnUrl);
        }
    }
}