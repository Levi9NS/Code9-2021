using Microsoft.AspNetCore.Mvc;
using SurveyAPI.DTO;
using SurveyAPI.Interfaces;
using SurveyAPI.Models;
using static SurveyAPI.Models.OfferedAnswerResult;

namespace SurveyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : Controller
    {
        private readonly ISurveyService _surveyService;

        public SurveyController(ISurveyService _svc)
        {
            _surveyService = _svc;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet()]
        [Route("{surveyId}/Answers")]
        public JsonResult GetSurveyResult(int surveyId)
        {
            return Json(_surveyService.GetSurveyResults(surveyId));
        }

        [HttpGet()]
        [Route("{surveyId}")]
        public JsonResult GetSurvey(int surveyId)
        {
            return Json(_surveyService.GetSurveyQuestions(surveyId));
        }

        [HttpGet()]
        [Route("{surveyId}/OfferedAnswers")]
        public JsonResult GetOfferedAnswersForSurvey(int surveyId)
        {
            return Json(_surveyService.GetOfferedAnswersForSurvey(surveyId));
        }

       

        [HttpDelete]
        [Route("{surveyId}")]
        public JsonResult DeleteSurvey(int surveyId)
        {
            _surveyService.DeleteSurvey(surveyId);
            return Json(Ok());
        }
    

        /****************** my methods *****************************/
        [HttpGet()]
        [Route("Questions")]
        public JsonResult GetQuestions()
        {
            return Json(_surveyService.GetQuestions());
        }

        [HttpPost]
        [Route("AddQuestion")]
        public JsonResult AddQuestion([FromBody] Question q)
        {
            return Json(_surveyService.AddQuestion(q));
        }

        [HttpGet()]
        [Route("GetAllSurveys")]
        public JsonResult GetAllSurveys()
        {
            return Json(_surveyService.GetAllSurveyGeneralnformation());
        }

        [HttpPost]
        [Route("AddSurvey")]
        public JsonResult AddSurvey([FromBody] Survey survey)
        {
            return Json(_surveyService.AddSurveyGeneralnformation(survey));
        }

        [HttpGet()]
        [Route("GetOfferedAnswers")]
        public JsonResult GetOfferedAnswers()
        {
            return Json(_surveyService.GetOfferedAnswers());
        }

        [HttpPost]
        [Route("AddQuestionToSurvey")]
        public JsonResult AddQuestionToSurvey([FromBody] SurveyQuestionRelation surveyQuestionRelation)
        {
            return Json(_surveyService.AddQuestionToSurvey(surveyQuestionRelation));
        }

        [HttpPost]
        [Route("AddParticipantAnswers")]
        public JsonResult AddParticipantAnswers([FromBody] ParticipantsAnswerDTO participantsAnswerDTO)
        {
            return Json(_surveyService.AddParticipantAnswers(participantsAnswerDTO));
        }

        [HttpGet()]
        [Route("{surveyId}/SurveyAnswers")]
        public JsonResult GetSurveyAnswers(int surveyId)
        {
            return Json(_surveyService.GetSurveyAnswers(surveyId));
        }

        [HttpPost]
        [Route("CloseSurvey")]
        public JsonResult CloseSurvey([FromBody] HelperDTO s)
        {
            return Json(_surveyService.closeSurvey(s));
        }

        [HttpPost]
        [Route("AddOfferedAnswer")]
        public JsonResult AddOfferedAnswer([FromBody] OfferedAnswer oa)
        { 
            return Json(_surveyService.addOfferedAnswer(oa));
        }
    }
}
