using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTask.Domain.Entities
{
    public class ProgramForm
    {
        public Guid id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public PersonalInformation personalInformation { get; set; }
        public List<CustomQuestion> CustomQuestions { get; set; } = new();
    }
}
