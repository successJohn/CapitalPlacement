using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTask.Infrastructure.Services
{
    public class ProgramFormService
    {
        private readonly Container _programDetailContainer;

        public ProgramFormService(CosmosClient cosmosClient, IConfiguration configuration)
        {
            var databaseName = configuration["CosmosDbSettings:DatabaseName"];
            _programDetailContainer = cosmosClient.GetContainer(databaseName, "ProgramDetail");
        }
        public async Task<Guid> CreateProgram(CreateProgramDTO model)
        {
            var programId = Guid.NewGuid();
            var personalInfoId = Guid.NewGuid();
            var program = new ProgramDetail
            {
                id = programId,
                Title = model.Title,
                Description = model.Description,

                personalInformation = new PersonalInformation
                {
                    Id = personalInfoId,
                    FirstName = model.personalInformation?.FirstName,
                    LastName = model.personalInformation?.LastName,
                    Email = model.personalInformation?.Email,
                    Nationality = model.personalInformation?.Nationality,
                    DateOfBirth = model.personalInformation!.DateOfBirth,
                    IdNumber = model.personalInformation?.IdNumber,
                    CurrentResidence = model.personalInformation?.CurrentResidence

                },
                PersonalInformationId = personalInfoId/*,
                CustomQuestions = model.createQuestionDTO.Select(x => new Question
                {

                }).ToList(),*/

            };

            await _programDetailContainer.CreateItemAsync(program);

            return program.id;
        }

        public async Task<ProgramDetail> GetApplicationForm(Guid id)
        {

            var applicationForm = await _programDetailContainer.ReadItemAsync<ProgramDetail>(id.ToString(), new PartitionKey(id.ToString()));


            return applicationForm.Resource;

        }
    }
}
