using SurveyAPI.Models;

namespace SurveyAPI.Interfaces
{
    public interface ISurveyRepository
    {
        Survey GetSurvey(int surveyId);
        Survey AddSurvey(Survey survey);
        SurveyResult GetSurveyResult(int surveyId);
        Answer AddSurveyResult(Answer answer);
        OfferedAnswerResult GetOfferedAnswersForSurvey(int surveyId);
        void DeleteSurvey(int surveyId);
        Participant AddSurveyParticipant(Participant participant);
        Question AddSurveyQuestion(Question question);
        OfferedAnswer AddOfferedAnswer(OfferedAnswer offeredAnswer);
    }
}