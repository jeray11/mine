using mine.core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.services.Configuration
{
    public interface ISettingService
    {
        T LoadSetting<T>(int storeId = 0) where T : ISettings, new();

        void SaveSetting<T>(T settings,int storeId=0) where T : ISettings, new();
    }
}
