using CapitalPlacementTask.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTask.Application.DTO.ProgramForm
{
    public class CreateProgramFormDto
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required PersonalInformationDto PersonalInformation { get; set; }
    }

    public class PersonalInformationDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string Phone { get; set; }
        public string Nationality { get; set; }
        public string CurrentResidence { get; set; }
        public string IdNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public List<QuestionDTO> CustomQuestions { get; set; } = [];

    }
    public class QuestionDTO
    {
        public required QuestionType QuestionType { get; set; }
        public required string Question { get; set; }
        public List<string>? Choices { get; set; }
        public int? MaxChoices { get; set; }
        public bool? HasOtherOption { get; set; }
    }
}
