using CapitalPlacementTask.Application.DTO.ProgramForm;
using CapitalPlacementTask.Application.Interfaces;
using CapitalPlacementTask.Application.Utils;
using CapitalPlacementTask.Domain.Entities;
using CapitalPlacementTask.Domain.Enums;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTask.Infrastructure.Services
{
    public class ProgramFormService : IProgramFormService
    {
        private readonly Container _programDetailContainer;

        public ProgramFormService(CosmosClient cosmosClient, IConfiguration configuration)
        {
            var databaseName = configuration["CosmosDbSettings:DatabaseName"];
            _programDetailContainer = cosmosClient.GetContainer(databaseName, "ProgramDetail");
        }

        public async Task<BaseResponse<string>> CreateAsync(CreateProgramFormDto programDto)
        {
            var program = new ProgramForm
            {
                id = Guid.NewGuid(),
                Title = programDto.Title,
                Description = programDto.Description,
                PersonalInformation = new PersonalInformation
                {
                    FirstName = programDto.PersonalInformation.FirstName,
                    LastName = programDto.PersonalInformation.LastName,
                    Email = programDto.PersonalInformation.Email,
                    Phone = programDto.PersonalInformation.Phone,
                    Nationality = programDto.PersonalInformation.Nationality

                }
            };

            // creates custom questions to add to the personal information
            var personalInformationQuestion = CreateAdditionalQuestions(programDto.PersonalInformation.CustomQuestions);
            if (personalInformationQuestion.Errors.Any())
            {
                return new BaseResponse<string>(personalInformationQuestion.ResponseMessage, 400);
            }

            program.PersonalInformation.CustomQuestions.AddRange(personalInformationQuestion.Data);

            program.id = Guid.NewGuid();
            await _programDetailContainer.CreateItemAsync(program);

            return new BaseResponse<string>("Created");
        }

        private BaseResponse<List<CustomQuestion>> CreateAdditionalQuestions(List<QuestionDTO> additionalQuestions)
        {
            List<CustomQuestion> customQuestions = new();

            var response = new BaseResponse<List<CustomQuestion>>();

            foreach (var questionDto in additionalQuestions)
            {
                var question = CreateCustomQuestion(questionDto);

                if (questionDto.QuestionType == QuestionType.Dropdown || questionDto.QuestionType == QuestionType.MultiChoice)
                {
                    //multichoice and dropdown questions must have more than one choice
                    if (questionDto.Choices == null || questionDto.Choices.Count < 1)
                    {
                        response.ResponseMessage = "you must select more than one choice";
                        return response;
                    }
                    question = CreateQuestionsWithMultipleChoices(question, questionDto);// this works for dropdown and multiple choice questions
                }

                customQuestions.Add(question);
            }

            response.Data = customQuestions;
            return response;
        }

        private CustomQuestion CreateCustomQuestion(QuestionDTO questionDto)
        {
            var question = new CustomQuestion
            {
                id = Guid.NewGuid(),
                Question = questionDto.Question,
                Type = questionDto.QuestionType,
                MaxChoices = questionDto.MaxChoices,
                HasOtherOption = questionDto.HasOtherOption,
               
            };

            return question;
        }

        private CustomQuestion CreateQuestionsWithMultipleChoices(CustomQuestion question, QuestionDTO questionDto)
        {
            var choices = questionDto.Choices!.Select(x => new QuestionChoices
            {
                id = Guid.NewGuid(),
                Choice = x,
                CustomQuestionId = question.id
            });

            question.Choices.AddRange(choices);
            return question;
        }


        public async Task<BaseResponse<bool>> UpdateAsync(Guid id, CreateProgramFormDto programDto)
        {
            var applicationForm = await _programDetailContainer.ReadItemAsync<ProgramForm>(id.ToString(), new PartitionKey(id.ToString()));

            if (applicationForm == null)
            {
                return new BaseResponse<bool>("Form not found");
            }

            applicationForm.Resource.Title = programDto.Title;
            applicationForm.Resource.Description = programDto.Description;
            applicationForm.Resource.PersonalInformation.FirstName = string.IsNullOrWhiteSpace(programDto.PersonalInformation.FirstName) ? applicationForm.Resource.PersonalInformation.FirstName : programDto.PersonalInformation.FirstName;
            applicationForm.Resource.PersonalInformation.LastName = string.IsNullOrWhiteSpace(programDto.PersonalInformation.LastName) ? applicationForm.Resource.PersonalInformation.LastName : programDto.PersonalInformation.LastName;
            applicationForm.Resource.PersonalInformation.Email = string.IsNullOrWhiteSpace(programDto.PersonalInformation.Email) ? applicationForm.Resource.PersonalInformation.Email : programDto.PersonalInformation.Email;
            applicationForm.Resource.PersonalInformation.Phone = string.IsNullOrWhiteSpace(programDto.PersonalInformation.Phone) ? applicationForm.Resource.PersonalInformation.Phone : programDto.PersonalInformation.Phone;
            applicationForm.Resource.PersonalInformation.Nationality = string.IsNullOrWhiteSpace(programDto.PersonalInformation.Nationality) ? applicationForm.Resource.PersonalInformation.Nationality : programDto.PersonalInformation.Nationality;
            applicationForm.Resource.PersonalInformation.CurrentResidence = string.IsNullOrWhiteSpace(programDto.PersonalInformation.CurrentResidence) ? applicationForm.Resource.PersonalInformation.CurrentResidence : programDto.PersonalInformation.CurrentResidence;
            applicationForm.Resource.PersonalInformation.IdNumber = string.IsNullOrWhiteSpace(programDto.PersonalInformation.IdNumber) ? applicationForm.Resource.PersonalInformation.IdNumber : programDto.PersonalInformation.IdNumber;
            applicationForm.Resource.PersonalInformation.DateOfBirth = programDto.PersonalInformation.DateOfBirth;
            // validate questions in the put method
            var questionsFormat = CreateAdditionalQuestions(programDto.PersonalInformation.CustomQuestions).Data;
            applicationForm.Resource.PersonalInformation!.CustomQuestions.AddRange(questionsFormat);
            await _programDetailContainer.ReplaceItemAsync(applicationForm.Resource, applicationForm.Resource.id.ToString());
            return new BaseResponse<bool>(true);
        }
    }
}

   
