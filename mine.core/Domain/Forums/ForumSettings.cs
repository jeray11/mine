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
        /// <summary>
        /// 论坛中 允许私信
        /// </summary>
        public bool AllowPrivateMessages { get; set; }
        /// <summary>
        /// 是否弹出提示私信
        /// </summary>
        public bool ShowAlertForPM { get; set; }
        /// <summary>
        /// 专题详细页显示多少个主题
        /// </summary>
        public int TopicsPageSize { get; set; }
    }
}
