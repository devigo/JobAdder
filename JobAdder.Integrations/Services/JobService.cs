using JobAdder.Integrations.Common;
using JobAdder.Integrations.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace JobAdder.Integrations.Services
{
    public class JobService
    {
        #region Declarations

        // Declare a HttpClient
        private readonly HttpClient _httpClient;

        // Declare and set a Cache
        private Cache _cache = new Cache();

        #endregion

        public JobService(HttpClient httpClient) // This is the singleton instance of HttpClient
        {
            // Assign it to the local HttpClient
            _httpClient = httpClient;
        }

        #region ListAllJobsAsync

        /// <summary>
        /// List all jobs from JobAdder
        /// </summary>
        /// <returns>Return a list of jobs</returns>
        public async Task<List<Job>> ListAllJobsAsync()
        {
            // Set the key name
            const string key = "ListAllJobsAsync";
            // Get the list of values from the cache
            List<Job> jobs = _cache.GetObject<Job>(key);

            if (jobs == null)
            {
                // Set the URI
                string uri = Uri.Get() + "jobs";

                HttpResponseMessage response = await _httpClient.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    jobs = await response.Content.ReadAsAsync<List<Job>>();
                }

                _cache.SetObject(key, jobs);
            }

            return jobs;
        }

        #endregion
    }
}