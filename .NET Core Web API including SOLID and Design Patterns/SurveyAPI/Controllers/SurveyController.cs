using Microsoft.AspNetCore.Mvc;
using SurveyAPI.Interfaces;
using SurveyAPI.Models;
using System;
using static SurveyAPI.Models.OfferedAnswers;

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

        [HttpGet]
        [Route("{surveyId}/Answers")]
        public JsonResult GetSurveyResult(int surveyId)
        {
            return Json(_surveyService.GetSurveyResults(surveyId));
        }

        [HttpPost]
        [Route("SurveyResult/Add")]
        public JsonResult AddSurveyResult([FromBody] Answers surveyResult)
        {
            Answers sr = _surveyService.AddSurveyResult(surveyResult);
            return Json(sr);
        }
        

        [HttpGet]
        [Route("{surveyId}")]
        public JsonResult GetSurvey(int surveyId)
        {
            return Json(_surveyService.GetSurvey(surveyId));
        }

        [HttpGet]
        [Route("{surveyId}/OfferedAnswers")]
        public JsonResult GetOfferedAnswersForSurvey(int surveyId)
        {
            return Json(_surveyService.GetOfferedAnswersForSurvey(surveyId));
        }

        [HttpPost]
        [Route("NewSurvey/Add")]
        public JsonResult AddSurvey([FromBody] SurveyAddModel survey)
        {
            SurveyAddModel sr = _surveyService.AddSurvey(survey);
            return Json(sr);
        }

        [HttpPost]
        [Route("QuestionAndAnswers/Add")]
        public JsonResult AddQuestionWithAnswers([FromBody] QuestionAndAnswers questionAndAnswers)
        {
            QuestionAndAnswers qA = _surveyService.AddQuestionWithAnswers(questionAndAnswers);
            return Json(qA);
        }

        [HttpPost]
        [Route("OfferedAnswerResult/Add")]
        public JsonResult AddOfferedAnswersForQuestion([FromBody] OfferedAnswerResult offeredAnswerResult)
        {
            OfferedAnswerResult oA = _surveyService.AddOfferedAnswersForQuestion(offeredAnswerResult);
            return Json(oA);
        }

        [HttpDelete]
        [Route("{surveyId}/Delete")]
        public JsonResult DeleteSurvey(int surveyId)
        { 
            _surveyService.DeleteSurvey(surveyId);
            return Json(Ok());
        }

        [HttpGet]
        [Route("{surveyId}/GetSurveyQuestions")]
        public JsonResult GetSurveyQuestions(int surveyId)
        {
            return Json(_surveyService.GetSurveyQuestions(surveyId));
        }

        [HttpPost]
        [Route("Participant/Add")]
        public JsonResult AddParticipant([FromBody] Participant participant)
        {
            Participant p = _surveyService.AddParticipant(participant);
            return Json(p);
        }

        [HttpGet]
        [Route("{surveyId}/General")]
        public JsonResult GetGeneralInformations(int surveyId)
        {
            return Json(_surveyService.GetGeneralInformations(surveyId));
        }

        [HttpGet]
        [Route("Survey/AllInformations")]
        public JsonResult GetAllGeneralInformations()
        {
            return Json(_surveyService.GetAllGeneralInformations());
        }

        [HttpGet]
        [Route("Survey/GetOfferedAnswers")]
        public JsonResult GetOfferedAnswers()
        {
            return Json(_surveyService.GetOfferedAnswers());
        }

        [HttpGet]
        [Route("Survey/GetLastParticipantAdded")]
        public JsonResult GetLastParticipantAdded()
        {
            return Json(_surveyService.GetLastParticipantAdded());
        }

        [HttpPost]
        [Route("GeneralInformations/Add")]
        public JsonResult AddGeneralInformations([FromBody] GeneralInformations generalInformations)
        {
            GeneralInformations p = _surveyService.AddGeneralInformations(generalInformations);
            return Json(p);
        }
    }
}
