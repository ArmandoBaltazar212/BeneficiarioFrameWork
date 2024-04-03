using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BeneficiarioAPI.Models;

[Table("Empleado")]
public partial class Empleado
{
    [Key]
    public int Id { get; set; }

    [StringLength(80)]
    public string? Nombre { get; set; }

    [StringLength(100)]
    public string? Apellidos { get; set; }

    [Column(TypeName = "date")]
    public DateTime? FechaNacimiento { get; set; }

    public int? NumeroEmpleado { get; set; }

    [Column("CURP")]
    [StringLength(18)]
    public string? Curp { get; set; }

    [Column("SSN")]
    [StringLength(12)]
    public string? Ssn { get; set; }

    [StringLength(12)]
    public string? Telefono { get; set; }

    [StringLength(30)]
    public string? Nacionalidad { get; set; }

    [InverseProperty("IdEmpleadoNavigation")]
    public virtual ICollection<Beneficiario> Beneficiarios { get; set; } = new List<Beneficiario>();
}
