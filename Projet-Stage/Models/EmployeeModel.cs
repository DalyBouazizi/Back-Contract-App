
using System.Text.Json.Serialization;

namespace Projet_Stage.Models
{
    public class EmployeeModel
    {
       
        public int Matricule { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Poste { get; set; }
        public string Adresse { get; set; }

        public DateTime? DateNaissance { get; set; }
        public string LieuNaissance { get; set; }
        public string Cin { get; set; }

      
        public DateTime? DateCin { get; set; }
        public string CategoriePro { get; set; }
        public decimal? Salaireb { get; set; }
        public decimal? Salairen { get; set; }
        //public ICollection<ContractModel> Contracts { get; set; }
    }
}
