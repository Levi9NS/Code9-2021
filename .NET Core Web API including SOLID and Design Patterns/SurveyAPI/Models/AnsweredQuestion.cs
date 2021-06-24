using System.Collections.Generic;

namespace SurveyAPI.Models
{
    public class AnsweredQuestion
    {
        public string text { get; set; }
        public List<AnsweredQuestionList> responses { get; set; }
        public int Id { get; internal set; }
    }

    public class AnsweredQuestionList
    {
        public string response { get; set; }
        public int count { get; set; }
    }
}
