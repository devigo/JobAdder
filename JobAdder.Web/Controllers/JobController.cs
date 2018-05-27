using JobAdder.Integrations.Models;
using JobAdder.Integrations.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JobAdder.Web.Controllers
{
    public class JobController : Controller
    {
         private readonly JobService _service = new JobService();

        public JobController()
        {
        }

        // GET: Job
        public async Task<ActionResult> Index()
        {
            List<Job> jobs = await _service.ListAllJobsAsync();

            return View(jobs.OrderBy(q => q.name).ToList());
        }
    }
}