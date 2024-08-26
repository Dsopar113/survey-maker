using System.ComponentModel.DataAnnotations;

namespace WebApp1.Models
{
    public class SurveySection
    {
        [Key]
        public Guid SurveySectionId { get; set; }
        public string? SectionName { get; set; }
        public string? SectionDescription { get; set; }
        public Guid SurveyHeaderId { get; set; }
        public virtual IList<Question> Questions { get; set; }
        public SurveySection() { 
            //SurveySectionId = Guid.NewGuid();
        }
    }
}
