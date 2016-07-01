using mine.core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.core.Domain.Cms
{
   public class WidgetSettings:ISettings
    {
       public WidgetSettings()
       {
           ActiveWidgetSystemNames = new List<string>();
       }
       public List<string> ActiveWidgetSystemNames { get; set; }
    }
}
