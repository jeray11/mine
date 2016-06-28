using mine.core.Caching;
using mine.core.Data;
using mine.core.Domain.Localization;
using mine.services.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.services.Localization
{
   public class LanguageService:ILanguageService
    {
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : show hidden records?
        /// </remarks>
        private const string LANGUAGES_ALL_KEY = "Nop.language.all-{0}";

       private readonly ICacheManager _cacheManager;
       private readonly IRepository<Language> _repository;
       private readonly IStoreMappingService _storeMappingService;
       public LanguageService(ICacheManager cacheManager,IRepository<Language> repository,IStoreMappingService storeMappingService) 
       {
           this._cacheManager = cacheManager;
           this._repository = repository;
           this._storeMappingService = storeMappingService;
       }
        public IList<core.Domain.Localization.Language> GetAllLanguages(bool showHidden = false, int storeId = 0)
        {
            string key = string.Format(LANGUAGES_ALL_KEY, showHidden);
            var languages = _cacheManager.Get(key, () =>
            {
                var query = _repository.Table;
                if (!showHidden)
                    query = query.Where(l => l.Published);
                query = query.OrderBy(l => l.DisplayOrder);
                return query.ToList();
            });
            //store mapping
            if (storeId > 0)
            {
                languages = languages
                    .Where(l => _storeMappingService.Authorize(l, storeId))
                    .ToList();
            }
            return languages;
        }
    }
}
