using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Data.Features
{
    public class FilterCriteria
    {
        public List<string> Positions { get; set; } = new List<string>();
        public List<string> Categories { get; set; } = new List<string>();
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
    }
}
