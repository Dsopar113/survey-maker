using System.ComponentModel.DataAnnotations;

namespace WebApp1.Models
{
    public class SurveyHeader
    {
        [Key]
        public Guid SurveyId { get; set; }
        public string? SurveyName { get; set; }
        public string? SurveyDescription { get; set; }
        public IList<SurveySection> SurveySections { get; set; }
        public SurveyHeader()
        {
            SurveyId = Guid.NewGuid();
            SurveySections = new List<SurveySection>();
        }
    }
}
