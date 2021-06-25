﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public JsonResult AddSurveyResult([FromBody] Answers survey)
        {
            Answers sr = _surveyService.AddSurveyResult(survey);
            return Json(sr);
        }
        

        [HttpGet()]
        [Route("{surveyId}")]
        public JsonResult GetSurvey(int surveyId)
        {
            return Json(_surveyService.GetSurvey(surveyId));
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

        [HttpPost]
        public JsonResult AddQuestionWithAnswers([FromBody] QuestionAndAnswers qAndA)
        {
            QuestionAndAnswers qA = _surveyService.AddQuestionWithAnswers(qAndA);
            return Json(qA);
        }

        [HttpPost]
        public JsonResult AddOfferedAnswersForQuestion([FromBody] OfferedAnswerResult offeredAnswerResult)
        {
            OfferedAnswerResult oA = _surveyService.AddOfferedAnswersForQuestion(offeredAnswerResult);
            return Json(oA);
        }

        [HttpDelete]
        [Route("{surveyId}")]
        public JsonResult DeleteSurvey(int surveyId)
        {
            _surveyService.DeleteSurvey(surveyId);
            return Json(Ok());
        }

        [HttpGet()]
        [Route("{surveyId}/GetSurveyQuestions")]
        public JsonResult GetSurveyQuestions(int surveyId)
        {
            return Json(_surveyService.GetSurveyQuestions(surveyId));
        }

        [HttpPost]
        public JsonResult AddParticipant([FromBody] Participant participant)
        {
            Participant p = _surveyService.AddParticipant(participant);
            return Json(p);
        }

        [HttpGet()]
        [Route("{surveyId}/GetGeneralInformations")]
        public JsonResult GetGeneralInformations(int surveyId)
        {
            return Json(_surveyService.GetGeneralInformations(surveyId));
        }

        [HttpPost]
        public JsonResult AddGeneralInformations([FromBody] GeneralInformations survey)
        {
            GeneralInformations p = _surveyService.AddGeneralInformations(survey);
            return Json(p);
        }
    }
}
