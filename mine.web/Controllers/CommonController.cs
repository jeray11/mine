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
using mine.core.Domain.Localization;
using mine.core.Domain.Forums;
using mine.core.Domain.Customers;
using mine.services.Forums;
using mine.services;
using mine.services.Common;
using mine.services.Security;

namespace mine.web.Controllers
{
    public class CommonController : Controller
    {
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly ICurrencyService _currencyService;
        private readonly ILanguageService _languageService;
        private readonly LocalizationSettings _localizationSettings;
        private readonly ForumSettings _forumSettings;
        private readonly IForumService _forumService;
        private readonly ILocalizationService _localizationService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly CustomerSettings _customerSettings;
        private readonly IPermissionService _permissionService;
        public CommonController(
            ICacheManager cacheManager, 
            IWorkContext workContext, 
            IStoreContext storeContext, 
            ICurrencyService currencyService, 
            ILanguageService languageService,
            LocalizationSettings localizationSettings,
            ForumSettings forumSettings,
            IForumService forumService,
            ILocalizationService localizationService,
            IGenericAttributeService genericAttributeService,
            CustomerSettings customerSettings,
            IPermissionService permissionService)
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._currencyService = currencyService;
            this._languageService = languageService;
            this._localizationSettings = localizationSettings;
            this._forumSettings = forumSettings;
            this._forumService = forumService;
            this._localizationService = localizationService;
            this._genericAttributeService = genericAttributeService;
            this._customerSettings = customerSettings;
            this._permissionService = permissionService;
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

                return query.ToList();
            });
            var model = new CurrencySelectorModel
            {
                CurrentCurrencyId = _workContext.WorkingCurrency.Id,
                AvailableCurrencies = availableCurrencies
            };

            if (model.AvailableCurrencies.Count == 1)
                Content("");

            return PartialView(model);
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
            var selModel = new LanguageSelectorModel
            {
                AvailableLanguages = avaliableLanguages,
                CurrentLanguageId = _workContext.WorkingLanguage.Id,
                UseImages=_localizationSettings.UseImagesForLanguageSelection
            };
            if (selModel.AvailableLanguages.Count == 1)
                return Content("");
            return PartialView(selModel);
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

        public ActionResult HeaderLinks() 
        {
            var customer = _workContext.CurrentCustomer;
            var unreadMessageCount = GetUnreadPrivateMessages();
            var unreadMessage = string.Empty;
            var alertMessage = string.Empty;
            if (unreadMessageCount > 0)
            {
                unreadMessage = string.Format(_localizationService.GetResource("PrivateMessages.TotalUnread"), unreadMessageCount);
                //notifications here
                if (_forumSettings.ShowAlertForPM &&
                    !customer.GetAttribute<bool>(SystemCustomerAttributeNames.NotifiedAboutNewPrivateMessages, _storeContext.CurrentStore.Id))
                {
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.NotifiedAboutNewPrivateMessages, true, _storeContext.CurrentStore.Id);
                    alertMessage = string.Format(_localizationService.GetResource("PrivateMessages.YouHaveUnreadPM"), unreadMessageCount);
                }
            }

            var vm = new HeaderLinksModel 
            {
                IsAuthenticated=customer.IsRegistered(),
                AllowPrivateMessages = customer.IsRegistered() && _forumSettings.AllowPrivateMessages,
                AlertMessage = alertMessage,
                 UnreadPrivateMessages=unreadMessage,
                CustomerEmailUsername = customer.IsRegistered() ? (_customerSettings.UsernamesEnabled ? customer.Username : customer.Email) : "",
                ShoppingCartEnabled = _permissionService.Authorize(StandardPermissionProvider.EnableShoppingCart),
                WishlistEnabled = _permissionService.Authorize(StandardPermissionProvider.EnableWishlist),
            };
        }

        [NonAction]
        protected virtual int GetUnreadPrivateMessages()
        {
            var result = 0;
            var customer = _workContext.CurrentCustomer;
            if (_forumSettings.AllowPrivateMessages && !customer.IsGuest())
            {
                var privateMessages = _forumService.GetAllPrivateMessages(_storeContext.CurrentStore.Id,
                    0, customer.Id, false, null, false, string.Empty, 0, 1);

                if (privateMessages.TotalCount > 0)
                {
                    result = privateMessages.TotalCount;
                }
            }

            return result;
        }
    }
}