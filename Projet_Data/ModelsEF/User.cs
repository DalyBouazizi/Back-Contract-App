using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Projet_Data.ModelsEF;

[Table("User")]
[Index("Matricule", Name = "UQ__User__0FB9FB4377FA4A46", IsUnique = true)]
public partial class User
{
    [Key]
    public int Id { get; set; }

    public int Matricule { get; set; }

    [StringLength(100)]
    public string? Nom { get; set; }

    [StringLength(100)]
    public string? Prenom { get; set; }

    [StringLength(100)]
    public string? Password { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Users")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
