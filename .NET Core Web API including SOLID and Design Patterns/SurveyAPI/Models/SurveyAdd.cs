using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.Models
{
    public class SurveyAdd
    {
        public int SurveyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IEnumerable<SurveyAnswer> Answers { get; set; }
    }

    public class SurveyAnswer
    {
        public int SurveyId { get; set; }
        public int QuestionId { get; set; }
        public int QuestionAnswersId { get; set; }

    }
}
