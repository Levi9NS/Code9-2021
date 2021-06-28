using SurveyAPI.Models;
using System.Collections.Generic;
using static SurveyAPI.Models.OfferedAnswerResult;

namespace SurveyAPI.Interfaces
{
    public interface ISurveyRepository
    {
        Survey GetSurvey(int surveyId);
        Survey AddSurvey(Survey survey); 
        SurveyResult GetSurveyResult(int surveyId);
        Answers AddSurveyResult(Answers survey);
        OfferedAnswerResult GetOfferedAnswersForSurvey(int surveyId);
        void DeleteSurvey(int surveyId);
        GeneralInformations AddGeneralInformations(GeneralInformations survey);
        GeneralInformations GetGeneralInformations(int surveyId);
        List<Question> GetSurveyQuestions(int surveyId);
        List<GeneralInformations> GetAllGeneralInformations();
        List<OfferedAnswers> GetOfferedAnswers();
        QuestionAndAnswers AddQuestionWithAnswers(QuestionAndAnswers qAndA);
        OfferedAnswerResult AddOfferedAnswersForQuestion(OfferedAnswerResult offeredAnswer);
        Participant AddParticipant(Participant participant);
    }
}
