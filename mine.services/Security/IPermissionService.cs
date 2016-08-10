using mine.core.Domain.Customers;
using mine.core.Domain.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.services.Security
{
    public interface IPermissionService
    {
        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permission">Permission record</param>
        /// <returns>true - authorized; otherwise, false</returns>
        bool Authorize(PermissionRecord permission);

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permission">Permission record</param>
        /// <param name="customer">customer</param>
        /// <returns>true - authorized; otherwise, false</returns>
        bool Authorize(PermissionRecord permission,Customer customer);

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permissionRecordSystemName">Permission record system name</param>
        /// <param name="customer">Customer</param>
        /// <returns>true - authorized; otherwise, false</returns>
        public virtual bool Authorize(string permissionRecordSystemName, Customer customer);
        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permissionRecordSystemName">Permission record system name</param>
        /// <param name="customerRole">Customer role</param>
        /// <returns>true - authorized; otherwise, false</returns>
        protected virtual bool Authorize(string permissionRecordSystemName, CustomerRole customerRole);
    }
}
