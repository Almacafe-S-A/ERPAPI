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
    public class LlegadasTardeController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger logger;

        public LlegadasTardeController(ApplicationDbContext context, ILogger<LlegadasTardeController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        [HttpGet("[action]/{fecha}/{todos}")]
        public async Task<ActionResult> GetLlegadasTardesDisponibles(DateTime fecha, int todos)
        {
            try
            {
                DateTime fechaBusqueda = new DateTime(fecha.Year, fecha.Month,fecha.Day,0,0,0);
                if (todos == 1)
                {
                    var llegadas = await context.LlegadasTardeBiometrico
                        .Include(e => e.Empleado)
                        .Include(e => e.Estado)
                        .Where(l => l.Encabezado.Fecha.Equals(fechaBusqueda))
                        .ToListAsync();
                    return Ok(llegadas);
                }
                else
                {
                    var llegadas = await context.LlegadasTardeBiometrico
                        .Include(e => e.Empleado)
                        .Include(e => e.Estado)
                        .Where(l => l.Encabezado.Fecha.Equals(fechaBusqueda) && l.IdEstado == 97)
                        .ToListAsync();
                    return Ok(llegadas);
                }
                

                
            }
            catch (Exception ex)
            {
                logger.LogError(ex,"Error al cargar las llegadas tardes cargadas.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]/{idLlegadaTarde}")]
        public async Task<ActionResult> AprobarLlegadaTarde(long idLlegadaTarde)
        {
            try
            {
                var registro = await context.LlegadasTardeBiometrico.FirstOrDefaultAsync(r => r.Id == idLlegadaTarde);
                if (registro == null)
                    throw new Exception("El registro de la llegada tarde a aprobar no existe.");
                if (registro.IdEstado != 97) //Estado 97 = Pendiente de Aprobacion
                    throw new Exception("Solo se puede aprobar registros en estado de Cargado.");
                registro.IdEstado = 71;

                var registroentrada = new ControlAsistencias()
                {
                    //falta revision de id de control de asistencia
                    Id = 0,
                    IdEmpleado = registro.IdEmpleado,
                    Fecha = registro.Fecha,
                    TipoAsistencia = 83,
                    Dia = ((int)registro.Fecha.DayOfWeek),
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = User.Identity.Name,
                    FechaModificacion = DateTime.Now,
                    UsuarioModificacion = User.Identity.Name
                };
                context.ControlAsistencias.Add(registroentrada);
                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(context, logger, User.Identity.Name).SetAuditor();

                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex,"Error al aprobar la llegada tarde");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]/{idLlegadaTarde}")]
        public async Task<ActionResult> RechazarLlegadaTarde(long idLlegadaTarde)
        {
            try
            {
                var registro = await context.LlegadasTardeBiometrico.FirstOrDefaultAsync(r => r.Id == idLlegadaTarde);
                if (registro == null)
                    throw new Exception("El registro de la llegada tarde a rechazar no existe.");
                if (registro.IdEstado != 97)
                    throw new Exception("Solo se puede rechazar registros en estado de Cargado.");
                registro.IdEstado = 72;

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(context, logger, User.Identity.Name).SetAuditor();

                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al rechazar la llegada tarde");
                return BadRequest(ex.Message);
            }
        }

    }
}