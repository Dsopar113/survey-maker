using System.ComponentModel.DataAnnotations;

namespace WebApp1.Models
{
    public class Participant
    {
        [Key] public Guid ParticipantId { get; set; }
        public String? ParticipantMail { get; set; }
        public String? ParticipantName { get; set; }
        public String? ParticipantLastName { get; set; }
        public IList<SurveyHeader_Participant> SurveyHeader_Participants { get; set; }
        public Participant()
        {
            //ParticipantId = Guid.NewGuid();
            SurveyHeader_Participants = new List<SurveyHeader_Participant>();
        }
    }
}
