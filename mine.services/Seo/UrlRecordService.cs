using mine.core.Caching;
using mine.core.Data;
using mine.core.Domain.Localization;
using mine.core.Domain.Seo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.services.Seo
{
    public class UrlRecordService:IUrlRecordService
    {
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : entity ID
        /// {1} : entity name
        /// {2} : language ID
        /// </remarks>
        private const string URLRECORD_ACTIVE_BY_ID_NAME_LANGUAGE_KEY = "Nop.urlrecord.active.id-name-language-{0}-{1}-{2}";
        /// <summary>
        /// Key for caching
        /// </summary>
        private const string URLRECORD_ALL_KEY = "Nop.urlrecord.all";
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<UrlRecord> _urlRecordRepository;
        private readonly LocalizationSettings _localizationSettings;
        public UrlRecordService(ICacheManager cacheManager, 
            IRepository<UrlRecord> urlRecordRepository,
            LocalizationSettings localizationSettings) 
        {
            this._cacheManager = cacheManager;
            this._urlRecordRepository = urlRecordRepository;
            this._localizationSettings = localizationSettings;
        }
        public string GetActiveSlug(int entityId, string entityName, int languageId)
        {
            string key = string.Format(URLRECORD_ACTIVE_BY_ID_NAME_LANGUAGE_KEY, entityId, entityName, languageId);
            if (_localizationSettings.LoadAllUrlRecordsOnStartup)
            {
                var result = _cacheManager.Get(key, () =>
                {
                    var source = GetAllUrlRecordsCached();
                    var query = from s in source
                                where s.EntityId == entityId && s.EntityName == entityName && s.LanguageId == languageId && s.IsActive
                                orderby s.Id descending
                                select s.Slug;
                    var slug= query.FirstOrDefault();
                    if (slug == null)
                        slug = "";
                    return slug;
                });
                return result;
            }
            else
            {
                var result = _cacheManager.Get(key,() =>
                {
                    var query = from s in _urlRecordRepository.TableNoTracking
                                where s.EntityId == entityId && s.EntityName == entityName && s.LanguageId == languageId && s.IsActive
                                orderby s.Id descending
                                select s.Slug;
                    var slug= query.FirstOrDefault();
                    if (slug == null)
                        slug="";
                    return slug;
                });
                return result;
            }
        }
         /// <summary>
        /// Gets all cached URL records
        /// </summary>
        /// <returns>cached URL records</returns>
        protected virtual IList<UrlRecordForCaching> GetAllUrlRecordsCached()
        {
            string key = string.Format(URLRECORD_ALL_KEY);
            return _cacheManager.Get(key, () => {
                var query = _urlRecordRepository.TableNoTracking;
                List<UrlRecordForCaching> records=new List<UrlRecordForCaching>();
                foreach (var q in query) 
                {
                    records.Add(new UrlRecordForCaching { 
                        Id=q.Id,
                        EntityId=q.EntityId,
                        EntityName=q.EntityName,
                        Slug=q.Slug,
                        IsActive=q.IsActive,
                        LanguageId=q.LanguageId
                    });
                }
                return records;
            });
        }
        public class UrlRecordForCaching 
        {
            public int Id { get; set; }
            public int EntityId { get; set; }
            public string EntityName { get; set; }
            public string Slug { get; set; }
            public bool IsActive { get; set; }
            public int LanguageId { get; set; }
        }
    }
}
