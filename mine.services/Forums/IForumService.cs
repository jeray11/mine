using mine.core.Domain.Forums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.services.Forums
{
    public interface IForumService
    {
        IList<ForumGroup> GetAllForumGroups();

        IList<Forum> GetAllForumsByGroupId(int forumgroupid);
    }
}
