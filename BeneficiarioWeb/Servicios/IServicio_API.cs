using BeneficiarioWeb.Models;

namespace BeneficiarioWeb.Servicios
{
    public interface IServicio_API
    {
        Task<List<Empleado>> ListaEmpleados();
        Task<List<Beneficiario>> listaBeneficiario(int idEmpleado);
        Task<Empleado> DetalleEmpleado(int idEmpleado);
        Task<bool> BorraEmpleado(int idEmpleado);
        Task<bool> BorraBeneficiario(int idBeneficiario);
        Task<bool> CreaEmpleado(Empleado empleado);
        Task<bool> CreaBeneficiario(Beneficiario empleado);
        Task<Empleado> ObtenerEmpleado(int idEmpleado);
        Task<Beneficiario> ObtenerBeneficiario(int idBeneficiario);
        Task<bool> GuardarEmpleado(Empleado objeto);
        Task<bool> GuardarBeneficiario(Beneficiario objeto);
        Task<int> TotalPorcentaje(int idEmpleado, int Porcentaje);
        Task<int> TotalPorcentajeMenosBeneficiario(int idEmpleado, int idBeneficiario, int Porcentaje);
    }
}
