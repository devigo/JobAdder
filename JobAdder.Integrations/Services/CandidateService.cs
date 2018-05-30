using JobAdder.Integrations.Common;
using JobAdder.Integrations.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace JobAdder.Integrations.Services
{
    public class CandidateService
    {
        #region Declarations

        // Declare a HttpClient
        private readonly HttpClient _httpClient;

        // Declare and set a Cache
        private Cache _cache = new Cache();

        #endregion

        public CandidateService(HttpClient httpClient) // This is the singleton instance of HttpClient
        {
            // Assign it to the local HttpClient
            _httpClient = httpClient;
        }

        #region ListAllCandidatesAsync

        /// <summary>
        /// List all candidates from JobAdder
        /// </summary>
        /// <returns>Return a list of candidates</returns>
        public async Task<List<Candidate>> ListAllCandidatesAsync()
        {
            // Set the key name
            const string key = "ListAllCandidatesAsync";
            // Get the list of values from the cache
            List<Candidate> candidates = _cache.GetObject<Candidate>(key);

            if (candidates == null)
            {
                // Set the URI
                string uri = Uri.Get() + "candidates";

                HttpResponseMessage response = await _httpClient.GetAsync(uri);
                
                if (response.IsSuccessStatusCode)
                {
                    candidates = await response.Content.ReadAsAsync<List<Candidate>>();
                }

                _cache.SetObject(key, candidates);
            }

            return candidates;
        }

        #endregion
    }
}