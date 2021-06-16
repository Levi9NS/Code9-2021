using Microsoft.Extensions.Configuration;
using SurveyAPI.Interfaces;
using SurveyAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static SurveyAPI.Models.OfferedAnswerResult;
using OfferedAnswer = SurveyAPI.Models.OfferedAnswer;


namespace SurveyAPI.Repositories
{

    public class SurveyRepository : ISurveyRepository
    {

        private readonly ApplicationDBContext _context;

        public SurveyRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public void DeleteSurvey(int surveyId)
        {
            var surveyInDb = _context.GeneralInformations
                .Include(q => q.SurveyQuestionRelations)
                .SingleOrDefault(s => s.Id == surveyId);

            foreach (var sqr in surveyInDb.SurveyQuestionRelations)
            {
                _context.SurveyQuestionRelations.Remove(sqr);
            }
            _context.GeneralInformations.Remove(surveyInDb);
            _context.SaveChanges();
        }

        public Survey GetSurvey(int surveyId)
        {
            var surveyInDb = _context.GeneralInformations
                .Include(s => s.SurveyQuestionRelations)
                .ThenInclude(s => s.Question)
                .SingleOrDefault(s => s.Id == surveyId);

            List<Question> questions = new List<Question>();
            Survey survey = new Survey
            {
                Id = surveyInDb.Id,
                Name = surveyInDb.Description,
                EndDate = surveyInDb.EndDate,
                StartDate = surveyInDb.StartDate
            };

            foreach (var sqr in surveyInDb.SurveyQuestionRelations)
            {
                Question question = new Question
                {
                    Id = sqr.Question.Id,
                    QuestionText = sqr.Question.QuestionText
                };
                questions.Add(question);
            }
            survey.Questions = questions;
            return survey;
        }

        public SurveyResult GetSurveyResult(int surveyId)
        {
            var answersInDb = _context.Answers
                .Include(a => a.Survey)
                .Include(a => a.Participant)
                .Include(a => a.Question)
                .Include(a => a.QuestionAnswers)
                .Where(a => a.Survey.Id == surveyId).ToList();

            SurveyResult survey = new SurveyResult();
            List<AnsweredQuestion> answeredList = new List<AnsweredQuestion>();

            foreach (var r in answersInDb)
            {
                AnsweredQuestion _question = new AnsweredQuestion()
                {
                    text = r.Question.QuestionText,
                    response = r.QuestionAnswers.Text,
                    Id = r.Id,
                    count = answersInDb.Where(c => c.QuestionId == r.QuestionId && c.QuestionAnswersId == r.QuestionAnswersId).Count()
                };

                answeredList.Add(_question);
            }

            survey.Questions = answeredList;

            return survey;
        }
        public List<OfferedAnswer> GetOfferedAnswers()
        {


           // List<OfferedAnswer> offeredAnswers = new List<OfferedAnswer>;
            return _context.OfferedAnswers.Select(p=>p).ToList<OfferedAnswer>();

           // foreach (var r in answersInDb)
           // {
           //     offeredAnswers.Add(r);
           // }

           // return offeredAnswers;
        }

        
        public Survey AddSurvey(Survey survey)
        {
            foreach (var q in survey.Questions)
            {
                q.CreatedBy = "User";
                q.CreateDate = DateTime.Now;
                _context.Questions.Add(q);
            }

            GeneralInformation generalInformation = new GeneralInformation
            {
                Description = survey.Name,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                IsOpen = true,
                CreatedBy = "User",
                CreateDate = DateTime.Now,
                  ChangedBy = "User",
                ChangedDate = DateTime.Now
            };
            _context.GeneralInformations.Add(generalInformation);
            _context.SaveChanges();

            foreach (var q in survey.Questions)
            {
                SurveyQuestionRelation s = new SurveyQuestionRelation
                {
                    CreatedBy = "User",
                    CreateDate = DateTime.Now,
                    QuestionId = q.Id,
                    SurveyId = generalInformation.Id,
                    Survey = generalInformation,
                    Question = q,
                };
                _context.SurveyQuestionRelations.Add(s);
            }
            _context.SaveChanges();

            return survey;
        }

        public Answer AddSurveyResult(Answer answer)
        {
            answer.CreatedBy = "User";
            answer.CreateDate = DateTime.Now;
            answer.ChangedDate = DateTime.Now;
            answer.ChangedBy = "User";
            _context.Answers.Add(answer);
            _context.SaveChanges();
            return answer;
        }

        public OfferedAnswerResult GetOfferedAnswersForSurvey(int surveyId)
        {

            var answersInDb = _context.OfferedAnswers
                .Include(a => a.QuestionOfferedAnswerRelations)
                .ThenInclude(a => a.Question)
                .ThenInclude(a => a.SurveyQuestionRelations.Where(s => s.SurveyId == surveyId));

            OfferedAnswerResult result = new OfferedAnswerResult();
            List<OfferedAnswerResult.OfferedAnswer> _offeredAnswers = new List<OfferedAnswerResult.OfferedAnswer>();

            foreach (var a in answersInDb)
            {
                foreach (var b in a.QuestionOfferedAnswerRelations)
                {
                    foreach (var c in b.Question.SurveyQuestionRelations)
                    {
                        if (c.SurveyId == surveyId)
                        {
                            OfferedAnswerResult.OfferedAnswer _answer = new OfferedAnswerResult.OfferedAnswer
                            {
                                QuestionId = b.QuestionId,
                                QuestionAnswer = b.OfferedAnswer.Text,
                                AnswerId= b.OfferedAnswer.Id
                            };
                            _offeredAnswers.Add(_answer);
                        }
                    }
                }
            }

            result.OfferedAnswers = _offeredAnswers;
            return result;
        }
        public Participant AddSurveyParticipant(Participant participant)
        {
            participant.CreatedBy = "User";
            participant.CreateDate = DateTime.Now;
            _context.Participants.Add(participant);
            _context.SaveChanges();
            return participant;
        }

        public QuestionOfferedAnswerRelation OfferedAnswerRelation(QuestionOfferedAnswerRelation offeredAnswerRelation)
        {
            offeredAnswerRelation.CreatedBy = "User";
            offeredAnswerRelation.CreateDate = DateTime.Now;
        
            _context.QuestionOfferedAnswerRelations.Add(offeredAnswerRelation);
            _context.SaveChanges();
            return offeredAnswerRelation;
           
        }


        public Question AddSurveyQuestion(Question question)
        {
            question.CreatedBy = "User";
            question.CreateDate = DateTime.Now;
            _context.Questions.Add(question);

            foreach (var q in question.SurveyQuestionRelations)
            {
                q.CreatedBy = "User";
                q.CreateDate = DateTime.Now;
                q.QuestionId = question.Id;
                _context.SurveyQuestionRelations.Add(q);
            }

            _context.SaveChanges();
            return question;
        }

        public OfferedAnswer AddOfferedAnswer(OfferedAnswer offeredAnswer)
        {
            offeredAnswer.CreatedBy = "User";
            offeredAnswer.CreateDate = DateTime.Now;
            _context.OfferedAnswers.Add(offeredAnswer);

            foreach (var qoar in offeredAnswer.QuestionOfferedAnswerRelations)
            {
                qoar.CreatedBy = "User";
                qoar.CreateDate = DateTime.Now;
                qoar.OfferedAnswerId = offeredAnswer.Id;
                _context.QuestionOfferedAnswerRelations.Add(qoar);
            }

            _context.SaveChanges();
            return offeredAnswer;
        }
    }
}
