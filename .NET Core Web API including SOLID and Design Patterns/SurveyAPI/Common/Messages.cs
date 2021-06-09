using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.Common
{
    public static class Messages
    {
        #region Survey
        public const string SURVEY_CREATION_ERROR = "Error occurred while creating new survey, please try again.";
        public const string SURVEY_ID_NULL = "Error occurred while getting survey by Id, please try again.";
        public const string SURVEY_DELETE_ERROR = "Error occurred while deleting survey general informations, please try again.";
        #endregion

        #region Answer
        public const string ANSWER_NOT_FOR_SAME_SURVEY = "Error occurred because not all answers are for the same survey, please try again.";
        public const string ANSWER_NOT_ALL_SUBMITED = "Error occurred because the number of answers does not match to the number of survey questions, please try again.";
        #endregion

        #region Question
        public const string QUESTION_CREATION_ERROR = "Error occurred while creating new question, please try again.";
        public const string QUESTION_ID_NULL = "Error occurred while getting queastion by Id, please try again.";
        public const string QUESTION_NOT_FOUND_FOR_SURVEY = "Error occurred while getting queastions by survey id, please try again.";
        public const string QUESTION_ANSWER_NO_RELATION = "Error occurred because there is no relationship between the given question and answer, please try again.";
        public const string QUESTION_REPEATED_ERROR = "Error occurred because question is repeated, please try again.";
        #endregion

        #region SurveyQuestionRelationships
        public const string SURVEY_QUESTION_RELATION_CREATION_ERROR = "Error occurred while creating new relationship between survey and question, please try again.";
        public const string SURVEY_QUESTION_RELATION_DELETE_ERROR = "Error occurred while deleting relationship between survey and question, please try again.";
        public const string SURVEY_QUESTION_RELATION_NO_RELATION = "Error occurred because there is no relationship between the given survey and question, please try again.";
        #endregion

        #region Participant
        public const string PARTICIPANT_CREATION_ERROR = "Error occurred while creating new partisipant, please try again.";
        #endregion
    }
}
