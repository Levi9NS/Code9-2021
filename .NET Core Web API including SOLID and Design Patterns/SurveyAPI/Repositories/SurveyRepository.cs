﻿using Microsoft.Extensions.Configuration;
using SurveyAPI.Interfaces;
using SurveyAPI.Models;
using System;
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
                               "   GO " +
                               "   DELETE FROM [Survey].GeneralInformations" +
                               "         WHERE Id  = '{0}' " 
                               , surveyId);

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);
            _sqlcommand.ExecuteNonQuery();
            _connection.Close();
        }

        public Survey GetSurvey(int surveyId)
        {
            string _statement = string.Format("SELECT DISTINCT ge.id,  ge.Description as Name, ge.StartDate, ge.EndDate,q.QuestionText, q.id " +
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
            string _statement = string.Format(@"SELECT ge.Description, q.QuestionText as Question, oa.text as Answer, COUNT(*) AS count
                                FROM Survey.GeneralInformations ge
                                   INNER JOIN Survey.SurveyQuestionRelations sqr
                                       ON ge.id = sqr.SurveyId
                                   INNER JOIN Survey.Questions q
                                       ON sqr.QuestionId = q.id
                                   INNER JOIN Survey.QuestionOfferedAnswerRelations qoar
                                       ON q.id = qoar.QuestionId
                                   INNER JOIN Survey.OfferedAnswers oa
                                       ON qoar.OfferedAnswerId = oa.id
                                   INNER JOIN Survey.Participants pa
                                       ON pa.SurveyId = pa.SurveyId
                                   INNER JOIN Survey.Answers a
                                       ON pa.id = a.ParticipantId AND qoar.QuestionId = a.QuestionId AND qoar.OfferedAnswerId = a.QuestionAnswersId
                                   WHERE ge.Id = '{0}'
								   GROUP BY ge.Description, q.QuestionText,oa.Text ORDER BY q.QuestionText", surveyId);

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);

            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    sr.Name = _reader.GetString(0);
                    AnsweredQuestion _question = new AnsweredQuestion()
                    {
                        question = _reader.GetString(1),
                        answer = _reader.GetString(2),
                        count = _reader.GetInt32(3)  
                    };
                    _lquestions.Add(_question);
                }
            }
            _connection.Close();

            sr.Questions = _lquestions;

            return sr;
        }

        public SurveyAddModel AddSurvey(SurveyAddModel survey)
        {
            string command = string.Empty;

            command += "DECLARE @QuestionsIdSTable Table (ID int);";
            command += "DECLARE @SurveyId Int;";
            command += "DECLARE @AnswersIdSTable Table (ID int);";

            command += string.Format(@"INSERT INTO [Survey].[GeneralInformations]
                                       ([Description]
                                       ,[StartDate]
                                       ,[EndDate]
                                       ,[ChangedBy]
                                       ,[ChangedDate]
                                       ,[CreatedBy]
                                       ,[CreateDate])
                                 VALUES
                                       ( '{0}'
                                       , '{1}'
                                       , '{2}'
                                       , '{3}'
                                       , '{4}'
                                       , '{5}'
                                       , '{6}');
                                SET @SurveyId = SCOPE_IDENTITY();
                              ", survey.Name, survey.StartDate.ToString("yyyy-MM-dd"), survey.EndDate.ToString("yyyy-MM-dd"), "DejanSkiljic", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                              "DejanSkiljic", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));


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
                           ( '{0}'
                           , '{1}'
                           , '{2}'
                           , '{3}'
                           , '{4}');
                            ", q.QuestionText, "DejanSkiljic", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                           "DejanSkiljic", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                command += string.Format(@"INSERT INTO [Survey].[SurveyQuestionRelations]
                            ([SurveyId]
                            ,[QuestionId]
                            ,[ChangedBy]
                            ,[ChangedDate]
                            ,[CreatedBy]
                            ,[CreateDate])
                        
                        SELECT @SurveyId, Id, '{0}' , '{1}', '{2}', '{3}'
                               FROM @QuestionsIdSTable;
                ", "DejanSkiljic", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                 "DejanSkiljic", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                foreach (var item in q.Answers)
                {
                    command += String.Format(@"INSERT INTO [Survey].[OfferedAnswers]
                                    ([Text]
                                    ,[ChangedBy]
                                    ,[ChangedDate]
                                    ,[CreatedBy]
                                    ,[CreateDate])
                                    OUTPUT inserted.Id INTO @AnswersIdSTable
                                    VALUES
                                    ( '{0}'        
	                                , '{1}'       
	                                , '{2}'        
	                                , '{3}'        
	                                , '{4}');", item,
                                    "Dejan Skiljic", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                                    "Dejan Skiljic", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                }

                command += String.Format(@"INSERT INTO [Survey].[QuestionOfferedAnswerRelations]
                                   ([QuestionId]
                                   ,[OfferedAnswerId]
                                   ,[ChangedBy]
                                   ,[ChangedDate]
                                   ,[CreatedBy]
                                   ,[CreateDate])
                                    SELECT qt.Id, at.Id, '{0}' , '{1}' , '{2}', '{3}'
                                    FROM @QuestionsIdSTable qt, @AnswersIdSTable at;
                                    ",
                    "DejanSkiljic", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    "DejanSkiljic", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                command += String.Format("DELETE FROM @AnswersIdSTable; ");
                command += String.Format("DELETE FROM @QuestionsIdSTable; ");

            }

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(command, _connection);
            _sqlcommand.ExecuteNonQuery();
            _connection.Close();
            return survey;
        }

       
        public OfferedAnswerResult GetOfferedAnswersForSurvey(int surveyId)
        {
            string _statement = string.Format(@"SELECT DISTINCT q.id, oa.id, oa.Text FROM
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
                        AnswerId = _reader.GetInt32(1),
                        QuestionAnswer = _reader.GetString(2),
                    };
                    _offeredAnswers.Add(_answer);
                }
            }

            result.OfferedAnswers = _offeredAnswers;
            _connection.Close();

            return result;
        }

        public List<Question> GetSurveyQuestions(int surveyId)
        {
            List<Question> _questions = new List<Question>();

            string _command = String.Format(@"SELECT q.id, q.QuestionText FROM
                                Survey.GeneralInformations gi 
                                INNER JOIN Survey.SurveyQuestionRelations qr 
                                ON qr.SurveyId = gi.id
		                        INNER JOIN Survey.Questions q 
                                ON q.id = qr.QuestionId
                                WHERE gi.id = {0}", surveyId);

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(_command, _connection);

            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    Question _question = new Question
                    {
                        Id = _reader.GetInt32(0),
                        QuestionText = _reader.GetString(1),
                    };
                    _questions.Add(_question);
                }
            }
            _connection.Close();

            return _questions;
        }

       
        public QuestionAndAnswers AddQuestionWithAnswers(QuestionAndAnswers qAndA)
        {
            string _command = String.Empty;

            _command += "DECLARE @QuestionId Int;";
            _command += "DECLARE @AnswerId Int;";

            _command += String.Format(@"INSERT INTO [Survey].[Questions] 
                                    ([QuestionText]
                                   ,[ChangedBy]
                                   ,[ChangedDate]
                                   ,[CreatedBy]
                                   ,[CreateDate])
                                    VALUES
                                    (   '{0}'
	                                    ,'{1}'
	                                    ,'{2}'
	                                    ,'{3}'
	                                    ,'{4}'
	                                );
                                    SET @QuestionId = SCOPE_IDENTITY();", qAndA.QuestionText,
                                    "DejanSkiljic", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                                    "DejanSkiljic", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

            _command += String.Format(@"INSERT INTO [Survey].[SurveyQuestionRelations]
                                    ([SurveyId]
                                   ,[QuestionId]
                                   ,[ChangedBy]
                                   ,[ChangedDate]
                                   ,[CreatedBy]
                                   ,[CreateDate])
                                    VALUES
                                    (   
                                        '{0}'
                                        ,@QuestionId
                                        ,'{1}'        
	                                    ,'{2}'        
	                                    ,'{3}'        
	                                    ,'{4}'        
	                                );
                                    ", qAndA.SurveyId,
                                   "DejanSkiljic", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                                   "DejanSkiljic", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

            foreach ( var item in qAndA.Answers) 
            {
                _command += String.Format(@"INSERT INTO [Survey].[OfferedAnswers]
                                    ([Text]
                                   ,[ChangedBy]
                                   ,[ChangedDate]
                                   ,[CreatedBy]
                                   ,[CreateDate])
                                    VALUES
                                    (   '{0}',        
	                                    '{1}',        
	                                    '{2}',        
	                                    '{3}',        
	                                    '{4}'         
	                                )
                                    SET @AnswerId = SCOPE_IDENTITY();", item,
                                    "Dejan Skiljic", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                                    "Dejan Skiljic", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                _command += String.Format(@"INSERT INTO [Survey].[QuestionOfferedAnswerRelations]
                                       ([QuestionId]
                                       ,[OfferedAnswerId]
                                       ,[ChangedBy]
                                       ,[ChangedDate]
                                       ,[CreatedBy]
                                       ,[CreateDate])
                                        VALUES
                                        (   @QuestionId   
                                            ,@AnswerId
	                                        ,'{0}'        
	                                        ,'{1}'        
	                                        ,'{2}'        
	                                        ,'{3}'         
	                                    );
                                    ",
                                   "DejanSkiljic", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                                   "DejanSkiljic", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            }

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(_command, _connection);
            _sqlcommand.ExecuteNonQuery();
            _connection.Close();
            return qAndA;
        }

        public OfferedAnswerResult AddOfferedAnswersForQuestion(OfferedAnswerResult offeredAnswer)
        {
            string _command = String.Empty;
            _command += "DECLARE @AnswerId Int;";

            foreach (var iteam in offeredAnswer.OfferedAnswers) 
            {
                _command += String.Format(@"INSERT INTO [Survey].[OfferedAnswers] 
                                   (
                                        [Text],
                                        ChangedBy,
                                        ChangedDate,
                                        CreatedBy,
                                        CreateDate
                                    )
                                    VALUES
                                    (   '{0}'        
	                                    ,'{1}'        
	                                    ,'{2}'        
	                                    ,'{3}'        
	                                    ,'{4}'         
	                                )
                                    
                                    SET @AnswerId = SCOPE_IDENTITY();", iteam.QuestionAnswer,
                                   "DejanSkiljic", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                                   "DejanSkiljic", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                _command += String.Format(@"INSERT INTO [Survey].[QuestionOfferedAnswerRelations] 
                                   (
                                        QuestionId,
                                        OfferedAnswerId,
                                        ChangedBy,
                                        ChangedDate,
                                        CreatedBy,
                                        CreateDate
                                    )
                                    VALUES
                                    (   '{0}'   
                                        @AnswerId,
	                                    '{1}',        
	                                    '{2}',        
	                                    '{3}',        
	                                    '{4}'         
	                                );
                                    ", iteam.QuestionId,
                                  "DejanSkiljic", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                                  "DejanSkiljic", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));


            }

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(_command, _connection);
            _sqlcommand.ExecuteNonQuery();
            _connection.Close();
            return offeredAnswer;
        }

        public Participant AddParticipant(Participant participant)
        {
            string _command = String.Empty;

            _command += String.Format(@"INSERT INTO [Survey].[Participants]
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
                                        ,'{8}');", 
                                    participant.SurveyId, participant.FirstName, participant.LastName, 
                                    participant.Email, participant.Password,
                                   "DejanSkiljic", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                                   "DejanSkiljic", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(_command, _connection);
            _sqlcommand.ExecuteNonQuery();
            _connection.Close();
            return participant;
        }

        public GeneralInformations AddGeneralInformations(GeneralInformations survey)
        {
            string _command = String.Empty;

            _command += String.Format(@"INSERT INTO [Survey].[GeneralInformations] 
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
                                
                                SET @SurveyId = SCOPE_IDENTITY();", survey.Name, survey.StartDate.ToString("yyyy-MM-dd HH:mm:ss.fff"), survey.EndDate.ToString("yyyy-MM-dd HH:mm:ss.fff"), 
                                "DejanSkiljic", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                                "DejanSkiljic", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));


            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(_command, _connection);
            _sqlcommand.ExecuteNonQuery();
            _connection.Close();
            return survey;
        }

        public GeneralInformations GetGeneralInformations(int surveyId)
        {
            string _statement = string.Format("SELECT ge.id,  ge.Description as Name, ge.StartDate, ge.EndDate, ge.IsOpen " +
                                "FROM Survey.[GeneralInformations] ge" +
                                "   WHERE ge.Id = '{0}'"
                                , surveyId);

            GeneralInformations survey = new GeneralInformations();

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
                    survey.IsOpen = _reader.GetBoolean(4);
                }

            }

            _connection.Close();

            return survey;
        }

        public Answers AddSurveyResult(Answers survey)
        {
            string _command = String.Empty;

            foreach (var iteam in survey.AnsweredQuestions) 
            {
                _command += String.Format(@"INSERT INTO [Survey].[Answers] 
                                   ([ParticipantId]
                                       ,[SurveyId]
                                       ,[QuestionId]
                                       ,[QuestionAnswersId]    
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
                                       ,'{7}');
                                ", survey.ParticipantID, survey.SurveyID, iteam.QuestionId, iteam.QuestionAnswersId,
                                   "DejanSkiljic", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                                   "DejanSkiljic", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            }

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(_command, _connection);
            _sqlcommand.ExecuteNonQuery();
            _connection.Close();

            return survey;
        }

        public List<GeneralInformations> GetAllGeneralInformations()
        {
            string _statement = string.Format("SELECT ge.id,  ge.Description as Name, ge.StartDate, ge.EndDate, ge.IsOpen " +
                               "FROM Survey.[GeneralInformations] ge");

            List<GeneralInformations> generalInformations = new List<GeneralInformations>();

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);

            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    GeneralInformations generalInformation = new GeneralInformations
                    {
                        Id = _reader.GetInt32(0),
                        Name = _reader.GetString(1),
                        StartDate = _reader.GetDateTime(2),
                        EndDate = _reader.GetDateTime(3),
                        IsOpen = _reader.GetBoolean(4)
                    };
                    generalInformations.Add(generalInformation);
                }
            }
            _connection.Close();

            return generalInformations;
        }

        public List<OfferedAnswers> GetOfferedAnswers()
        {
            string _statement = string.Format(@"SELECT [id]
                                            ,[Text]
                                            FROM [Survey].[OfferedAnswers]");
            List<OfferedAnswers> offeredAnswers = new List<OfferedAnswers>();

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);

            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    OfferedAnswers generalInformation = new OfferedAnswers
                    {
                        Id = _reader.GetInt32(0),
                        Text = _reader.GetString(1)
                    };
                    offeredAnswers.Add(generalInformation);
                }
            }
            _connection.Close();

            return offeredAnswers;
        }

        public Participant GetLastParticipantAdded()
        {
            string _statement = string.Format(@"SELECT TOP 1 [id]
                                            ,[SurveyId]
                                            ,[FirstName]
                                            ,[LastName]
                                            ,[Email]
                                            ,[Password] 
                                            FROM [Survey].[Participants] ORDER BY id DESC");
            Participant participant = new Participant();

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);

            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    participant = new Participant
                    {
                        id = _reader.GetInt32(0),
                        SurveyId = _reader.GetInt32(1),
                        FirstName = _reader.GetString(2),
                        LastName = _reader.GetString(3),
                        Email = _reader.GetString(4),
                        Password = _reader.GetString(5),
                    };
                }
            }
            _connection.Close();

            return participant;
        }
    }
}
