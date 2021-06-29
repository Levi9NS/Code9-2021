using SurveyAPI.DTO;
using SurveyAPI.Models;
using System.Collections.Generic;
using static SurveyAPI.Models.OfferedAnswerResult;

namespace SurveyAPI.Interfaces
{
    public interface ISurveyRepository
    {
        Survey GetSurvey(int surveyId);
        SurveyResult GetSurveyResult(int surveyId);
        SurveyResult AddSurveyResult(SurveyResult survey);
        OfferedAnswerResult GetOfferedAnswersForSurvey(int surveyId);
        void DeleteSurvey(int surveyId);

        Participant AddParticipant(Participant participant);
        Answer AddAnswer(Answer answer);
        Survey AddGeneralInformations(Survey survey);
        OfferedAnswer AddOfferedAnswer(OfferedAnswer offeredAnswer);
        Participant GetParticipant(int id);

        /*************** my methods ********************/
        List<Question> GetQuestions();
        Question AddQuestion(Question question);
        List<Survey> GetAllSurveyGeneralnformation();
        Survey AddSurveyGeneralInformation(Survey s);
        List<OfferedAnswer> GetOfferedAnswers();
        SurveyQuestionRelation AddQuestionToSurvey(SurveyQuestionRelation surveyQuestionRelation);
        Participant AddParticipantAnswers(ParticipantsAnswerDTO participantsAnswerDTO);
        List<SurveyResultDTO> GetSurveyAnswers(int surveyId);
        string closeSurvey(HelperDTO surveyId);
        string addOfferedAnswer(OfferedAnswer text);
    }
}
