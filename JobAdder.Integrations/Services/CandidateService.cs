using JobAdder.Integrations.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace JobAdder.Integrations.Services
{
    public class CandidateService
    {
        #region HttpClient

        private static HttpClient _client = new HttpClient();

        #endregion

        #region Cache

        private static MemoryCache cache = MemoryCache.Default;

        #endregion

        #region ListAllCandidates

        public async Task<List<Candidate>> ListAllCandidatesAsync()
        {
            // Set the key name
            const string key = "ListAllCandidatesAsync";

            if (cache[key] == null)
            {
                // Set the URI
                string uri = Common.Uri.Get() + "candidates";

                HttpResponseMessage response = await _client.GetAsync(uri);
                List<Candidate> listValues = null;

                if (response.IsSuccessStatusCode)
                {
                    listValues = await response.Content.ReadAsAsync<List<Candidate>>();
                }

                //Get expiration time
                int.TryParse(ConfigurationManager.AppSettings["CacheExpiration"], out int cacheExpiration);

                // Set cache expiration time
                CacheItemPolicy policy = new CacheItemPolicy { SlidingExpiration = new TimeSpan(cacheExpiration, 0, 0) };

                // Add object to cache
                cache.Add(key, listValues, policy);
            }

            // Get the list of values from the cache
            return (List<Candidate>)cache[key];
        }

        #endregion
    }
}