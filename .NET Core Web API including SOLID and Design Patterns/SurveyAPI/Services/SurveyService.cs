using SurveyAPI.Interfaces;
using SurveyAPI.Models;
using System;

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

        public Survey GetSurveyQuestions(int surveyId)
        {
            return _repository.GetSurvey(surveyId);
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
    }
}
