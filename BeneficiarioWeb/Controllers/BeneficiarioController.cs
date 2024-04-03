using BeneficiarioWeb.Models;
using BeneficiarioWeb.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace BeneficiarioWeb.Controllers
{
    public class BeneficiarioController : Controller
    {
        private IServicio_API _Api; // Consume los servicios API
        public BeneficiarioController(IServicio_API servicioApi)
        {
            _Api = servicioApi;
        }

        // GET: BeneficiarioController
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ListaBeneficiarios(int idEmpleado)
        {
            var reg = await _Api.listaBeneficiario(idEmpleado);
            return PartialView("_ListaBeneficiarios", reg);
        }

        // GET: BeneficiarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BeneficiarioController/Create
        public ActionResult Create(int idEmpleado)
        {
            ViewData["empleadoID"] = idEmpleado;
            return View();
        }

        // POST: BeneficiarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Beneficiario beneficiario)
        {
            if (ModelState.IsValid)
            {
                bool reg = await _Api.CreaBeneficiario(beneficiario);
                return RedirectToAction("Detalle", "Empleado", new { id = beneficiario.IdEmpleado });
            }
            else
            {
                return View(beneficiario);
            }

        }

        // GET: BeneficiarioController/Edit/5
        public async Task<IActionResult> Edita(int id)
        {
            // Se toman los datos del beneficiario
            var reg = await _Api.ObtenerBeneficiario(id);
            ViewBag.titulo = "Edita Beneficiario";
            return View(reg);
        }

        // POST: BeneficiarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edita(Beneficiario beneficiario)
        {

            if (ModelState.IsValid)
            {
                var reg = await _Api.GuardarBeneficiario(beneficiario);

                return RedirectToAction("Detalle", "Empleado", new { id = beneficiario.IdEmpleado });
            }
            else
            {
                return View(beneficiario);
            }
        }

        public async Task<IActionResult> Eliminar(int? id, int idEmpleado)
        {
            int idBeneficiario;
            if (id == null)
            {
                return NotFound();
            }
            else
                idBeneficiario = id.Value;

            bool reg = await _Api.BorraBeneficiario(idBeneficiario);

            if (reg == false)
            {
                return NotFound();
            }
            return RedirectToAction("Detalle", "Empleado", new { id = idEmpleado });
        }

        [HttpGet]
        public async Task<JsonResult> PorcentajeTotal(int idEmpleado, int Porcentaje)
        {
            // Este servicio calcula el porcentaje total de un empleado sin en beneficiario actual (para Create)
            // y con el parámetro Porcentaje determina si se puede guardar o sobrepasa el 100%

            int total = await _Api.TotalPorcentaje(idEmpleado, Porcentaje);

            return Json(total);
        }

        [HttpGet]
        public async Task<JsonResult> PorcentajeTotalMenosBeneficiario(int idEmpleado, int idBeneficiario, int Porcentaje)
        {
            // Este servicio calcula el porcentaje total de un empleado sin en beneficiario actual (para Edit)
            // y con el parámetro Porcentaje determina si se puede guardar o sobrepasa el 100%

            int total = await _Api.TotalPorcentajeMenosBeneficiario(idEmpleado, idBeneficiario, Porcentaje);

            return Json(total);
        }
    }



}
