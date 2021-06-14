using SurveyAPI.Interfaces;
using SurveyAPI.Models;
using System;
using System.Collections.Generic;

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
            _repository.DeleteSurvey(surveyId);
        }

        public Survey GetSurvey(int surveyId)
        {
            return _repository.GetSurvey(surveyId);
        }

        public Survey AddSurvey(Survey survey)
        {
            return _repository.AddSurvey(survey);
        }

        public Question AddQuestion(Question question)
        {
            return _repository.AddQuestion(question);
        }

        public List<QuestionModel> GetSurveyQuestions(int surveyId)
        {
            return _repository.GetSurveyQuestions(surveyId);
        }

        public Answer AddAnswer(Answer answer)
        {
            return _repository.AddAnswer(answer);
        }

        public SurveyResult AddSurveyAnswer(SurveyResult survey)
        {
            return _repository.AddSurveyResult(survey);
        }

        public OfferedAnswerResult GetOfferedAnswersForSurvey(int surveyId)
        {
            return _repository.GetOfferedAnswersForSurvey(surveyId);
        }

        public void DeleteQuestion(int surveyId, int questionId)
        {
            _repository.DeleteQuestion(surveyId, questionId);
        }

        public void LinkOfferedAnswerToquestion(int questionId, int answerId)
        {
            _repository.LinkOfferedAnswerToquestion(questionId, answerId);
        }

        public OfferedAnswerModel AddOfferedAnswer(OfferedAnswerModel answerModel)
        {
            return _repository.AddOfferedAnswer(answerModel);
        }
    }
}
