using mine.core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.services.Customers
{
   public interface ICustomerService
    {
        /// <summary>
        /// Get customer by system role
        /// </summary>
        /// <param name="systemName">System name</param>
        /// <returns>Customer</returns>
       Customer GetCustomerBySystemName(string systemName);

        Customer GetCustomerByUsername(string userName);
        Customer GetCustomerByEmail(string email);

        Customer GetCustomerById(int customerId);
        Customer GetCustomerByGuid(Guid customerGuid);
        Customer InsertGuestCustomer();
        /// <summary>
        /// Gets a customer role
        /// </summary>
        /// <param name="systemName">Customer role system name</param>
        /// <returns>Customer role</returns>
        CustomerRole GetCustomerRoleBySystemName(string systemName);
    }
}
