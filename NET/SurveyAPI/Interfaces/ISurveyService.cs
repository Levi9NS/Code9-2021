using System.Collections.Generic;
using SurveyAPI.Models;

namespace SurveyAPI.Interfaces
{
    public interface ISurveyService
    {
        SurveyResult GetSurveyResults(int surveyId);
        QuestionOfferedAnswerRelation OfferedAnswerRelation(QuestionOfferedAnswerRelation offeredAnswerRelation);
        List<OfferedAnswer> GetOfferedAnswers();
        Survey AddSurvey(Survey survey);

        void DeleteSurvey(int surveyId);

        Survey GetSurveyQuestions(int surveyId);

        Answer AddSurveyAnswer(Answer answer);

        OfferedAnswerResult GetOfferedAnswersForSurvey(int surveyId);

        Participant AddSurveyParticipant(Participant participant);

        Question AddSurveyQuestion(Question question);

        OfferedAnswer AddOfferedAnswer(OfferedAnswer offeredAnswer);
    }
}