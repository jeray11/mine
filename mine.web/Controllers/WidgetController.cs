using mine.core;
using mine.core.Caching;
using mine.services.Cms;
using mine.web.Infrastructure;
using mine.web.Models.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace mine.web.Controllers
{
    public class WidgetController : Controller
    {
        private readonly IStoreContext _storeContext;
        private readonly ICacheManager _cacheManager;
        private readonly IWidgetService _widgetService;
        public WidgetController(ICacheManager cacheManager,IStoreContext storeContext, IWidgetService widgetService)
        {
            this._cacheManager = cacheManager;
            this._storeContext = storeContext;
            this._widgetService = widgetService;
        }
        [ChildActionOnly]
        public ActionResult WidgetsByZone(string widgetZone, object additionalData = null)
        {
            var cacheKey = string.Format(ModelCacheEventConsumer.WIDGET_MODEL_KEY, _storeContext.CurrentStore.Id, widgetZone);
            var cacheModel = _cacheManager.Get(cacheKey, () =>
            {
                //model
                var model = new List<RenderWidgetModel>();

                var widgets = _widgetService.LoadActiveWidgetsByWidgetZone(widgetZone, _storeContext.CurrentStore.Id);
                foreach (var widget in widgets)
                {
                    var widgetModel = new RenderWidgetModel();

                    string actionName;
                    string controllerName;
                    RouteValueDictionary routeValues;
                    widget.GetDisplayWidgetRoute(widgetZone, out actionName, out controllerName, out routeValues);
                    widgetModel.ActionName = actionName;
                    widgetModel.ControllerName = controllerName;
                    widgetModel.RouteValues = routeValues;

                    model.Add(widgetModel);
                }
                return model;
            });

            //no data?
            if (cacheModel.Count == 0)
                return Content("");

            //"RouteValues" property of widget models depends on "additionalData".
            //We need to clone the cached model before modifications (the updated one should not be cached)
            var clonedModel = new List<RenderWidgetModel>();
            foreach (var widgetModel in cacheModel)
            {
                var clonedWidgetModel = new RenderWidgetModel();
                clonedWidgetModel.ActionName = widgetModel.ActionName;
                clonedWidgetModel.ControllerName = widgetModel.ControllerName;
                if (widgetModel.RouteValues != null)
                    clonedWidgetModel.RouteValues = new RouteValueDictionary(widgetModel.RouteValues);

                if (additionalData != null)
                {
                    if (clonedWidgetModel.RouteValues == null)
                        clonedWidgetModel.RouteValues = new RouteValueDictionary();
                    clonedWidgetModel.RouteValues.Add("additionalData", additionalData);
                }

                clonedModel.Add(clonedWidgetModel);
            }

            return PartialView(clonedModel);
        }
    }
}