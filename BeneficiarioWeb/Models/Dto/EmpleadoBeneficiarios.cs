namespace BeneficiarioWeb.Models.Dto
{
    public class EmpleadoBeneficiarios
    {
        public Empleado Empleado { get; set; }
        public IEnumerable<Beneficiario> Beneficiarios { get; set; }
    }
}
