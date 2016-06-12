using mine.core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.core.Infrastructure
{
    public class WebAppTypeFinder : AppDomainTypeFinder
    {
        private bool _ensureBinFolderAssembliesLoaded = true;
        public WebAppTypeFinder(MineConfig config)
        {
            this._ensureBinFolderAssembliesLoaded = config.DynamicDiscovery;

        }
    }
}
