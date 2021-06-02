using SurveyAPI.Dtos;
using SurveyAPI.Models;

namespace SurveyAPI.Interfaces
{
    public interface ISurveyService
    {
        SurveyResult GetSurveyResults(int surveyId);

        Survey AddSurvey(Survey survey);

        void DeleteSurvey(int surveyId);

        Survey GetSurveyQuestions(int surveyId);

        SurveyResultDto AddSurveyAnswer(SurveyResultDto survey);

        OfferedAnswerResult GetOfferedAnswersForSurvey(int surveyId);
    }
}
