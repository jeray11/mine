using mine.core.Caching;
using mine.web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mine.web.Controllers
{
    public class CommonController : Controller
    {
        private readonly ICacheManager _cacheManager;
        public CommonController(ICacheManager cacheManager) 
        {
            this._cacheManager = cacheManager;
        }
        //
        // GET: /Common/
        [ChildActionOnly]
        public ActionResult CurrencySelector()
        {
            var availableCurrencies = _cacheManager.Get(string.Format(ModelCacheEventConsumer.AVAILABLE_CURRENCIES_MODEL_KEY);
            return View();
        }
	}
}