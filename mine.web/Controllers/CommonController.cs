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
        public CommonController(ICacheManager cacheManager,IWorkContext workContext, IStoreContext storeContext,ICurrencyService currencyService)
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._currencyService = currencyService;
        }
        //
        // GET: /Common/
        [ChildActionOnly]
        public ActionResult CurrencySelector()
        {
            var availableCurrencies = _cacheManager.Get(string.Format(ModelCacheEventConsumer.AVAILABLE_CURRENCIES_MODEL_KEY,_workContext.WorkingLanguage.Id,_storeContext.CurrentStore.Id),()=> {
                var query = _currencyService.GetAllCurrencies(storeId: _storeContext.CurrentStore.Id)
                    .Select(c => {
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
            return View();
        }
	}
}