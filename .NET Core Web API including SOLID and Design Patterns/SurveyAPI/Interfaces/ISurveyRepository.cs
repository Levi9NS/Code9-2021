﻿using SurveyAPI.Models;
using System.Collections.Generic;
using static SurveyAPI.Models.OfferedAnswerResult;

namespace SurveyAPI.Interfaces
{
    public interface ISurveyRepository
    {
        Survey GetSurvey(int surveyId);
        Survey AddSurvey(Survey survey); 
        SurveyResult GetSurveyResult(int surveyId);
        SurveyResult AddSurveyResult(SurveyResult survey);
        OfferedAnswerResult GetOfferedAnswersForSurvey(int surveyId);
        void DeleteSurvey(int surveyId);

        List<Question> GetSurveyQuestions(int surveyId);
        QuestionAndAnswers AddQuestionWithAnswers(QuestionAndAnswers qAndA);
        OfferedAnswerResult AddOfferedAnswersForQuestion(OfferedAnswerResult offeredAnswer);
    }
}
