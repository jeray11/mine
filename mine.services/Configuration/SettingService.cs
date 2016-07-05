using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mine.core.Configuration;
using mine.core.Caching;
using mine.core.Data;
using mine.core;

namespace mine.services.Configuration
{
    public class SettingService : ISettingService
    {
        /// <summary>
        /// Key for caching
        /// </summary>
        private const string SETTINGS_ALL_KEY = "Nop.setting.all";

        private readonly ICacheManager _cacheManager;
        private readonly IRepository<Setting> _settingRepository;

        public SettingService(ICacheManager cacheManager, IRepository<Setting> settingRepository)
        {
            this._cacheManager = cacheManager;
            this._settingRepository = settingRepository;
        }
        public T LoadSetting<T>(int storeId = 0) where T : ISettings, new()
        {
            var settings = Activator.CreateInstance<T>();
            foreach (var prop in typeof(T).GetProperties())
            {
                // get properties we can read and write to
                if (!prop.CanRead || !prop.CanWrite)
                    continue;

                var key = typeof(T).Name + "." + prop.Name;
                //load by store
                var setting = GetSettingByKey<string>(key, storeId: storeId, loadSharedValueIfNotFound: true);
                if (setting == null)
                    continue;

                if (!CommonHelper.GetMineCustomTypeConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
                    continue;

                if (!CommonHelper.GetMineCustomTypeConverter(prop.PropertyType).IsValid(setting))
                    continue;

                object value = CommonHelper.GetMineCustomTypeConverter(prop.PropertyType).ConvertFromInvariantString(setting);

                //set property
                prop.SetValue(settings, value, null);
            }
            return settings;
        }

        /// <summary>
        /// Get setting value by key
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">Key</param>
        /// <param name="defaultValue">Default value</param>
        /// <param name="storeId">Store identifier</param>
        /// <param name="loadSharedValueIfNotFound">A value indicating whether a shared (for all stores) value should be loaded if a value specific for a certain is not found</param>
        /// <returns>Setting value</returns>
        public virtual T GetSettingByKey<T>(string key, T defaultValue = default(T),
            int storeId = 0, bool loadSharedValueIfNotFound = false)
        {
            if (String.IsNullOrEmpty(key))
                return defaultValue;

            var settings = GetAllSettingsCached();
            key = key.Trim().ToLowerInvariant();
            if (settings.ContainsKey(key))
            {
                var settingsByKey = settings[key];
                var setting = settingsByKey.FirstOrDefault(x => x.StoreId == storeId);

                //load shared value?
                if (setting == null && storeId > 0 && loadSharedValueIfNotFound)
                    setting = settingsByKey.FirstOrDefault(x => x.StoreId == 0);

                if (setting != null)
                    return CommonHelper.To<T>(setting.Value);
            }

            return defaultValue;
        }

        /// <summary>
        /// Gets all settings
        /// </summary>
        /// <returns>Settings</returns>
        protected virtual IDictionary<string, IList<SettingForCaching>> GetAllSettingsCached()
        {
            //cache
            string key = string.Format(SETTINGS_ALL_KEY);
            return _cacheManager.Get(key, () =>
            {
                //we use no tracking here for performance optimization
                //anyway records are loaded only for read-only operations
                var query = from s in _settingRepository.TableNoTracking
                            orderby s.Name, s.StoreId
                            select s;
                var settings = query.ToList();
                var dictionary = new Dictionary<string, IList<SettingForCaching>>();
                foreach (var s in settings)
                {
                    var resourceName = s.Name.ToLowerInvariant();
                    var settingForCaching = new SettingForCaching
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Value = s.Value,
                        StoreId = s.StoreId
                    };
                    if (!dictionary.ContainsKey(resourceName))
                    {
                        //first setting
                        dictionary.Add(resourceName, new List<SettingForCaching>
                        {
                            settingForCaching
                        });
                    }
                    else
                    {
                        //already added
                        //most probably it's the setting with the same name but for some certain store (storeId > 0)
                        dictionary[resourceName].Add(settingForCaching);
                    }
                }
                return dictionary;
            });
        }

        [Serializable]
        public class SettingForCaching
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Value { get; set; }
            public int StoreId { get; set; }
        }


        public void SaveSetting<T>(T settings, int storeId=0) where T : ISettings, new()
        {
            if (settings == null)
                throw new ArgumentNullException("settings");
            var type = typeof(T);
            string typename=type.Name;
            var propinfos=type.GetProperties();
            List<Setting> list = new List<Setting>();
            foreach (var prop in propinfos)
            {
                Setting setting=new Setting{
                    Name = string.Format("{0}.{1}", typename, prop.Name),
                     StoreId=storeId,
                     Value=Convert.ToString(prop.GetValue(settings))
                };
                list.Add(setting);
            }
            
        }
    }
}
