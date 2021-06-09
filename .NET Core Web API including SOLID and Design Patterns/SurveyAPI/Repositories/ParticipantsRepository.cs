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
    public interface IParticipantsRepository : IRepository<Participant>
    {
        bool DeleteBySurveyId(int surveyId);
    }
    public class ParticipantsRepository : IParticipantsRepository
    {
        private readonly SurveyConetxt _surveyContext;
        public ParticipantsRepository(SurveyConetxt surveyContext)
        {
            _surveyContext = surveyContext;
        }

        public Participant Delete(int id)
        {
            var data = _surveyContext.Participants.Find(id);
            var deleted = _surveyContext.Participants.Remove(data);
            return deleted.Entity;
        }

        public bool DeleteBySurveyId(int surveyId)
        {
            var paricipantd = _surveyContext.Participants.Where(q => q.SurveyId == surveyId);

            List<Participant> deleted = new List<Participant>();
            foreach (var p in paricipantd)
            {
                var delete = _surveyContext.Participants.Remove(p);
                deleted.Add(delete.Entity);
            }

            return deleted != null ? true : false;
        }

        public async Task<Participant> GetByIdAsync(int id)
        {
            var data = await _surveyContext.Participants.Where(q => q.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<Participant> InsertAsync(Participant obj)
        {
            var inserted = await _surveyContext.Participants.AddAsync(obj);
            return inserted.Entity;
        }

        public async Task SaveAsync()
        {
            await _surveyContext.SaveChangesAsync();
        }
    }
}
