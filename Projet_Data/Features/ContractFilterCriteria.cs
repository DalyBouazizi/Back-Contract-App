using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Data.Features
{
    public class ContractFilterCriteria
    {
        public List<string> Types { get; set; } = new List<string>();  // Ensure it's initialized to avoid null issues

        public bool? IsActive { get; set; }
        public bool? IsEndingSoon { get; set; }
        public bool? IsEndedRecently { get; set; }
        public bool? EndedOverOneMonth { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
