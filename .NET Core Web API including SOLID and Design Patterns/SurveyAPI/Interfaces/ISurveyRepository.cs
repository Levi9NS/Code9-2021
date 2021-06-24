using System.Collections.Generic;
using SurveyAPI.Models;

namespace SurveyAPI.Interfaces
{
    public interface ISurveyRepository
    {
        Survey GetSurvey(int surveyId);
        Survey AddSurvey(Survey survey);
        SurveyResult GetSurveyResult(int surveyId);
        SurveyAdd AddSurveyResult(SurveyAdd answer);
        OfferedAnswerResult GetOfferedAnswersForSurvey(int surveyId);
        void DeleteSurvey(int surveyId);
        Participant AddSurveyParticipant(Participant participant);
        Question AddSurveyQuestion(Question question);
        OfferedAnswer AddOfferedAnswer(OfferedAnswerBasic offeredAnswer);
        List<Survey> GetAllSurveys();
        List<Question> GetAllQuestions();

    }
}