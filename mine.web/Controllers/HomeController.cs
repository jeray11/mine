using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mine.web.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            TempData["onlyonce"] = "123";
            return View();
        }


        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";
            ViewBag.Message = TempData["onlyonce"];
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}