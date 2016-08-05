using mine.core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.core.Domain.Directory
{
    public class CurrencySettings : ISettings
    {
        public int PrimaryStoreCurrencyId { get; set; }
    }
}
