using CapitalPlacementTask.Application.DTO.CandidateApplication;
using CapitalPlacementTask.Application.Interfaces;
using CapitalPlacementTask.Application.Utils;
using CapitalPlacementTask.Domain.Entities;
using CapitalPlacementTask.Domain.Enums;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTask.Infrastructure.Services
{
    public class CandidateApplicationService: IApplicationService
    {
        private readonly Container _programDetailContainer;
        private readonly Container _applicationFormContainer;

        public CandidateApplicationService(CosmosClient cosmosClient, IConfiguration configuration)
        {
            var databaseName = configuration["CosmosDbSettings:DatabaseName"];
            _applicationFormContainer = cosmosClient.GetContainer(databaseName, "CandidateApplication");
            _programDetailContainer = cosmosClient.GetContainer(databaseName, "ProgramDetail");
        }
        
        public async Task<BaseResponse<string>> ApplyAsync(Guid programId, CreateCandidateApplicationDto candidateApplicationDto)
        {
            var questionsAnswer = candidateApplicationDto.PersonalInformation?.AdditionalQuestions;
            // get the form details for which the candidate intends to apply to
            var programForm = await _programDetailContainer.ReadItemAsync<ProgramForm>(programId.ToString(), new PartitionKey(programId.ToString()));

            
            // if return an error if the program does not exist
            if (programForm == null)
            {
                return new BaseResponse<string>("Program form not found");
            }

            // Initialize the application with the program details and personal Info that are constant across all applications
            
            var newCandidateApplication = new CandidateApplication
            {
                ProgramId = programId,
                id = Guid.NewGuid(),
                PersonalInformation = new PersonalInformationAnswer
                {
                    FirstName = candidateApplicationDto.PersonalInformation.FirstName,
                    Email = candidateApplicationDto.PersonalInformation.Email,
                    LastName = candidateApplicationDto.PersonalInformation.LastName,
                    Gender = candidateApplicationDto.PersonalInformation.Gender,
                    Phone = candidateApplicationDto.PersonalInformation.Phone,
                    CurrentResidence = candidateApplicationDto.PersonalInformation.CurrentResidence,
                }
            };

            var customersAnswer = newCandidateApplication.PersonalInformation.AdditionalQuestions;

            // get all the custom questions attached to the Program Form for the application
            var customquestions = programForm.Resource.PersonalInformation?.CustomQuestions;

            foreach(var question in questionsAnswer)
            {
                var questionAnswer = CreateQuestionsAnswer(question);
                customersAnswer.Add(questionAnswer);
            }
            
            await _applicationFormContainer.CreateItemAsync(newCandidateApplication);

            return new BaseResponse<string>("Created");
        }


        private static CustomQuestionsAnswer CreateQuestionsAnswer(CustomQuestionsAnswerDto question)
        {
            return new CustomQuestionsAnswer
            {
                Type = question.Type,
                YesNo = question.Type == QuestionType.YesOrNo ? question.YesNoAnswer:null,
                Answer = question.Type == QuestionType.Paragraph ? question.ParagraphAnswer : null,
                DateAnswer = question.Type == QuestionType.Date ? question.DateAnswer : null,
                Number = question.Type == QuestionType.Number ? question.NumberAnswer: 0,
                Id = Guid.NewGuid().ToString(),
                QuestionId = question.QuestionId,
                Choices = question.Choices.Any() ? question.Choices.Select(x => new Choices
                {
                    SelectedChoiceId = x.SelectedChoiceId,
                }).ToList(): null
            };
        }
        
    }
}
