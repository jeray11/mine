using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace mine.core.Caching
{
    public class MemoryCacheManager : ICacheManager
    {
        /// <summary>
        /// Variable (lock) to support thread-safe
        /// </summary>
        private static readonly object _syncObject = new object();
        /// <summary>
        /// Cache object
        /// </summary>
        protected ObjectCache Cache
        {
            get
            {
                return MemoryCache.Default;
            }
        }
        public T Get<T>(string key)
        {
            return (T)Cache[key];
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
            return Cache.Contains(key);
        }

        public void Set(string key, object data, int cacheTime)
        {
            if (data == null)
                return;
            Cache.Set(key, data, DateTime.Now.AddMinutes(cacheTime));
        }

        public void RemoveByPattern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = new List<String>();
            foreach (var item in Cache)
                if (regex.IsMatch(item.Key))
                    keysToRemove.Add(item.Key);
            foreach (string key in keysToRemove)
            {
                Remove(key);
            }
        }
        public void Remove(string key) 
        {
            Cache.Remove(key);
        }
    }
}
