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

        public Survey GetSurveyQuestions(int surveyId)
        {
            return _repository.GetSurvey(surveyId);
        }
        public List<OfferedAnswer> GetOfferedAnswers()
        {
            return _repository.GetOfferedAnswers();
        }
        

        public Survey AddSurvey(Survey survey)
        {
            return _repository.AddSurvey(survey);
        }

        public QuestionOfferedAnswerRelation OfferedAnswerRelation(QuestionOfferedAnswerRelation offeredAnswerRelation)
        {
            return _repository.OfferedAnswerRelation(offeredAnswerRelation);
        }

        public Answer AddSurveyAnswer(Answer answer)
        {
            return _repository.AddSurveyResult(answer);
        }

        public OfferedAnswerResult GetOfferedAnswersForSurvey(int surveyId)
        {
            return _repository.GetOfferedAnswersForSurvey(surveyId);
        }

        public Participant AddSurveyParticipant(Participant participant)
        {
            return _repository.AddSurveyParticipant(participant);
        }

        public Question AddSurveyQuestion(Question question)
        {
            return _repository.AddSurveyQuestion(question);
        }

        public OfferedAnswer AddOfferedAnswer(OfferedAnswer offeredAnswer)
        {
            return _repository.AddOfferedAnswer(offeredAnswer);
        }
    }
}