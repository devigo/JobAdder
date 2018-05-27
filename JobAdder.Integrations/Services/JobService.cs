using JobAdder.Integrations.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace JobAdder.Integrations.Services
{
    public class JobService
    {
        #region HttpClient

        static HttpClient client = new HttpClient();

        #endregion

        #region URI

        const string uri = "http://private-76432-jobadder1.apiary-mock.com/jobs";

        #endregion

        #region ListAllJobs

        public async Task<List<Job>> ListAllJobsAsync()
        {
            List<Job> jobs = null;
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                jobs = await response.Content.ReadAsAsync<List<Job>>();
            }

            return jobs;
        }

        #endregion
    }
}