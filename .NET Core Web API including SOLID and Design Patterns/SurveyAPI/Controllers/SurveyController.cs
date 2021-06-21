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

        [HttpGet]
        [Route("{surveyId}/short")]
        public JsonResult GetShortSurveyModel(int surveyId)
        {
            return Json(_surveyService.GetShortSurveyModel(surveyId));
        }

        [HttpGet]
        [Route("survey/shorts")]
        public JsonResult GetShortSurveyModels()
        {
            return Json(_surveyService.GetShortSurveyModels());
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

        // adding general questions
        [HttpPost]
        [Route("question/add")]
        public JsonResult AddQuestion([FromBody] QuestionWithAnswers question)
        {
            QuestionWithAnswers q = _surveyService.AddQuestion(question);
            return Json(q);
        }

        [HttpPost]
        [Route("question/add/survey")]
        public JsonResult AddQuestionToSurvey([FromBody] QuestionWithSurveyId question)
        {
            QuestionWithSurveyId q = _surveyService.AddQuestionToSurvey(question);
            return Json(q);
        }

        [HttpGet]
        [Route("survey/getAllQuestions")]
        public JsonResult GetAllQuestions()
        {
            return Json(_surveyService.GetAllQuestions());
        }

        [HttpPost]
        [Route("offeredAnswer/add")]
        public JsonResult AddOfferedAnswer([FromBody] OfferedAnswerModel answerModel)
        {
            OfferedAnswerModel model = _surveyService.AddOfferedAnswer(answerModel);
            return Json(model);
        }

        [HttpGet]
        [Route("survey/getAllOfferedAnswers")]
        public JsonResult GetAllOfferedAnswers()
        {
            return Json(_surveyService.GetAllOfferedAnswers());
        }

        // getting questions of a survey for either removal or addition
        [HttpGet]
        [Route("{surveyId}/questions")]
        public JsonResult GetSurveyQuestions(int surveyId)
        {
            return Json(_surveyService.GetSurveyQuestions(surveyId));
        }

        // 2 variants of adding a question, 1 directly to survey and 1 in general

        // submit a survey result
        [HttpPost]
        [Route("survey/submit")]
        public JsonResult AddSurveyAnswer([FromBody] Answer answer)
        {
            Answer ans = _surveyService.AddSurveyAnswer(answer);
            return Json(ans);
        }

        [HttpDelete]
        [Route("{surveyId}")]
        public JsonResult DeleteSurvey(int surveyId)
        {
            _surveyService.DeleteSurvey(surveyId);
            return Json(Ok());
        }
        
        // remove a question from survey, but the question will still exist
        [HttpDelete]
        [Route("{surveyId}/questions/{questionId}")]
        public JsonResult RemoveSurveyQuestion(int surveyId, int questionId)
        {
            _surveyService.RemoveSurveyQuestion(surveyId, questionId);
            return Json(Ok());
        }

        // delete a question
        [HttpDelete]
        [Route("survey/getAllQuestions/{questionId}")]
        public JsonResult DeleteQuestion(int questionId)
        {
            _surveyService.DeleteQuestion(questionId);
            return Json(Ok());
        }

        [HttpPost]
        [Route("survey/linkQuestion")]
        public JsonResult LinkQuestion([FromBody] SurveyLink link)
        {
            SurveyLink surveyLink = _surveyService.LinkQuestion(link);
            return Json(surveyLink);
        }
    }
}
