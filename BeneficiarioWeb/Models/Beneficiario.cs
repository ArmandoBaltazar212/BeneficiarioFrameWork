using System.ComponentModel.DataAnnotations;

namespace BeneficiarioWeb.Models
{
    public class Beneficiario
    {
        public int Id { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Se requiere el nombre")]
        public string Nombre { get; set; }
        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "Se requieren los apellidos")]
        public string Apellidos { get; set; }
        [Display(Name = "Fecha de nacimiento")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }
        [Display(Name = "CURP")]
        [Required(ErrorMessage = "Se requiere la clave CURP")]
        public string CURP { get; set; }
        [Display(Name = "Número de Seguro Social")]
        [Required(ErrorMessage = "Se requiere la clave del Seguro social")]
        public string SSN { get; set; }
        [Phone]
        [Display(Name = "Teléfono")]
        [StringLength(10, ErrorMessage = "Se requieren 10 digitos")]
        [Required(ErrorMessage = "Se requiere un número de teléfono")]
        public string Telefono { get; set; }
        [Display(Name = "Nacionalidad")]
        [Required(ErrorMessage = "Se requiere la nacionalidad")]
        public string Nacionalidad { get; set; }

        [Display(Name = "Porcentaje")]
        [Required(ErrorMessage = "Se requiere el porcentaje de 1 a 100 con números enteros")]
        [Range(1, 100, ErrorMessage = "Rango permitido 1 al 100")]
        public int Porcentaje { get; set; }
        public int IdEmpleado { get; set; }
    }
}
