using JobAdder.Integrations.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Caching;

namespace JobAdder.Integrations.Common
{
    public class Cache : ICache
    {
        #region Declarations

        // Declare a Cache
        private readonly MemoryCache _cache;

        #endregion

        public Cache()
        {
            // Set it to the local Cache
            _cache = MemoryCache.Default;
        }

        /// <summary>
        /// Get the cache
        /// </summary>
        /// <typeparam name="T">Object</typeparam>
        /// <param name="key">Cache key</param>
        /// <returns>Return an object list</returns>
        public List<T> GetObject<T>(string key) where T : class
        {
            return (List<T>)_cache.Get((key));
        }

        /// <summary>
        /// Set the cache
        /// </summary>
        /// <param name="key">Cache key</param>
        /// <param name="value">Object</param>
        public void SetObject(string key, object value)
        {
            // Get expiration time
            int.TryParse(ConfigurationManager.AppSettings["CacheExpiration"], out int cacheExpiration);

            // Set cache expiration time
            CacheItemPolicy policy = new CacheItemPolicy { SlidingExpiration = new TimeSpan(cacheExpiration, 0, 0) };

            // Add object to cache
            _cache.Add(key, value, policy);
        }
    }
}