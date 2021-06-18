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

        public QuestionWithAnswers AddQuestion(QuestionWithAnswers question)
        {
            return _repository.AddQuestion(question);
        }

        public List<QuestionModel> GetAllQuestions()
        {
            return _repository.GetAllQuestions();
        }

        public List<QuestionModel> GetSurveyQuestions(int surveyId)
        {
            return _repository.GetSurveyQuestions(surveyId);
        }

        public Answer AddSurveyAnswer(Answer answer)
        {
            return _repository.AddSurveyAnswer(answer);
        }

        public OfferedAnswerResult GetOfferedAnswersForSurvey(int surveyId)
        {
            return _repository.GetOfferedAnswersForSurvey(surveyId);
        }

        public void RemoveSurveyQuestion(int surveyId, int questionId)
        {
            _repository.RemoveSurveyQuestion(surveyId, questionId);
        }

        public OfferedAnswerModel AddOfferedAnswer(OfferedAnswerModel answerModel)
        {
            return _repository.AddOfferedAnswer(answerModel);
        }

        public void DeleteQuestion(int questionId)
        {
            _repository.DeleteQuestion(questionId);
        }

        public ShortSurveyModel GetShortSurveyModel(int surveyId)
        {
            return _repository.GetShortSurveyModel(surveyId);
        }

        public List<ShortSurveyModel> GetShortSurveyModels()
        {
            return _repository.GetShortSurveyModels();
        }

        public QuestionWithSurveyId AddQuestionToSurvey(QuestionWithSurveyId question)
        {
            return _repository.AddQuestionToSurvey(question);
        }

        public List<OfferedAnswerModel> GetAllOfferedAnswers()
        {
            return _repository.GetAllOfferedAnswers();
        }

        public SurveyLink LinkQuestion(SurveyLink link)
        {
            return _repository.LinkQuestion(link);
        }
    }
}
