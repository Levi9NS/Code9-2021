using SurveyAPI.Models;
using System.Collections.Generic;

namespace SurveyAPI.Interfaces
{
    public interface ISurveyRepository
    {
        Survey GetSurvey(int surveyId);
        Survey AddSurvey(Survey survey); 
        SurveyResult GetSurveyResult(int surveyId);
        SurveyResultAdd AddSurveyResult(SurveyResultAdd survey);
        OfferedAnswerResult GetOfferedAnswersForSurvey(int surveyId);
        void DeleteSurvey(int surveyId);

        Participant AddParticipant(Participant participant);


        Answer AddAnswer(Answer answer);

        Question AddQuestion(Question question);
        Survey AddGeneralInformations(Survey survey);

        OfferedAnswer AddOfferedAnswer(OfferedAnswer offeredAnswer);
        List<Survey> getSurveys();

        Participant GetParticipant(int id);
        List<OfferedAnswer> GetOfferedAnswers();

        List<Question> GetQuestions();
    }
}
