using System.ComponentModel.DataAnnotations;

namespace WebApp1.ViewModels
{
    public class SurveyHeaderViewModel
    {
        [Required]
        [MaxLength(30, ErrorMessage ="Survey header cannot be longer than 30 letters.")] 
        public string SurveyName { get; set; }
        public string SurveyDescription { get; set; }
        public List<SurveySectionViewModel> SurveySections { get; set; }
    }
}
