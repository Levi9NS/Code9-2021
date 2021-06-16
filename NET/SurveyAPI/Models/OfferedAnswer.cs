using System;
using System.Collections.Generic;

#nullable disable

namespace SurveyAPI.Models
{
    public partial class OfferedAnswer
    {
        public OfferedAnswer()
        {
            Answers = new HashSet<Answer>();
            QuestionOfferedAnswerRelations = new HashSet<QuestionOfferedAnswerRelation>();
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<QuestionOfferedAnswerRelation> QuestionOfferedAnswerRelations { get; set; }
    }
}
