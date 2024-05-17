using CapitalPlacementTask.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTask.Domain.Entities
{
    public class PersonalInformation
    {
        public Guid id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string Nationality { get; set; }

        public string CurrentResidence { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? IdNumber { get; set; }
        public Gender gender { get; set; }
        public List<CustomQuestion> CustomQuestions { get; set; } = new();
    }
}
