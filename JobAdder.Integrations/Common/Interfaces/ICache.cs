using System.Collections.Generic;

namespace JobAdder.Integrations.Common.Interfaces
{
    public interface ICache
    {
        /// <summary>
        /// Get the cache
        /// </summary>
        /// <typeparam name="T">Object</typeparam>
        /// <param name="key">Cache key</param>
        /// <returns>Return an object list</returns>
        List<T> GetObject<T>(string key) where T : class;

        /// <summary>
        /// Set the cache
        /// </summary>
        /// <param name="key">Cache key</param>
        /// <param name="value">Object</param>
        void SetObject(string key, object value);
    }
}