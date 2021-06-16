using SurveyAPI.Models;
using System.Collections.Generic;
namespace SurveyAPI.Interfaces
{
    public interface ISurveyRepository
    {
        QuestionOfferedAnswerRelation OfferedAnswerRelation(QuestionOfferedAnswerRelation offeredAnswerRelation);
        List<OfferedAnswer> GetOfferedAnswers();
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