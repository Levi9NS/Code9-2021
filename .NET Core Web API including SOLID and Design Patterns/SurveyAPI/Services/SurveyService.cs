using SurveyAPI.Common;
using SurveyAPI.DBModels;
using SurveyAPI.Interfaces;
using SurveyAPI.Models;
using SurveyAPI.Models.ServiceModels;
using SurveyAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using static SurveyAPI.Models.OfferedAnswerResultServiceModel;

namespace SurveyAPI.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly IGeneraInformationsRepository _generaInformationsRepository;
        private readonly IAnswersRepository _answersRepository;
        private readonly IOfferedAnswersRepository _offeredAnswersRepository;
        private readonly IQuestionsRepository _questionsRepository;
        private readonly ISurveyQuestionRelationRepository _surveyQuestionRelationRepository;
        private readonly IParticipantsRepository _participantsRepository;
        private readonly IQuestionOfferedAnswerRelationRepository _questionOfferedAnswerRelationRepository;

        public SurveyService(IGeneraInformationsRepository generaInformationsRepository,
                            IAnswersRepository answersRepository,
                            IOfferedAnswersRepository offeredAnswersRepository,
                            IQuestionsRepository questionsRepository,
                            ISurveyQuestionRelationRepository surveyQuestionRelationRepository,
                            IParticipantsRepository participantsRepository,
                            IQuestionOfferedAnswerRelationRepository questionOfferedAnswerRelationRepository)
        {
            _generaInformationsRepository = generaInformationsRepository;
            _answersRepository = answersRepository;
            _offeredAnswersRepository = offeredAnswersRepository;
            _questionsRepository = questionsRepository;
            _surveyQuestionRelationRepository = surveyQuestionRelationRepository;
            _participantsRepository = participantsRepository;
            _questionOfferedAnswerRelationRepository = questionOfferedAnswerRelationRepository;
        }


        public async Task<GenericResponseModel<SurveyServiceModel>> GetAll()
        {
            var surveys = await _generaInformationsRepository.GetAllAsync();
            return new GenericResponseModel<SurveyServiceModel>
            {
                DataList = surveys.Select(s => new SurveyServiceModel
                {
                    Id=s.Id,
                    Name = s.Description,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate
                }).ToList()
            };
        }

        public async Task<GenericResponseModel<SurveyResultServiceModel>> GetSurveyResults(int surveyId)
        {
            var survey = await _generaInformationsRepository.GetByIdAsync(surveyId);
            if (survey == null)
            {
                return new GenericResponseModel<SurveyResultServiceModel>
                {
                    IsSuccessful = false,
                    ErrorMessage = Messages.SURVEY_ID_NULL
                };
            }
            var offeredAnswers = await GetOfferedAnswersForSurvey(surveyId);
            var survayResult = new SurveyResultServiceModel
            {
                Name = survey.Description,
                Questions = offeredAnswers.Data.OfferedAnswers.Select( item => new AnsweredQuestionServiceModel
                {
                    questionId = item.QuestionId,
                    questionText = item.QuestionText,
                    response = item.QuestionAnswer,
                    count = _answersRepository.GetCountForQuestionAnswer(surveyId, item.QuestionId, item.OfferedAnswerId)
                }).ToList()
            };

            return new GenericResponseModel<SurveyResultServiceModel>
            {
                IsSuccessful = true,
                Data = survayResult
            };
        }


        public async Task<GenericResponseModel<SurveyServiceModel>> GetSurveyQuestions(int surveyId)
        {
            var survey = await _generaInformationsRepository.GetByIdAsync(surveyId);
            if (survey == null)
            {
                return new GenericResponseModel<SurveyServiceModel>
                {
                    IsSuccessful = false,
                    ErrorMessage = Messages.SURVEY_ID_NULL
                };
            }
            var survay = new SurveyServiceModel
            {
                Id = survey.Id,
                StartDate = survey.StartDate,
                EndDate = survey.EndDate,
                Name = survey.Description,
                Questions = survey.SurveyQuestionRelations.Select(item =>new QuestionServiceModel
                    {
                        QuestionText = item.Question.QuestionText,
                        Id = item.Question.Id,
                        OfferedAnswers = item.Question.QuestionOfferedAnswerRelations.Select(oa => new OfferedAnswerServiceModel 
                        { 
                            Id=oa.OfferedAnswerId,
                            Text=oa.OfferedAnswer.Text
                        }).ToList()
                    }).ToList()
            };

            return new GenericResponseModel<SurveyServiceModel>
            {
                IsSuccessful = true,
                Data = survay
            };
        }



        public async Task<GenericResponseModel<OfferedAnswerResultServiceModel>> GetOfferedAnswersForSurvey(int surveyId)
        {
            var survey = await _generaInformationsRepository.GetByIdAsync(surveyId);
            if (survey == null)
            {
                return new GenericResponseModel<OfferedAnswerResultServiceModel>
                {
                    IsSuccessful = false,
                    ErrorMessage = Messages.SURVEY_ID_NULL
                };
            }
            var questions = await _questionsRepository.GetBySurveyId(surveyId);
            if (questions == null)
            {
                return new GenericResponseModel<OfferedAnswerResultServiceModel>
                {
                    IsSuccessful = false,
                    ErrorMessage = Messages.QUESTION_NOT_FOUND_FOR_SURVEY
                };
            }

            var offeredAnswers = new OfferedAnswerResultServiceModel
            {
                OfferedAnswers = questions.SelectMany(q=>
                    q.QuestionOfferedAnswerRelations.Select(qa =>
                        new AnswerResultServiceModel
                        {
                            OfferedAnswerId = qa.OfferedAnswer.Id,
                            QuestionId= qa.QuestionId,
                            QuestionAnswer= qa.OfferedAnswer.Text,
                            QuestionText = qa.Question.QuestionText
                        }).ToList()
                ).ToList()
            };

            return new GenericResponseModel<OfferedAnswerResultServiceModel>
            {
                IsSuccessful = true,
                Data = offeredAnswers
            };
        }



        public async Task<GenericResponseModel<SurveyServiceModel>> CreateSurvey(SurveyServiceModel survey)
        {
            var newSurvey = new GeneralInformation
            {
                CreatedBy = "User",
                CreateDate = DateTime.Now,
                Description = survey.Name,
                StartDate = survey.StartDate,
                EndDate = survey.EndDate,
                IsOpen = true
            };

            var insertedSurvey = await _generaInformationsRepository.InsertAsync(newSurvey);
            if (insertedSurvey == null)
            {
                return new GenericResponseModel<SurveyServiceModel>
                {
                    IsSuccessful = false,
                    ErrorMessage = Messages.SURVEY_CREATION_ERROR
                };
            }
            //We are passing a list of QuestionServiceModel-s so we could get the same list just with Id values
            var insertedQuestions = await _questionsRepository.InsertQuestionsListAsync(survey.Questions);
            if (insertedQuestions == null)
            {
                return new GenericResponseModel<SurveyServiceModel>
                {
                    IsSuccessful = false,
                    ErrorMessage = Messages.QUESTION_CREATION_ERROR
                };
            }
            await _generaInformationsRepository.SaveAsync();

            var insertedSurveyQuestionRelationships = await _surveyQuestionRelationRepository
                .AddRelationships(insertedSurvey.Id, insertedQuestions.Select(item => item.Id ).ToList());
            if (insertedSurveyQuestionRelationships == null)
            {
                return new GenericResponseModel<SurveyServiceModel>
                {
                    IsSuccessful = false,
                    ErrorMessage = Messages.SURVEY_QUESTION_RELATION_CREATION_ERROR
                };
            }

            //Insert Offered Answers and their relationship
            foreach (var question in insertedQuestions)
            {
                List<OfferedAnswer> offeredAnswerList = new List<OfferedAnswer>();
                List<OfferedAnswer> existingOfferedAnswer = new List<OfferedAnswer>();

                List<Models.ServiceModels.OfferedAnswerServiceModel> insertedAnswersWithIds = new List<Models.ServiceModels.OfferedAnswerServiceModel>();

                var offeredAnswersIds = new List<int>();

                foreach (var answer in question.OfferedAnswers)
                {
                    //Check for existing offered answer with the same text
                    var checkExisting = await _offeredAnswersRepository.GetByText(answer.Text);
                    if(checkExisting != null)
                    {
                        existingOfferedAnswer.Add(checkExisting);
                        offeredAnswersIds.Add(checkExisting.Id);//Add existing for relation

                        insertedAnswersWithIds.Add(new Models.ServiceModels.OfferedAnswerServiceModel
                        {
                            Id = checkExisting.Id,
                            Text = checkExisting.Text
                        });

                    }
                    else
                    {
                        var offeredAnswer = new OfferedAnswer
                        {
                            CreateDate = DateTime.Now,
                            CreatedBy = "User",
                            Text = answer.Text
                        };
                        offeredAnswerList.Add(offeredAnswer);
                    }
                }

                var insertedOfferedAnswers = await _offeredAnswersRepository.InsertOfferedAnswersListAsync(offeredAnswerList);
                await _offeredAnswersRepository.SaveAsync();

                //ID-s for relation
                offeredAnswersIds.AddRange(insertedOfferedAnswers.Select(item => item.Id).ToList());//Add new ids for relation
               

                var insertedQuestionAnswersRelathionship = await _questionOfferedAnswerRelationRepository
                    .AddRelationships(question.Id, offeredAnswersIds);


                //Add inserted offered answers with Id-s
                insertedAnswersWithIds.AddRange(insertedOfferedAnswers.Select(item => new OfferedAnswerServiceModel
                    {
                        Id = item.Id,
                        Text = item.Text
                    }).ToList());

                //Return answers with Id-s
                question.OfferedAnswers = insertedAnswersWithIds;

            }

            await _offeredAnswersRepository.SaveAsync();

            return new GenericResponseModel<SurveyServiceModel>
            {
                IsSuccessful = true,
                Data = new SurveyServiceModel
                {
                    Id = insertedSurvey.Id,
                    Name = insertedSurvey.Description,
                    StartDate = insertedSurvey.StartDate,
                    EndDate = insertedSurvey.EndDate,
                    Questions = insertedQuestions.ToList()
                }
            };

        }




        public async Task<GenericResponseModel<SubmitSurveyServiceModel>> SubmitSurvey(SubmitSurveyServiceModel submit)
        {

            var validate = await ValidateSubmitAsync(submit);
            if(validate != null)
            {
                return validate;
            }

            var participant = new Participant
            {
                FirstName = submit.Participant.FirstName,
                LastName = submit.Participant.LastName,
                Email = submit.Participant.Email,
                Password = submit.Participant.Password,
                SurveyId = submit.Participant.SurveyId,
                CreateDate = DateTime.Now,
                CreatedBy = "User"
            };

            var insertedParticipant = await _participantsRepository.InsertAsync(participant);
            if (insertedParticipant == null)
            {
                return new GenericResponseModel<SubmitSurveyServiceModel>
                {
                    IsSuccessful = false,
                    ErrorMessage = Messages.PARTICIPANT_CREATION_ERROR
                };
            }
            
            await _participantsRepository.SaveAsync();

            List<Answer> insertedAnsvers = new List<Answer>();
            foreach(var answer in submit.Answers)
            {
                var insert = await _answersRepository.InsertAsync(new Answer
                {
                    ParticipantId = insertedParticipant.Id,
                    SurveyId = answer.SurveyId,
                    QuestionId = answer.QuestionId,
                    QuestionAnswersId = answer.QuestionAnswersId,
                    CreateDate = DateTime.Now,
                    CreatedBy = "User"
                });
                insertedAnsvers.Add(insert);
            }

            await _answersRepository.SaveAsync();

            return new GenericResponseModel<SubmitSurveyServiceModel>
            {
                IsSuccessful = true,
                Data = new SubmitSurveyServiceModel
                {
                    Participant = new ParticipantServiceModel
                    {
                        Id = insertedParticipant.Id,
                        FirstName = insertedParticipant.FirstName,
                        LastName = insertedParticipant.LastName,
                        Email = insertedParticipant.Email,
                        Password = insertedParticipant.Password,
                        SurveyId = insertedParticipant.SurveyId
                    },
                    Answers = insertedAnsvers.Select(item => new AnswerServiceModel
                    {
                        Id = item.Id,
                        ParticipantId = item.ParticipantId,
                        SurveyId = item.SurveyId,
                        QuestionId = item.QuestionId,
                        QuestionAnswersId = item.QuestionAnswersId
                    }).ToList()

                }
            };
        }

        private async Task<GenericResponseModel<SubmitSurveyServiceModel>> ValidateSubmitAsync(SubmitSurveyServiceModel submit)
        {
            //Check if all answers are for the same survey
            if (submit.Answers.Any(o => o.SurveyId != submit.Answers[0].SurveyId))
            {
                return new GenericResponseModel<SubmitSurveyServiceModel>
                {
                    IsSuccessful = false,
                    ErrorMessage = Messages.ANSWER_NOT_FOR_SAME_SURVEY
                };
            }

            //Checking survey
            var checkSurveyFromParticipant = await _generaInformationsRepository.GetByIdAsync(submit.Participant.SurveyId);
            var checkSurveyFromAnswers = await _generaInformationsRepository.GetByIdAsync(submit.Answers[0].SurveyId);
            if (checkSurveyFromParticipant == null || checkSurveyFromAnswers == null)
            {
                return new GenericResponseModel<SubmitSurveyServiceModel>
                {
                    IsSuccessful = false,
                    ErrorMessage = Messages.SURVEY_ID_NULL
                };
            }

            //Check for number of answers
            var questionsForSurvey = await _questionsRepository.GetBySurveyId(submit.Participant.SurveyId);
            if (questionsForSurvey.Count() != submit.Answers.Count())
            {
                return new GenericResponseModel<SubmitSurveyServiceModel>
                {
                    IsSuccessful = false,
                    ErrorMessage = Messages.ANSWER_NOT_ALL_SUBMITED
                };
            }

            //Check for repeated questions
            if(submit.Answers.Select(a=>a.QuestionId).ToList().Distinct().Count() != submit.Answers.Count())
            {
                return new GenericResponseModel<SubmitSurveyServiceModel>
                {
                    IsSuccessful = false,
                    ErrorMessage = Messages.QUESTION_REPEATED_ERROR
                };
            }

            //Checking relationships
            foreach (var answer in submit.Answers)
            {
                var checkQuestionSurvey = _surveyQuestionRelationRepository.CheckQuestionForServey(answer.QuestionId, answer.SurveyId);
                if (!checkQuestionSurvey)
                {
                    return new GenericResponseModel<SubmitSurveyServiceModel>
                    {
                        IsSuccessful = false,
                        ErrorMessage = Messages.SURVEY_QUESTION_RELATION_NO_RELATION
                    };
                }

                var checkQuestionAnswer = _questionOfferedAnswerRelationRepository.CheckOfferedAnswerForQuestion(answer.QuestionId, answer.QuestionAnswersId);
                if (!checkQuestionAnswer)
                {
                    return new GenericResponseModel<SubmitSurveyServiceModel>
                    {
                        IsSuccessful = false,
                        ErrorMessage = Messages.QUESTION_ANSWER_NO_RELATION
                    };
                }
            }

            return null;
        }


        public async Task<GenericResponseModel<SurveyServiceModel>> DeleteSurvey(int surveyId)
        {
            var deletedServeyQuestionRelationship = _surveyQuestionRelationRepository.DeleteBySurveyId(surveyId);
            if (!deletedServeyQuestionRelationship)
            {
                return new GenericResponseModel<SurveyServiceModel>
                {
                    IsSuccessful = false,
                    ErrorMessage = Messages.SURVEY_QUESTION_RELATION_DELETE_ERROR
                };
            }

            //There is no checking because the tables can be empty
            var deletedParticipants = _participantsRepository.DeleteBySurveyId(surveyId);
            var deletedAnswers = _answersRepository.DeleteBySurveyId(surveyId);

            var deletedGeneralInfo = _generaInformationsRepository.Delete(surveyId);
            if (deletedGeneralInfo == null)
            {
                return new GenericResponseModel<SurveyServiceModel>
                {
                    IsSuccessful = false,
                    ErrorMessage = Messages.SURVEY_DELETE_ERROR
                };
            }
            await _generaInformationsRepository.SaveAsync();

            return new GenericResponseModel<SurveyServiceModel>
            {
                IsSuccessful = true,
                Data = new SurveyServiceModel
                {
                    Id = deletedGeneralInfo.Id,
                    StartDate = deletedGeneralInfo.StartDate,
                    EndDate = deletedGeneralInfo.EndDate,
                    Name = deletedGeneralInfo.Description
                }
            };
        }
    }
}
