using Microsoft.AspNetCore.Mvc;
using SurveyAPI.Interfaces;
using SurveyAPI.Models;

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
            return Json(_surveyService.GetSurvey(surveyId));
        }

        // link betweetn survey questions and answers
        [HttpGet()]
        [Route("{surveyId}/OfferedAnswers")]
        public JsonResult GetOfferedAnswersForSurvey(int surveyId)
        {
            return Json(_surveyService.GetOfferedAnswersForSurvey(surveyId));
        }

        // adding survey with it's questions
        [HttpPost]
        [Route("survey/add")]
        public JsonResult AddSurvey([FromBody] Survey survey)
        {
            Survey sr = _surveyService.AddSurvey(survey);
            return Json(sr);
        }

        // adding individual questions for surveys
        [HttpPost]
        [Route("question/add")]
        public JsonResult AddQuestion([FromBody] Question question)
        {
            Question q = _surveyService.AddQuestion(question);
            return Json(q);
        }

        [HttpPost]
        [Route("offeredAnswer/add")]
        public JsonResult AddOfferedAnswer([FromBody] OfferedAnswerModel answerModel)
        {
            OfferedAnswerModel model = _surveyService.AddOfferedAnswer(answerModel);
            return Json(model);
        }

        // getting questions of a survey for either deletion or addition
        [HttpGet]
        [Route("{surveyId}/Questions")]
        public JsonResult GetSurveyQuestions(int surveyId)
        {
            return Json(_surveyService.GetSurveyQuestions(surveyId));
        }

        // submit a survey result
        [HttpPost]
        [Route("{surveyId}/submit")]
        public JsonResult AddAnswer([FromBody] Answer answer)
        {
            Answer ans = _surveyService.AddAnswer(answer);
            return Json(ans);
        }

        [HttpPost]
        [Route("link/{questionId}/{answerId}")]
        public JsonResult LinkOfferedAnswerToQuestion(int questionId, int answerId)
        {
            _surveyService.LinkOfferedAnswerToquestion(questionId, answerId);
            return Json(Ok());
        }

        [HttpDelete]
        [Route("{surveyId}")]
        public JsonResult DeleteSurvey(int surveyId)
        {
            _surveyService.DeleteSurvey(surveyId);
            return Json(Ok());
        }
        
        // delete a survey question and all its relations
        [HttpDelete]
        [Route("{surveyId}/Questions/{questionId}")]
        public JsonResult DeleteQuestion(int surveyId, int questionId)
        {
            _surveyService.DeleteQuestion(surveyId, questionId);
            return Json(Ok());
        }
    }
}
