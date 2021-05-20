using System.Collections.Generic;

namespace SurveyAPI.Models
{
    public class OfferedAnswerResult
    {
        public string Name { get; set; }

        public List<OfferedAnswer> OfferedAnswers { get; set; }
    }
}
