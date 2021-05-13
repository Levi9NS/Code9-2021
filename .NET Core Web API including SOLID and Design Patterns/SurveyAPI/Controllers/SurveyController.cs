using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SurveyAPI.Interfaces;
using SurveyAPI.Models;
using System;

namespace SurveyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : Controller
    {
        private readonly ISurveyService _surveyService;
        private readonly IConfiguration _configuration;

        public SurveyController(ISurveyService _svc, IConfiguration configuration)
        {
            _surveyService = _svc;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet()]
        [Route("{surveyId}/Answers")]
        public JsonResult GetSurveyResult(int surveyId)
        {
            SurveyResult sr = _surveyService.GetSurveyResults(surveyId);
            return Json(sr);
        }

        [HttpGet()]
        [Route("{surveyId}")]
        public JsonResult GetSurvey(int surveyId)
        {
            Survey sr = _surveyService.GetSurveyQuestions(surveyId);
            return Json(sr);
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
    }
}
