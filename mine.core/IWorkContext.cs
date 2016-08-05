using mine.core.Domain.Customers;
using mine.core.Domain.Directory;
using mine.core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.core
{
    public interface IWorkContext
    {
        /// <summary>
        /// Get or set current user working language
        /// </summary>
        Language WorkingLanguage { get; }
        /// <summary>
        /// Gets or sets the current customer
        /// </summary>
        Customer CurrentCustomer { get; set; }
        /// <summary>
        /// 当前货币
        /// </summary>
        Currency WorkingCurrency { get; set; }
        /// <summary>
        /// Get or set value indicating whether we're in admin area
        /// </summary>
        bool IsAdmin { get; set; }
    }
}
