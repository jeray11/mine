using mine.core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.core.Domain.Localization
{
    public class LocalizationSettings:ISettings
    {
        /// <summary>
        /// A value indicating whether to load all LocaleStringResource records on application startup
        /// </summary>
        public bool LoadAllLocaleRecordsOnStartup { get; set; }
    }
}
