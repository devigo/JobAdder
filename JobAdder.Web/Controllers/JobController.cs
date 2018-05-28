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
                // Create a dictionary list of job skills
                Dictionary<string, int> listJobSkills = new Dictionary<string, int>();

                // Get the number of skills for the job
                int value = job.Skills != null ? job.Skills.Split(',').Count() : 0;

                // Create a list of skills for the job and set the value
                foreach (string skill in job.Skills?.Split(','))
                {
                    listJobSkills.Add(skill.Trim().ToString(), value--);
                }

                // List all candidates
                List<Candidate> candidates = await _candidateService.ListAllCandidatesAsync();

                // Create a list of skills for each candidate
                foreach (Candidate candidate in candidates)
                {
                    string[] skillTags = candidates.FirstOrDefault(q => q.CandidateId == candidate.CandidateId).SkillTags?.Split(',');

                    Dictionary<string, int> listCandidateSkills = new Dictionary<string, int>();

                    foreach (string skill in skillTags)
                    {
                        string key = $"CandidateId: {candidate.CandidateId} - Skill: {skill.Trim().ToString()}";

                        if (!listCandidateSkills.Any(q => q.Key == key))
                        {
                            listCandidateSkills.Add(key, listJobSkills.FirstOrDefault(q => q.Key == skill.Trim().ToString()).Value);
                        }
                    }

                    // Get the total of common skills
                    int totalSkills = listCandidateSkills.Where(q => q.Value > 0).Count();

                    // Get the total of values (it helps to order by the most relevant candidate)
                    int totalValue = listCandidateSkills.Sum(q => q.Value);

                    model.Add(new BestFitViewModel
                    {
                        CandidateId = candidate.CandidateId,
                        Name = candidate.Name,
                        SkillTags = candidate.SkillTags,
                        Total = totalSkills,
                        Value = totalValue
                    });
                }

                // Set the total of candidates
                const int totalCandidates = 10;

                // Set the title of the page
                ViewBag.Title = $"Top {totalCandidates} candidates for {job.Name}";
                ViewBag.Subtitle = $"Skills for this job: {job.Skills}";

                return View(model.OrderByDescending(q => q.Total).OrderByDescending(q => q.Value).Take(totalCandidates));
            }

            return View(model);
        }
    }
}