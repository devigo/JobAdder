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

            return View(jobs.OrderBy(q => q.Name).ToList());
        }

        public async Task<ActionResult> BestFit(int jobId)
        {
            List<BestFitViewModel> model = new List<BestFitViewModel>();

            // List all jobs
            List<Job> jobs = await _jobService.ListAllJobsAsync();

            // Get the current job
            Job job = jobs.FirstOrDefault(q => q.JobId == jobId);

            if (job != null)
            {
                // Create a list of job skills
                List<string> listJobSkills = new List<string>();
                listJobSkills.AddRange(job.Skills.Split(','));

                // List all candidates
                List<Candidate> candidates = await _candidateService.ListAllCandidatesAsync();

                // Create a list of skills for each candidate
                foreach (Candidate item in candidates)
                {
                    List<string> listCandidateSkills = new List<string>();
                    listCandidateSkills
                        .AddRange(candidates
                        .FirstOrDefault(q => q.CandidateId == item.CandidateId).SkillTags.Split(','));

                    // Compare both lists to search common skills
                    int total = listCandidateSkills.Intersect(listJobSkills).Count();

                    model.Add(new BestFitViewModel
                    {
                        CandidateId = item.CandidateId,
                        Name = item.Name,
                        SkillTags = item.SkillTags,
                        Total = total
                    });
                }

                // Set the total of candidates
                const int totalOfCandidates = 10;

                // Set the title of the page
                ViewBag.Title = $"Top {totalOfCandidates} candidates for {job.Name}";

                return View(model.OrderByDescending(q => q.Total).Take(totalOfCandidates));
            }

            return View(model);
        }
    }
}