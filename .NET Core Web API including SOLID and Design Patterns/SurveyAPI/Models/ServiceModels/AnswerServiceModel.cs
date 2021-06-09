using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.Models.ServiceModels
{
    public class AnswerServiceModel
    {
        public int Id { get; set; }
        public int ParticipantId { get; set; }
        public int SurveyId { get; set; }
        public int QuestionId { get; set; }
        public int QuestionAnswersId { get; set; }
    }
}
