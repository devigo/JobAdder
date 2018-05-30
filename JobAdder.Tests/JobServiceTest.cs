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
    public class JobServiceTest
    {
        private readonly JobService _service = new JobService(new HttpClient());

        [TestMethod]
        public void Method_ListAllJobsAsync()
        {
            Task<List<Job>> actual = _service.ListAllJobsAsync();

            // Check if there is any job
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Result.Count >= 0);

            // Check if the id is always an integer
            foreach (Job item in actual.Result)
            {
                int.TryParse(Convert.ToString(item.JobId), out int jobId);
                Assert.IsTrue(jobId > 0);
            }
        }
    }
}