using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.DTO
{
    public class SurveyResultDTO
    {
        public string  QuestionText { get; set; }
        //offeredAnswertext and counter of that
        public Dictionary<string, int> offeredAnswers { get; set; }


    }
}
