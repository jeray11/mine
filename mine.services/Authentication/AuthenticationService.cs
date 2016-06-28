using mine.core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Web;
using mine.services.Customers;

namespace mine.services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private Customer _cachedCustomer;
        private HttpContextBase _httpContext;
        private CustomerSettings _customerSettings;
        private ICustomerService _customerService;
        public AuthenticationService(HttpContextBase httpContext, CustomerSettings customerSettings, ICustomerService customerService)
        {
            this._httpContext = httpContext;
            this._customerSettings = customerSettings;
            this._customerService = customerService;
        }
        public void SignIn(Customer customer, bool createPersistentCookie)
        {
            var now = DateTime.UtcNow.ToLocalTime();
            AuthenticationProperties properties = new AuthenticationProperties { IsPersistent = createPersistentCookie };
            Claim claim = null;
            if (_customerSettings.UsernamesEnabled)
                claim = new Claim("UserName", customer.Username);
            else
                claim = new Claim("Email", customer.Email);
            var claimsIdentity = new ClaimsIdentity(new Claim[] { claim });
            AuthenticationManager.SignIn(properties, claimsIdentity);
        }

        public void SignOut()
        {
            _cachedCustomer = null;
            AuthenticationManager.SignOut();
        }

        public Customer GetAuthenticatedCustomer()
        {
            if (_cachedCustomer != null)
                return _cachedCustomer;
            if (_httpContext == null ||
                _httpContext.Request == null ||
                !_httpContext.Request.IsAuthenticated ||
                !(_httpContext.User.Identity is ClaimsIdentity))
            {
                return null;
            }
            var claimsIdentity = (ClaimsIdentity)_httpContext.User.Identity;
            var claim = claimsIdentity.Claims.ToArray()[0];
            var customer = GetAuthenticatedCustomerFromClaim(claim);
            if (customer != null && customer.Active && !customer.Deleted && customer.IsRegistered())
                _cachedCustomer = customer;
            return _cachedCustomer;
        }
        public virtual Customer GetAuthenticatedCustomerFromClaim(Claim claim)
        {
            if (claim == null)
                throw new ArgumentNullException("claim");
            var usernameOrEmail = claim.Value;
            if (String.IsNullOrWhiteSpace(usernameOrEmail))
                return null;
            var customer = _customerSettings.UsernamesEnabled
                ? _customerService.GetCustomerByUsername(usernameOrEmail)
                : _customerService.GetCustomerByEmail(usernameOrEmail);
            return customer;
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return _httpContext.GetOwinContext().Authentication;
            }
        }
    }
}
