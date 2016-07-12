using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.core.Domain.Customers
{
    public static class SystemCustomerAttributeNames
    {
        public static string ImpersonatedCustomerId { get { return "ImpersonatedCustomerId"; } }
        public static string LanguageAutomaticallyDetected { get { return "LanguageAutomaticallyDetected"; } }
        public static string LanguageId { get { return "LanguageId"; } }
        public static string TimeZoneId { get { return "TimeZoneId"; } }
        public static string FirstName { get { return "FirstName"; } }
        public static string LastName { get { return "LastName"; } }
    }
}
