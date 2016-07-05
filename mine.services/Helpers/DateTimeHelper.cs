using mine.core;
using mine.core.Domain.Customers;
using mine.services.Common;
using mine.services.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.services.Helpers
{
    public class DateTimeHelper:IDateTimeHelper
    {
        private readonly DateTimeSettings _dateTimeSettings;
        private readonly GenericAttributeService _genericAttributeService;
        private readonly IWorkContext _workContext;
        private readonly ISettingService _settingService;
        public DateTimeHelper(DateTimeSettings dateTimeSettings,GenericAttributeService genericAttributeService,IWorkContext workContext,ISettingService settingService) 
        {
            this._dateTimeSettings = dateTimeSettings;
            this._genericAttributeService = genericAttributeService;
            this._workContext = workContext;
            this._settingService = settingService;
        }
        public DateTime ConvertToUserTime(DateTime dt)
        {
            return ConvertToUserTime(dt, dt.Kind);
        }
        /// <summary>
        /// Converts the date and time to current user date and time
        /// </summary>
        /// <param name="dt">The date and time (respesents local system time or UTC time) to convert.</param>
        /// <param name="sourceDateTimeKind">The source datetimekind</param>
        /// <returns>A DateTime value that represents time that corresponds to the dateTime parameter in customer time zone.</returns>
        public DateTime ConvertToUserTime(DateTime dt, DateTimeKind sourceDateTimeKind)
        {
            dt = DateTime.SpecifyKind(dt, sourceDateTimeKind);
            var currentUserTimeZoneInfo = this.CurrentTimeZone;
            return TimeZoneInfo.ConvertTime(dt, currentUserTimeZoneInfo);
        }
        /// <summary>
        /// Gets or sets the current user time zone
        /// </summary>
        public virtual TimeZoneInfo CurrentTimeZone
        {
            get
            {
                return GetCustomerTimeZone(_workContext.CurrentCustomer);
            }
            set
            {
                if (!_dateTimeSettings.AllowCustomersToSetTimeZone)
                    return;

                string timeZoneId = string.Empty;
                if (value != null)
                {
                    timeZoneId = value.Id;
                }

                _genericAttributeService.SaveAttribute(_workContext.CurrentCustomer,
                    SystemCustomerAttributeNames.TimeZoneId, timeZoneId);
            }
        }
        /// <summary>
        /// Gets a customer time zone
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <returns>Customer time zone; if customer is null, then default store time zone</returns>
        public virtual TimeZoneInfo GetCustomerTimeZone(Customer customer)
        {
            //registered user
            TimeZoneInfo timeZoneInfo = null;
            if (_dateTimeSettings.AllowCustomersToSetTimeZone)
            {
                string timeZoneId = string.Empty;
                if (customer != null)
                    timeZoneId = customer.GetAttribute<string>(SystemCustomerAttributeNames.TimeZoneId, _genericAttributeService);

                try
                {
                    if (!String.IsNullOrEmpty(timeZoneId))
                        timeZoneInfo = FindTimeZoneById(timeZoneId);
                }
                catch (Exception exc)
                {
                    Debug.Write(exc.ToString());
                }
            }

            //default timezone
            if (timeZoneInfo == null)
                timeZoneInfo = this.DefaultStoreTimeZone;

            return timeZoneInfo;
        }
        /// <summary>
        /// Retrieves a System.TimeZoneInfo object from the registry based on its identifier.
        /// </summary>
        /// <param name="id">The time zone identifier, which corresponds to the System.TimeZoneInfo.Id property.</param>
        /// <returns>A System.TimeZoneInfo object whose identifier is the value of the id parameter.</returns>
        public virtual TimeZoneInfo FindTimeZoneById(string id)
        {
            return TimeZoneInfo.FindSystemTimeZoneById(id);
        }
        /// <summary>
        /// Gets or sets a default store time zone
        /// </summary>
        public virtual TimeZoneInfo DefaultStoreTimeZone
        {
            get
            {
                TimeZoneInfo timeZoneInfo = null;
                try
                {
                    if (!String.IsNullOrEmpty(_dateTimeSettings.DefaultStoreTimeZoneId))
                        timeZoneInfo = FindTimeZoneById(_dateTimeSettings.DefaultStoreTimeZoneId);
                }
                catch (Exception exc)
                {
                    Debug.Write(exc.ToString());
                }

                if (timeZoneInfo == null)
                    timeZoneInfo = TimeZoneInfo.Local;

                return timeZoneInfo;
            }
            set
            {
                string defaultTimeZoneId = string.Empty;
                if (value != null)
                {
                    defaultTimeZoneId = value.Id;
                }

                _dateTimeSettings.DefaultStoreTimeZoneId = defaultTimeZoneId;
                _settingService.SaveSetting(_dateTimeSettings);
            }
        }
    }
}
