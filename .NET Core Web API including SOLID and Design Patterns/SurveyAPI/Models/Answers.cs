using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.Models
{
    public class Answers
    {
        public int SurveyID { get; set; }
        public int ParticipantID { get; set; }
        public List<ParticipantAnswers> AnsweredQuestions { get; set; }

    }
}
