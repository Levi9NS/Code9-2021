using System.Collections.Generic;

namespace SurveyAPI.Models
{
    public class OfferedAnswerResult
    {
        public List<OfferedAnswer> OfferedAnswers { get; set; }

        public class OfferedAnswer
        {
            public string QuestionAnswer { get; set; }
            public int QuestionId { get; internal set; }
        }
    }
}
