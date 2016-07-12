using mine.core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.core.Domain.Seo
{
    public class SeoSettings : ISettings
    {
        public bool ConvertNonWesternChars { get; set; }
        public bool AllowUnicodeCharsInUrls { get; set; }
    }
}
