using System;
using System.Collections.Generic;

#nullable disable

namespace SurveyAPI.DBModels
{
    public partial class QuestionOfferedAnswerRelation
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int OfferedAnswerId { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual OfferedAnswer OfferedAnswer { get; set; }
        public virtual Question Question { get; set; }
    }
}
