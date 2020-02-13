using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class DeduccionEmpleadoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public DeduccionEmpleadoController(ILogger<DeduccionEmpleadoController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<DeduccionEmpleado>> Guardar([FromBody] DeduccionEmpleado deduccion)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        if (deduccion.Id != 0)
                        {
                            DeduccionEmpleado deduccionExistente =
                                await _context.DeduccionesEmpleados.FirstOrDefaultAsync(d => d.Id == deduccion.Id);
                            if (deduccionExistente == null)
                            {
                                throw new Exception("No existe la deducción del empleado a modificar");
                            }

                            _context.Entry(deduccionExistente).CurrentValues.SetValues(deduccion);
                            await _context.SaveChangesAsync();
                            BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                               {
                                   IdOperacion = deduccionExistente.Id,
                                   DocType = "DeduccionEmpleado",
                                   ClaseInicial =
                                       Newtonsoft.Json.JsonConvert.SerializeObject(deduccionExistente, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                                   Accion = "Update",
                                   FechaCreacion = DateTime.Now,
                                   FechaModificacion = DateTime.Now,
                                   UsuarioCreacion = deduccionExistente.UsuarioCreacion,
                                   UsuarioModificacion = deduccionExistente.UsuarioModificacion,
                                   UsuarioEjecucion = deduccionExistente.UsuarioModificacion,

                               });

                            await _context.SaveChangesAsync();
                            transaction.Commit();
                            return Ok(deduccionExistente);
                        }
                        else
                        {
                            _context.DeduccionesEmpleados.Add(deduccion);
                            await _context.SaveChangesAsync();
                            BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                                                                               {
                                                                                   IdOperacion = deduccion.Id,
                                                                                   DocType = "DeduccionEmpleado",
                                                                                   ClaseInicial =
                                                                                       Newtonsoft.Json.JsonConvert.SerializeObject(deduccion, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                                                                                   Accion = "Insert",
                                                                                   FechaCreacion = DateTime.Now,
                                                                                   FechaModificacion = DateTime.Now,
                                                                                   UsuarioCreacion = deduccion.UsuarioCreacion,
                                                                                   UsuarioModificacion = deduccion.UsuarioModificacion,
                                                                                   UsuarioEjecucion = deduccion.UsuarioModificacion,

                                                                               });
                            await _context.SaveChangesAsync();
                            transaction.Commit();
                            return Ok(deduccion);
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError(ex, "Error Guardar Deducción Empleado");
                        return BadRequest(ex);
                    }
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error Guardar Deducción Empleado");
                return BadRequest(ex);
            }
        }

        [HttpGet("[action]/{empleadoId}")]
        public async Task<ActionResult<List<DeduccionEmpleado>>> GetDeduccionesPorEmpleado(long empleadoId)
        {
            try
            {
                List<DeduccionEmpleado> deducciones =
                    await _context.DeduccionesEmpleados.Include(s=> s.Deduccion).Where(d => d.EmpleadoId == empleadoId).ToListAsync();
                return Ok(deducciones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Guardar Deducción Empleado");
                return BadRequest(ex);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<DeduccionesEmpleadoDTO>>> GetDeduccionesEmpleados()
        {
            try
            {
                var detalleEmpleado = from emp in _context.Employees
                    join ded in _context.DeduccionesEmpleados on emp.IdEmpleado equals ded.EmpleadoId into dedEmp
                    from subDed in dedEmp.DefaultIfEmpty()
                    group new {emp, subDed} by emp.IdEmpleado
                    into g
                    select new DeduccionesEmpleadoDTO()
                           {
                               EmpleadoId = g.Key,
                               NombreEmpleado = g.FirstOrDefault().emp.NombreEmpleado,
                               CantidadDeducciones = g.Count(s=> s.subDed != null),
                               TotalDeducciones = g.Sum(s => s.subDed == null ? 0 : s.subDed.Monto ),
                               SalarioEmpleado = (double) (g.First().emp.Salario.HasValue ? g.First().emp.Salario.Value : 0)
                           };
                List<DeduccionesEmpleadoDTO> deducciones = await detalleEmpleado.ToListAsync();
                return Ok(deducciones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Guardar Deducción Empleado");
                return BadRequest(ex);
            }
        }
    }
}