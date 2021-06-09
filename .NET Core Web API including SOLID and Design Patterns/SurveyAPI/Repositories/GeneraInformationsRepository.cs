using Microsoft.EntityFrameworkCore;
using SurveyAPI.DBModels;
using SurveyAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.Repositories
{
    public interface IGeneraInformationsRepository : IRepository<GeneralInformation>
    {
        Task<IEnumerable<GeneralInformation>> GetAllAsync();
    }
    public class GeneraInformationsRepository : IGeneraInformationsRepository
    {
        private readonly SurveyConetxt _surveyContext;

        public GeneraInformationsRepository(SurveyConetxt surveyContext)
        {
            _surveyContext = surveyContext;
        }

        public GeneralInformation Delete(int id)
        {
            var data = _surveyContext.GeneralInformations.Find(id);
            var deleted = _surveyContext.GeneralInformations.Remove(data);

            return deleted.Entity;
        }

        public async Task<IEnumerable<GeneralInformation>> GetAllAsync()
        {
            var data = await _surveyContext.GeneralInformations.ToListAsync();
            return data;
        }

        public async Task<GeneralInformation> GetByIdAsync(int id)
        {
            var data = await _surveyContext.GeneralInformations
                .Include(g => g.SurveyQuestionRelations)
                .ThenInclude(sq=>sq.Question)
                .ThenInclude(q=>q.QuestionOfferedAnswerRelations)
                .ThenInclude(qa=>qa.OfferedAnswer)
                .FirstOrDefaultAsync(s => s.Id == id);

            return data;
        }

        public async Task<GeneralInformation> InsertAsync(GeneralInformation obj)
        {
            var inserted = await _surveyContext.GeneralInformations.AddAsync(obj);
            return inserted.Entity;
        }

        public async Task SaveAsync()
        {
          await _surveyContext.SaveChangesAsync();
        }
    }
}
