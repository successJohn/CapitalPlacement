using CapitalPlacementTask.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTask.Application.DTO.CandidateApplication
{
    public class CreateCandidateApplicationDto
    {
        public PersonalInformationAnswerDto PersonalInformation { get; set; }

    }

    public class PersonalInformationAnswerDto // stores answers to the program personal info questions
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string Phone { get; set; }
        public string CurrentResidence { get; set; }
        public string Gender { get; set; }
        public List<CustomQuestionsAnswerDto>? AdditionalQuestions { get; set; } = new();
    }

    public class CustomQuestionsAnswerDto
    {
        public string QuestionId { get; set; } // id of the question the candidate answered
        public QuestionType Type { get; set; }
        public DateTime? DateAnswer { get; set; } // for date answers
        public bool? YesNoAnswer { get; set; } // yes or no answers
        public string? ParagraphAnswer { get; set; } // paragraph answers
        public int NumberAnswer { get; set; } // numbers answers

        public List<ChoicesAnswer>? Choices { get; set; } = new();// holds multichoice, dropdown answers
    }

    public class ChoicesAnswer
    {
        public string? SelectedChoiceId { get; set; }
    }
}
