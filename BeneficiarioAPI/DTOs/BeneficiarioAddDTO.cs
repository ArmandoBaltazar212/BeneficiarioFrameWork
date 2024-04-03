using BeneficiarioAPI.Models;

namespace BeneficiarioAPI.DTOs
{
    public class BeneficiarioAddDTO
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Curp { get; set; }
        public string Ssn { get; set; }
        public string Telefono { get; set; }
        public string Nacionalidad { get; set; }
        public int Porcentaje { get; set; }
        public int IdEmpleado { get; set; }
        public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;
    }
}
