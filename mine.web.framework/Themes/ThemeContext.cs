using mine.core;
using mine.core.Domain;
using mine.core.Domain.Customers;
using mine.services;
using mine.services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.web.framework.Themes
{
    public class ThemeContext:IThemeContext
    {
        private bool _themeIsCached;
        private string _cachedThemeName;
        private readonly StoreInformationSettings _storeInformationSettings;
        private readonly IWorkContext _workContext;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IStoreContext _storeContext;
        private readonly IThemeProvider _themeProvider;
        public ThemeContext(StoreInformationSettings storeInformationSettings, 
            IWorkContext workContext, 
            IGenericAttributeService genericAttributeService,
            IStoreContext storeContext,
            IThemeProvider themeProvider) 
        {
            this._storeInformationSettings = storeInformationSettings;
            this._workContext = workContext;
            this._genericAttributeService = genericAttributeService;
            this._storeContext = storeContext;
            this._themeProvider = themeProvider;
        }
        public string WorkingThemeName
        {
            get
            {
                if (_themeIsCached)
                    return _cachedThemeName;
                string theme = "";
                if (_storeInformationSettings.AllowCustomerToSelectTheme)
                {
                    if (_workContext.CurrentCustomer != null)
                        theme = _workContext.CurrentCustomer.GetAttribute<string>(SystemCustomerAttributeNames.WorkingThemeName, _genericAttributeService, _storeContext.CurrentStore.Id);
                }
                //default store theme
                if (string.IsNullOrEmpty(theme))
                    theme = _storeInformationSettings.DefaultStoreTheme;
                //ensure that theme exists
                if (!_themeProvider.ThemeConfigurationExists(theme))
                {
                    var themeInstance = _themeProvider.GetThemeConfigurations()
                        .FirstOrDefault();
                    if (themeInstance == null)
                        throw new Exception("No theme could be loaded");
                    theme = themeInstance.ThemeName;
                }
                //cache theme
                this._cachedThemeName = theme;
                this._themeIsCached = true;
                return theme;
            }
            set
            {
                if (!_storeInformationSettings.AllowCustomerToSelectTheme)
                    return;
                if (_workContext.CurrentCustomer == null)
                    return;
                _genericAttributeService.SaveAttribute(_workContext.CurrentCustomer, SystemCustomerAttributeNames.WorkingThemeName, value, _storeContext.CurrentStore.Id);
                //clear cache
                this._themeIsCached = false;
            }
        }
    }
}
