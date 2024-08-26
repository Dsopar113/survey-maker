using System.ComponentModel.DataAnnotations;

namespace WebApp1.ViewModels
{
    public class SurveyParticipantViewModel
    {
        [Required]
        public string ParticipantEmail {  get; set; }
        [MinLength(1)]
        [MaxLength(40)]
        public string ParticipantFirstName { get; set; }
        [MinLength(1)]
        [MaxLength(40)]
        public string ParticipantLastName { get; set; }
    }
}
