using JobAdder.Integrations.Models;
using JobAdder.Integrations.Services;
using JobAdder.Web.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JobAdder.Web.Controllers
{
    public class JobController : Controller
    {
        #region Declarations

        private readonly JobService _jobService;
        private readonly CandidateService _candidateService;

        #endregion

        public JobController() : this(new HttpClient())
        {
        }

        public JobController(HttpClient httpClient)
        {
            _jobService = new JobService(httpClient);
            _candidateService = new CandidateService(httpClient);
        }

        // GET: Job
        public async Task<ActionResult> Index()
        {
            List<Job> jobs = await _jobService.ListAllJobsAsync();

            return View(jobs.OrderBy(q => q.Name).ToList());
        }

        public async Task<ActionResult> BestFit(int id)
        {
            List<BestFitViewModel> model = new List<BestFitViewModel>();

            // List all jobs
            List<Job> jobs = await _jobService.ListAllJobsAsync();

            // Get the current job
            Job job = jobs.FirstOrDefault(q => q.JobId == id);

            if (job != null)
            {
                // Create to the job a dictionary of skills and value
                Dictionary<string, int> listJobSkills = new Dictionary<string, int>();

                // Get the number of skills for the job
                int value = job.Skills != null ? job.Skills.Split(',').Count() : 0;

                // Add to the job list each skill and value
                foreach (string skill in job.Skills?.Split(','))
                {
                    listJobSkills.Add(skill.Trim().ToString(), value--);
                }

                // List all candidates
                List<Candidate> candidates = await _candidateService.ListAllCandidatesAsync();

                foreach (Candidate candidate in candidates)
                {
                    // Create to each candidate a dictionary of skills and value 
                    Dictionary<string, int> listCandidateSkills = new Dictionary<string, int>();

                    string[] skillTags = candidates.FirstOrDefault(q => q.CandidateId == candidate.CandidateId).SkillTags?.Split(',');

                    foreach (string skill in skillTags)
                    {
                        // Set the dictionary key
                        string key = $"CandidateId: {candidate.CandidateId} - Skill: {skill.Trim().ToString()}";

                        // Add to the candidate list each skill and value
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

                // Get the total of candidates who will be retrieved per job
                int.TryParse(ConfigurationManager.AppSettings["Candidates"], out int totalCandidates);

                // Set the title and subtitle of the page
                ViewBag.Title = $"Top {totalCandidates} candidates for {job.Name}";
                ViewBag.Subtitle = $"Skills for this job: {job.Skills}";

                return View(model.OrderByDescending(q => q.Total).OrderByDescending(q => q.Value).Take(totalCandidates));
            }

            return View(model);
        }
    }
}