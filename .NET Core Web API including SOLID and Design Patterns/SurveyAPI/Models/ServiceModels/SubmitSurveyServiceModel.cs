using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.Models.ServiceModels
{
    public class SubmitSurveyServiceModel
    {
        public ParticipantServiceModel Participant { get; set; }
        public List<AnswerServiceModel> Answers { get; set; }
    }
}
