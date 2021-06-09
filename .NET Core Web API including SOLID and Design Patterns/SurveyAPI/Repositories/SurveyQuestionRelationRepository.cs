using SurveyAPI.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.Repositories
{
    public interface ISurveyQuestionRelationRepository
    {
        Task<IEnumerable<SurveyQuestionRelation>> AddRelationships(int surveyId, List<int> questionId);
        bool DeleteBySurveyId(int surveyId);
        bool CheckQuestionForServey(int questionId, int surveyId);
    }
    public class SurveyQuestionRelationRepository : ISurveyQuestionRelationRepository
    {
        private readonly SurveyConetxt _surveyContext;
        public SurveyQuestionRelationRepository(SurveyConetxt surveyContext)
        {
            _surveyContext = surveyContext;
        }
        public async Task<IEnumerable<SurveyQuestionRelation>> AddRelationships(int surveyId, List<int> questionsIds)
        {
            List<SurveyQuestionRelation> result = new List<SurveyQuestionRelation>();

            foreach (var qId in questionsIds)
            {
                var inserted = await _surveyContext.SurveyQuestionRelations.AddAsync(new SurveyQuestionRelation
                {
                    CreatedBy = "user",
                    CreateDate = DateTime.Now,
                    QuestionId = qId,
                    SurveyId = surveyId
                });

                result.Add(inserted.Entity);
            }

            return result;
        }

        public bool DeleteBySurveyId(int surveyId)
        {
            var surveyQuestionRelations = _surveyContext.SurveyQuestionRelations.Where(x => x.SurveyId == surveyId).ToList();

            List<SurveyQuestionRelation> deleted = new List<SurveyQuestionRelation>();
            foreach (var r in surveyQuestionRelations)
            {
                var delete = _surveyContext.SurveyQuestionRelations.Remove(r);
                deleted.Add(delete.Entity);
            }

            return deleted == null ? false : true;
        }


        public bool CheckQuestionForServey(int questionId, int surveyId)
        {
            var exist =  _surveyContext.SurveyQuestionRelations.Where(sq => sq.QuestionId == questionId && sq.SurveyId == surveyId).ToList();

            return (exist != null && exist.Count > 0) ? true : false;
        }
    }
}
