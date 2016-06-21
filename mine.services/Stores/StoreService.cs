using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mine.core.Domain.Stores;
using mine.core.Caching;
using mine.core.Data;

namespace mine.services.Stores
{
    public class StoreService : IStoreService
    {
        /// <summary>
        /// Key for caching
        /// </summary>
        private const string STORES_ALL_KEY = "Nop.stores.all";

        private ICacheManager _cacheManager;
        private IRepository<Store> _storeRepository;
        public StoreService(ICacheManager cacheManager, IRepository<Store> storeRepository)
        {
            this._cacheManager = cacheManager;
            this._storeRepository = storeRepository;
        }
        public List<Store> GetAllStores()
        {
            string key = STORES_ALL_KEY;
            return _cacheManager.Get(key, () =>
            {
                var query = from s in _storeRepository.Table
                            orderby s.DisplayOrder, s.Id
                            select s;
                var stores = query.ToList();
                return stores;
            });
        }
    }
}
