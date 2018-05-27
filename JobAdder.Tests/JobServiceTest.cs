using JobAdder.Integrations.Models;
using JobAdder.Integrations.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobAdder.Tests
{
    [TestClass]
    public class JobServiceTest
    {
        private readonly JobService _service = new JobService();

        [TestMethod]
        public void Method_ListAllJobsAsync()
        {
            Task<List<Job>> list = _service.ListAllJobsAsync();

            Assert.IsNotNull(list);
            Assert.IsTrue(list.Result.Count >= 0);
        }
    }
}