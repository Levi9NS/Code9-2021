using System.ComponentModel.DataAnnotations;

namespace SurveyAPI.Models.ControllerModels
{
    public class ParticipantModel
    {
        [Required]
        public int SurveyId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}