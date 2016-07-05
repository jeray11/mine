using mine.core.Domain.Customers;
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
    }
}
