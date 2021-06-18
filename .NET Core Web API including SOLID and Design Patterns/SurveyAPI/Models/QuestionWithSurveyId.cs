using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.Models
{
    public class QuestionWithSurveyId
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public int SurveyId { get; set; }
        public List<int> Answers { get; set; }
    }
}
