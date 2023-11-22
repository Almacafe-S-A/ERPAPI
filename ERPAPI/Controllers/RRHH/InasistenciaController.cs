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
            try
            {
                //Solo debe mostrar los empleados si existe la Carga de Biométrico de la fecha consultada.
                var biometricofecha = this.context.Biometricos.Where(q => q.Fecha.Date == fecha.Date).FirstOrDefault();
                if (biometricofecha == null)
                {
                    throw new Exception("No existe una carga biometrica para esta fecha");
                }
                var empleados = await (from emp in context.Employees
                                       where !context.DetallesBiometricos.Any(
                                               d => d.Encabezado.Fecha.Equals(fecha) && d.Encabezado.IdEstado == 62 && d.IdEmpleado == emp.IdEmpleado) &&
                      !context.Inasistencias.Any(i=> i.Fecha.Equals(fecha) && i.IdEmpleado == emp.IdEmpleado)
                                       select emp).ToListAsync();
                foreach (var emp in empleados)
                {
                    var horarioEmpleado = await context.DetallesBiometricos.FirstOrDefaultAsync(e => e.IdEmpleado == emp.IdEmpleado);
                    if (horarioEmpleado == null)
                    {
                        continue;                   
                    }

                    else
                    {
                        string horaEmpleadoString = horarioEmpleado.FechaHora.ToString("HH:mm:ss");

                        Inasistencia registro = new Inasistencia()
                        {
                            Id = 0,
                            Fecha = fecha,
                            IdEmpleado = emp.IdEmpleado,
                            Observacion = "",
                            TipoInasistencia = 13,
                            IdEstado = 97,//ESTADO 97 PENDIENTE DE APROBACION
                            FechaCreacion = DateTime.Today,
                            FechaModificacion = DateTime.Today,
                            UsuarioCreacion = "",
                            UsuarioModificacion = "",
                            HoraLlegada = horaEmpleadoString
                        };

                        context.Inasistencias.Add(registro);
                    }
                }

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al generar inasistencias: {0}", ex.Message);
                throw ex;
            }
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
                    .Where(i => i.Fecha.Equals(fecha) && i.IdEstado != 81 && i.Empleado.IdEstado == 1).ToListAsync();//81 ESTADO ANULADO
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
                registroExistente.Observacion = comentario;
                registroExistente.TipoInasistencia = idTipo;
                //SE GUARDA REGISTRO DE ASISTENCIA EN EL CONTROLADOR DE INASISTENCIA
                var asistencias = new ControlAsistencias()
                {
                    Id = 0,
                    IdEmpleado = registroExistente.IdEmpleado,
                    Fecha = registroExistente.Fecha,
                    TipoAsistencia = idTipo,
                    Dia = ((int)registroExistente.Fecha.DayOfWeek),
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = User.Identity.Name,
                    FechaModificacion = DateTime.Now,
                    UsuarioModificacion = User.Identity.Name
                };
                context.ControlAsistencias.Add(asistencias);
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
                registroExistente.IdEstado = 83; //ESTADO 83 = RECHAZADO

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