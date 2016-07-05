using mine.core.Caching;
using mine.core.Data;
using mine.core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.services.Localization
{
   public class LocalizedEntityService:ILocalizedEntityService
    {
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : language ID
        /// {1} : entity ID
        /// {2} : locale key group
        /// {3} : locale key
        /// </remarks>
        private const string LOCALIZEDPROPERTY_KEY = "Nop.localizedproperty.value-{0}-{1}-{2}-{3}";
        /// <summary>
        /// Key for caching
        /// </summary>
        private const string LOCALIZEDPROPERTY_ALL_KEY = "Nop.localizedproperty.all";
       private readonly LocalizationSettings _localizationSettings;
       private readonly ICacheManager _cacheManager;
       private readonly IRepository<LocalizedProperty> _localizedPropertyRepository;
       public LocalizedEntityService(ICacheManager cacheManager, LocalizationSettings localizationSettings, IRepository<LocalizedProperty> localizedPropertyRepository) 
       {
           this._cacheManager = cacheManager;
           this._localizationSettings = localizationSettings;
           this._localizedPropertyRepository = localizedPropertyRepository;
       }
        /// <summary>
        /// Find localized value
        /// </summary>
        /// <param name="languageId">Language identifier</param>
        /// <param name="entityId">Entity identifier</param>
        /// <param name="localeKeyGroup">Locale key group</param>
        /// <param name="localeKey">Locale key</param>
        /// <returns>Found localized value</returns>
        public string GetLocalizedValue(int languageId, int entityId, string localeKeyGroup, string localeKey)
        {
            if (_localizationSettings.LoadAllLocalizedPropertiesOnStartup)
            {
                string key = string.Format(LOCALIZEDPROPERTY_KEY, languageId, entityId, localeKeyGroup, localeKey);
                return _cacheManager.Get(key, () =>
                {
                    //load all records (we know they are cached)
                    var source = GetAllLocalizedPropertiesCached();
                    var query = from lp in source
                                where lp.LanguageId == languageId &&
                                lp.EntityId == entityId &&
                                lp.LocaleKeyGroup == localeKeyGroup &&
                                lp.LocaleKey == localeKey
                                select lp.LocaleValue;
                    var localeValue = query.FirstOrDefault();
                    //little hack here. nulls aren't cacheable so set it to ""
                    if (localeValue == null)
                        localeValue = "";
                    return localeValue;
                });

            }
            else
            {
                //gradual loading
                string key = string.Format(LOCALIZEDPROPERTY_KEY, languageId, entityId, localeKeyGroup, localeKey);
                return _cacheManager.Get(key, () =>
                {
                    var source = _localizedPropertyRepository.Table;
                    var query = from lp in source
                                where lp.LanguageId == languageId &&
                                lp.EntityId == entityId &&
                                lp.LocaleKeyGroup == localeKeyGroup &&
                                lp.LocaleKey == localeKey
                                select lp.LocaleValue;
                    var localeValue = query.FirstOrDefault();
                    //little hack here. nulls aren't cacheable so set it to ""
                    if (localeValue == null)
                        localeValue = "";
                    return localeValue;
                });
            }
        }
        /// <summary>
        /// Gets all cached localized properties
        /// </summary>
        /// <returns>Cached localized properties</returns>
        protected virtual IList<LocalizedPropertyForCaching> GetAllLocalizedPropertiesCached()
        {
            //cache
            string key = string.Format(LOCALIZEDPROPERTY_ALL_KEY);
            return _cacheManager.Get(key, () =>
            {
                var query = from lp in _localizedPropertyRepository.Table
                            select lp;
                var localizedProperties = query.ToList();
                var list = new List<LocalizedPropertyForCaching>();
                foreach (var lp in localizedProperties)
                {
                    var localizedPropertyForCaching = new LocalizedPropertyForCaching
                    {
                        Id = lp.Id,
                        EntityId = lp.EntityId,
                        LanguageId = lp.LanguageId,
                        LocaleKeyGroup = lp.LocaleKeyGroup,
                        LocaleKey = lp.LocaleKey,
                        LocaleValue = lp.LocaleValue
                    };
                    list.Add(localizedPropertyForCaching);
                }
                return list;
            });
        }
        #region Nested classes

        [Serializable]
        public class LocalizedPropertyForCaching
        {
            public int Id { get; set; }
            public int EntityId { get; set; }
            public int LanguageId { get; set; }
            public string LocaleKeyGroup { get; set; }
            public string LocaleKey { get; set; }
            public string LocaleValue { get; set; }
        }

        #endregion
    }
}
