﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mine.web.Controllers
{
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/
        public ActionResult Info()
        {
            return View();
        }

        public ActionResult Logout() 
        {
            return View();
        }

        public ActionResult Login() 
        {
            return View();
        }

        public ActionResult Register() 
        {
            return View();
        }
	}
}