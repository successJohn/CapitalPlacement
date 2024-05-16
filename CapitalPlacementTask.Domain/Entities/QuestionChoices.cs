using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTask.Domain.Entities
{
    public class QuestionChoices
    {
        public Guid id { get; set; }
        public Guid CustomQuestionId { get; set; }
        public string Choice { get; set; }
    }
}
