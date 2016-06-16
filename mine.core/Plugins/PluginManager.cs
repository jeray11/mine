using mine.core.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

[assembly: PreApplicationStartMethod(typeof(PluginManager), "Initialize")]
namespace mine.core.Plugins
{
    public class PluginManager
    {
        private static readonly ReaderWriterLockSlim Locker = new ReaderWriterLockSlim();
        /// <summary>
        /// Returns a collection of all referenced plugin assemblies that have been shadow copied
        /// </summary>
        public static IEnumerable<PluginDescriptor> ReferencedPlugins { get; set; }

        public static void Initialize() 
        {
            
        }
    }
}
