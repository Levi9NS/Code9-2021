using System.Collections.Generic;
using SurveyAPI.Models;

namespace SurveyAPI.Interfaces
{
    public interface ISurveyService
    {
        SurveyResult GetSurveyResults(int surveyId);

        Survey AddSurvey(Survey survey);

        void DeleteSurvey(int surveyId);

        Survey GetSurveyQuestions(int surveyId);

        SurveyAdd AddSurveyAnswer(SurveyAdd SurveyAdd);

        OfferedAnswerResult GetOfferedAnswersForSurvey(int surveyId);

        Participant AddSurveyParticipant(Participant participant);

        Question AddSurveyQuestion(Question question);

        OfferedAnswer AddOfferedAnswer(OfferedAnswerBasic offeredAnswer);

        List<Survey> GetAllSurveys();

        List<Question> GetAllQuestions();
    }
}