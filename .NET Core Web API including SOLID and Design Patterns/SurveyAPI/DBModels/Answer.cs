using System;
using System.Collections.Generic;

#nullable disable

namespace SurveyAPI.DBModels
{
    public partial class Answer
    {
        public int Id { get; set; }
        public int ParticipantId { get; set; }
        public int SurveyId { get; set; }
        public int QuestionId { get; set; }
        public int QuestionAnswersId { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Participant Participant { get; set; }
        public virtual Question Question { get; set; }
        public virtual OfferedAnswer QuestionAnswers { get; set; }
        public virtual GeneralInformation Survey { get; set; }
    }
}
