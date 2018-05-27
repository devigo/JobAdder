using JobAdder.Integrations.Models;
using JobAdder.Integrations.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JobAdder.Web.Controllers
{
    public class CandidateController : Controller
    {
        private readonly CandidateService _service = new CandidateService();

        public CandidateController()
        {
        }

        // GET: Candidate
        public async Task<ActionResult> Index()
        {
            List<Candidate> candidates = await _service.ListAllCandidatesAsync();

            return View(candidates.OrderBy(q => q.name).ToList());
        }
    }
}