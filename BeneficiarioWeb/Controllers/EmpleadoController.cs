using BeneficiarioWeb.Models;
using BeneficiarioWeb.Models.Dto;
using BeneficiarioWeb.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace BeneficiarioWeb.Controllers
{
    public class EmpleadoController : Controller
    {
        private IServicio_API _Api; // Consume los servicios API
        public EmpleadoController(IServicio_API servicioApi)
        {
            _Api = servicioApi;
        }

        public async Task<IActionResult> Index()
        {
            List<Empleado> reg = await _Api.ListaEmpleados();
            ViewBag.titulo = "Empleados";
            return View(reg);
        }

        public async Task<IActionResult> Detalle(int? id)
        {
            Empleado obj1;
            IEnumerable<Beneficiario> obj2;
            EmpleadoBeneficiarios obj3 = new EmpleadoBeneficiarios();

            int idEmpleado;

            if (id == null)
                return NotFound();
            else
                idEmpleado = id.Value;

            obj1 = await _Api.DetalleEmpleado(idEmpleado);
            obj2 = await _Api.listaBeneficiario(idEmpleado);
            obj3.Empleado = obj1;
            obj3.Beneficiarios = obj2;
            ViewBag.titulo = "Empleados";
            return View(obj3);
        }

        public async Task<IActionResult> Eliminar(int? id)
        {
            int idEmpleado;
            if (id == null)
            {
                return NotFound();
            }
            else
                idEmpleado = id.Value;

            var reg = await _Api.BorraEmpleado(idEmpleado);

            if (reg == false)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Empresas/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Empleado empleado)
        {
            bool reg = false;
            if (ModelState.IsValid)
            {
                reg = await _Api.CreaEmpleado(empleado);
                if (reg)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }

            return View(empleado);
        }


        public async Task<IActionResult> Edita(int? id)
        {
            int idEmpleado;

            if (id == null)
                return NotFound();
            else
                idEmpleado = id.Value;
            // Se toman los datos del empleado
            var reg = await _Api.ObtenerEmpleado(idEmpleado);
            ViewBag.titulo = "Edita Empleado";
            return View(reg);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edita(Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                bool r = await _Api.GuardarEmpleado(empleado);
                if (r)
                    return RedirectToAction("Index");
                else
                    return NotFound();
            }
            else
            {
                return View(empleado);
            }
        }
    }


}
