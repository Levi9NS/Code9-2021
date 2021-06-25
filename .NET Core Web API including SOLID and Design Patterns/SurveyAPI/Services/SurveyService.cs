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

        public SurveyResultAdd AddSurveyAnswer(SurveyResultAdd survey)
        {
            return _repository.AddSurveyResult(survey);
        }

        public OfferedAnswerResult GetOfferedAnswersForSurvey(int surveyId)
        {
            return _repository.GetOfferedAnswersForSurvey(surveyId);
        }

        public Participant AddParticipant(Participant participant)
        {
            return _repository.AddParticipant(participant);
        }

        public Answer AddAnswer(Answer answer)
        {
            return _repository.AddAnswer(answer);
        }

        public Question AddQuestion(Question question)
        {
            return _repository.AddQuestion(question);
        }

        public Survey AddGeneralInformations(Survey survey)
        {
            return _repository.AddGeneralInformations(survey);
        }

        public OfferedAnswer AddOfferedAnswer(OfferedAnswer offeredAnswer)
        {
            return _repository.AddOfferedAnswer(offeredAnswer);
        }

        public List<Survey> getSurveys()
        {
            return _repository.getSurveys();
        }

        public Participant GetParticipant(int id)
        {
            return _repository.GetParticipant(id);
        }

        public List<OfferedAnswer> GetOfferedAnswers()
        {
            return _repository.GetOfferedAnswers();
        }

        public List<Question> GetQuestions()
        {
            return _repository.GetQuestions();
        }
    }
}
