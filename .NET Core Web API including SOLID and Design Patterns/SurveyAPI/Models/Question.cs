using System.Collections.Generic;

namespace SurveyAPI.Models
{
    public class Question
    {
        public string QuestionText { get; set; }

      public ICollection<OfferedAnswer> offeredAnswers { get; set; }
        public int Id { get; internal set; }
    }
}
