﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.core.Plugins
{
   public class PluginFinder:IPluginFinder
    {
       private IList<PluginDescriptor> _plugins;
       private bool _arePluginsLoaded;
       public IEnumerable<T> GetPlugins<T>(LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly, int storeId = 0, string group = null) where T : class, IPlugin
        {
            return GetPluginDescriptors<T>(loadMode, storeId, group).Select(p=>p.Instance<T>());
        }


       public IEnumerable<PluginDescriptor> GetPluginDescriptors(LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly, int storeId = 0, string group = null)
       {
           //ensure plugins are loaded
           EnsurePluginsAreLoaded();
           return _plugins.Where(p => CheckLoadMode(p, loadMode) && AuthenticateStore(p, storeId) && CheckGroup(p, group));
       }
       /// <summary>
       /// Get plugin descriptors
       /// </summary>
       /// <typeparam name="T">The type of plugin to get.</typeparam>
       /// <param name="loadMode">Load plugins mode</param>
       /// <param name="storeId">Load records allowed only in a specified store; pass 0 to load all records</param>
       /// <param name="group">Filter by plugin group; pass null to load all records</param>
       /// <returns>Plugin descriptors</returns>
       public virtual IEnumerable<PluginDescriptor> GetPluginDescriptors<T>(LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly,
           int storeId = 0, string group = null)
           where T : class, IPlugin
       {
           return GetPluginDescriptors(loadMode, storeId, group)
               .Where(p => typeof(T).IsAssignableFrom(p.PluginType));
       }
       /// <summary>
       /// Ensure plugins are loaded
       /// </summary>
       protected virtual void EnsurePluginsAreLoaded()
       {
           if (!_arePluginsLoaded)
           {
               var foundPlugins = PluginManager.ReferencedPlugins.ToList();
               foundPlugins.Sort();
               _plugins = foundPlugins.ToList();

               _arePluginsLoaded = true;
           }
       }
       /// <summary>
       /// Check whether the plugin is available in a certain store
       /// </summary>
       /// <param name="pluginDescriptor">Plugin descriptor to check</param>
       /// <param name="loadMode">Load plugins mode</param>
       /// <returns>true - available; false - no</returns>
       protected virtual bool CheckLoadMode(PluginDescriptor pluginDescriptor, LoadPluginsMode loadMode)
       {
           if (pluginDescriptor == null)
               throw new ArgumentNullException("pluginDescriptor");

           switch (loadMode)
           {
               case LoadPluginsMode.All:
                   //no filering
                   return true;
               case LoadPluginsMode.InstalledOnly:
                   return pluginDescriptor.Installed;
               case LoadPluginsMode.NotInstalledOnly:
                   return !pluginDescriptor.Installed;
               default:
                   throw new Exception("Not supported LoadPluginsMode");
           }
       }
       /// <summary>
       /// Check whether the plugin is available in a certain store
       /// </summary>
       /// <param name="pluginDescriptor">Plugin descriptor to check</param>
       /// <param name="storeId">Store identifier to check</param>
       /// <returns>true - available; false - no</returns>
       public virtual bool AuthenticateStore(PluginDescriptor pluginDescriptor, int storeId)
       {
           if (pluginDescriptor == null)
               throw new ArgumentNullException("pluginDescriptor");

           //no validation required
           if (storeId == 0)
               return true;

           if (pluginDescriptor.LimitedToStores.Count == 0)
               return true;

           return pluginDescriptor.LimitedToStores.Contains(storeId);
       }
       /// <summary>
       /// Check whether the plugin is in a certain group
       /// </summary>
       /// <param name="pluginDescriptor">Plugin descriptor to check</param>
       /// <param name="group">Group</param>
       /// <returns>true - available; false - no</returns>
       protected virtual bool CheckGroup(PluginDescriptor pluginDescriptor, string group)
       {
           if (pluginDescriptor == null)
               throw new ArgumentNullException("pluginDescriptor");

           if (String.IsNullOrEmpty(group))
               return true;

           return group.Equals(pluginDescriptor.Group, StringComparison.InvariantCultureIgnoreCase);
       }
    }
}
