using mine.core;
using mine.core.Domain.Customers;
using mine.core.Domain.Forums;
using mine.core.Domain.Media;
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
            var id = Thread.CurrentThread.ManagedThreadId;
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
