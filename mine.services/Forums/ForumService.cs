using mine.core.Caching;
using mine.core.Data;
using mine.core.Domain.Forums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.services.Forums
{
    public class ForumService:IForumService
    {
        /// <summary>
        /// Key for caching
        /// </summary>
        private const string FORUMGROUP_ALL_KEY = "Nop.forumgroup.all";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : forum group ID
        /// </remarks>
        private const string FORUM_ALLBYFORUMGROUPID_KEY = "Nop.forum.allbyforumgroupid-{0}";

        private ICacheManager _cacheManager;
        private IRepository<ForumGroup> _forumGroupRepository;
        private IRepository<Forum> _forumRepository;
        public ForumService(ICacheManager cacheManager, 
            IRepository<ForumGroup> forumGroupRepository,
            IRepository<Forum> forumRepository)
        {
            this._cacheManager = cacheManager;
            this._forumGroupRepository = forumGroupRepository;
            this._forumRepository = forumRepository;
        }
        public IList<ForumGroup> GetAllForumGroups()
        {
            string key = string.Format(FORUMGROUP_ALL_KEY);
            return _cacheManager.Get(key, () => {
                var query = from fg in _forumGroupRepository.Table
                            orderby fg.DisplayOrder
                            select fg;
                return query.ToList();
            });
        }

        public IList<Forum> GetAllForumsByGroupId(int forumgroupid)
        {
            string key = string.Format(FORUM_ALLBYFORUMGROUPID_KEY, FORUMGROUP_ALL_KEY);
            return _cacheManager.Get(key, () => {
                var query = from f in _forumRepository.Table
                            orderby f.DisplayOrder
                            where f.ForumGroupId == forumgroupid
                            select f;
                return query.ToList();
            });
        }
    }
}
