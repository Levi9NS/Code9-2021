using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public Participant Participant { get; set; }
        public int SurveyId { get; set; }
        public List<QuestionAnswer> QuestionAnswers { get; set; }
    }
}
