using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mine.core.Domain.Customers;
using mine.core.Domain.Logging;
using mine.core;
using mine.core.Data;

namespace mine.services.Logging
{
    public class DefaultLogger : ILogger
    {
        private readonly IWebHelper _webHelper;
        private readonly IRepository<Log> _logRepository;
        public DefaultLogger(IWebHelper webHelper, IRepository<Log> logRepository)
        {
            this._webHelper = webHelper;
            this._logRepository = logRepository;
        }
        public Log InsertLog(LogLevel logLevel, string shortMessage, string fullMessage = "", Customer customer = null)
        {
            //check ignore word/phrase list?
            if (IgnoreLog(shortMessage) || IgnoreLog(fullMessage))
                return null;

            var log = new Log
            {
                LogLevel = logLevel,
                ShortMessage = shortMessage,
                FullMessage = fullMessage,
                IpAddress = _webHelper.GetCurrentIpAddress(),
                Customer = customer,
                PageUrl = _webHelper.GetThisPageUrl(true),
                ReferrerUrl = _webHelper.GetUrlReferrer(),
                CreatedOnUtc = DateTime.UtcNow
            };

            _logRepository.Insert(log);

            return log;
        }

        public bool IsEnabled(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return false;
                default:
                    return true;
            }
        }

        public void Warning(string message, Exception exception = null, Customer customer = null)
        {
            FilteredLog(LogLevel.Warning, message, exception, customer);
        }

        private void FilteredLog(LogLevel level, string message, Exception exception = null, Customer customer = null)
        {
            //don't log thread abort exception
            if (exception is System.Threading.ThreadAbortException)
                return;

            if (this.IsEnabled(level))
            {
                string fullMessage = exception == null ? string.Empty : exception.ToString();
                this.InsertLog(level, message, fullMessage, customer);
            }
        }
    }
}
