using Microsoft.Extensions.Configuration;
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
     

            string _statement = string.Format("SELECT ge.Description, q.QuestionText, oa.text, COUNT(*) AS count " +
                                         "   FROM " +

                                             "   Survey.GeneralInformations ge " +

                                             "   INNER JOIN Survey.SurveyQuestionRelations sqr " +

                                                "    ON ge.id = sqr.SurveyId " +

                                            "    INNER JOIN Survey.Questions q " +

                                               "     ON sqr.QuestionId = q.id " +

                                           "     INNER JOIN Survey.QuestionOfferedAnswerRelations qoar " +

                                              "      ON q.id = qoar.QuestionId " +

                                            "    INNER JOIN Survey.OfferedAnswers oa " +

                                              "      ON qoar.OfferedAnswerId = oa.id " +

                                           "     INNER JOIN Survey.Participants pa " +

                                              "      ON pa.SurveyId = pa.SurveyId " +

                                           "     INNER JOIN Survey.Answers a " +

                                             "       ON pa.id = a.ParticipantId AND qoar.QuestionId = a.QuestionId AND qoar.OfferedAnswerId = a.QuestionAnswersId " +

                                             "       where ge.id = '{0}' " +
                                         "   GROUP BY ge.id, ge.Description, oa.id, q.id, q.QuestionText, oa.Text " +
                                         "   ORDER BY ge.id, q.id, oa.id", surveyId);

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
                        text = _reader.GetString(1),
                        response = _reader.GetString(2),
                     //   Id = _reader.GetInt32(4),
                        count = _reader.GetInt32(3),
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
            command += "DECLARE @QuestionId Int;";
            command += "DECLARE @OfferedAnswerId Int;";
            command += "DECLARE @OfferedAnswerIdTable Table (ID int);";


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
                               
                                SET @SurveyId = SCOPE_IDENTITY();", survey.Name, survey.StartDate.ToString("yyyy-MM-dd HH:mm:ss.fff"), survey.EndDate.ToString("yyyy-MM-dd HH:mm:ss.fff"), "Marija", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));


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
                           ,'{2}')
                            SET @QuestionId = SCOPE_IDENTITY()
                            ", q.QuestionText, "Marija", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") );

                foreach (var p in q.offeredAnswers)
                {
                    command += string.Format(@"INSERT INTO [Survey].[OfferedAnswers]
                                       ([Text]
                                       ,[CreatedBy]
                                       ,[CreateDate])
                                     OUTPUT inserted.Id INTO @OfferedAnswerIdTable
                                 VALUES
                                       ('{0}'
                                       ,'{1}'
                                       ,'{2}')
                                        SET @OfferedAnswerId = SCOPE_IDENTITY()
                                        ", p.text, "Marija", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));


                    command += string.Format(@"INSERT INTO [Survey].[QuestionOfferedAnswerRelations]
                            ([QuestionId]
                            ,[OfferedAnswerId]
                            ,[CreatedBy]
                            ,[CreateDate])
                            VALUES
                                       (@QuestionId
                                       ,@OfferedAnswerId
                                       ,'{0}'
                                       ,'{1}')", "Marija", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                }
                command += string.Format(@"INSERT INTO [Survey].[SurveyQuestionRelations]
                            ([SurveyId]
                            ,[QuestionId]
                            ,[CreatedBy]
                            ,[CreateDate])
                        
                        VALUES
                                       (@SurveyId
                                       ,@QuestionId
                                       ,'{0}'
                                       ,'{1}')", "Marija", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

            }

         

           


        

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(command, _connection);
            _sqlcommand.ExecuteNonQuery();
            _connection.Close();
            return survey;
        }

        public SurveyResultAdd AddSurveyResult(SurveyResultAdd survey)
        {
            string command = string.Empty;
            foreach (var s in survey.answers)
            {
                command += string.Format(@"INSERT INTO [Survey].[Answers]
                                       ([ParticipantId]
                                       ,[SurveyId]
                                       ,[QuestionId]
                                       ,[QuestionAnswersId]
                                       ,[CreatedBy]
                                       ,[CreateDate])
                                 VALUES
                                       ('{0}'
                                       ,'{1}'
                                       ,'{2}'
                                       ,'{3}'
                                       ,'{4}'
                                       ,'{5}'     )", survey.ParticipantId, survey.SurveyId, s.QuestionId,s.QuestionAnswerId, "Marija", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
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
            string _statement = string.Format(@"SELECT DISTINCT q.id, oa.Text, oa.id FROM
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
            List<OfferedAnswerResult.OfferedAnswer> _offeredAnswers = new List<OfferedAnswerResult.OfferedAnswer>();

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);

            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    OfferedAnswerResult.OfferedAnswer _answer = new OfferedAnswerResult.OfferedAnswer
                    {
                        QuestionId = _reader.GetInt32(0),
                        QuestionAnswer = _reader.GetString(1),
                        OfferedAnswerid = _reader.GetInt32(2)
                    };
                    _offeredAnswers.Add(_answer);
                }
            }

            result.OfferedAnswers = _offeredAnswers;
            _connection.Close();

            return result;
        }

        public Participant AddParticipant(Participant participant)
        {
            
            string command = string.Empty;

            command += string.Format(@"INSERT INTO [Survey].[Participants]
                           ([FirstName]
                           ,[LastName]
                           ,[Email]
                           ,[Password]
                           ,[SurveyId]
                           ,[CreatedBy]
                           ,[CreateDate])
                        VALUES
                           ('{0}'
                           ,'{1}'
                           ,'{2}'
                           ,'{3}'
                           ,'{4}'
                           ,'{5}'
                           ,'{6}')
                            ", participant.FirstName, participant.LastName,participant.Email,participant.Password,participant.SurveyId, "Marija", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));


           
            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(command, _connection);
            _sqlcommand.ExecuteNonQuery();


            string _statement = string.Format(@"SELECT id from Survey.Participants where Email='{0}' and SurveyId='{1}' ", participant.Email, participant.SurveyId);
            int id = 0;
            SqlCommand _sqlcommand1 = new SqlCommand(_statement, _connection);

            using (SqlDataReader _reader = _sqlcommand1.ExecuteReader())
            {
                while (_reader.Read())
                {
                    id = _reader.GetInt32(0);
                }
            }
        
            participant.Id = id;
            return participant;
        }

        public Answer AddAnswer(Answer answer)
        {
            try
            {
                string command = string.Empty;

                command += string.Format(@"INSERT INTO [Survey].[Answers]
                               ([ParticipantId]
                               ,[SurveyId]
                               ,[QuestionId]
                               ,[QuestionAnswersId]
                               ,[CreatedBy]
                               ,[CreateDate])
                            VALUES
                               ('{0}'
                               ,'{1}'
                               ,'{2}'
                               ,'{3}'
                               ,'{4}'
                               ,'{5}')", answer.ParticipantId, answer.SurveyId, answer.QuestionId, answer.QuestionAnsweredId, "Marija", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));


                SqlConnection _connection = GetConnection(_connectionString);

                _connection.Open();
                SqlCommand _sqlcommand = new SqlCommand(command, _connection);
                _sqlcommand.ExecuteNonQuery();
                return answer;
            }
            catch
            {
                return null;
            }
        }

        public Question AddQuestion(Question question)
        {

            string command = string.Empty;

            command += string.Format(@"INSERT INTO [Survey].[Questions]
                           ([QuestionText]
                           ,[CreatedBy]
                           ,[CreateDate])
                        VALUES
                           ('{0}'
                           ,'{1}'
                           ,'{2}')", question.QuestionText, "Marija", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

             SqlConnection _connection = GetConnection(_connectionString);

                _connection.Open();
                SqlCommand _sqlcommand = new SqlCommand(command, _connection);
                _sqlcommand.ExecuteNonQuery();
                return question;

        }

        public Survey AddGeneralInformations(Survey survey)
        {
            string command = string.Empty;
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
                                       ,'{4}');", survey.Name, survey.StartDate.ToString("yyyy-MM-dd HH:mm:ss.fff"), survey.EndDate.ToString("yyyy-MM-dd HH:mm:ss.fff"), "user", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(command, _connection);
            _sqlcommand.ExecuteNonQuery();
            return survey;


        }

        public Models.OfferedAnswer AddOfferedAnswer(Models.OfferedAnswer offeredAnswer)
        {
            string command = string.Empty;
            command += string.Format(@"INSERT INTO [Survey].[OfferedAnswers]
                                       ([Text]
                                       ,[CreatedBy]
                                       ,[CreateDate])
                                 VALUES
                                       ('{0}'
                                       ,'{1}'
                                       ,'{2}');", offeredAnswer.text,  "Marija", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(command, _connection);
            _sqlcommand.ExecuteNonQuery();
            return offeredAnswer;
        }

        public List<Survey> getSurveys()
        {
            SurveyResult sr = new SurveyResult();

            List<Survey> _surveys = new List<Survey>();
            string _statement = string.Format("select id, Description, StartDate, EndDate from Survey.GeneralInformations where isOpen=1");

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);

            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    
                    Survey _survey = new Survey()
                    {
                        Id = _reader.GetInt32(0),
                        Name = _reader.GetString(1),
                        StartDate = _reader.GetDateTime(2),
                        EndDate = _reader.GetDateTime(3),
                    };

                    _surveys.Add(_survey);
                }
            }
            _connection.Close();

          

            return _surveys;
        }

        public Participant GetParticipant(int id)
        {

            string _statement = string.Empty;

            _statement += string.Format("Select id, FirstName, LastName,Email,Password,SurveyId from  [Survey].[Participants] where id='{0}'", id);

            Participant p = new Participant();
            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);

            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {




                    p.SurveyId = _reader.GetInt32(5);

                    p.FirstName = _reader.GetString(1);
                    p.LastName = _reader.GetString(2);
                    p.Email = _reader.GetString(3);
                    p.Password = _reader.GetString(4);

                    p.Id = _reader.GetInt32(0);

                     

                   
                }
            }
            _connection.Close();

            

            return p;

        }

        public List<Models.OfferedAnswer> GetOfferedAnswers()
        {

            string _statement = string.Empty;

            _statement += string.Format("Select id, text from  [Survey].[OfferedAnswers] ");

            List<Models.OfferedAnswer> list = new List<Models.OfferedAnswer>();
            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);

            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {


                    Models.OfferedAnswer answer = new Models.OfferedAnswer();

                    answer.text = _reader.GetString(1);
                    answer.id = _reader.GetInt32(0);

                    list.Add(answer);


                }
            }
            _connection.Close();



            return list;
        }

        public List<Question> GetQuestions()
        {
            string _statement = string.Empty;

            _statement += string.Format("Select q.id, q.questionText, oa.text, oa.id from  [Survey].[Questions] q " +
                "INNER JOIN  [Survey].[QuestionOfferedAnswerRelations] rel " +
                "ON  q.id=rel.QuestionId " +
                "INNER JOIN  [Survey].[OfferedAnswers] oa" +
                " on rel.OfferedAnswerId=oa.id ");

            List<Question> list = new List<Question>();
            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);

            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {


                    if(!list.Exists(x=>x.Id== _reader.GetInt32(0)))
                    {
                        Question question = new Question();
                        question.QuestionText = _reader.GetString(1);
                        question.Id = _reader.GetInt32(0);
                        List<Models.OfferedAnswer> offeredAnswers = new List<Models.OfferedAnswer>();
                        Models.OfferedAnswer oa = new Models.OfferedAnswer();
                        oa.text = _reader.GetString(2);
                        oa.id = _reader.GetInt32(3);
                        offeredAnswers.Add(oa);
                        question.offeredAnswers = offeredAnswers;
                        list.Add(question);

                    }
                    else
                    {
                        Question question = new Question();
                        question.QuestionText = _reader.GetString(1);
                        question.Id = _reader.GetInt32(0);
                        Models.OfferedAnswer oa = new Models.OfferedAnswer();
                        oa.text = _reader.GetString(2);
                        oa.id = _reader.GetInt32(3);
                        list.Find(x => x.Id == _reader.GetInt32(0)).offeredAnswers.Add(oa);
                      


                    }
                   
                   

                   
                }
            }
            _connection.Close();


            return list;
        }
    }
}
