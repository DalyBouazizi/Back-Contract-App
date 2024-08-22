using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Projet_Data.ModelsEF;

[Table("Alert")]
public partial class Alert
{
    [Key]
    public int AlertId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime AlertDate { get; set; }

    [Column("ContractID")]
    public int ContractId { get; set; }

    [ForeignKey("ContractId")]
    [InverseProperty("Alerts")]
    public virtual Contract Contract { get; set; } = null!;
}
