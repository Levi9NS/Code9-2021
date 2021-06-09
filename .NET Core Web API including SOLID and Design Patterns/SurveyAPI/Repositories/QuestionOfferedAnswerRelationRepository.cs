using SurveyAPI.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.Repositories
{
    public interface IQuestionOfferedAnswerRelationRepository
    {
        Task<IEnumerable<QuestionOfferedAnswerRelation>> AddRelationships(int questionId, List<int> offeredAnswerId);

        bool CheckOfferedAnswerForQuestion(int questionId, int offeredAnswerId);
    }
    public class QuestionOfferedAnswerRelationRepository : IQuestionOfferedAnswerRelationRepository
    {
        private readonly SurveyConetxt _surveyContext;
        public QuestionOfferedAnswerRelationRepository(SurveyConetxt surveyContext)
        {
            _surveyContext = surveyContext;
        }

        public async Task<IEnumerable<QuestionOfferedAnswerRelation>> AddRelationships(int questionId, List<int> offeredAnswerId)
        {
            List<QuestionOfferedAnswerRelation> result = new List<QuestionOfferedAnswerRelation>();

            foreach (var id in offeredAnswerId)
            {
                var inserted = await _surveyContext.QuestionOfferedAnswerRelations.AddAsync(new QuestionOfferedAnswerRelation
                {
                    CreatedBy = "user",
                    CreateDate = DateTime.Now,
                    QuestionId = questionId,
                    OfferedAnswerId=id,
                });
                result.Add(inserted.Entity);
            }

            return result;
        }

        public bool CheckOfferedAnswerForQuestion(int questionId, int offeredAnswerId)
        {
            var exist = _surveyContext.QuestionOfferedAnswerRelations.Where(qa => qa.QuestionId == questionId && qa.OfferedAnswerId == offeredAnswerId).ToList();

            return (exist != null && exist.Count > 0) ? true : false;
        }
    }
}
