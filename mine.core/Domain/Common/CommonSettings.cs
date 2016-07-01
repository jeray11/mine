using mine.core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.core.Domain.Common
{
    public class CommonSettings:ISettings
    {
        public CommonSettings()
        {
            IgnoreLogWordlist = new List<string>();
        }
        /// <summary>
        /// Gets or sets a ignore words (phrases) to be ignored when logging errors/messages
        /// </summary>
        public List<string> IgnoreLogWordlist { get; set; }

        public bool RenderXuaCompatible {get;set; }

        public string XuaCompatibleValue { get; set; }
    }
}
