﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mine.web.Controllers
{
    public class CatalogController : Controller
    {
        //
        // GET: /Catalog/
        public ActionResult SearchBox()
        {
            return PartialView();
        }
	}
}