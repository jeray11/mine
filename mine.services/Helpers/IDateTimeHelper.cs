using mine.core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.services.Helpers
{
   public interface IDateTimeHelper
    {
        /// <summary>
        /// Retrieves a System.TimeZoneInfo object from the registry based on its identifier.
        /// </summary>
        /// <param name="id">The time zone identifier, which corresponds to the System.TimeZoneInfo.Id property.</param>
        /// <returns>A System.TimeZoneInfo object whose identifier is the value of the id parameter.</returns>
        TimeZoneInfo FindTimeZoneById(string id);
        /// <summary>
        /// Converts the date and time to current user date and time
        /// </summary>
        /// <param name="dt">The date and time (respesents local system time or UTC time) to convert.</param>
        /// <returns>A DateTime value that represents time that corresponds to the dateTime parameter in customer time zone.</returns>
        DateTime ConvertToUserTime(DateTime dt);
        /// <summary>
        /// Converts the date and time to current user date and time
        /// </summary>
        /// <param name="dt">The date and time (respesents local system time or UTC time) to convert.</param>
        /// <param name="sourceDateTimeKind">The source datetimekind</param>
        /// <returns>A DateTime value that represents time that corresponds to the dateTime parameter in customer time zone.</returns>
        DateTime ConvertToUserTime(DateTime dt, DateTimeKind sourceDateTimeKind);
        /// <summary>
        /// Gets or sets the current user time zone
        /// </summary>
        TimeZoneInfo CurrentTimeZone { get; set; }
        /// <summary>
        /// Gets a customer time zone
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <returns>Customer time zone; if customer is null, then default store time zone</returns>
        TimeZoneInfo GetCustomerTimeZone(Customer customer);
        /// <summary>
        /// Gets or sets a default store time zone
        /// </summary>
        TimeZoneInfo DefaultStoreTimeZone { get; set; }
    }
}
