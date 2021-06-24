using Microsoft.Extensions.Configuration;
using SurveyAPI.Interfaces;
using SurveyAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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
                .ThenInclude(qa => qa.QuestionOfferedAnswerRelations)
				.Include(a => a.QuestionAnswers)
				.Where(a => a.Survey.Id == surveyId)
				.ToList();

			SurveyResult survey = new SurveyResult();
			List<AnsweredQuestion> answeredList = new List<AnsweredQuestion>();

			foreach (var r in answersInDb)
			{
				if (!answeredList.Any(q => q.text == r.Question.QuestionText)) {
					survey.Name = r.Survey.Description;
                    AnsweredQuestion _question = new AnsweredQuestion()
                    {
                        text = r.Question.QuestionText,
						responses = r.Question.QuestionOfferedAnswerRelations.Select(qa => new AnsweredQuestionList
                        {
							response = qa.OfferedAnswer.Text,
							count = answersInDb.Where(c => c.QuestionId == qa.QuestionId && c.QuestionAnswersId == qa.OfferedAnswerId).Count()
						}).ToList()
                        
                    };
                    answeredList.Add(_question);
				}
			}

			survey.Questions = answeredList.OrderBy(a => a.text).ToList();
            return survey;
		}

		public Survey AddSurvey(Survey survey)
		{
			GeneralInformation generalInformation = new GeneralInformation
			{
				Description = survey.Name,
				StartDate = DateTime.Now,
				EndDate = survey.EndDate,
				IsOpen = true,
				CreatedBy = "User",
				CreateDate = DateTime.Now
			};
			_context.GeneralInformations.Add(generalInformation);
			_context.SaveChanges();
			return survey;
		}

		public SurveyAdd AddSurveyResult(SurveyAdd answer)
        {
            Participant newParticipant = new Participant
            {
				SurveyId = answer.SurveyId,
				FirstName = answer.FirstName,
				LastName = answer.LastName,
				Email = answer.Email,
				Password = answer.Password,
                CreatedBy = "User",
                CreateDate = DateTime.Now
			};
			_context.Participants.Add(newParticipant);
            _context.SaveChanges();

            foreach (var a in answer.Answers)
            {
                Answer newAnswer = new Answer
                {
					SurveyId = a.SurveyId,
					QuestionId = a.QuestionId,
					QuestionAnswersId = a.QuestionAnswersId,
					ParticipantId = newParticipant.Id,
                    CreatedBy = "User",
                    CreateDate = DateTime.Now
			    };
                _context.Answers.Add(newAnswer);
		    }
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
								OaId = b.OfferedAnswer.Id
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

		public OfferedAnswer AddOfferedAnswer(OfferedAnswerBasic offeredAnswer)
		{
			OfferedAnswer newOA = new OfferedAnswer
			{
				CreatedBy = "User",
				CreateDate = DateTime.Now,
				Text = offeredAnswer.QuestionAnswer
			};
			_context.OfferedAnswers.Add(newOA);
			_context.SaveChanges();

			QuestionOfferedAnswerRelation qoar = new QuestionOfferedAnswerRelation
			{
				CreatedBy = "User",
				CreateDate = DateTime.Now,
				QuestionId = offeredAnswer.QuestionId,
				OfferedAnswerId = newOA.Id
			};
			_context.QuestionOfferedAnswerRelations.Add(qoar);
			_context.SaveChanges();
			return newOA;
		}
		
		public List<Question> GetAllQuestions()
		{
			return _context.Questions.ToList();
		}

		public List<Survey> GetAllSurveys()
		{
			var generalInDb = _context.GeneralInformations.ToList();

			List<Survey> surveys = new List<Survey>();
			foreach (var g in generalInDb)
			{
				Survey s = new Survey
				{
					Id = g.Id,
					Name = g.Description,
				};
				surveys.Add(s);
			}
			return surveys;
		}
	}
}
