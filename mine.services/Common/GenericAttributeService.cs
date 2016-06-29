using mine.core;
using mine.core.Caching;
using mine.core.Data;
using mine.core.Domain;
using mine.core.Domain.Common;
using mine.data;
using mine.services.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.services.Common
{
    public class GenericAttributeService:IGenericAttributeService
    {
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : entity ID
        /// {1} : key group
        /// </remarks>
        private const string GENERICATTRIBUTE_KEY = "Nop.genericattribute.{0}-{1}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string GENERICATTRIBUTE_PATTERN_KEY = "Nop.genericattribute.";
        private ICacheManager _cacheManager;
        private IRepository<GenericAttribute> _repository;
        private IEventPublisher _eventPublisher;
        public GenericAttributeService(ICacheManager cacheManager,IRepository<GenericAttribute> repository,IEventPublisher eventPublisher) 
        {
            this._cacheManager = cacheManager;
            this._repository = repository;
            this._eventPublisher = eventPublisher;
        }
        public IList<GenericAttribute> GetAttributesForEntity(int entityId, string keyGroup)
        {
            string key = string.Format(GENERICATTRIBUTE_KEY,entityId,keyGroup);
            return _cacheManager.Get(key, () => {
                var query = from g in _repository.Table
                            where g.EntityId == entityId && g.KeyGroup == keyGroup
                            select g;
                return query.ToList();
            });
        }
        public void SaveAttribute<TPropType>(BaseEntity entity, string key, TPropType value, int storeId = 0) 
        {
            if(entity==null)
                throw new ArgumentNullException("entity");
            if(string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");
            string keyGroup = entity.GetUnproxiedEntityType().Name;
            var props = GetAttributesForEntity(entity.Id, keyGroup)
                .Where(x => x.StoreId == storeId)
                .ToList();
            var prop = props.FirstOrDefault(ga =>
                ga.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)); //should be culture invariant
            var valueStr = CommonHelper.To<string>(value);
            if (prop != null)
            {
                if (string.IsNullOrWhiteSpace(valueStr))
                {
                    //delete
                    DeleteAttribute(prop);
                }
                else
                {
                    //update
                    prop.Value = valueStr;
                    UpdateAttribute(prop);
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(valueStr))
                {
                    //insert
                    prop = new GenericAttribute
                    {
                        EntityId = entity.Id,
                        Key = key,
                        KeyGroup = keyGroup,
                        Value = valueStr,
                        StoreId = storeId,

                    };
                    InsertAttribute(prop);
                }
            }
        }

        public void DeleteAttribute(GenericAttribute attribute) 
        {
            if (attribute == null)
                throw new ArgumentNullException("attribute");
            _repository.Delete(attribute);
            //cache
            string key = string.Format(GENERICATTRIBUTE_KEY,attribute.EntityId,attribute.KeyGroup);
            //_cacheManager.RemoveByPattern(GENERICATTRIBUTE_PATTERN_KEY);nop的做法 感觉没有必要 都删
            _cacheManager.Remove(key);
            //event notification
            _eventPublisher.EntityDeleted(attribute);
        }
        public void UpdateAttribute(GenericAttribute attribute) 
        {
            if (attribute == null)
                throw new ArgumentNullException("attribute");
            _repository.Update(attribute);
            string key = string.Format(GENERICATTRIBUTE_KEY, attribute.EntityId, attribute.KeyGroup);
            //cache
            //_cacheManager.RemoveByPattern(GENERICATTRIBUTE_PATTERN_KEY);nop的做法 感觉没有必要 都删
            _cacheManager.Remove(key);
            //event notification
            _eventPublisher.EntityUpdated(attribute);
        }
        public void InsertAttribute(GenericAttribute attribute)
        {
            if (attribute == null)
                throw new ArgumentNullException("attribute");
            _repository.Insert(attribute);
            string key = string.Format(GENERICATTRIBUTE_KEY, attribute.EntityId, attribute.KeyGroup);
            //cache
            //_cacheManager.RemoveByPattern(GENERICATTRIBUTE_PATTERN_KEY);nop的做法 感觉没有必要 都删
            _cacheManager.Remove(key);
            //event notification
            _eventPublisher.EntityInserted(attribute);
        }
    }
}
