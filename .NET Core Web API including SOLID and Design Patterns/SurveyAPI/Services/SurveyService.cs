using SurveyAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.Services
{
    public class SurveyService
    {
        private SqlConnection GetConnection(string _connection_string)
        {
            return new SqlConnection(_connection_string);
        }

        public SurveyResult GetSurveyResults(string _connection_string, string surveyName)
        {
            SurveyResult sr = new SurveyResult();
            List<Questions> _lquestions = new List<Questions>();
            string _statement = string.Format("SELECT ge.Description,q.QuestionText, oa.text, COUNT(*) AS count " +
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
                                "   WHERE ge.Description = '{0}'" +
                                "GROUP BY ge.Description,q.QuestionText,oa.Text", surveyName);

            SqlConnection _connection = GetConnection(_connection_string);

            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);

            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    Questions _question = new Questions()
                    {
                        text = _reader.GetString(1),
                        response = _reader.GetString(2),
                        count = _reader.GetInt32(3)
                    };

                    _lquestions.Add(_question);
                }
            }
            _connection.Close();

            sr.name = surveyName;
            sr.questions = _lquestions;

            return sr;
        }
    }
}
