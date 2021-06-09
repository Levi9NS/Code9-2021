namespace SurveyAPI.Models
{
    public class AnsweredQuestionServiceModel
    {
        public string questionText { get; set; }
        public string response { get; set; }
        public int count { get; set; }
        public int questionId { get; internal set; }
    }
}
