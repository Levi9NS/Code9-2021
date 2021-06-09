using SurveyAPI.Models;
using SurveyAPI.Models.ServiceModels;
using System.Threading.Tasks;

namespace SurveyAPI.Interfaces
{
    public interface ISurveyService
    {
        Task<GenericResponseModel<SurveyServiceModel>> GetAll();
        Task<GenericResponseModel<SurveyResultServiceModel>> GetSurveyResults(int surveyId);

        Task<GenericResponseModel<SurveyServiceModel>> CreateSurvey(SurveyServiceModel survey);

        Task<GenericResponseModel<SubmitSurveyServiceModel>> SubmitSurvey(SubmitSurveyServiceModel submit);

        Task<GenericResponseModel<SurveyServiceModel>> DeleteSurvey(int surveyId);

        Task<GenericResponseModel<SurveyServiceModel>> GetSurveyQuestions(int surveyId);

        Task<GenericResponseModel<OfferedAnswerResultServiceModel>> GetOfferedAnswersForSurvey(int surveyId);
    }
}
