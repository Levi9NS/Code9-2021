using System;
using System.Collections.Generic;
using static SurveyAPI.Models.OfferedAnswerResult;

namespace SurveyAPI.Models
{
    public class Question
    {
        public string QuestionText { get; set; }

        public int Id { get; internal set; }

        public List<int> offeredAnswersIds { get; set; }
    }
}
