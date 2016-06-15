using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace mine.core.Plugins
{
    public class PluginDescriptor
    {
        /// <summary>
        /// The assembly that has been shadow copied that is active in the application
        /// </summary>
        public virtual Assembly ReferencedAssembly { get; internal set; }
        /// <summary>
        /// Gets or sets the value indicating whether plugin is installed
        /// </summary>
        public virtual bool Installed { get; set; }

    }
}
