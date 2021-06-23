using SurveyAPI.Interfaces;
using SurveyAPI.Models;
using System;
using System.Collections.Generic;
using static SurveyAPI.Models.OfferedAnswerResult;

namespace SurveyAPI.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly ISurveyRepository _repository;
        public SurveyService(ISurveyRepository repository)
        {
            _repository = repository;
        }        

        public SurveyResult GetSurveyResults(int surveyId)
        {
            return _repository.GetSurveyResult(surveyId);
                        
        }

        public void DeleteSurvey(int surveyId)
        {
            _repository.GetSurveyResult(surveyId);
        }

        public Survey AddSurvey(Survey survey)
        {
            return _repository.AddSurvey(survey);
        }

        public SurveyResult AddSurveyAnswer(SurveyResult survey)
        {
            return _repository.AddSurveyResult(survey);
        }

        public OfferedAnswerResult GetOfferedAnswersForSurvey(int surveyId)
        {
            return _repository.GetOfferedAnswersForSurvey(surveyId);
        }

        List<Question> ISurveyService.GetSurveyQuestions(int surveyId)
        {
            return _repository.GetSurveyQuestions(surveyId);
        }

        public QuestionAndAnswers AddQuestionWithAnswers(QuestionAndAnswers qAndA)
        {
            return _repository.AddQuestionWithAnswers(qAndA);
        }

        public OfferedAnswerResult AddOfferedAnswersForQuestion(OfferedAnswerResult offeredAnswer)
        {
            return _repository.AddOfferedAnswersForQuestion(offeredAnswer);
        }
    }
}
