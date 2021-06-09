using System.ComponentModel.DataAnnotations;

namespace SurveyAPI.Models.ControllerModels
{
    public class AnswerModel
    {
        [Required]
        public int SurveyId { get; set; }
        [Required]
        public int QuestionId { get; set; }
        [Required]
        public int QuestionAnswersId { get; set; }
    }
}