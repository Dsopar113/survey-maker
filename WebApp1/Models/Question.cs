using System.ComponentModel.DataAnnotations;
using Microsoft.CodeAnalysis.Options;

namespace WebApp1.Models
{
    public class Question
    {
        [Key]
        public Guid QuestionId { get; set; }
        public string? QuestionContent { get; set; }
        public Guid SurveySectionId { get; set; }
        public virtual IList<Option> Options { get; set; }
        public Question()
        {
            QuestionId = Guid.NewGuid();
        }
    }
}
