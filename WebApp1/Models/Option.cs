using System.ComponentModel.DataAnnotations;

namespace WebApp1.Models
{
    public class Option
    {
        [Key]
        public Guid OptionId { get; set; }
        public string OptionText { get; set; }
        public int? OptionValue { get; set; }
        public Guid QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}
