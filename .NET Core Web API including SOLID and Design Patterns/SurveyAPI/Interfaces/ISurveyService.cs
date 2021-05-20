using SurveyAPI.Models;

namespace SurveyAPI.Interfaces
{
    public interface ISurveyService
    {
        SurveyResult GetSurveyResults(int surveyId);

        Survey AddSurvey(Survey survey);

        void DeleteSurvey(int surveyId);

        Survey GetSurveyQuestions(int surveyId);

        SurveyResult AddSurveyAnswer(SurveyResult survey);

        OfferedAnswerResult GetOfferedAnswersForSurvey(int surveyId);

        OfferedAnswerResult GetAllOfferedAnswers();

    }
}
