using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mine.web.Models.Boards
{
    public class ForumRowModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SeName { get; set; }
        public string Description { get; set; }
        public int NumTopics { get; set; }
        public int NumPosts { get; set; }
        public int LastPostId { get; set; }
    }
}