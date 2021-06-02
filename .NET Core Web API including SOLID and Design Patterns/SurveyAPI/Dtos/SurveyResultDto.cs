using System.Collections.Generic;

namespace SurveyAPI.Dtos
{
    public class SurveyResultDto
    {
        public int SurveyId { get; set; }
        public int ParticipantId { get; set; }
        public List<QuestionAnswer> Answers { get; set; }
    }

    public class QuestionAnswer
    {
        public int QuestionId { get; set; }
        public int QuestionAnswerId { get; set; }
    }
}