using SurveyAPI.DTO;
using SurveyAPI.Models;
using System.Collections.Generic;
using static SurveyAPI.Models.OfferedAnswerResult;

namespace SurveyAPI.Interfaces
{
    public interface ISurveyService
    {
        SurveyResult GetSurveyResults(int surveyId);
        void DeleteSurvey(int surveyId);
        Survey GetSurveyQuestions(int surveyId);
        OfferedAnswerResult GetOfferedAnswersForSurvey(int surveyId);

        /***************** my methods ***************************/
        List<Question> GetQuestions();
        Question AddQuestion(Question question);
        List<Survey> GetAllSurveyGeneralnformation();
        Survey AddSurveyGeneralnformation(Survey s);
        List<OfferedAnswer> GetOfferedAnswers();
        SurveyQuestionRelation AddQuestionToSurvey(SurveyQuestionRelation surveyQuestionRelation);
        Participant AddParticipantAnswers(ParticipantsAnswerDTO participantsAnswerDTO);
        List<SurveyResultDTO> GetSurveyAnswers(int surveyId);
        string closeSurvey(HelperDTO surveyId);
        string addOfferedAnswer(OfferedAnswer text);

    }
}
