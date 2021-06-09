using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.Models.ControllerModels
{
    public class SubmitSurveyModel
    {
        [Required]
        public ParticipantModel Participant { get; set; }
        [Required]
        public List<AnswerModel> Answers { get; set; }
    }
}
