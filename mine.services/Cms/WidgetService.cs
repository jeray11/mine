using mine.core.Domain.Cms;
using mine.core.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.services.Cms
{
    public class WidgetService:IWidgetService
    {
        private readonly IPluginFinder _pluginFinder;
        private readonly WidgetSettings _widgetSettings;
        public WidgetService(IPluginFinder pluginFinder, WidgetSettings widgetSettings) 
        {
            this._pluginFinder = pluginFinder;
            this._widgetSettings = widgetSettings;
        }
        public IList<IWidgetPlugin> LoadActiveWidgetsByWidgetZone(string widgetZone, int storeId = 0)
        {
            if (String.IsNullOrWhiteSpace(widgetZone))
                return new List<IWidgetPlugin>();
            return LoadActiveWidgets(storeId)
                  .Where(x => x.GetWidgetZones().Contains(widgetZone, StringComparer.InvariantCultureIgnoreCase))
                  .ToList();
        }

        public IList<IWidgetPlugin> LoadActiveWidgets(int storeId = 0)
        {
            return LoadAllWidgets(storeId).Where(p => _widgetSettings.ActiveWidgetSystemNames.Contains(p.PluginDescriptor.SystemName, StringComparer.InvariantCultureIgnoreCase))
                .ToList() ;
        }

        public IList<IWidgetPlugin> LoadAllWidgets(int storeId = 0)
        {
            return _pluginFinder.GetPlugins<IWidgetPlugin>(storeId: storeId).ToList();
        }
    }
}
