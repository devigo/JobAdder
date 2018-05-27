using JobAdder.Integrations.Models;
using JobAdder.Integrations.Services;
using JobAdder.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JobAdder.Web.Controllers
{
    public class JobController : Controller
    {
        private readonly JobService _jobService = new JobService();
        private readonly CandidateService _candidateService = new CandidateService();

        public JobController()
        {
        }

        // GET: Job
        public async Task<ActionResult> Index()
        {
            List<Job> jobs = await _jobService.ListAllJobsAsync();

            return View(jobs.OrderBy(q => q.name).ToList());
        }

        public async Task<ActionResult> BestFit(int jobId)
        {
            List<BestFitViewModel> model = new List<BestFitViewModel>();

            // List all jobs
            List<Job> jobs = await _jobService.ListAllJobsAsync();

            // Get the current job
            Job job = jobs.FirstOrDefault(q => q.jobId == jobId);

            if (job != null)
            {
                // Create a list of job skills
                List<string> listJobSkills = new List<string>();
                listJobSkills.AddRange(job.skills.Split(','));

                // List all candidates
                List<Candidate> candidates = await _candidateService.ListAllCandidatesAsync();

                // Create a list of skills for each candidate
                foreach (Candidate item in candidates)
                {
                    List<string> listCandidateSkills = new List<string>();
                    listCandidateSkills
                        .AddRange(candidates
                        .FirstOrDefault(q => q.candidateId == item.candidateId).skillTags.Split(','));

                    // Compare both lists to search common skills
                    int total = listCandidateSkills.Intersect(listJobSkills).Count();

                    model.Add(new BestFitViewModel
                    {
                        candidateId = item.candidateId,
                        name = item.name,
                        skillTags = item.skillTags,
                        Total = total
                    });
                }

                // Set the title of the page
                ViewBag.Title = $"List of the best candidates for {job.name}";

                return View(model.OrderByDescending(q => q.Total).Take(10));
            }

            return View(model);
        }
    }
}