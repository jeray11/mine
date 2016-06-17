using System;
using System.Collections.Generic;
using System.IO;
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

        public string Group { get; set; }

        public string FriendlyName { get; set; }

        public string SystemName { get; set; }

        public string Version { get; set; }

        public List<string> SupportedVersions { get; set; }

        public string Author { get; set; }

        public int DisplayOrder { get; set; }

        public string PluginFileName { get; set; }

        public List<int> LimitedToStores { get; set; }

        public FileInfo OriginalAssemblyFile{get;set;}

        public Type PluginType { get; set; }


    }
}
