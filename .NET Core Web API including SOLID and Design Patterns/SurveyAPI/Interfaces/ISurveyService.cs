using SurveyAPI.Models;
using System.Collections.Generic;

namespace SurveyAPI.Interfaces
{
    public interface ISurveyService
    {
        Survey AddSurvey(Survey survey);
        void DeleteSurvey(int surveyId);
        SurveyResult GetSurveyResults(int surveyId);
        Survey GetSurvey(int surveyId);
        SurveyResult AddSurveyAnswer(SurveyResult survey);
        OfferedAnswerResult GetOfferedAnswersForSurvey(int surveyId);
        Question AddQuestion(Question question);
        Answer AddAnswer(Answer answer);
        List<QuestionModel> GetSurveyQuestions(int surveyId);
        void DeleteQuestion(int surveyId, int questionId);
        void LinkOfferedAnswerToquestion(int questionId, int answerId);
        OfferedAnswerModel AddOfferedAnswer(OfferedAnswerModel answerModel);
    }
}
