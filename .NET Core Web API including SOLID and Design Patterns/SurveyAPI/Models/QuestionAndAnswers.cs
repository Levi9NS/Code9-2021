using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.Models
{
    public class QuestionAndAnswers
    {
        public string QuestionText { get; set; }
        public int SurveyId { get; internal set; } 
        public List<string> Answers { get; set; }     
    }
}
