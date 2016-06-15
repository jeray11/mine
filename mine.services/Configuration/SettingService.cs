using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mine.core.Configuration;

namespace mine.services.Configuration
{
    public class SettingService : ISettingService
    {
        public T LoadSetting<T>(int storeId = 0) where T : ISettings, new()
        {
            var settings = Activator.CreateInstance<T>();
            return settings;
        }
    }
}
