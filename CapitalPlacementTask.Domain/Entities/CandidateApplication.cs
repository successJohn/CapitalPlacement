using CapitalPlacementTask.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTask.Domain.Entities
{
   public class CandidateApplication
    {
        public Guid id { get; set; }
        public Guid ProgramId { get; set; }  // get the program info the candidate applies to
        public PersonalInformationAnswer PersonalInformation { get; set; }
    }

    public class PersonalInformationAnswer // stores answers to the program personal info questions
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string Phone { get; set; }
        public string CurrentResidence { get; set; }
        public string Gender { get; set; }
        public List<CustomQuestionsAnswer>? AdditionalQuestions { get; set; } = new();
    }

    public class CustomQuestionsAnswer
    {
        public string Id { get; set; }
        public string QuestionId { get; set; } // id of the question the candidate answered
        public QuestionType Type { get; set; }
        public DateTime? DateAnswer { get; set; } // for date answers
        public bool? YesNo { get; set; } // yes or no answers
        public string? Answer { get; set; } // paragraph answers
        public int Number { get; set; } // numbers answers

        public List<Choices>? Choices { get; set; } = new();// holds multichoice, dropdown answers
    }

    public class Choices
    {
        public string? SelectedChoiceId { get; set; }
    }
}
