using mine.core.Domain.Forums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mine.web.Models.Boards
{
    public class ForumTopicRowModel
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string SeName { get; set; }
        public int LastPostId { get; set; }

        public int NumPosts { get; set; }
        public int Views { get; set; }
        public int NumReplies { get; set; }
        public ForumTopicType ForumTopicType { get; set; }

        public int CustomerId { get; set; }
        public bool AllowViewingProfiles { get; set; }
        public string CustomerName { get; set; }
        public bool IsCustomerGuest { get; set; }

        //posts
        public int TotalPostPages { get; set; }
    }
}