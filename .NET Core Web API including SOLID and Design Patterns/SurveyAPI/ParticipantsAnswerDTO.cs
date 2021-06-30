using SurveyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI
{
    public class ParticipantsAnswerDTO
    {
        public Participant participant { get; set; }
        public List<Answer> answers { get; set; }

    }
}
