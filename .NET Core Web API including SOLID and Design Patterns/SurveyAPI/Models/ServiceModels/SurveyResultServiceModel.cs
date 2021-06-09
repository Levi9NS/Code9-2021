using System.Collections.Generic;

namespace SurveyAPI.Models
{
    public class SurveyResultServiceModel
    {
        public string Name { get; set; }
        public List<AnsweredQuestionServiceModel> Questions { get; set; }
    }
}
