using SurveyAPI.Models;

namespace SurveyAPI.Interfaces
{
    public interface ISurveyRepository
    {
        Survey GetSurvey(int surveyId);
        Survey AddSurvey(Survey survey); 
        SurveyResult GetSurveyResult(int surveyId);
        SurveyResult AddSurveyResult(SurveyResult survey);

        void DeleteSurvey(int surveyId);
    }
}
