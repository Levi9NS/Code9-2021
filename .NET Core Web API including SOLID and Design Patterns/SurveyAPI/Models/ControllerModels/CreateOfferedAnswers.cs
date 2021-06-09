using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.Models
{
    public class CreateOfferedAnswers
    {
        [Required]
        public String Text { get; set; }
    }
}
