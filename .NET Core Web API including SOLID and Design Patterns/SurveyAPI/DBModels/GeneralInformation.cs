using System;
using System.Collections.Generic;

#nullable disable

namespace SurveyAPI.DBModels
{
    public partial class GeneralInformation
    {
        public GeneralInformation()
        {
            Answers = new HashSet<Answer>();
            Participants = new HashSet<Participant>();
            SurveyQuestionRelations = new HashSet<SurveyQuestionRelation>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsOpen { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Participant> Participants { get; set; }
        public virtual ICollection<SurveyQuestionRelation> SurveyQuestionRelations { get; set; }
    }
}
