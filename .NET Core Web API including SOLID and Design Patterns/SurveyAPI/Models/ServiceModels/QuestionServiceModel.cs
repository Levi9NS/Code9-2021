using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.Models.ServiceModels
{
    public class QuestionServiceModel
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public List<OfferedAnswerServiceModel> OfferedAnswers { get; set; }
    }
}
