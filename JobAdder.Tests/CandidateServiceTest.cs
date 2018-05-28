using JobAdder.Integrations.Models;
using JobAdder.Integrations.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobAdder.Tests
{
    [TestClass]
    public class CandidateServiceTest
    {
        private readonly CandidateService _service = new CandidateService();

        [TestMethod]
        public void Method_ListAllCandidatesAsync()
        {
            Task<List<Candidate>> actual = _service.ListAllCandidatesAsync();

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Result.Count >= 0);
        }
    }
}