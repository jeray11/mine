using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.core.Plugins
{
    public enum LoadPluginsMode
    {
        /// <summary>
        /// All (Installed & Not installed)
        /// </summary>
        All = 0,
        /// <summary>
        /// Installed only
        /// </summary>
        InstalledOnly = 10,
        /// <summary>
        /// Not installed only
        /// </summary>
        NotInstalledOnly = 20
    }
}
