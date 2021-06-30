using Microsoft.Extensions.Configuration;
using SurveyAPI.DTO;
using SurveyAPI.Interfaces;
using SurveyAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
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

                    survey.Description = _reader.GetString(1);
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


        public SurveyResult AddSurveyResult(SurveyResult survey)
        {
            return null;
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
                                       ,'{4}');", survey.Description, survey.StartDate.ToString("yyyy-MM-dd HH:mm:ss.fff"), survey.EndDate.ToString("yyyy-MM-dd HH:mm:ss.fff"), "user", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(command, _connection);
            _sqlcommand.ExecuteNonQuery();
            return survey;


        }

        public OfferedAnswer AddOfferedAnswer(OfferedAnswer offeredAnswer)
        {
            string command = string.Empty;
            command += string.Format(@"INSERT INTO [Survey].[OfferedAnswers]
                                       ([Text]
                                       ,[CreatedBy]
                                       ,[CreateDate])
                                 VALUES
                                       ('{0}'
                                       ,'{1}'
                                       ,'{2}');", offeredAnswer.QuestionAnswer, "Micolina", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(command, _connection);
            _sqlcommand.ExecuteNonQuery();
            return offeredAnswer;
        }

        public List<Survey> getSurveys()
        {
            return null;
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

        /******************************************* my methods ************************************************/
        public Question AddQuestion(Question question)
        {
            int returnValue = -1;

            string command = string.Empty;

            command += string.Format(@"INSERT INTO [Survey].[Questions]
                           ([QuestionText]
                           ,[CreatedBy]
                           ,[CreateDate])
                        VALUES
                           ('{0}'
                           ,'{1}'
                           ,'{2}');
                        SELECT SCOPE_IDENTITY();", question.QuestionText, "Micolina", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(command, _connection);
            object returnObj = _sqlcommand.ExecuteScalar();


            if (_sqlcommand != null)
                int.TryParse(returnObj.ToString(), out returnValue);

            _connection.Close();


            command = string.Empty;
            List<int> offeredAnswersId = question.offeredAnswersIds;
            foreach(var id in offeredAnswersId)
            {
                command += string.Format(@"INSERT INTO [Survey].[QuestionOfferedAnswerRelations]
                           ([QuestionId]
                            ,[OfferedAnswerId]
                           ,[ChangedBy]
                           ,[ChangedDate]
                            ,[CreatedBy]
                            ,[CreateDate])
                        VALUES
                           ('{0}'
                           ,'{1}' ,'{2}' ,'{3}' ,'{4}', '{5}' );
                            ", returnValue, id, "Micolina", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), "Milica", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            }

            SqlConnection _connection1 = GetConnection(_connectionString);

            _connection1.Open();
            SqlCommand _sqlcommand1 = new SqlCommand(command, _connection1);
            _sqlcommand1.ExecuteNonQuery();
          
            
            return question;

        }
        public List<Question> GetQuestions()
        {
            string _statement = string.Empty;

            _statement += string.Format("SELECT * FROM[Survey].[Questions] ");

            List<Question> list = new List<Question>();
            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);

            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {

                    Question question = new Question();
                    question.Id = _reader.GetInt32(0);
                    question.QuestionText = _reader.GetString(1);
                    list.Add(question);

                }
            }
            _connection.Close();


            return list;
        }
        public List<Survey> GetAllSurveyGeneralnformation()
        {
            string _statement = string.Format("select id, Description, StartDate, EndDate from Survey.GeneralInformations where isOpen=1");

            List<Survey> generalInformations = new List<Survey>();

            SqlConnection _connection = GetConnection(_connectionString);
            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);

            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    Survey gi = new Survey()
                    {
                        Id = _reader.GetInt32(0),
                        Description = _reader.GetString(1),
                        StartDate = _reader.GetDateTime(2),
                        EndDate = _reader.GetDateTime(3),
                        //IsOpen = _reader.GetBoolean(4),
                      
                    };
                    generalInformations.Add(gi);
                }

            }
            _connection.Close();

            return generalInformations;
        }
        public Survey AddSurveyGeneralInformation(Survey survey)
        {
            string command = string.Empty;
            command += string.Format(@"INSERT INTO [Survey].[GeneralInformations]
                                    ([Description]
                                    ,[StartDate]
                                    ,[EndDate]
                                    ,[IsOpen]
                                    ,[CreatedBy]
                                    ,[CreateDate])
                                VALUES
                                    ('{0}'
                                    ,'{1}'
                                    ,'{2}'
                                    ,'{3}'
                                    ,'{4}','{5}');", survey.Description, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), survey.EndDate.ToString("yyyy-MM-dd HH:mm:ss.fff"),"1", "user", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(command, _connection);
            _sqlcommand.ExecuteNonQuery();
            return survey;
        }
        public List<OfferedAnswer> GetOfferedAnswers()
        {

            string _statement = string.Empty;

            _statement += string.Format("Select id, text from  [Survey].[OfferedAnswers] ");

            List<OfferedAnswer> list = new List<OfferedAnswer>();
            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);

            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    OfferedAnswer offans = new OfferedAnswer();
                    offans.QuestionAnswer = _reader.GetString(1);
                    offans.QuestionId = _reader.GetInt32(0);
                    list.Add(offans);
                }
            }
            _connection.Close();
            return list;
        }

        public SurveyQuestionRelation AddQuestionToSurvey(SurveyQuestionRelation surveyQuestionRelation)
        {
            int surveyId = surveyQuestionRelation.SurveyId;
            int questionId = surveyQuestionRelation.QuestionId;
            string command = string.Empty;

            command += string.Format(@"INSERT INTO [Survey].[SurveyQuestionRelations]
                           ([SurveyId]
                            ,[QuestionId]
                           ,[ChangedBy]
                           ,[ChangedDate]
                           ,[CreatedBy]
                           ,[CreateDate])
                        VALUES
                           ('{0}'
                           ,'{1}' ,'{2}' ,'{3}' ,'{4}', '{5}' );
                            ", surveyId, questionId, "Micolina", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), "Milica", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
           
            SqlConnection _connection= GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand1 = new SqlCommand(command, _connection);
            _sqlcommand1.ExecuteNonQuery();
            _connection.Close();

            return surveyQuestionRelation;
        }

        public Participant AddParticipantAnswers(ParticipantsAnswerDTO participantsAnswerDTO)
        {
            Participant participant = AddParticipant(participantsAnswerDTO.participant);

            List<Answer> answers = participantsAnswerDTO.answers;

            foreach (var a in answers)
            {
                int surveyId = a.SurveyId;
                int questionId = a.QuestionId;
                List<int> questionAnsweredIds = a.QuestionAnsweredIds;
                foreach (var id in questionAnsweredIds)
                {
                    AddAnswer(questionId, surveyId, id, participant.Id);
                }
            }
            return participant;
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
                            ", participant.FirstName, participant.LastName, participant.Email, participant.Password, participant.SurveyId, "Micolina", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));



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

        public void AddAnswer(int qid, int sid, int offeredanswerid, int participantid) => AddAnswer(new Answer { QuestionId = qid, SurveyId = sid, QuestionAnsweredId = offeredanswerid, ParticipantId = participantid });
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
                               ,'{5}')", answer.ParticipantId, answer.SurveyId, answer.QuestionId, answer.QuestionAnsweredId, "Micolina", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));


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

        public SurveyResult GetSurveyResult(int surveyId)
        {
            return null;
        }

        public List<SurveyResultDTO> GetSurveyAnswers(int surveyId)
        {
            SurveyResult sr = new SurveyResult();
            string command = string.Empty;

            List<int> questionIds = new List<int>();
            SurveyResultDTO srDto = null;
          
            command += string.Format("SELECT distinct [QuestionId] as qId FROM Survey.Answers  a WHERE a.SurveyId='{0}'"
                , surveyId);

            SqlConnection _connection = GetConnection(_connectionString);
            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(command, _connection);
            _sqlcommand.ExecuteNonQuery();


            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                     var questionId =  _reader[0];
                     questionIds.Add((int)questionId);
                }
            }


            List<SurveyResultDTO> resultList = new List<SurveyResultDTO>();

            foreach (var qid in questionIds)
            {
                List<int> offeredAnswersForQuestion = getQuestionOfferedAnswerRelations(qid);
               
                srDto = new SurveyResultDTO();
                srDto.offeredAnswers = new Dictionary<string, int>();
                srDto.QuestionText = getQuestionText(qid);

                foreach (var ofaid in offeredAnswersForQuestion)
                {
                    //vraca id offered answera i counter
                    KeyValuePair<int, int> result = getResultOfQuestionOfferedAnswerRelations(qid,ofaid, surveyId);

                    string offeredAnswerText = getOfferedAnswerTxt(ofaid); //getofferedansertext
                    
                    srDto.offeredAnswers.Add(offeredAnswerText, result.Value);
                }
                resultList.Add(srDto);
            }

            return resultList;
        }

        public List<int> getQuestionOfferedAnswerRelations(int questionId)
        {
            List<int> offeredAnswers = new List<int>();
            string command = "";
            command += string.Format("SELECT [OfferedAnswerId] as id FROM Survey.[QuestionOfferedAnswerRelations]  qofa  WHERE  qofa.[QuestionId]='{0}'"
               , questionId);

            SqlConnection _connection = GetConnection(_connectionString);
            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(command, _connection);
            _sqlcommand.ExecuteNonQuery();


            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    var qofid = _reader[0];
                    offeredAnswers.Add((int)qofid);
                }
            }

            return offeredAnswers;
        }

        public KeyValuePair<int, int> getResultOfQuestionOfferedAnswerRelations(int questionId, int offeredAnswerId, int surveyId)
        {
            KeyValuePair<int, int> countSpecificAnswers = new KeyValuePair<int, int>(0,0);
            string command = "";
            command += string.Format("select count(*) as counter  from Survey.Answers as s  where s.QuestionId = '{0}' and s.QuestionAnswersId = '{1}' and s.SurveyId = '{2}'", questionId, offeredAnswerId, surveyId);

            SqlConnection _connection = GetConnection(_connectionString);
            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(command, _connection);
            _sqlcommand.ExecuteNonQuery();


            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    countSpecificAnswers = new KeyValuePair<int, int>((int)offeredAnswerId, (int)_reader[0]);
                }
            }
            return countSpecificAnswers;
        }

        public string getQuestionText(int questionId)
        {
            string qtext = "";
            string command = "";
            command += string.Format("SELECT  [QuestionText]  FROM[Survey].[Questions] where id = '{0}'", questionId);

            SqlConnection _connection = GetConnection(_connectionString);
            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(command, _connection);
            _sqlcommand.ExecuteNonQuery();


            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    qtext = _reader[0].ToString();
                }
            }
            return qtext;
        }

        public string getOfferedAnswerTxt(int ofansid)
        {
            string txt = "";
            string command = "";
            command += string.Format("SELECT [Text] FROM [Survey].[OfferedAnswers]  where id = '{0}'", ofansid);

            SqlConnection _connection = GetConnection(_connectionString);
            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(command, _connection);
            _sqlcommand.ExecuteNonQuery();
            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    txt = _reader[0].ToString();
                }
            }
            return txt;
        }

        public string closeSurvey(HelperDTO s)
        {
            SurveyResult sr = new SurveyResult();

            List<Survey> _surveys = new List<Survey>();
            string _statement = string.Format("UPDATE [Survey].[GeneralInformations] SET  [IsOpen] = 0 WHERE id = '{0}'", s.id);

            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);
            _sqlcommand.ExecuteNonQuery();
            _connection.Close();

            return "ok";
        }

        public string addOfferedAnswer(OfferedAnswer oa)
        {
            string _statement = "";
            _statement += String.Format(@"INSERT INTO [Survey].[OfferedAnswers] 
                                   (
                                        [Text],
                                        ChangedBy,
                                        ChangedDate,
                                        CreatedBy,
                                        CreateDate
                                    )
                                    VALUES
                                    (   '{0}',        
	                                    '{1}',        
	                                    '{2}',        
	                                    '{3}',        
	                                    '{4}'         
	                                );"
                                    , oa.QuestionAnswer,
                                  "Micolina", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                                  "Micolina", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            SqlConnection _connection = GetConnection(_connectionString);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);
            _sqlcommand.ExecuteNonQuery();
            _connection.Close();
            return "ok";
        }
    }
}
