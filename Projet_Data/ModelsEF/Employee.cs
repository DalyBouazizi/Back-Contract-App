using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Projet_Data.ModelsEF;

[Table("Employee")]
[Index("Matricule", Name = "UQ__Employee__0FB9FB431CD7E067", IsUnique = true)]
[Index("Cin", Name = "UQ__Employee__C1FFD81E6DAE6A76", IsUnique = true)]
public partial class Employee
{
    [Key]
    public int Id { get; set; }

    public int Matricule { get; set; }

    [StringLength(100)]
    public string? Nom { get; set; }

    [StringLength(100)]
    public string? Prenom { get; set; }

    [StringLength(100)]
    public string? Poste { get; set; }

    [StringLength(255)]
    public string? Adresse { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateNaissance { get; set; }

    [StringLength(100)]
    public string? LieuNaissance { get; set; }

    [StringLength(50)]
    public string? Cin { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateCin { get; set; }

    [StringLength(50)]
    public string? CategoriePro { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Salaireb { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Salairen { get; set; }

    [InverseProperty("Employee")]
    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    [ForeignKey("EmployeeId")]
    [InverseProperty("Employees")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
