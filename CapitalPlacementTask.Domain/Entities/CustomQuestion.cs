using CapitalPlacementTask.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTask.Domain.Entities
{
    public class CustomQuestion
    {       
        public Guid Id { get; set; }
        public string Question { get; set; }
        public QuestionType Type { get; set; }
        public List<QuestionChoices> Choices { get; set; } = new();
        public int? MaxChoices { get; set; }
        public bool? HasOtherOption { get; set; }
    }
}
