using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.services.Forums
{
    public class ForumService:IForumService
    {
        public IList<core.Domain.Forums.ForumGroup> GetAllForumGroups()
        {
            throw new NotImplementedException();
        }

        public IList<core.Domain.Forums.Forum> GetAllForumsByGroupId(int forumgroupid)
        {
            throw new NotImplementedException();
        }
    }
}
