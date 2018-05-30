using JobAdder.Integrations.Models;
using JobAdder.Integrations.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JobAdder.Web.Controllers
{
    public class CandidateController : Controller
    {
        #region Declarations

        private readonly CandidateService _candidateService;

        #endregion

        public CandidateController() : this(new HttpClient())
        {
        }

        public CandidateController(HttpClient httpClient)
        {
            _candidateService = new CandidateService(httpClient);
        }

        // GET: Candidate
        public async Task<ActionResult> Index()
        {
            List<Candidate> candidates = await _candidateService.ListAllCandidatesAsync();

            return View(candidates.OrderBy(q => q.Name).ToList());
        }
    }
}