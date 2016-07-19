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
        public ThemeContext(StoreInformationSettings storeInformationSettings, 
            IWorkContext workContext, 
            IGenericAttributeService genericAttributeService,
            IStoreContext storeContext) 
        {
            this._storeInformationSettings = storeInformationSettings;
            this._workContext = workContext;
            this._genericAttributeService = genericAttributeService;
            this._storeContext = storeContext;
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
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
