using System;
using System.Collections.Generic;

#nullable disable

namespace SurveyAPI.Models
{
    public partial class SurveyQuestionRelation
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public int QuestionId { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Question Question { get; set; }
        public virtual GeneralInformation Survey { get; set; }
    }
}
