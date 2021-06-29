using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.Models
{
    public class QuestionWithAnswersOnly
    {
        public string QuestionText { get; set; }
        public List<string> Answers { get; set; }
    }
}
