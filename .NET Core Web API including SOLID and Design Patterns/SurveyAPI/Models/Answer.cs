using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.Models
{
    public class Answer
    {
        public int ParticipantId { get; set; }
        public int SurveyId { get; set; }
        public int QuestionId { get; set; }
        public int QuestionAnsweredId { get; set; }
        public int Id { get; internal set; }
    }
}
