using mine.core;
using mine.core.Caching;
using mine.core.Domain.Customers;
using mine.core.Domain.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.services.Security
{
    public class PermissionService:IPermissionService
    {
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : customer role ID
        /// {1} : permission system name
        /// </remarks>
        private const string PERMISSIONS_ALLOWED_KEY = "Nop.permission.allowed-{0}-{1}";
        private readonly IWorkContext _workContext;
        private readonly ICacheManager _cacheManager;
        public PermissionService(IWorkContext workContext,ICacheManager cacheManager) 
        {
            this._workContext = workContext;
            this._cacheManager = cacheManager;
        }
        public bool Authorize(PermissionRecord permission)
        {
            return Authorize(permission, _workContext.CurrentCustomer);
        }


        public bool Authorize(PermissionRecord permission, Customer customer)
        {
            if (permission == null)
                return false;

            if (customer == null)
                return false;
            return Authorize(permission.SystemName, customer);
        }

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permissionRecordSystemName">Permission record system name</param>
        /// <param name="customer">Customer</param>
        /// <returns>true - authorized; otherwise, false</returns>
        public virtual bool Authorize(string permissionRecordSystemName, Customer customer)
        {
            if (String.IsNullOrEmpty(permissionRecordSystemName))
                return false;

            var customerRoles = customer.CustomerRoles.Where(cr => cr.Active);
            foreach (var role in customerRoles)
                if (Authorize(permissionRecordSystemName, role))
                    //yes, we have such permission
                    return true;

            //no permission found
            return false;
        }
        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permissionRecordSystemName">Permission record system name</param>
        /// <param name="customerRole">Customer role</param>
        /// <returns>true - authorized; otherwise, false</returns>
        protected virtual bool Authorize(string permissionRecordSystemName, CustomerRole customerRole)
        {
            if (String.IsNullOrEmpty(permissionRecordSystemName))
                return false;

            string key = string.Format(PERMISSIONS_ALLOWED_KEY, customerRole.Id, permissionRecordSystemName);
            return _cacheManager.Get(key, () =>
            {
                foreach (var permission1 in customerRole.PermissionRecords)
                    if (permission1.SystemName.Equals(permissionRecordSystemName, StringComparison.InvariantCultureIgnoreCase))
                        return true;

                return false;
            });
        }
    }
}
