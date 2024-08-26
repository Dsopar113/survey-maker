using System.ComponentModel.DataAnnotations;

namespace WebApp1.Models
{
    public class Question
    {
        [Key]
        public Guid QuestionId { get; set; }
        public string? QuestionContent { get; set; }
        public Guid SurveySectionId { get; set; }
        public int Answer {  get; set; }
        public Question()
        {
            //QuestionId = Guid.NewGuid();
        }
    }
}
