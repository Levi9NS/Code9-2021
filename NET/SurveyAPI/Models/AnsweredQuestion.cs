namespace SurveyAPI.Models
{
    public class AnsweredQuestion
    {
        public string text { get; set; }
        public string response { get; set; }
        public int count { get; set; }
        public int Id { get; internal set; }
    }
}
