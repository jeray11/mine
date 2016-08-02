using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mine.core.Domain.Directory;
using mine.core.Data;
using mine.core.Caching;
using mine.services.Stores;

namespace mine.services.Directory
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IRepository<Currency> _repository;
        private readonly ICacheManager _cacheManager;
        private readonly IStoreMappingService _storeMappingService;
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : show hidden records?
        /// </remarks>
        private const string CURRENCIES_ALL_KEY = "Nop.currency.all-{0}";
        public CurrencyService(ICacheManager  cacheManager,IRepository<Currency> repository,IStoreMappingService storeMappingService)
        {
            this._cacheManager = cacheManager;
            this._repository = repository;
            this._storeMappingService = storeMappingService;
        }
        public IList<Currency> GetAllCurrencies(bool showHidden = false, int storeId = 0)
        {
            string key = string.Format(CURRENCIES_ALL_KEY, showHidden);
            var currencies = _cacheManager.Get(key,()=> {
                var query = _repository.Table;
                if (!showHidden)
                    query = query.Where(c=>c.Published);
                query = query.OrderBy(c=>c.DisplayOrder);
                return query.ToList();
            });
            if (storeId > 0)
                currencies = currencies.Where(c => _storeMappingService.Authorize(c,storeId)).ToList();
            return currencies;
        }
    }
}
