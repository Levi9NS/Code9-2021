using Microsoft.EntityFrameworkCore;
using SurveyAPI.DBModels;
using SurveyAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.Repositories
{
    public interface IAnswersRepository: IRepository<Answer>
    {
        Task<IEnumerable<Answer>> GetBySurveyId(int surveyId);
        int GetCountForQuestionAnswer(int surveyId ,int questionId, int offeredAnswerId);
        bool DeleteBySurveyId(int surveyId);
    }
    public class AnswersRepository : IAnswersRepository
    {
        private readonly SurveyConetxt _surveyContext;
        public AnswersRepository(SurveyConetxt surveyContext)
        {
            _surveyContext = surveyContext;
        }

        public Answer Delete(int id)
        {
            var data = _surveyContext.Answers.Find(id);
            var deleted = _surveyContext.Answers.Remove(data);
            return deleted.Entity;
        }

        public bool DeleteBySurveyId(int surveyId)
        {
            var answers = _surveyContext.Answers.Where(q => q.SurveyId == surveyId);

            List<Answer> deleted = new List<Answer>();
            foreach (var a in answers)
            {
                var delete = _surveyContext.Answers.Remove(a);
                deleted.Add(delete.Entity);
            }

            return deleted != null ? true : false;
        }

        public async Task<Answer> GetByIdAsync(int id)
        {
            var data = await _surveyContext.Answers
                .Include(a=>a.Question)
                .Include(a => a.Participant)
                .Include(a => a.Survey)
                .Include(a => a.QuestionAnswers)
                .FirstOrDefaultAsync(q => q.Id == id);
            return data;
        }

        public async Task<IEnumerable<Answer>> GetBySurveyId(int surveyId)
        {
            var data = await _surveyContext.Answers
                .Include(a => a.Question)
                .Include(a => a.QuestionAnswers)
                .Include(a => a.QuestionAnswers.QuestionOfferedAnswerRelations)
                .Include(a => a.Survey)
                .Where(q => q.Survey.Id == surveyId)
                .ToListAsync();
            return data;
        }

        public  int GetCountForQuestionAnswer(int surveyId, int questionId, int offeredAnswerId)
        {
            var data =  _surveyContext.Answers.Where(a => a.SurveyId == surveyId 
                                                    && a.QuestionId == questionId 
                                                    && a.QuestionAnswersId == offeredAnswerId).Count();
            return data;
        }

        public async Task<Answer> InsertAsync(Answer obj)
        {
            var inserted = await _surveyContext.Answers.AddAsync(obj);
            return inserted.Entity;
        }

        public async Task SaveAsync()
        {
            await _surveyContext.SaveChangesAsync();
        }
    }
}
