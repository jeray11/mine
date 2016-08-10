using mine.core.Domain.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.services.Security
{
   public class StandardPermissionProvider
    {
        //public store permissions
        public static readonly PermissionRecord EnableShoppingCart = new PermissionRecord { Name = "Public store. Enable shopping cart", SystemName = "EnableShoppingCart", Category = "PublicStore" };
        public static readonly PermissionRecord EnableWishlist = new PermissionRecord { Name = "Public store. Enable wishlist", SystemName = "EnableWishlist", Category = "PublicStore" };

    }
}
