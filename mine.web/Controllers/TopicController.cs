using mine.core;
using mine.core.Caching;
using mine.core.Domain.Topics;
using mine.services.Stores;
using mine.services.Topics;
using mine.services.Localization;
using mine.web.Infrastructure;
using mine.web.Models.Topics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mine.web.Controllers
{
    public class TopicController : Controller
    {
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly ICacheManager _cacheManager;
        private readonly ITopicService _topicService;
        private readonly IStoreMappingService _storeMappingService;
        public TopicController(ICacheManager cacheManager,IWorkContext workContext,IStoreContext storeContext,ITopicService topicService,IStoreMappingService storeMappingService) 
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._topicService = topicService;
            this._storeMappingService = storeMappingService;
        }
        //
        // GET: /Topic/
        [ChildActionOnly]
        public ActionResult TopicBlock(string systemName)
        {
            var cacheKey = string.Format(ModelCacheEventConsumer.TOPIC_MODEL_BY_SYSTEMNAME_KEY, systemName, _workContext.WorkingLanguage.Id, _storeContext.CurrentStore.Id);
            var cacheModel = _cacheManager.Get(cacheKey, () =>
            {
                //load by store
                var topic = _topicService.GetTopicBySystemName(systemName, _storeContext.CurrentStore.Id);
                if (topic == null)
                    return null;
                //Store mapping
                if (!_storeMappingService.Authorize(topic))
                    return null;
                return PrepareTopicModel(topic);
            });

            if (cacheModel == null)
                return Content("");

            return PartialView(cacheModel);
        }
        [NonAction]
        protected virtual TopicModel PrepareTopicModel(Topic topic)
        {
            if (topic == null)
                throw new ArgumentNullException("topic");

            var model = new TopicModel
            {
                Id = topic.Id,
                SystemName = topic.SystemName,
                IncludeInSitemap = topic.IncludeInSitemap,
                IsPasswordProtected = topic.IsPasswordProtected,
                Title = topic.IsPasswordProtected ? "" : topic.GetLocalized(x => x.Title),
                Body = topic.IsPasswordProtected ? "" : topic.GetLocalized(x => x.Body),
                MetaKeywords = topic.GetLocalized(x => x.MetaKeywords),
                MetaDescription = topic.GetLocalized(x => x.MetaDescription),
                MetaTitle = topic.GetLocalized(x => x.MetaTitle),
                SeName = topic.GetSeName(),
                TopicTemplateId = topic.TopicTemplateId
            };
            return model;
        }
	}
}