using System.Collections.Generic;

namespace SurveyAPI.Models
{
    public class AnsweredQuestionServiceModel
    {
        public string questionText { get; set; }
        public int questionId { get; internal set; }

        public List<AnswersResultServiceModel> answerResult { get; set; }
    }

    public class AnswersResultServiceModel
    {
        public string answer { get; set; }
        public int count { get; set; }
    }
}
