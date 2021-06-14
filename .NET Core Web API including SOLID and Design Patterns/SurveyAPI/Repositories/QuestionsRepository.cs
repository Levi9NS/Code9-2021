using Microsoft.EntityFrameworkCore;
using SurveyAPI.DBModels;
using SurveyAPI.Interfaces;
using SurveyAPI.Models.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.Repositories
{
    public interface IQuestionsRepository : IRepository<Question>
    {
        Task<IEnumerable<Question>> GetBySurveyIdAsync(int surveyId);
        Task<IEnumerable<QuestionServiceModel>> InsertQuestionsListAsync(List<QuestionServiceModel> questions);
    }
    public class QuestionsRepository : IQuestionsRepository
    {
        private readonly SurveyConetxt _surveyContext;
        public QuestionsRepository(SurveyConetxt surveyContext)
        {
            _surveyContext = surveyContext;
        }

        public Question Delete(int id)
        {
            var data = _surveyContext.Questions.Find(id);
            var deleted = _surveyContext.Questions.Remove(data);
            return deleted.Entity;
        }

        public async Task<Question> GetByIdAsync(int id)
        {
            var data = await _surveyContext.Questions.Where(q => q.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<IEnumerable<Question>> GetBySurveyIdAsync(int surveyId)
        {
            var data = await _surveyContext.Questions
                .Include(q => q.SurveyQuestionRelations)
                .Include(q => q.QuestionOfferedAnswerRelations).ThenInclude(qa => qa.OfferedAnswer)
                .Where(q => q.SurveyQuestionRelations.Any(sq => sq.SurveyId == surveyId)).ToListAsync();

            return data;
        }

        public async Task<Question> InsertAsync(Question obj)
        {
            var inserted = await _surveyContext.Questions.AddAsync(obj);
            return inserted.Entity;
        }

        //After insert  return the same QuestionServiceModel objects list but with added Id-s
        public async Task<IEnumerable<QuestionServiceModel>> InsertQuestionsListAsync(List<QuestionServiceModel> questionsModel)
        {
            List<Question> insertedQuestions = new List<Question>();
            foreach (var q in questionsModel)
            {
                var newQuestion = new Question
                {
                    QuestionText = q.QuestionText,
                    CreatedBy = "user",
                    CreateDate = DateTime.Now
                };
                var inserted = await _surveyContext.Questions.AddAsync(newQuestion);
                insertedQuestions.Add(inserted.Entity);
            }
            await _surveyContext.SaveChangesAsync();

            var result = insertedQuestions.Select((q, index) => new QuestionServiceModel
            {
                Id = q.Id,
                QuestionText =q.QuestionText,
                OfferedAnswers = questionsModel[index].OfferedAnswers
            }).ToList();

            return result;   
        }

        public async Task SaveAsync()
        {
            await _surveyContext.SaveChangesAsync();
        }
    }
}
