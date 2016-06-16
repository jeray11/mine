﻿using mine.core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace mine.web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //initialize engine context
            EngineContext.Initialize(false);
            var id=Thread.CurrentThread.ManagedThreadId;
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            var id = Thread.CurrentThread.ManagedThreadId;
        }

        protected void Application_Error(object sender, EventArgs e)
        {

            var id = Thread.CurrentThread.ManagedThreadId;
            Exception ex = Server.GetLastError();
        }
    }
}
