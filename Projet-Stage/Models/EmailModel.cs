using System.ComponentModel.DataAnnotations;

namespace Projet_Stage.Models
{
    public class EmailModel
    {


        public IEnumerable<string> to { get; set; }  // Changed to IEnumerable<string>


        public string subject { get; set; }

      
        public string body { get; set; }
    }
}
