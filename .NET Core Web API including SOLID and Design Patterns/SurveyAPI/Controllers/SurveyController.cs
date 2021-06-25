using Microsoft.AspNetCore.Mvc;
using SurveyAPI.Interfaces;
using SurveyAPI.Models;
using System.Collections.Generic;

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

        //public IActionResult Index()
        //{
        //    return View();
        //}

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

        [HttpPost]
        public JsonResult AddSurvey([FromBody] Survey survey)
        {
            Survey sr = _surveyService.AddSurvey(survey);
            return Json(sr);
        }

        [HttpDelete]
        [Route("{surveyId}")]
        public JsonResult DeleteSurvey(int surveyId)
        {
            _surveyService.DeleteSurvey(surveyId);
            return Json(Ok());
        }

        [HttpPost]
        [Route("AddParticipant")]

        public JsonResult AddParticipant([FromBody] Participant participant)
        {
            Participant p = _surveyService.AddParticipant(participant);
            return Json(p);
        }


        [HttpPost]
        [Route("AddAnswer")]

        public ActionResult<Answer> AddAnswer([FromBody] Answer answer)
        {

            Answer a = _surveyService.AddAnswer(answer);
            if(a==null)
            {
                return Ok("Odgovor je vec jednom zabelezen!");
            }
            return Ok(Json(a));
        }



        [HttpPost]
        [Route("AddQuestion")]

        public JsonResult AddQuestion([FromBody] Question question)
        {
            Question q = _surveyService.AddQuestion(question);
            return Json(q);
        }

        [HttpPost]
        [Route("AddGeneralInformations")]

        public JsonResult AddGeneralInformations([FromBody] Survey survey)
        {
            Survey q = _surveyService.AddGeneralInformations(survey);
            return Json(q);
        }



        [HttpPost]
        [Route("AddOfferedAnswer")]

        public JsonResult AddOfferedAnswer([FromBody] OfferedAnswer offeredAnswer)
        {
            OfferedAnswer q = _surveyService.AddOfferedAnswer(offeredAnswer);
            return Json(q);
        }

        [HttpPost]
        [Route("AddResult")]
        public JsonResult AddSurveyResult([FromBody] SurveyResultAdd survey)
        {
            SurveyResultAdd s = _surveyService.AddSurveyAnswer(survey);
            return Json(s);

        }

        [HttpGet]
        [Route("GetSurveys")]
        public JsonResult GetGeneralInformations()
        {
            List<Survey> s = _surveyService.getSurveys();
            return Json(s);
        }

        [HttpGet]
        [Route("GetParticipant")]

        public JsonResult GetParticipant(int id)
        {
            Participant p = _surveyService.GetParticipant(id);
            return Json(p);
        }

        [HttpGet]
        [Route("GetOfferedAnswers")]

        public JsonResult GetOfferedAnswers()
        {
           List<OfferedAnswer> p = _surveyService.GetOfferedAnswers();
            return Json(p);
        }

        [HttpGet]
        [Route("GetQuestions")]

        public JsonResult GetQuestions()
        {
            List<Question> p = _surveyService.GetQuestions();
            return Json(p);
        }



    }
}
