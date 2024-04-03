using AutoMapper;
using BeneficiarioAPI.DTOs;
using BeneficiarioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
// global using Microsoft.EntityFrameworkCore;

namespace BeneficiarioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiariosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public BeneficiariosController(ApplicationDbContext _context, IMapper mapper)
        {
            this.context = _context;
            this.mapper = mapper;
        }

        [HttpGet("ListaBeneficiarios{idEmpleado}")]
        public async Task<ActionResult<IEnumerable<Beneficiario>>> GetBeneficiarios(int idEmpleado)
        {
            return await context.Beneficiarios.Where(u => u.IdEmpleado == idEmpleado).OrderByDescending(a => a.Nombre).ToListAsync();
        }


        [HttpGet("SelecionaBeneficiario{id}")]
        public async Task<Beneficiario> GetBeneficiario(int id)
        {
            return await context.Beneficiarios.FirstAsync(u => u.Id == id);
        }

        [HttpGet("TotalPorcentajes{idEmpleado}")]
        public async Task<int> GetTotalPorcentaje(int idEmpleado)
        {
            // Ejemplo Sum()
            //var price = new List<float> { 40.01F, 20.02F, 40.03F };
            //var t = price.Sum();

            var total = (int)await context.Beneficiarios
                .Where(u => u.IdEmpleado == idEmpleado)
                .SumAsync(p => p.Porcentaje);
            return total;
        }

        [HttpGet("TotalPorcentajesMenosBeneficiario{idEmpleado}/{idBeneficiario}")]
        public async Task<int> GetTotalPorcentajeMenosBeneficiario(int idEmpleado, int idBeneficiario)
        {            
            // calcula el total de Porcentaje de un Empleado menos el Beneficiario
            var total = (int)await context.Beneficiarios
                .Where(u => u.IdEmpleado == idEmpleado && u.Id != idBeneficiario)
                .SumAsync(p => p.Porcentaje);
            return total;
        }

        // Ejemplo con Mapeo manual, sin AutoMapper
        //[HttpPost("AgregaBeneficiario")]
        //public async Task<ActionResult> AgregaBeneficiario(BeneficiarioDTO reg)
        //{
        //    Beneficiario beneficiario = new()
        //    {
        //        Id = reg.Id,
        //        Nombre = reg.Nombre,
        //        Apellidos = reg.Apellidos,
        //        FechaNacimiento = reg.FechaNacimiento,
        //        Curp = reg.Curp,
        //        Ssn = reg.Ssn,
        //        Telefono = reg.Telefono,
        //        Nacionalidad = reg.Nacionalidad,
        //        Porcentaje = reg.Porcentaje,
        //        IdEmpleado = reg.IdEmpleado
        //    };
        //    context.Beneficiarios.Add(beneficiario);
        //    await context.SaveChangesAsync();
        //    return Ok();
        //}


        // Con AutoMapper
        [HttpPost("AgregaBeneficiario")]
        public async Task<ActionResult> AgregaBeneficiario(BeneficiarioAddDTO beneficiarioAdd)
        {
            var reg = mapper.Map<Beneficiario>(beneficiarioAdd);
            context.Beneficiarios.Add(reg);
            await context.SaveChangesAsync();
            return Ok();
        }

        // PUT api/<BeneficiarioController>/5
        [HttpPut("ActualizaBeneficiario")]
        public async Task<ActionResult> ActualizaBeneficiario(BeneficiarioUpdateDTO beneficiarioCreacionDTO)
        {
            var reg = mapper.Map<Beneficiario>(beneficiarioCreacionDTO);
               context.Beneficiarios.Update(reg);
            await context.SaveChangesAsync();
            return Ok();
        }


        // DELETE api/<BeneficiarioController>/5
        [HttpDelete("BorraBeneficiario{id}")]
        public async Task<ActionResult> DeleteBeneficiario(int id)
        {
            var filasEliminadas = await context.Beneficiarios.Where(u => u.Id == id).ExecuteDeleteAsync();

            if (filasEliminadas == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
