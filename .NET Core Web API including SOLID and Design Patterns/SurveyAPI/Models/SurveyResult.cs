using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.Models
{
    public class SurveyResult
    {
        public string name { get; set; }

        public List<Questions> questions { get; set; }
    }
}
