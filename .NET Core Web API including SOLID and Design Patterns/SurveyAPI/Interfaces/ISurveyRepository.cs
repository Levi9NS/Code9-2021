using SurveyAPI.Models;
using System.Collections.Generic;

namespace SurveyAPI.Interfaces
{
    public interface ISurveyRepository
    {
        Survey GetSurvey(int surveyId);
        Survey AddSurvey(Survey survey); 
        SurveyResult GetSurveyResult(int surveyId);
        SurveyResult AddSurveyResult(SurveyResult survey);
        OfferedAnswerResult GetOfferedAnswersForSurvey(int surveyId);
        void DeleteSurvey(int surveyId);
        Question AddQuestion(Question question);
        Answer AddAnswer(Answer answer);
        List<QuestionModel> GetSurveyQuestions(int surveyId);
        void DeleteQuestion(int surveyId, int questionId);
        void LinkOfferedAnswerToquestion(int questionId, int answerId);
        OfferedAnswerModel AddOfferedAnswer(OfferedAnswerModel answerModel);
    }
}
