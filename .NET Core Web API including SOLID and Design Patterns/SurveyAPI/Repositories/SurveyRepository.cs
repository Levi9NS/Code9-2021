using Microsoft.Extensions.Configuration;
using SurveyAPI.Interfaces;
using SurveyAPI.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using static SurveyAPI.Models.OfferedAnswerResult;

namespace SurveyAPI.Repositories
{

    public class SurveyRepository : ISurveyRepository
    {

        private readonly string _connectionString;

        public SurveyRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SQLConnection");
        }

        private SqlConnection GetConnection(string _connection_string)
        {
            return new SqlConnection(_connection_string);
        }

        public void DeleteSurvey(int surveyId)
        {
            string _statement = string.Format("DELETE FROM [Survey].[SurveyQuestionRelations] " +
                               "   WHERE SurveyId =  '{0}'" +
                               "   DELETE FROM [Survey].GeneralInformations" +
                               "         WHERE Id  = '{0}' "
                               , surveyId);

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);
            _sqlcommand.ExecuteNonQuery();
        }

        public Survey GetSurvey(int surveyId)
        {
            string _statement = string.Format("SELECT ge.id,  ge.Description as Name, ge.StartDate, ge.EndDate,q.QuestionText, q.id " +
                                "FROM" +
                                "   Survey.GeneralInformations ge" +
                                "   INNER JOIN Survey.SurveyQuestionRelations sqr" +
                                "       ON ge.id = sqr.SurveyId" +
                                "   INNER JOIN Survey.Questions q" +
                                "       ON sqr.QuestionId = q.id" +
                                "   WHERE ge.Id = '{0}'"
                                , surveyId);

            Survey survey = new Survey();
            List<Question> _lquestions = new List<Question>();

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);

            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    survey.Id = _reader.GetInt32(0);

                    survey.Name = _reader.GetString(1);
                    survey.StartDate = _reader.GetDateTime(2);
                    survey.EndDate = _reader.GetDateTime(3);
                    Question _question = new Question()
                    {
                        QuestionText = _reader.GetString(4),
                        Id = _reader.GetInt32(5),
                    };
                    _lquestions.Add(_question);

                }

            }

            survey.Questions = _lquestions;

            _connection.Close();

            return survey;
        }

        public SurveyResult GetSurveyResult(int surveyId)
        {
            SurveyResult sr = new SurveyResult();

            List<AnsweredQuestion> _lquestions = new List<AnsweredQuestion>();
            string _statement = string.Format("SELECT ge.id, ge.Description,q.QuestionText, oa.text, q.id, a.id, COUNT(*) AS count " +
                                "FROM" +
                                "   Survey.GeneralInformations ge" +
                                "   INNER JOIN Survey.SurveyQuestionRelations sqr" +
                                "       ON ge.id = sqr.SurveyId" +
                                "   INNER JOIN Survey.Questions q" +
                                "       ON sqr.QuestionId = q.id" +
                                "   INNER JOIN Survey.QuestionOfferedAnswerRelations qoar" +
                                "       ON q.id = qoar.QuestionId" +
                                "   INNER JOIN Survey.OfferedAnswers oa" +
                                "       ON qoar.OfferedAnswerId = oa.id" +
                                "   INNER JOIN Survey.Participants pa" +
                                "       ON pa.SurveyId = pa.SurveyId" +
                                "   INNER JOIN Survey.Answers a" +
                                "       ON pa.id = a.ParticipantId AND qoar.QuestionId = a.QuestionId AND qoar.OfferedAnswerId = a.QuestionAnswersId" +
                                "   WHERE ge.Id = '{0}'" +
                                " GROUP BY ge.id,ge.Description,oa.id,q.id,a.id,q.QuestionText,oa.Text ORDER BY ge.id,q.id,oa.id", surveyId);

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);

            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    sr.Name = _reader.GetString(1);
                    AnsweredQuestion _question = new AnsweredQuestion()
                    {
                        text = _reader.GetString(2),
                        response = _reader.GetString(3),
                        Id = _reader.GetInt32(4),
                        count = _reader.GetInt32(5),                      
                    };

                    _lquestions.Add(_question);
                }
            }
            _connection.Close();

            sr.Questions = _lquestions;

            return sr;
        }

        public Survey AddSurvey(Survey survey)
        {
            List<Question> questions = new List<Question>();
            string command = string.Empty;

            command += "DECLARE @QuestionsIdSTable Table (ID int);";
            command += "DECLARE @SurveyId Int;";

            foreach (var q in survey.Questions)
            {
                command += string.Format(@"INSERT INTO [Survey].[Questions]
                           ([QuestionText]
                           ,[ChangedBy]
                           ,[ChangedDate]
                           ,[CreatedBy]
                           ,[CreateDate])
                        OUTPUT inserted.Id INTO @QuestionsIdSTable
                        VALUES
                           ('{0}'
                           ,'{1}'
                           ,'{2}'
                           ,'{3}'
                           ,'{4}');", q.QuestionText, 
                           "Ognjen", 
                           DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), 
                           "Ognjen", 
                           DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));               
            }

            command += string.Format(@"INSERT INTO [Survey].[GeneralInformations]
                                       ([Description]
                                       ,[StartDate]
                                       ,[EndDate]
                                       ,[ChangedBy]
                                       ,[ChangedDate]
                                       ,[CreatedBy]
                                       ,[CreateDate])
                                 VALUES
                                       ('{0}'
                                       ,'{1}'
                                       ,'{2}'
                                       ,'{3}'
                                       ,'{4}'
                                       ,'{5}'
                                       ,'{6}');
                                SET @SurveyId = SCOPE_IDENTITY();",
                                survey.Name, 
                                survey.StartDate.ToString("yyyy-MM-dd HH:mm:ss.fff"), 
                                survey.EndDate.ToString("yyyy-MM-dd HH:mm:ss.fff"), 
                                "Ognjen", 
                                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                                "Ognjen",
                                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));


            foreach (var q in survey.Questions)
            {
                command += string.Format(@"INSERT INTO [Survey].[SurveyQuestionRelations]
                            ([SurveyId]
                            ,[QuestionId]
                            ,[ChangedBy]
                            ,[ChangedDate]
                            ,[CreatedBy]
                            ,[CreateDate])    
                         VALUES
                            (@SurveyId,
                             '{0}',
                             '{1}',
                             '{2}',
                             '{3}',
                             '{4}');", q.Id, "Ognjen", 
                             DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), 
                             "Ognjen", 
                             DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            }

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(command, _connection);
            _sqlcommand.ExecuteNonQuery();
            return survey;
        }

        public Question AddQuestion(Question question)
        {
            string command = String.Empty;

            command += String.Format(@"INSERT INTO [Survey].[Questions]
                                       ([QuestionText]
                                       ,[CreatedBy]
                                       ,[CreateDate])
                                 VALUES
                                       ('{0}'
                                       ,'{1}'
                                       ,'{2}');
                                SET @QuestionId = SCOPE_IDENTITY();", 
                                question.QuestionText, 
                                "Ognjen", 
                                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(command, _connection);
            _sqlcommand.ExecuteNonQuery();
            return question;
        }

        public List<QuestionModel> GetSurveyQuestions(int surveyId)
        {
            List<QuestionModel> _questions = new List<QuestionModel>();

            string command = String.Empty;

            command += String.Format(@"Select q.Id, q.QuestionText, q.CreatedBy, q.CreateDate from [Survey].SurveyQuestionRelations sq inner join [Survey].Questions q on sq.QuestionId = q.Id where SurveyId = '{0}'", surveyId);

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(command, _connection);

            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    QuestionModel _question = new QuestionModel
                    {
                        Id = _reader.GetInt32(0),
                        QuestionText = _reader.GetString(1),
                        CreatedBy = _reader.GetString(2),
                        CreateDate = _reader.GetDateTime(3)
                    };
                    _questions.Add(_question);
                }
            }

            _connection.Close();

            return _questions;
        }

        public Answer AddAnswer(Answer answer)
        {
            string command = String.Empty;

            command += "DECLARE @ParticipantId Int;";

            command += String.Format(@"INSERT INTO [Survey].[Participants]
                                       ([SurveyId]
                                       ,[FirstName]
                                       ,[LastName]
                                       ,[Email]
                                       ,[Password]
                                       ,[ChangedBy]
                                       ,[ChangedDate]
                                       ,[CreatedBy]
                                       ,[CreateDate])
                                 VALUES
                                       ('{0}'
                                       ,'{1}'
                                       ,'{2}'
                                       ,'{3}'
                                       ,'{4}'
                                       ,'{5}'
                                       ,'{6}'
                                       ,'{7}'
                                       ,'{8}');
                                SET @ParticipantId = SCOPE_IDENTITY();",
                                answer.SurveyId,
                                answer.Participant.FirstName,
                                answer.Participant.LastName,
                                answer.Participant.Email,
                                answer.Participant.Password,
                                "Ognjen",
                                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                                "Ognjen",
                                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

            foreach (var f in answer.QuestionAnswers)
            {
                command += String.Format(@"insert into [Survey].[Answers]
                                        ([ParticipantId],
                                        [SurveyId],
                                        [QuestionId],
                                        [QuestionAnswersId],
                                        [ChangedBy]
                                       ,[ChangedDate],
                                       ,[CreatedBy]
                                       ,[CreateDate])
                                    VALUES
                                        (@ParticipantId,
                                        '{0}',
                                        '{1}',
                                        '{2}',
                                        '{3}',
                                        '{4}',
                                        '{5}',
                                        '{6}');
                                    SET @AnswersId = SCOPE_IDENTITY();",
                                    answer.SurveyId,
                                    f.QuestionId,
                                    f.AnswerId,
                                    "Ognjen",
                                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                                    "Ognjen",
                                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            }

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(command, _connection);
            _sqlcommand.ExecuteNonQuery();
            return answer;
        }

        public SurveyResult AddSurveyResult(SurveyResult survey)
        {
            throw new NotImplementedException();
        }

        public OfferedAnswerResult GetOfferedAnswersForSurvey(int surveyId)
        {
            string _statement = string.Format(@"SELECT DISTINCT q.id, oa.Text FROM
                                   Survey.GeneralInformations ge
                                   INNER JOIN Survey.SurveyQuestionRelations sqr
                                       ON ge.id = sqr.SurveyId
                                   INNER JOIN Survey.Questions q
                                       ON sqr.QuestionId = q.id
                                   INNER JOIN Survey.QuestionOfferedAnswerRelations qoar
                                       ON q.id = qoar.QuestionId
                                   INNER JOIN Survey.OfferedAnswers oa
                                       ON qoar.OfferedAnswerId = oa.id
                                   WHERE ge.Id = '{0}'", surveyId);
            
            OfferedAnswerResult result = new OfferedAnswerResult();
            List<OfferedAnswer> _offeredAnswers = new List<OfferedAnswer>();

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);

            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {                    
                    OfferedAnswer _answer = new OfferedAnswer
                    {
                        QuestionId = _reader.GetInt32(0),
                        QuestionAnswer = _reader.GetString(1),
                    };
                    _offeredAnswers.Add(_answer);
                }
            }

            result.OfferedAnswers = _offeredAnswers;
            _connection.Close();

            return result;
        }

        public void DeleteQuestion(int surveyId, int questionId)
        {
            string _statement = string.Format("DELETE FROM [Survey].[QuestionOfferedAnswerRelations] WHERE QuestionId =  '{0}'" +
                   "DELETE FROM [Survey].[SurveyQuestionRelations] WHERE SurveyId  = '{1}' AND QuestionId = '{0}'" +
                   "DELETE FROM [Survey].Questions where Id = '{0}'"
                   , questionId, surveyId);

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);
            _sqlcommand.ExecuteNonQuery();
        }

        public void LinkOfferedAnswerToquestion(int questionId, int answerId)
        {
            string command = String.Empty;

            command += String.Format(@"insert into [Survey].[QuestionOfferedAnswerRelations]
                                        ([QuestionId],
                                        [OfferedAnswerId],
                                        [ChangedBy],
                                        [ChangedDate],
                                        [CreatedBy],
                                        [CreateDate])
                                        VALUES
                                        ('{0}',
                                         '{1}',
                                         '{2}',
                                         '{3}',
                                         '{4}',
                                         '{5}');", questionId, answerId, "Ognjen", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                                         "Ognjen", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(command, _connection);
            _sqlcommand.ExecuteNonQuery();
        }

        public OfferedAnswerModel AddOfferedAnswer(OfferedAnswerModel answerModel)
        {
            string command = String.Empty;

            command += String.Format(@"INSERT INTO [Survey].[OfferedAnswers]
                                       ([Text]
                                       ,[ChangedBy]
                                       ,[ChangedDate]
                                       ,[CreatedBy]
                                       ,[CreateDate])
                                 VALUES
                                       ('{0}'
                                       ,'{1}'
                                       ,'{2}'
                                       ,'{3}'
                                       ,'{4}');
                                SET @QuestionId = SCOPE_IDENTITY();",
                                answerModel.Text,
                                "Ognjen",
                                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                                "Ognjen",
                                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(command, _connection);
            _sqlcommand.ExecuteNonQuery();
            return answerModel;
        }
    }
}
