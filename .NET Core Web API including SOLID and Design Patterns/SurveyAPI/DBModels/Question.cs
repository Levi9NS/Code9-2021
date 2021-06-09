using System;
using System.Collections.Generic;

#nullable disable

namespace SurveyAPI.DBModels
{
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
            QuestionOfferedAnswerRelations = new HashSet<QuestionOfferedAnswerRelation>();
            SurveyQuestionRelations = new HashSet<SurveyQuestionRelation>();
        }

        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<QuestionOfferedAnswerRelation> QuestionOfferedAnswerRelations { get; set; }
        public virtual ICollection<SurveyQuestionRelation> SurveyQuestionRelations { get; set; }
    }
}
