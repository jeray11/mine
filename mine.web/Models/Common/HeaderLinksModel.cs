using mine.web.framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mine.web.Models.Common
{
    public class HeaderLinksModel:BaseMineViewModel
    {
        public bool IsAuthenticated { get; set; }
        public string CustomerEmailUsername { get; set; }

        public bool ShoppingCartEnabled { get; set; }
        public int ShoppingCartItems { get; set; }

        public bool WishlistEnabled { get; set; }
        public int WishlistItems { get; set; }

        public bool AllowPrivateMessages { get; set; }
        public string UnreadPrivateMessages { get; set; }
        public string AlertMessage { get; set; }
    }
}