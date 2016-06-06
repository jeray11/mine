using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mine.web.Models.Boards
{
    public class BoardsIndexModel
    {
        public BoardsIndexModel()
        {
            this.ForumGroups = new List<ForumGroupModel>();
        }

        public IList<ForumGroupModel> ForumGroups { get; set; }
    }
}