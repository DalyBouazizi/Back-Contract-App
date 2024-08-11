using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Projet_Data.ModelsEF;

namespace Projet_Data.ModelsEF2;

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

    [ForeignKey("EmployeeId")]
    [InverseProperty("Contracts")]
    public virtual Employee? Employee { get; set; }
}
