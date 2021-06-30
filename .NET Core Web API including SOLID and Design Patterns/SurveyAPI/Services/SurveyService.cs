using SurveyAPI.DTO;
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
        public Survey GetSurveyQuestions(int surveyId)
        {
            return _repository.GetSurvey(surveyId);
        }
        public SurveyResult AddSurveyAnswer(SurveyResult survey)
        {
            return _repository.AddSurveyResult(survey);
        }
        public OfferedAnswerResult GetOfferedAnswersForSurvey(int surveyId)
        {
            return _repository.GetOfferedAnswersForSurvey(surveyId);
        }


     

        /**************************************** my methods **********************************************/

        public List<Question> GetQuestions()
        {
            return _repository.GetQuestions();
        }
        public Question AddQuestion(Question question)
        {
            return _repository.AddQuestion(question);
        }


        public List<Survey> GetAllSurveyGeneralnformation()
        {
            return _repository.GetAllSurveyGeneralnformation();
        }

        public Survey AddSurveyGeneralnformation(Survey s)
        {
            return _repository.AddSurveyGeneralInformation(s);
        }
        public List<OfferedAnswer> GetOfferedAnswers()
        {
            return _repository.GetOfferedAnswers();
        }
        public SurveyQuestionRelation AddQuestionToSurvey(SurveyQuestionRelation surveyQuestionRelation)
        {
            return _repository.AddQuestionToSurvey(surveyQuestionRelation);
        }
        public Participant AddParticipantAnswers(ParticipantsAnswerDTO participantsAnswerDTO)
        {
            return _repository.AddParticipantAnswers(participantsAnswerDTO);

        }
        public List<SurveyResultDTO> GetSurveyAnswers(int surveyId)
        {
            return  _repository.GetSurveyAnswers(surveyId);
        }

        public string closeSurvey(HelperDTO s)
        {
            return _repository.closeSurvey(s);
        }
        public string addOfferedAnswer(OfferedAnswer oa) 
        { 
            return _repository.addOfferedAnswer(oa);
        }

}
}
