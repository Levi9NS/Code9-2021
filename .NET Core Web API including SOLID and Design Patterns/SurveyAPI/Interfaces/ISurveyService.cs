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
        OfferedAnswerResult GetOfferedAnswersForSurvey(int surveyId);
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
