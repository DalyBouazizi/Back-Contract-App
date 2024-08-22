using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Projet_Data.ModelsEF;

[Table("Contract")]
public partial class Contract
{
    [Key]
    public int Idcontrat { get; set; }

    [StringLength(50)]
    public string? Type { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Datedeb { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateFin { get; set; }

    public int EmployeeId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Salaireb { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Salairen { get; set; }

    [InverseProperty("Contract")]
    public virtual ICollection<Alert> Alerts { get; set; } = new List<Alert>();

    [ForeignKey("EmployeeId")]
    [InverseProperty("Contracts")]
    public virtual Employee? Employee { get; set; }
}
