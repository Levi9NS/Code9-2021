using SurveyAPI.Models;
using System.Collections.Generic;
using static SurveyAPI.Models.OfferedAnswerResult;

namespace SurveyAPI.Interfaces
{
    public interface ISurveyService
    {
        SurveyResult GetSurveyResults(int surveyId);

        Survey AddSurvey(Survey survey);

        void DeleteSurvey(int surveyId);

        List<Question> GetSurveyQuestions(int surveyId);

        SurveyResult AddSurveyAnswer(SurveyResult survey);

        OfferedAnswerResult GetOfferedAnswersForSurvey(int surveyId);

        QuestionAndAnswers AddQuestionWithAnswers(QuestionAndAnswers qAndA);

        OfferedAnswerResult AddOfferedAnswersForQuestion(OfferedAnswerResult offeredAnswer);
    }
}
