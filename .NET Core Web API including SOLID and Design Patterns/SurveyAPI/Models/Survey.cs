using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.Models
{
    public class Survey
    {
        public string Name { get; set; }

        public List<Question> Questions { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Id { get; internal set; }
    }
}
