using mine.core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.core.Domain.Forums
{
    public class ForumSettings:ISettings
    {
        /// <summary>
        /// 论坛是否可用
        /// </summary>
        public bool ForumsEnabled { get; set; }
    }
}
