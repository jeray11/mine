using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mine.web.Models.Boards
{
    public class LastPostModel
    {
        public int Id { get; set; }
        public int ForumTopicId { get; set; }
        public string ForumTopicSeName { get; set; }
        public string ForumTopicSubject { get; set; }

        public int CustomerId { get; set; }
        public bool AllowViewingProfiles { get; set; }
        public string CustomerName { get; set; }
        public bool IsCustomerGuest { get; set; }

        public string PostCreatedOnStr { get; set; }

        public bool ShowTopic { get; set; }
    }
}