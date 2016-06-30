using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace mine.core.Caching
{
    public class PerRequestCacheManager : ICacheManager
    {
        /// <summary>
        /// Variable (lock) to support thread-safe
        /// </summary>
        private static readonly object _syncObject = new object();
        private readonly HttpContextBase _context;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">Context</param>
        public PerRequestCacheManager(HttpContextBase context)
        {
            this._context = context;
        }

        /// <summary>
        /// Creates a new instance of the NopRequestCache class
        /// </summary>
        protected virtual IDictionary GetItems()
        {
            if (_context != null)
                return _context.Items;

            return null;
        }
        public T Get<T>(string key)
        {
            var items = GetItems();
            if (items != null)
                return (T)items[key];
            else
                return default(T);
        }

        public T Get<T>(string key, Func<T> acquire)
        {
            return Get(key, 60, acquire);
        }

        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="key">Cache key</param>
        /// <param name="cacheTime">Cache time in minutes (0 - do not cache)</param>
        /// <param name="acquire">Function to load item if it's not in the cache yet</param>
        /// <returns>Cached item</returns>
        public T Get<T>(string key, int cacheTime, Func<T> acquire)
        {
            lock (_syncObject)
            {
                if (IsSet(key))
                {
                    return Get<T>(key);
                }
                var result = acquire();
                if (cacheTime > 0)
                    Set(key, result, cacheTime);
                return result;
            }
        }

        public bool IsSet(string key)
        {
            var items = GetItems();
            if (items == null)
                return false;
            else
                return items.Contains(key);
        }

        public void Set(string key, object data, int cacheTime)
        {
            var items = GetItems();
            if (items == null)
                return;
            if (data != null)
                items[key] = data;
        }

        public void RemoveByPattern(string pattern)
        {
            var items = GetItems();
            if (items == null)
                return;
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            List<string> keysToRemove = new List<string>();
            foreach (var key in items.Keys)
                if (regex.IsMatch(key.ToString()))
                    keysToRemove.Add(key.ToString());
            foreach (var key in keysToRemove)
                items.Remove(key);
        }

        public void Remove(string key)
        {
            var items = GetItems();
            if (items == null)
                return;
            items.Remove(key);
        }
    }
}
