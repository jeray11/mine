using mine.core;
using mine.web.framework;
using mine.core.Domain.Customers;
using mine.core.Domain.Forums;
using mine.core.Domain.Media;
using mine.services.Customers;
using mine.services.Dictionary;
using mine.services.Forums;
using mine.services.Helpers;
using mine.services.Localization;
using mine.services.Media;
using mine.services.Seo;
using mine.web.Models.Boards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace mine.web.Controllers
{
    public class BoardsController : Controller
    {
        #region 私有变量

        private readonly IForumService _forumService;
        private readonly ILocalizationService _localizationService;
        private readonly IPictureService _pictureService;
        private readonly ICountryService _countryService;
        private readonly IWebHelper _webHelper;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly ForumSettings _forumSettings;
        private readonly CustomerSettings _customerSettings;
        private readonly MediaSettings _mediaSettings;
        private readonly IDateTimeHelper _dateTimeHelper;

        #endregion

        public BoardsController(IForumService forumService,
            ILocalizationService localizationService,
            IPictureService pictureService,
            ICountryService countryService,
            IWebHelper webHelper,
            IWorkContext workContext,
            IStoreContext storeContext,
            ForumSettings forumSettings,
            CustomerSettings customerSettings,
            MediaSettings mediaSettings,
            IDateTimeHelper dateTimeHelper) 
        {
            this._forumService = forumService;
            this._localizationService = localizationService;
            this._pictureService = pictureService;
            this._countryService = countryService;
            this._webHelper = webHelper;
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._forumSettings = forumSettings;
            this._customerSettings = customerSettings;
            this._mediaSettings = mediaSettings;
            this._dateTimeHelper = dateTimeHelper;
        }


        [NonAction]
        protected virtual ForumGroupModel PrepareForumGroupModel(ForumGroup forumGroup)
        {
            var forumGroupModel = new ForumGroupModel
            {
                Id = forumGroup.Id,
                Name = forumGroup.Name,
                SeName = forumGroup.GetSeName(),
            };
            var forums = _forumService.GetAllForumsByGroupId(forumGroup.Id);
            foreach (var forum in forums)
            {
                var forumModel = PrepareForumRowModel(forum);
                forumGroupModel.Forums.Add(forumModel);
            }
            return forumGroupModel;
        }
        [NonAction]
        protected virtual ForumRowModel PrepareForumRowModel(Forum forum)
        {
            var forumModel = new ForumRowModel
            {
                Id = forum.Id,
                Name = forum.Name,
                SeName = forum.GetSeName(),
                Description = forum.Description,
                NumTopics = forum.NumTopics,
                NumPosts = forum.NumPosts,
                LastPostId = forum.LastPostId,
            };
            return forumModel;
        }
        //
        // GET: /Boards/
        public ActionResult Index()
        {
            if (!_forumSettings.ForumsEnabled)
            {
                return RedirectToRoute("HomePage");
            }

            var forumGroups = _forumService.GetAllForumGroups();

            var model = new BoardsIndexModel();
            foreach (var forumGroup in forumGroups)
            {
                var forumGroupModel = PrepareForumGroupModel(forumGroup);
                model.ForumGroups.Add(forumGroupModel);
            }
            return View(model);
        }
        [ChildActionOnly]
        public ActionResult ActiveDiscussionsSmall()
        {
            if (!_forumSettings.ForumsEnabled)
            {
                return RedirectToRoute("HomePage");
            }

            var topics = _forumService.GetActiveTopics(0, 0, _forumSettings.HomePageActiveDiscussionsTopicCount);
            if (topics.Count == 0)
                return Content("");

            var model = new ActiveDiscussionsModel();
            foreach (var topic in topics)
            {
                var topicModel = PrepareForumTopicRowModel(topic);
                model.ForumTopics.Add(topicModel);
            }
            model.ViewAllLinkEnabled = true;
            model.ActiveDiscussionsFeedEnabled = _forumSettings.ActiveDiscussionsFeedEnabled;
            model.PostsPageSize = _forumSettings.PostsPageSize;

            return PartialView(model);
        }

        [NonAction]
        protected virtual ForumTopicRowModel PrepareForumTopicRowModel(ForumTopic topic)
        {
            var topicModel = new ForumTopicRowModel
            {
                Id = topic.Id,
                Subject = topic.Subject,
                SeName = topic.GetSeName(),
                LastPostId = topic.LastPostId,
                NumPosts = topic.NumPosts,
                Views = topic.Views,
                NumReplies = topic.NumReplies,
                ForumTopicType = topic.ForumTopicType,
                CustomerId = topic.CustomerId,
                AllowViewingProfiles = _customerSettings.AllowViewingProfiles,
                CustomerName = topic.Customer.FormatUserName(),
                IsCustomerGuest = topic.Customer.IsGuest()
            };

            var forumPosts = _forumService.GetAllPosts(topic.Id, 0, string.Empty, 1, _forumSettings.PostsPageSize);
            topicModel.TotalPostPages = forumPosts.TotalPages;

            return topicModel;
        }
        [ChildActionOnly]
        public ActionResult LastPost(int forumPostId, bool showTopic)
        {
            var post = _forumService.GetPostById(forumPostId);
            var model = new LastPostModel();
            if (post != null)
            {
                model.Id = post.Id;
                model.ForumTopicId = post.TopicId;
                model.ForumTopicSeName = post.ForumTopic.GetSeName();
                model.ForumTopicSubject = post.ForumTopic.StripTopicSubject();
                model.CustomerId = post.CustomerId;
                model.AllowViewingProfiles = _customerSettings.AllowViewingProfiles;
                model.CustomerName = post.Customer.FormatUserName();
                model.IsCustomerGuest = post.Customer.IsGuest();
                //created on string
                if (_forumSettings.RelativeDateTimeFormattingEnabled)
                    model.PostCreatedOnStr = post.CreatedOnUtc.RelativeFormat(true, "f");
                else
                    model.PostCreatedOnStr = _dateTimeHelper.ConvertToUserTime(post.CreatedOnUtc, DateTimeKind.Utc).ToString("f");
            }
            model.ShowTopic = showTopic;
            return PartialView(model);
        }

        public ActionResult Forum(int id, int page = 1) 
        {
            if (!_forumSettings.ForumsEnabled)
            {
                return RedirectToRoute("HomePage");
            }
            var forum = _forumService.GetForumById(id);
            if (forum != null)
            {
                var model = new ForumPageModel();
                model.Id = forum.Id;
                model.Name = forum.Name;
                model.SeName = forum.GetSeName();
                model.Description = forum.Description;
                int pageSize = _forumSettings.TopicsPageSize > 0 ? _forumSettings.TopicsPageSize : 10;
                if (_forumService.IsCustomerAllowedToSubscribe(_workContext.CurrentCustomer))
                {
                    model.WatchForumText = _localizationService.GetResource("Forum.WatchForum");

                    var forumSubscription = _forumService.GetAllSubscriptions(_workContext.CurrentCustomer.Id, forum.Id, 0, 0, 1).FirstOrDefault();
                    if (forumSubscription != null)
                    {
                        model.WatchForumText = _localizationService.GetResource("Forum.UnwatchForum");
                    }
                }
            }
            return RedirectToRoute("Boards");
        }

        //
        // GET: /Boards/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Boards/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Boards/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Boards/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Boards/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Boards/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Boards/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
