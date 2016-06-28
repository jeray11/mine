using mine.core;
using mine.core.Caching;
using mine.core.Data;
using mine.core.Domain;
using mine.core.Domain.Catalog;
using mine.core.Domain.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.services.Stores
{
    public class StoreMappingService : IStoreMappingService
    {
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : entity ID
        /// {1} : entity name
        /// </remarks>
        private const string STOREMAPPING_BY_ENTITYID_NAME_KEY = "Nop.storemapping.entityid-name-{0}-{1}";
        private readonly CatalogSettings _catalogSettings;
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<StoreMapping> _repository;
        private readonly IStoreContext _storeContext;
        public StoreMappingService(ICacheManager cacheManager,CatalogSettings catalogSettings,IRepository<StoreMapping> repository,IStoreContext storeContext) 
        {
            this._cacheManager = cacheManager;
            this._catalogSettings = catalogSettings;
            this._repository = repository;
            this._storeContext = storeContext;
        }
        public bool Authorize<T>(T entity, int storeId) where T : BaseEntity, IStoreMappingSupported
        {
            if (entity == null)
                return false;

            if (storeId == 0)
                //return true if no store specified/found
                return true;

            if (_catalogSettings.IgnoreStoreLimitations)
                return true;

            if (!entity.LimitedToStores)
                return true;

            foreach (var storeIdWithAccess in GetStoresIdsWithAccess(entity))
                if (storeId == storeIdWithAccess)
                    //yes, we have such permission
                    return true;

            //no permission found
            return false;
        }

        /// <summary>
        /// Find store identifiers with granted access (mapped to the entity)
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="entity">Wntity</param>
        /// <returns>Store identifiers</returns>
        public virtual int[] GetStoresIdsWithAccess<T>(T entity) where T : BaseEntity, IStoreMappingSupported
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            int entityId = entity.Id;
            string entityName = typeof(T).Name;

            string key = string.Format(STOREMAPPING_BY_ENTITYID_NAME_KEY, entityId, entityName);
            return _cacheManager.Get(key, () =>
            {
                var query = from sm in _repository.Table
                            where sm.EntityId == entityId &&
                            sm.EntityName == entityName
                            select sm.StoreId;
                return query.ToArray();
            });
        }

        public bool Authorize<T>(T entity) where T : BaseEntity, IStoreMappingSupported
        {
            return Authorize<T>(entity, _storeContext.CurrentStore.Id);
        }
    }
}
