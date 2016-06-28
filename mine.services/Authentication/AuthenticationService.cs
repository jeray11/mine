using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.services.Authentication
{
   public class AuthenticationService:IAuthenticationService
    {
        public void SignIn(core.Domain.Customers.Customer customer, bool createPersistentCookie)
        {
            throw new NotImplementedException();
        }

        public void SignOut()
        {
            throw new NotImplementedException();
        }

        public core.Domain.Customers.Customer GetAuthenticatedCustomer()
        {
            throw new NotImplementedException();
        }
    }
}
