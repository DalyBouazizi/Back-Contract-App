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

    public DateOnly? Datedeb { get; set; }

    public DateOnly? DateFin { get; set; }

    public int? EmployeeId { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("Contracts")]
    public virtual Employee? Employee { get; set; }
}
