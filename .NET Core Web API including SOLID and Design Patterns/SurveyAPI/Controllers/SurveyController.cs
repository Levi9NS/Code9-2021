using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyAPI.Interfaces;
using SurveyAPI.Models;
using SurveyAPI.Models.ControllerModels;
using SurveyAPI.Models.ServiceModels;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpGet()]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _surveyService.GetAll();

            return Ok(result.DataList);
        }

        [HttpGet()]
        [Route("{surveyId}/result")]
        public async Task<IActionResult> GetSurveyResult(int surveyId)
        {
            var result = await _surveyService.GetSurveyResults(surveyId);

            if (!result.IsSuccessful)
            {
                ErrorResponseModel errorResponse = new ErrorResponseModel
                {
                    ErrorMessage = result.ErrorMessage,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(errorResponse);
            }

            return Ok(result.Data);
        }

        [HttpGet()]
        [Route("{surveyId}/questions")]
        public async Task<IActionResult> GetSurveyQuestions(int surveyId)
        {
            var servey = await _surveyService.GetSurveyQuestions(surveyId);

            if (!servey.IsSuccessful)
            {
                ErrorResponseModel errorResponse = new ErrorResponseModel
                {
                    ErrorMessage = servey.ErrorMessage,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(errorResponse);
            }

            return Ok(servey.Data);
        }

        [HttpGet()]
        [Route("{surveyId}/offered-answers")]
        public async Task<IActionResult> GetOfferedAnswersForSurvey(int surveyId)
        {
            var offeredAnswers = await _surveyService.GetOfferedAnswersForSurvey(surveyId);

            if (!offeredAnswers.IsSuccessful)
            {
                ErrorResponseModel errorResponse = new ErrorResponseModel
                {
                    ErrorMessage = offeredAnswers.ErrorMessage,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(errorResponse);
            }

            return Ok(offeredAnswers.Data);
        }

        [HttpPost]
        [Route("submit")]
        public async Task<IActionResult> SubmitServey([FromBody] SubmitSurveyModel submit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var submitModel = new SubmitSurveyServiceModel
            {
                Participant = new ParticipantServiceModel
                {
                    FirstName = submit.Participant.FirstName,
                    LastName = submit.Participant.LastName,
                    Email = submit.Participant.Email,
                    Password = submit.Participant.Password,
                    SurveyId = submit.Participant.SurveyId
                },
                Answers = submit.Answers.Select(a => new AnswerServiceModel
                {
                    SurveyId = a.SurveyId,
                    QuestionId = a.QuestionId,
                    QuestionAnswersId = a.QuestionAnswersId
                }).ToList()
            };

            GenericResponseModel<SubmitSurveyServiceModel> submitetSurvey;
            try
            {
                submitetSurvey = await _surveyService.SubmitSurvey(submitModel);
            }
            catch (DbUpdateException e)
            {
                ErrorResponseModel errorResponse = new ErrorResponseModel
                {
                    ErrorMessage = e.InnerException.Message ?? e.Message,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(errorResponse);
            }

            if (!submitetSurvey.IsSuccessful)
            {
                ErrorResponseModel errorResponse = new ErrorResponseModel
                {
                    ErrorMessage = submitetSurvey.ErrorMessage,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(errorResponse);
            }

            return Ok(submitetSurvey.Data);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateSurvey([FromBody] CreateSurvey createSurvey)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var surveyModel = new SurveyServiceModel
            {
                Name = createSurvey.Name,
                StartDate = createSurvey.StartDate,
                EndDate = createSurvey.EndDate,
                Questions = createSurvey.Questions.Select(q => new QuestionServiceModel 
                { 
                    QuestionText = q.QuestionText,
                    OfferedAnswers = q.OfferedAnswers.Select(oa => new OfferedAnswerServiceModel
                    {
                       Text = oa.Text
                    }).ToList()  
                }).ToList()
            };

            GenericResponseModel<SurveyServiceModel> createdSurvey;
            try
            {
                createdSurvey = await _surveyService.CreateSurvey(surveyModel);
            }
            catch (DbUpdateException e)
            {
                ErrorResponseModel errorResponse = new ErrorResponseModel
                {
                    ErrorMessage = e.InnerException.Message ?? e.Message,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

                return BadRequest(errorResponse);
            }

            if (!createdSurvey.IsSuccessful)
            {
                ErrorResponseModel errorResponse = new ErrorResponseModel
                {
                    ErrorMessage = createdSurvey.ErrorMessage,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(errorResponse);
            }

            return CreatedAtAction(nameof(GetSurveyQuestions), new { Id = createdSurvey.Data.Id }, createdSurvey.Data);
        }

        [HttpDelete]
        [Route("{surveyId}")]
        public async Task<IActionResult> DeleteSurvey(int surveyId)
        {
            GenericResponseModel<SurveyServiceModel> deleted;
            try
            {
                deleted = await _surveyService.DeleteSurvey(surveyId);
            }
            catch (DbUpdateException e)
            {
                ErrorResponseModel errorResponse = new ErrorResponseModel
                {
                    ErrorMessage = e.InnerException.Message ?? e.Message,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(errorResponse);
            }

            if (!deleted.IsSuccessful)
            {
                ErrorResponseModel errorResponse = new ErrorResponseModel
                {
                    ErrorMessage = deleted.ErrorMessage,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(errorResponse);
            }

            return Ok(deleted.Data);
        }
    }
}
