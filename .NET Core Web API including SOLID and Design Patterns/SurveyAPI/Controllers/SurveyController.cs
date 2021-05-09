using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SurveyAPI.Models;
using SurveyAPI.Services;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SurveyAPI.Controllers
{
    public class SurveyController : Controller
    {
        private readonly SurveyService _survey_service;
        private readonly IConfiguration _configuration;

        public SurveyController(SurveyService _svc, IConfiguration configuration)
        {
            _survey_service = _svc;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Details(string surveyName)
        {
            SurveyResult sr = _survey_service.GetSurveyResults(_configuration.GetConnectionString("SQLConnection"), surveyName);
            return Json(sr);
        }
    }
}
