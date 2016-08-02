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
        /// <summary>
        /// Gets or sets the number of items to display for Active Discussions on forums home page
        /// </summary>
        public int HomePageActiveDiscussionsTopicCount { get; set; }
        /// <summary>
        /// 每页回复
        /// </summary>
        public int PostsPageSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool ActiveDiscussionsFeedEnabled { get; set; }
        /// <summary>
        /// Gets or sets the maximum length for stripped forum topic names
        /// </summary>
        public int StrippedTopicMaxLength { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool RelativeDateTimeFormattingEnabled { get; set; }
    }
}
