using Microsoft.Extensions.Configuration;
using SurveyAPI.Interfaces;
using SurveyAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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
                               "   GO " +
                               "   DELETE FROM [Survey].GeneralInformations" +
                               "         WHERE Id  = '{0}' " +
                               "   GO"
                               , surveyId);

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);
            _sqlcommand.ExecuteNonQuery();

        }

        public Survey GetSurvey(int surveyId)
        {
            string _statement = string.Format("SELECT ge.id,  ge.Description as Name, ge.StartDate, ge.EndDate,q.QuestionText " +
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
            string _statement = string.Format("SELECT ge.id, ge.Description,q.QuestionText, oa.text, COUNT(*) AS count " +
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
                                " GROUP BY ge.id,ge.Description,q.QuestionText,oa.Text", surveyId);

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
                        count = _reader.GetInt32(4)
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
            string command = string.Empty;

            command += "DECLARE @QuestionsIdSTable Table (ID int);";
            command += "DECLARE @SurveyId Int;";

            foreach (var q in survey.Questions)
            {
                command += string.Format(@"INSERT INTO [Survey].[Questions]
                           ([QuestionText]
                           ,[CreatedBy]
                           ,[CreateDate])
                        OUTPUT inserted.Id INTO @QuestionsIdSTable
                        VALUES
                           ('{0}'
                           ,'{1}'
                           ,'{2}'
                           ,'{3}')
                            GO;", q.QuestionText, "user", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") );
                
            }

            command += string.Format(@"INSERT INTO [Survey].[GeneralInformations]
                                       ([Description]
                                       ,[StartDate]
                                       ,[EndDate]
                                       ,[CreatedBy]
                                       ,[CreateDate])
                                 VALUES
                                       ('{0}'
                                       ,'{1}'
                                       ,'{2}'
                                       ,'{3}'
                                       ,'{4}')
                                GO;
                                SET @SurveyId = SCOPE_IDENTITY();", survey.Name, survey.StartDate.ToString("yyyy-MM-dd HH:mm:ss.fff"), survey.EndDate.ToString("yyyy-MM-dd HH:mm:ss.fff"), "user", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));


            foreach (var q in survey.Questions)
            {
                command += string.Format(@"INSERT INTO [Survey].[SurveyQuestionRelations]
                            ([SurveyId]
                            ,[QuestionId]
                            ,[CreatedBy]
                            ,[CreateDate])
                        
                        SELECT @SurveyId, Id, {0} ,{1}
                               FROM @QuestionsIdSTable);
                GO;", "user", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            }

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(command, _connection);
            _sqlcommand.ExecuteNonQuery();
            return survey;
        }

        public SurveyResult AddSurveyResult(SurveyResult survey)
        {
            throw new NotImplementedException();
        }
    }
}
