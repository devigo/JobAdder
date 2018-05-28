using JobAdder.Integrations.Models;

namespace JobAdder.Web.Models
{
    public class BestFitViewModel : Candidate
    {
        public int Total { get; set; }

        public int Value { get; set; }
    }
}