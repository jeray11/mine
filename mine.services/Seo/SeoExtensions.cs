using mine.core.Domain.Forums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.services.Seo
{
    public static class SeoExtensions
    {
        /// <summary>
        /// Gets ForumGroup SE (search engine) name
        /// </summary>
        /// <param name="forumGroup">ForumGroup</param>
        /// <returns>ForumGroup SE (search engine) name</returns>
        public static string GetSeName(this ForumGroup forumGroup)
        {
            if (forumGroup == null)
                throw new ArgumentNullException("forumGroup");
            string seName = GetSeName(forumGroup.Name);
            return seName;
        }

        /// <summary>
        /// Get SE name
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Result</returns>
        public static string GetSeName(string name)
        {
            //var seoSettings = EngineContext.Current.Resolve<SeoSettings>();
            //return GetSeName(name, seoSettings.ConvertNonWesternChars, seoSettings.AllowUnicodeCharsInUrls);
            return "";
        }
        /// <summary>
        /// Gets Forum SE (search engine) name
        /// </summary>
        /// <param name="forum">Forum</param>
        /// <returns>Forum SE (search engine) name</returns>
        public static string GetSeName(this Forum forum)
        {
            if (forum == null)
                throw new ArgumentNullException("forum");
            string seName = GetSeName(forum.Name);
            return seName;
        }
    }
}
