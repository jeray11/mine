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
    public class CustomerService : ICustomerService
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
        private readonly IRepository<CustomerRole> _repositoryCustomerRole;
        public CustomerService(ICacheManager cacheManager, IRepository<Customer> repository, IRepository<CustomerRole> repositoryCustomerRole)
        {
            this._cacheManager = cacheManager;
            this._repository = repository;
            this._repositoryCustomerRole = repositoryCustomerRole;
        }

        public Customer GetCustomerByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;
            var query = from c in _repository.Table
                        where c.Email == email
                        select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Get customer by system role
        /// </summary>
        /// <param name="systemName">System name</param>
        /// <returns>Customer</returns>
        public Customer GetCustomerBySystemName(string systemName)
        {
            if (string.IsNullOrEmpty(systemName))
                return null;
            var query = from c in _repository.Table
                        orderby c.Id
                        where c.SystemName == systemName
                        select c;
            return query.FirstOrDefault();
        }

        public Customer GetCustomerByUsername(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return null;
            var query = from c in _repository.Table
                        where c.Username == userName
                        select c;
            return query.FirstOrDefault();
        }

        public Customer GetCustomerById(int customerId)
        {
            if (customerId == 0)
                return null;
            return _repository.GetById(customerId);
        }

        public Customer GetCustomerByGuid(Guid customerGuid)
        {
            if (customerGuid == Guid.Empty)
                return null;
            var query = from c in _repository.Table
                        where c.CustomerGuid == customerGuid
                        orderby c.Id
                        select c;
            return query.FirstOrDefault();
        }
        public Customer InsertGuestCustomer()
        {
            var customer = new Customer
            {
                CustomerGuid = Guid.NewGuid(),
                Active = true,
                CreatedOnUtc = DateTime.UtcNow,
                LastActivityDateUtc = DateTime.UtcNow,
            };
            var guestRole = GetCustomerRoleBySystemName(SystemCustomerRoleNames.Guests);
            if (guestRole == null)
                throw new Exception("'Guests' role could not be loaded");
            customer.CustomerRoles.Add(guestRole);

            _repository.Insert(customer);

            return customer;
        }
        /// <summary>
        /// Gets a customer role
        /// </summary>
        /// <param name="systemName">Customer role system name</param>
        /// <returns>Customer role</returns>
        public CustomerRole GetCustomerRoleBySystemName(string systemName)
        {
            if (string.IsNullOrEmpty(systemName))
                return null;
            string key = string.Format(CUSTOMERROLES_BY_SYSTEMNAME_KEY, systemName);
            return _cacheManager.Get(key, () =>
            {
                var query = from cr in _repositoryCustomerRole.Table
                            orderby cr.Id
                            where cr.SystemName == systemName
                            select cr;
                var customerRole = query.FirstOrDefault();
                return customerRole;
            });
        }
    }
}
