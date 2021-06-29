﻿using SurveyAPI.Models;
using System.Collections.Generic;
using static SurveyAPI.Models.OfferedAnswerResult;

namespace SurveyAPI.Interfaces
{
    public interface ISurveyService
    {
        SurveyResult GetSurveyResults(int surveyId);
        Answers AddSurveyResult(Answers survey);

        SurveyAddModel AddSurvey(SurveyAddModel survey);
        Survey GetSurvey(int surveyId);

        GeneralInformations AddGeneralInformations(GeneralInformations survey);
        GeneralInformations GetGeneralInformations(int surveyId);

        void DeleteSurvey(int surveyId);

        List<Question> GetSurveyQuestions(int surveyId);
        List<GeneralInformations> GetAllGeneralInformations();
        List<OfferedAnswers> GetOfferedAnswers();

        OfferedAnswerResult GetOfferedAnswersForSurvey(int surveyId);

        QuestionAndAnswers AddQuestionWithAnswers(QuestionAndAnswers qAndA);

        OfferedAnswerResult AddOfferedAnswersForQuestion(OfferedAnswerResult offeredAnswer);

        Participant AddParticipant(Participant participant);

        Participant GetLastParticipantAdded();
    }
}
