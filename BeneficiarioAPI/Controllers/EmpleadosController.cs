using BeneficiarioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeneficiarioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadosController : ControllerBase
    {
        private readonly ApplicationDbContext context;        

        public EmpleadosController(ApplicationDbContext _context)
        {
            this.context = _context;            
        }

        [HttpGet("ListaEmpleado")]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleados()
        {
            return await context.Empleados.OrderBy(a => a.Nombre).ToListAsync();
            //return await context.Empleados.OrderDescending(a => a.Nombre).ToListAsync();
        }

        [HttpGet("GetEmpleado{id}")]
        public async Task<ActionResult<Empleado>> GetEmpleado(int id)
        {            
            return await context.Empleados.FirstAsync(u => u.Id == id);
        }

        [HttpPost("AgregaEmpleado")]
        public async Task<ActionResult> AgregaEmpleado(Empleado reg)
        {
            context.Empleados.Add(reg);
            await context.SaveChangesAsync();
            return Ok();
        }

        // PUT api/<EmpleadosController>/5
        [HttpPut("ActualizaEmpleado")]
        public async Task<ActionResult> ActualizaEmpleado(Empleado reg)
        {
            context.Empleados.Update(reg);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("BorrarEmpleado{id}")]
        public async Task<ActionResult> DeleteEmpleado(int id)
        {
            var filasEliminadas = await context.Empleados.Where(u => u.Id == id).ExecuteDeleteAsync();

            if (filasEliminadas == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
