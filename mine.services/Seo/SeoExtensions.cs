using mine.core;
using mine.core.Domain;
using mine.core.Domain.Forums;
using mine.core.Domain.Seo;
using mine.core.Infrastructure;
using mine.services.Localization;
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
        /// Gets ForumTopic SE (search engine) name
        /// </summary>
        /// <param name="forumTopic">ForumTopic</param>
        /// <returns>ForumTopic SE (search engine) name</returns>
        public static string GetSeName(this ForumTopic forumTopic)
        {
            if (forumTopic == null)
                throw new ArgumentNullException("forumTopic");
            string seName = GetSeName(forumTopic.Subject);

            // Trim SE name to avoid URLs that are too long
            var maxLength = 100;
            if (seName.Length > maxLength)
            {
                seName = seName.Substring(0, maxLength);
            }

            return seName;
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
        /// <summary>
        /// Get search engine friendly name (slug)
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="entity">Entity</param>
        /// <returns>Search engine  name (slug)</returns>
        public static string GetSeName<T>(this T entity)
            where T : BaseEntity, ISlugSupported
        {
            var workContext = EngineContext.Current.Resolve<IWorkContext>();
            return GetSeName(entity, workContext.WorkingLanguage.Id);
        }
        /// <summary>
        ///  Get search engine friendly name (slug)
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="languageId">Language identifier</param>
        /// <param name="returnDefaultValue">A value indicating whether to return default value (if language specified one is not found)</param>
        /// <param name="ensureTwoPublishedLanguages">A value indicating whether to ensure that we have at least two published languages; otherwise, load only default value</param>
        /// <returns>Search engine  name (slug)</returns>
        public static string GetSeName<T>(this T entity, int languageId, bool returnDefaultValue = true,
            bool ensureTwoPublishedLanguages = true)
            where T : BaseEntity, ISlugSupported
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            string entityName = typeof(T).Name;
            return GetSeName(entity.Id, entityName, languageId, returnDefaultValue, ensureTwoPublishedLanguages);
        }
        /// <summary>
        /// Get search engine friendly name (slug)
        /// </summary>
        /// <param name="entityId">Entity identifier</param>
        /// <param name="entityName">Entity name</param>
        /// <param name="languageId">Language identifier</param>
        /// <param name="returnDefaultValue">A value indicating whether to return default value (if language specified one is not found)</param>
        /// <param name="ensureTwoPublishedLanguages">A value indicating whether to ensure that we have at least two published languages; otherwise, load only default value</param>
        /// <returns>Search engine  name (slug)</returns>
        public static string GetSeName(int entityId, string entityName, int languageId, bool returnDefaultValue = true,
            bool ensureTwoPublishedLanguages = true)
        {
            string result = string.Empty;

            var urlRecordService = EngineContext.Current.Resolve<IUrlRecordService>();
            if (languageId > 0)
            {
                //ensure that we have at least two published languages
                bool loadLocalizedValue = true;
                if (ensureTwoPublishedLanguages)
                {
                    var lService = EngineContext.Current.Resolve<ILanguageService>();
                    var totalPublishedLanguages = lService.GetAllLanguages().Count;
                    loadLocalizedValue = totalPublishedLanguages >= 2;
                }
                //localized value
                if (loadLocalizedValue)
                {
                    result = urlRecordService.GetActiveSlug(entityId, entityName, languageId);
                }
            }
            //set default value if required
            if (String.IsNullOrEmpty(result) && returnDefaultValue)
            {
                result = urlRecordService.GetActiveSlug(entityId, entityName, 0);
            }

            return result;
        }
    }
}
