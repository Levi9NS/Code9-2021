using Microsoft.EntityFrameworkCore;
using SurveyAPI.DBModels;
using SurveyAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.Repositories
{
    public interface IOfferedAnswersRepository : IRepository<OfferedAnswer>
    {
        Task<IEnumerable<OfferedAnswer>> GetBySurveyId(int surveyId);
        Task<IEnumerable<OfferedAnswer>> InsertOfferedAnswersListAsync(List<OfferedAnswer> answers);
        Task<OfferedAnswer> GetByText(string text);
    }
    public class OfferedAnswersRepository : IOfferedAnswersRepository
    {
        private readonly SurveyConetxt _surveyContext;
        public OfferedAnswersRepository(SurveyConetxt surveyContext)
        {
            _surveyContext = surveyContext;
        }

        public OfferedAnswer Delete(int id)
        {
            var data = _surveyContext.OfferedAnswers.Find(id);
            var deleted = _surveyContext.OfferedAnswers.Remove(data);
            return deleted.Entity;
        }

        public async Task<OfferedAnswer> GetByIdAsync(int id)
        {
            var data = await _surveyContext.OfferedAnswers.Where(q => q.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<IEnumerable<OfferedAnswer>> GetBySurveyId(int surveyId)
        {
            var data = await _surveyContext.OfferedAnswers
                .Include(oa => oa.QuestionOfferedAnswerRelations)
                .ThenInclude(of => of.Question)
                .ThenInclude(q => q.SurveyQuestionRelations.Where(sq => sq.SurveyId == surveyId))
                .ToListAsync();

            return data;
        }

        public async Task<OfferedAnswer> GetByText(string text)
        {
            var offeredAnswer = await _surveyContext.OfferedAnswers.Where(oa => oa.Text.Equals(text)).FirstOrDefaultAsync();
            return offeredAnswer;
        }

        public async Task<OfferedAnswer> InsertAsync(OfferedAnswer obj)
        {
            var inserted = await _surveyContext.OfferedAnswers.AddAsync(obj);
            return inserted.Entity;
        }

        public async Task<IEnumerable<OfferedAnswer>> InsertOfferedAnswersListAsync(List<OfferedAnswer> answers)
        {
            List<OfferedAnswer> insertedOfferedAnswers = new List<OfferedAnswer>();
            foreach (var a in answers)
            {
                var inserted = await _surveyContext.OfferedAnswers.AddAsync(a);
                insertedOfferedAnswers.Add(inserted.Entity);
            }

            return insertedOfferedAnswers;
        }

        public async Task SaveAsync()
        {
            await _surveyContext.SaveChangesAsync();
        }
    }
}
