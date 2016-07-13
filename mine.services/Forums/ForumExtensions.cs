using mine.core.Domain.Forums;
using mine.core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.services.Forums
{
    public static class ForumExtensions
    {
        /// <summary>
        /// Strips the topic subject
        /// </summary>
        /// <param name="forumTopic">Forum topic</param>
        /// <returns>Formatted subject</returns>
        public static string StripTopicSubject(this ForumTopic forumTopic)
        {
            string subject = forumTopic.Subject;
            if (String.IsNullOrEmpty(subject))
            {
                return subject;
            }

            int strippedTopicMaxLength = EngineContext.Current.Resolve<ForumSettings>().StrippedTopicMaxLength;
            if (strippedTopicMaxLength > 0)
            {
                if (subject.Length > strippedTopicMaxLength)
                {
                    int index = subject.IndexOf(" ", strippedTopicMaxLength);
                    if (index > 0)
                    {
                        subject = subject.Substring(0, index);
                        subject += "...";
                    }
                }
            }

            return subject;
        }
    }
}
