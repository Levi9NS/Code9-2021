using System.Collections.Generic;

namespace SurveyAPI.Models
{
    public class OfferedAnswerResultServiceModel
    {
        public List<AnswerResultServiceModel> OfferedAnswers { get; set; }

        public class AnswerResultServiceModel
        {
            public string QuestionText { get; set; }
            public string QuestionAnswer { get; set; }
            public int QuestionId { get; internal set; }
            public int OfferedAnswerId { get; set; }
        }
    }
}
