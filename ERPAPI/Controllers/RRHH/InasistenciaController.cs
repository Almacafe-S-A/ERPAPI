using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class InasistenciaController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger logger;

        public InasistenciaController(ApplicationDbContext context, ILogger<InasistenciaController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        private async Task GenerarInasistencias(DateTime fecha)
        {
            var empleados = await (from emp in context.Employees
                where !context.DetallesBiometricos.Any(
                        d => d.Encabezado.Fecha.Equals(fecha) && d.Encabezado.IdEstado == 62 && d.IdEmpleado == emp.IdEmpleado) && 
                      !context.Inasistencias.Any(i=> i.Fecha.Equals(fecha) && i.IdEmpleado == emp.IdEmpleado)
                select emp).ToListAsync();

            foreach (var emp in empleados)
            {
                Inasistencia registro = new Inasistencia()
                                        {
                    Id = 0,
                    Fecha=fecha,
                    IdEmpleado = emp.IdEmpleado,
                    Observacion = "",
                    TipoInasistencia = 150,
                    IdEstado = 80,
                    FechaCreacion = DateTime.Today,
                    FechaModificacion = DateTime.Today,
                    UsuarioCreacion = "",
                    UsuarioModificacion = ""
                                        };

                context.Inasistencias.Add(registro);
            }

            await context.SaveChangesAsync();
        }

        [HttpGet("[action]/{fecha}")]
        public async Task<ActionResult> GetInasistenciasFecha(DateTime fecha)
        {
            try
            {
                await GenerarInasistencias(fecha);
                var inasistencias = await context.Inasistencias
                    .Include(e=>e.Estado)
                    .Include(e=>e.Tipo)
                    .Include(e=>e.Empleado)
                    .Where(i => i.Fecha.Equals(fecha) && i.IdEstado != 81).ToListAsync();
                return Ok(inasistencias);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al cargar la inasistencia");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Guardar(Inasistencia inasistencia)
        {
            try
            {
                if (inasistencia.Id == 0)
                {
                    context.Inasistencias.Add(inasistencia);

                    //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                    new appAuditor(context, logger, User.Identity.Name).SetAuditor();

                    await context.SaveChangesAsync();
                    return Ok(inasistencia);
                }
                
                var registroExistente = await context.Inasistencias.FirstOrDefaultAsync(i => i.Id == inasistencia.Id);
                if (registroExistente == null)
                    throw new Exception("Inasistencia a actualizar no existe.");
                context.Entry(registroExistente).CurrentValues.SetValues(inasistencia);
                await context.SaveChangesAsync();
                return Ok(registroExistente);
            }
            catch (Exception ex)
            {
                logger.LogError(ex,"Error al guardar inasistencia");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]/{idInasistencia}/{idTipo}/{comentario}")]
        public async Task<ActionResult> Aprobar(long idInasistencia, string comentario, long idTipo)
        {
            try
            {
                var registroExistente = await context.Inasistencias.FirstOrDefaultAsync(i => i.Id == idInasistencia);
                if (registroExistente == null)
                    throw new Exception("Inasistencia a actualizar no existe.");
                registroExistente.IdEstado = 82;

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(context, logger, User.Identity.Name).SetAuditor();

                await context.SaveChangesAsync();
                return Ok(registroExistente);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar inasistencia");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]/{idInasistencia}/{idTipo}/{comentario}")]
        public async Task<ActionResult> Rechazar(long idInasistencia, string comentario, long idTipo)
        {
            try
            {
                var registroExistente = await context.Inasistencias.FirstOrDefaultAsync(i => i.Id == idInasistencia);
                if (registroExistente == null)
                    throw new Exception("Inasistencia a actualizar no existe.");
                registroExistente.IdEstado = 83;

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(context, logger, User.Identity.Name).SetAuditor();

                await context.SaveChangesAsync();
                return Ok(registroExistente);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar inasistencia");
                return BadRequest(ex.Message);
            }
        }
    }
}