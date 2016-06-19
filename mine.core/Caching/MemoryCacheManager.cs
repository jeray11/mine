using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace mine.core.Caching
{
    public class MemoryCacheManager : ICacheManager
    {
        /// <summary>
        /// Variable (lock) to support thread-safe
        /// </summary>
        private static readonly object _syncObject = new object();

        public T Get<T>(string key)
        {
            return (T)MemoryCache.Default[key];
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
            return MemoryCache.Default.Contains(key);
        }

        public void Set(string key, object data, int cacheTime)
        {
            if (data == null)
                return;
            MemoryCache.Default.Set(key, data, DateTime.Now.AddMinutes(cacheTime));
        }
    }
}
