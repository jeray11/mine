using mine.core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using System.Security.Claims;

namespace mine.services.Authentication
{
   public class AuthenticationService:IAuthenticationService
    {
       private Customer _cachedCustomer;
        public void SignIn(Customer customer, bool createPersistentCookie)
        {
            var now = DateTime.UtcNow.ToLocalTime();
            ClaimsIdentity claim=new ClaimsIdentity();
            AuthenticationProperties proper=new AuthenticationProperties();
            AuthenticationTicket ticket = new AuthenticationTicket(claim, proper);
        }

        public void SignOut()
        {
            _cachedCustomer = null;
            
        }

        public Customer GetAuthenticatedCustomer()
        {
            throw new NotImplementedException();
        }
    }
}
