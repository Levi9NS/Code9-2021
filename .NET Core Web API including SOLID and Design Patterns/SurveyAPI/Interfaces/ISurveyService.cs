using SurveyAPI.Models;
using System.Collections.Generic;

namespace SurveyAPI.Interfaces
{
    public interface ISurveyService
    {
        SurveyResult GetSurveyResults(int surveyId);

        Survey AddSurvey(Survey survey);

        void DeleteSurvey(int surveyId);

        Survey GetSurveyQuestions(int surveyId);

        SurveyResultAdd AddSurveyAnswer(SurveyResultAdd survey);

        OfferedAnswerResult GetOfferedAnswersForSurvey(int surveyId);

        Participant AddParticipant(Participant participant);

        Question AddQuestion(Question question);
        Answer AddAnswer(Answer answer);

        Survey AddGeneralInformations(Survey survey);

         OfferedAnswer AddOfferedAnswer(OfferedAnswer offeredAnswer);
        List<Survey> getSurveys();
        Participant GetParticipant(int id);

        List<OfferedAnswer> GetOfferedAnswers();
        List<Question> GetQuestions();

    }
}
