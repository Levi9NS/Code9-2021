using System.Collections.Generic;

namespace SurveyAPI.Models
{
    public class SurveyResult
    {
        public string Name { get; set; }

        public List<AnsweredQuestion> Questions { get; set; }
    }
}
