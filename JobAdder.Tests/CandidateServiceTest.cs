using JobAdder.Integrations.Models;
using JobAdder.Integrations.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace JobAdder.Tests
{
    [TestClass]
    public class CandidateServiceTest
    {
        private readonly CandidateService _service = new CandidateService(new HttpClient());

        [TestMethod]
        public void Method_ListAllCandidatesAsync()
        {
            Task<List<Candidate>> actual = _service.ListAllCandidatesAsync();

            // Check if there is any candidate
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Result.Count >= 0);

            // Check if the id is always an integer
            foreach (Candidate item in actual.Result)
            {
                int.TryParse(Convert.ToString(item.CandidateId), out int candidateId);
                Assert.IsTrue(candidateId > 0);
            }
        }
    }
}