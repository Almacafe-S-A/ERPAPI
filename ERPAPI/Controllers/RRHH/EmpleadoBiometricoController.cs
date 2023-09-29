using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoBiometricoController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger logger;

        public EmpleadoBiometricoController(ApplicationDbContext context, ILogger<EmpleadoBiometricoController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetAsignacionesBiometrico()
        {
            try
            {
                var asignaciones = await (from emp in context.Employees
                    join asg in context.EmpleadosBiometrico on emp.IdEmpleado equals asg.EmpleadoId into easg
                    from empasg in easg.DefaultIfEmpty()
                    where (emp.IdEstado == 1)

                    select new EmpleadoBiometricoAsignacionDTO()
                           {
                               EmpleadoId = emp.IdEmpleado,
                               BiometricoId = (empasg == null ? 0 : empasg.BiometricoId),
                               NombreEmpleado = emp.NombreEmpleado,
                               FechaCreacion = (empasg == null ? new DateTime() : empasg.FechaCreacion),
                               FechaModificacion = (empasg == null ? new DateTime() : empasg.FechaModificacion),
                               UsuarioCreacion = (empasg == null ? "" : empasg.UsuarioCreacion),
                               UsuarioModificacion = (empasg == null ? "" : empasg.UsuarioModificacion)
                           }).ToListAsync();
                return Ok(asignaciones);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al cargar los registros de asignaciones biometrico");
                return BadRequest(ex);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Guardar(EmpleadoBiometricoAsignacionDTO asignacion)
        {
            try
            {
                var registroExistente = await 
                    context.EmpleadosBiometrico.FirstOrDefaultAsync(r => r.EmpleadoId == asignacion.EmpleadoId);

                if (registroExistente == null)
                {
                    var registro = new EmpleadoBiometrico()
                                   {
                                        EmpleadoId = asignacion.EmpleadoId,
                                        BiometricoId = asignacion.BiometricoId??0,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        UsuarioCreacion = asignacion.UsuarioModificacion,
                                        UsuarioModificacion = asignacion.UsuarioModificacion
                                   };

                    context.EmpleadosBiometrico.Add(registro);

                    //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                    new appAuditor(context, logger, User.Identity.Name).SetAuditor();

                    await context.SaveChangesAsync();

                    return Ok(registro);
                }
                else
                {
                    registroExistente.BiometricoId = asignacion.BiometricoId??0;
                    registroExistente.UsuarioModificacion = asignacion.UsuarioModificacion;
                    registroExistente.FechaModificacion = DateTime.Now;
                    await context.SaveChangesAsync();
                    return Ok(registroExistente);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar los registros de asignaciones biometrico");
                return BadRequest(ex);
            }
        }
    }
}