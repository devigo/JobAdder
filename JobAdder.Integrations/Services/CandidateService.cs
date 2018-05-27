using JobAdder.Integrations.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace JobAdder.Integrations.Services
{
    public class CandidateService
    {
        #region HttpClient

        static HttpClient client = new HttpClient();

        #endregion

        #region URI

        const string uri = "http://private-76432-jobadder1.apiary-mock.com/candidates";

        #endregion

        #region ListAllCandidates

        public async Task<List<Candidate>> ListAllCandidatesAsync()
        {
            List<Candidate> candidates = null;
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                candidates = await response.Content.ReadAsAsync<List<Candidate>>();

            }
            return candidates;
        }

        #endregion
    }
}