
using System.ComponentModel.DataAnnotations;

namespace BeneficiarioWeb.Models
{
    public class Empleado
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Se requiere el nombre")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "Se requieren los apellidos")]
        public string Apellidos { get; set; }
        [Display(Name = "Fecha de nacimiento")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }
        [Display(Name = "Número de empleado")]
        [Required(ErrorMessage = "Número de empleado requerido")]
        public int NumeroEmpleado { get; set; }
        [Display(Name = "CURP")]
        [Required(ErrorMessage = "Se requiere la clave CURP")]
        public string CURP { get; set; }
        [Display(Name = "Número de Seguro Social")]
        [Required(ErrorMessage = "Se requiere la clave del Seguro social")]
        public string SSN { get; set; }
        [Phone]
        [StringLength(10)]
        [Required(ErrorMessage = "Se requiere un número de teléfono")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }
        [Display(Name = "Nacionalidad")]
        [Required(ErrorMessage = "Se requiere la nacionalidad")]
        public string Nacionalidad { get; set; }
    }
}
