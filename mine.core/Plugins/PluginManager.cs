using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.core.Plugins
{
    public class PluginManager
    {
        /// <summary>
     /// Returns a collection of all referenced plugin assemblies that have been shadow copied
     /// </summary>
        public static IEnumerable<PluginDescriptor> ReferencedPlugins { get; set; }
    }
}
