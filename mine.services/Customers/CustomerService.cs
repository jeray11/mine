using mine.core.Caching;
using mine.core.Data;
using mine.core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.services.Customers
{
   public class CustomerService:ICustomerService
    {
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : system name
        /// </remarks>
        private const string CUSTOMERROLES_BY_SYSTEMNAME_KEY = "Nop.customerrole.systemname-{0}";

       private readonly ICacheManager _cacheManager;
       private readonly IRepository<Customer> _repository;
       public CustomerService(ICacheManager cacheManager,IRepository<Customer> repository) 
       {
           this._cacheManager = cacheManager;
           this._repository = repository;
       }
        /// <summary>
        /// Get customer by system role
        /// </summary>
        /// <param name="systemName">System name</param>
        /// <returns>Customer</returns>
       public Customer GetCustomerBySystemName(string systemName) 
       {
           string key = string.Format("CUSTOMERROLES_BY_SYSTEMNAME_KEY", systemName);
           return _cacheManager.Get(key, () => {
               var query = from c in _repository.Table
                           orderby c.Id
                           where c.SystemName == systemName
                           select c;
               return query.FirstOrDefault();
           });
       }
    }
}
