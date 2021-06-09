using System;
using System.Collections.Generic;

#nullable disable

namespace SurveyAPI.DBModels
{
    public partial class Participant
    {
        public Participant()
        {
            Answers = new HashSet<Answer>();
        }

        public int Id { get; set; }
        public int SurveyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual GeneralInformation Survey { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
