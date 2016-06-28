using mine.core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.core.Domain.Customers
{
    public class CustomerSettings: ISettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether usernames are used instead of emails
        /// </summary>
        public bool UsernamesEnabled { get; set; }
    }
}
