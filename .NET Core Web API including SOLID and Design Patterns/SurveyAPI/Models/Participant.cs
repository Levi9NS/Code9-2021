using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.Models
{
    public class Participant
    {
        public int Id { get; internal set; }
        public int SurveyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
