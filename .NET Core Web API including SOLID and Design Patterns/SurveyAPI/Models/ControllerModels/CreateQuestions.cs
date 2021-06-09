using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.Models
{
    public class CreateQuestions
    {
        [Required]
        public string QuestionText { get; set; }
        [Required]
        public List<CreateOfferedAnswers> OfferedAnswers { get; set; }
    }
}
