using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mine.web.Controllers
{
    public class ShoppingCartController : Controller
    {
        //
        // GET: /ShoppingCart/
        public ActionResult Wishlist(string customerGuid)
        {
            return View();
        }

        public ActionResult Cart() 
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult FlyoutShoppingCart()
        {
            return PartialView();
        }
	}
}