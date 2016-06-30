﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.web.framework.Localization
{
    public static class LocalizedUrlExtensions
    {
        private static int _seoCodeLength = 2;
        /// <summary>
        /// Returns a value indicating whether nopCommerce is run in virtual directory
        /// </summary>
        /// <param name="applicationPath">Application path</param>
        /// <returns>Result</returns>
        private static bool IsVirtualDirectory(this string applicationPath)
        {
            if (string.IsNullOrEmpty(applicationPath))
                throw new ArgumentException("Application path is not specified");

            return applicationPath != "/";
        }
        /// <summary>
        /// Remove application path from raw URL
        /// </summary>
        /// <param name="rawUrl">Raw URL</param>
        /// <param name="applicationPath">Application path</param>
        /// <returns>Result</returns>
        public static string RemoveApplicationPathFromRawUrl(this string rawUrl, string applicationPath)
        {
            if (string.IsNullOrEmpty(applicationPath))
                throw new ArgumentException("Application path is not specified");

            if (rawUrl.Length == applicationPath.Length)
                return "/";


            var result = rawUrl.Substring(applicationPath.Length);
            //raw url always starts with '/'
            if (!result.StartsWith("/"))
                result = "/" + result;
            return result;
        }
        public static bool IsLocalizedUrl(this string url, string applicationPath, bool isRawPath) 
        {
            if (string.IsNullOrEmpty(url))
                return false;
            if (isRawPath) 
            {
                if (applicationPath.IsVirtualDirectory())
                {
                    //we're in virtual directory. So remove its path
                    url = url.RemoveApplicationPathFromRawUrl(applicationPath);
                }
                int length = url.Length;
                //too short url
                if (length < 1 + _seoCodeLength)
                    return false;

                //url like "/en"
                if (length == 1 + _seoCodeLength)
                    return true;

                //urls like "/en/" or "/en/somethingelse"
                return (length > 1 + _seoCodeLength) && (url[1 + _seoCodeLength] == '/');
            }
            else 
            {
                int length = url.Length;
                //too short url
                if (length < 2 + _seoCodeLength)
                    return false;

                //url like "/en"
                if (length == 2 + _seoCodeLength)
                    return true;

                //urls like "/en/" or "/en/somethingelse"
                return (length > 2 + _seoCodeLength) && (url[2 + _seoCodeLength] == '/');
            }
            
        }
        /// <summary>
        /// Get language SEO code from URL
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="applicationPath">Application path</param>
        /// <param name="isRawPath">A value indicating whether war URL is passed</param>
        /// <returns>Result</returns>
        public static string GetLanguageSeoCodeFromUrl(this string url, string applicationPath, bool isRawPath)
        {
            if (isRawPath)
            {
                if (applicationPath.IsVirtualDirectory())
                {
                    //we're in virtual directory. So remove its path
                    url = url.RemoveApplicationPathFromRawUrl(applicationPath);
                }

                return url.Substring(1, _seoCodeLength);
            }

            return url.Substring(2, _seoCodeLength);
        }
    }
}