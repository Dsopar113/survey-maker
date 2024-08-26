using System.ComponentModel.DataAnnotations;

namespace WebApp1.Models
{
    public class SurveyHeader_Participant
    {
        [Key] public Guid SurveyHeader_ParticipantId { get; set; }
        public Guid SurveyId { get; set; }
        public Guid ParticipantId { get; set; }
        public Boolean IsFinished { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
