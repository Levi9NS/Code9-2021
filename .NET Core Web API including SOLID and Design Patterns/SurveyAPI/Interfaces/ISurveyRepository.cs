using SurveyAPI.Models;
using System.Collections.Generic;

namespace SurveyAPI.Interfaces
{
    public interface ISurveyRepository
    {
        Survey GetSurvey(int surveyId);
        Survey AddSurvey(Survey survey); 
        SurveyResult GetSurveyResult(int surveyId);
        OfferedAnswerResult GetOfferedAnswersForSurvey(int surveyId);
        void DeleteSurvey(int surveyId);
        QuestionWithAnswers AddQuestion(QuestionWithAnswers question);
        Answer AddSurveyAnswer(Answer answer);
        List<QuestionModel> GetSurveyQuestions(int surveyId);
        void RemoveSurveyQuestion(int surveyId, int questionId);
        OfferedAnswerModel AddOfferedAnswer(OfferedAnswerModel answerModel);
        List<QuestionModel> GetAllQuestions();
        void DeleteQuestion(int questionId);
        ShortSurveyModel GetShortSurveyModel(int surveyId);
        List<ShortSurveyModel> GetShortSurveyModels();
        QuestionWithSurveyId AddQuestionToSurvey(QuestionWithSurveyId question);
        List<OfferedAnswerModel> GetAllOfferedAnswers();
        SurveyLink LinkQuestion(SurveyLink link);
    }
}
