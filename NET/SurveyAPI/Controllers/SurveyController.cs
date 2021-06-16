﻿using Microsoft.AspNetCore.Mvc;
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

        /*Ometa swagger
        public IActionResult Index()
        {
            return View();
        }
        */
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
        [Route("OfferedAnswers")]
        public JsonResult GetOfferedAnswers()
        {
            return Json(_surveyService.GetOfferedAnswers());
        }

        [HttpGet()]
        [Route("{surveyId}/OfferedAnswers")]
        public JsonResult GetOfferedAnswersForSurvey(int surveyId)
        {
            return Json(_surveyService.GetOfferedAnswersForSurvey(surveyId));
        }

        [HttpPost]
        [Route("AddSurvey")]
        public JsonResult AddSurvey([FromBody] Survey survey)
        {
            Survey sr = _surveyService.AddSurvey(survey);
            return Json(sr);
        }

        [HttpPost]
        [Route("OfferedAnswerRelation")]
        public JsonResult OfferedAnswerRelation([FromBody] QuestionOfferedAnswerRelation offeredAnswerRelation)
        {
            QuestionOfferedAnswerRelation sr = _surveyService.OfferedAnswerRelation(offeredAnswerRelation);
            return Json(sr);
        }

        [HttpPost]
        [Route("Participants")]
        public JsonResult AddSurveyParticipant([FromBody] Participant participant)
        {
            Participant p = _surveyService.AddSurveyParticipant(participant);
            return Json(p);
        }

        [HttpPost]
        [Route("Questions")]
        public JsonResult AddSurveyQuestion([FromBody] Question question)
        {
            Question q = _surveyService.AddSurveyQuestion(question);
            return Json(q);
        }

        [HttpPost]
        [Route("OfferedAnswers")]
        public JsonResult AddOfferedAnswer([FromBody] OfferedAnswer offeredAnswer)
        {
            OfferedAnswer oa = _surveyService.AddOfferedAnswer(offeredAnswer);
            
            return Json(oa);
            
        }

        [HttpPost]
        [Route("Answers")]
        public JsonResult AddSurveyResult([FromBody] Answer answer)
        {
           
            Answer a = _surveyService.AddSurveyAnswer(answer);
            return Json(a);
        }

        [HttpDelete]
        [Route("{surveyId}")]
        public JsonResult DeleteSurvey(int surveyId)
        {
            _surveyService.DeleteSurvey(surveyId);
            return Json(Ok());
        }
    }
}