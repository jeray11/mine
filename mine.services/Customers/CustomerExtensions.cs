using mine.core;
using mine.core.Domain.Customers;
using mine.core.Infrastructure;
using mine.services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.services.Customers
{
    public static class CustomerExtensions
    {
        /// <summary>
        /// Formats the customer name
        /// </summary>
        /// <param name="customer">Source</param>
        /// <param name="stripTooLong">Strip too long customer name</param>
        /// <param name="maxLength">Maximum customer name length</param>
        /// <returns>Formatted text</returns>
        public static string FormatUserName(this Customer customer, bool stripTooLong = false, int maxLength = 0)
        {
            if (customer == null)
                return string.Empty;

            if (customer.IsGuest())
            {
                return EngineContext.Current.Resolve<ILocalizationService>().GetResource("Customer.Guest");
            }

            string result = string.Empty;
            switch (EngineContext.Current.Resolve<CustomerSettings>().CustomerNameFormat)
            {
                case CustomerNameFormat.ShowEmails:
                    result = customer.Email;
                    break;
                case CustomerNameFormat.ShowUsernames:
                    result = customer.Username;
                    break;
                case CustomerNameFormat.ShowFullNames:
                    result = customer.GetFullName();
                    break;
                case CustomerNameFormat.ShowFirstName:
                    result = customer.GetAttribute<string>(SystemCustomerAttributeNames.FirstName);
                    break;
                default:
                    break;
            }

            if (stripTooLong && maxLength > 0)
            {
                result = CommonHelper.EnsureMaximumLength(result, maxLength);
            }

            return result;
        }
         /// <summary>
        /// Get full name
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <returns>Customer full name</returns>
        public static string GetFullName(this Customer customer) 
        {
            if (customer == null)
                throw new ArgumentNullException("customer");
            string firstName = customer.GetAttribute<string>(SystemCustomerAttributeNames.FirstName);
            string lastName = customer.GetAttribute<string>(SystemCustomerAttributeNames.LastName);
            string fullName = "";
            if (!String.IsNullOrWhiteSpace(firstName) && !String.IsNullOrWhiteSpace(lastName))
                fullName = string.Format("{0} {1}", firstName, lastName);
            else
            {
                if (!String.IsNullOrWhiteSpace(firstName))
                    fullName = firstName;

                if (!String.IsNullOrWhiteSpace(lastName))
                    fullName = lastName;
            }
            return fullName;
        }
    }
}
