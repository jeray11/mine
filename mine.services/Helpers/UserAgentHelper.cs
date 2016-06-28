﻿using mine.core;
using mine.core.Configuration;
using mine.core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using UserAgentStringLibrary;

namespace mine.services.Helpers
{
    public class UserAgentHelper:IUserAgentHelper
    {
        private HttpContextBase _httpContext;
        private MineConfig _config;
        private IWebHelper _webHelper;
        public UserAgentHelper(MineConfig config, HttpContextBase httpContext,IWebHelper webHelper) 
        {
            this._httpContext = httpContext;
            this._config = config;
            this._webHelper = webHelper;
        }
        /// <summary>
        /// Get a value indicating whether the request is made by search engine (web crawler)
        /// </summary>
        /// <returns>Result</returns>
        public virtual bool IsSearchEngine()
        {
            if (_httpContext == null)
                return false;

            //we put required logic in try-catch block
            //more info: http://www.nopcommerce.com/boards/t/17711/unhandled-exception-request-is-not-available-in-this-context.aspx
            bool result = false;
            try
            {
                var uasParser = GetUasParser();

                //we cannot load parser
                if (uasParser == null)
                    return false;

                var userAgent = _httpContext.Request.UserAgent;
                result = uasParser.IsBot(userAgent);
                //result = context.Request.Browser.Crawler;
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
            }
            return result;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        protected virtual UasParser GetUasParser()
        {
            if (Singleton<UasParser>.Instance == null)
            {
                //no database created
                if (String.IsNullOrEmpty(_config.UserAgentStringsPath))
                    return null;

                var filePath = _webHelper.MapPath(_config.UserAgentStringsPath);
                var uasParser = new UasParser(filePath);
                Singleton<UasParser>.Instance = uasParser;
            }
            return Singleton<UasParser>.Instance;
        }
    }
}
