using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mine.web.Models.Boards
{
    public class ForumGroupModel
    {
        public ForumGroupModel()
        {
            this.Forums = new List<ForumRowModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string SeName { get; set; }

        public IList<ForumRowModel> Forums { get; set; }
    }
}