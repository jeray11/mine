using mine.core.Caching;
using mine.core.Data;
using mine.core.Domain.Topics;
using mine.services.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.services.Topics
{
    public class TopicService : ITopicService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<Topic> _repository;
        private readonly IStoreMappingService _storeMappingService;
        public TopicService(ICacheManager cacheManager, IRepository<Topic> repository, IStoreMappingService storeMappingService) 
        {
            this._cacheManager = cacheManager;
            this._repository = repository;
            this._storeMappingService = storeMappingService;
        }
        public Topic GetTopicBySystemName(string systemName, int storeId = 0)
        {
            if (String.IsNullOrEmpty(systemName))
                return null;
            var query = _repository.Table;
            query = query.Where(t => t.SystemName == systemName);
            query = query.OrderBy(t => t.Id);
            var topics = query.ToList();
            if (storeId > 0)
            {
                topics = topics.Where(x => _storeMappingService.Authorize(x, storeId)).ToList();
            }
            return topics.FirstOrDefault();
        }
    }
}
