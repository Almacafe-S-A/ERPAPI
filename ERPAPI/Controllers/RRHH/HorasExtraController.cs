using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/HorasExtra")]
    [ApiController]
    public class HorasExtraController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger logger;

        public HorasExtraController(ApplicationDbContext context, ILogger<HorasExtraController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        [HttpGet("[action]/{Fecha}/{Todos}")]
        public async Task<ActionResult> GetHorasExtrasFecha(DateTime Fecha, int Todos)
        {
            try
            {

                DateTime fechaBusqueda = new DateTime(Fecha.Year, Fecha.Month, Fecha.Day);
                if (Todos == 1)
                {
                    var horas = await context.HorasExtrasBiometrico
                        .Include(e => e.Empleado)
                        .Include(e => e.Estado)
                        .Where(l => l.Encabezado.Fecha.Equals(fechaBusqueda))
                        .ToListAsync();
                    return Ok(horas);
                }
                else
                {
                    var horas = await context.HorasExtrasBiometrico
                        .Include(e => e.Empleado)
                        .Include(e => e.Estado)
                        .Where(l => l.Encabezado.Fecha.Equals(fechaBusqueda) && l.IdEstado == 70)
                        .ToListAsync();
                    return Ok(horas);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex,"Ocurrio un error al cargar las horas extras");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]/{idHoraExtra}")]
        public async Task<ActionResult> AprobarHoraExtra(long idHoraExtra)
        {
            try
            {
                var registro = await context.HorasExtrasBiometrico.FirstOrDefaultAsync(r => r.Id == idHoraExtra);
                if (registro == null)
                    throw new Exception("El registro de la hora extra a aprobar no existe.");
                if (registro.IdEstado != 70)
                    throw new Exception("Solo se puede aprobar registros en estado de Cargado.");
                registro.IdEstado = 71;

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(context, logger, User.Identity.Name).SetAuditor();

                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al aprobar la hora extra");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]/{idHoraExtra}")]
        public async Task<ActionResult> RechazarHoraExtra(long idHoraExtra)
        {
            try
            {
                var registro = await context.HorasExtrasBiometrico.FirstOrDefaultAsync(r => r.Id == idHoraExtra);
                if (registro == null)
                    throw new Exception("El registro de la hora extra a rechazar no existe.");
                if (registro.IdEstado != 70)
                    throw new Exception("Solo se puede rechazar registros en estado de Cargado.");
                registro.IdEstado = 72;

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(context, logger, User.Identity.Name).SetAuditor();

                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al rechazar la hora extra");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<HorasExtraController>> Update(HorasExtraBiometrico horasExtra)
        {
            try
            {
                HorasExtraBiometrico horasExtraExistente = await context.HorasExtrasBiometrico.FirstOrDefaultAsync(f => f.Id == horasExtra.Id);
                if (horasExtraExistente == null)
                {
                    return NotFound("Registro de Hora Extra a actualizar no existe");
                }
                // Actualizar las propiedades del feriado existente con los valores del feriado recibido
                context.Entry(horasExtraExistente).CurrentValues.SetValues(horasExtra);

                // Actualizar los datos de auditoría
                new appAuditor(context, logger, User.Identity.Name).SetAuditor();

                await context.SaveChangesAsync();
                return Ok(horasExtraExistente);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualizar el registro de Hora Extra");
                return BadRequest("Error al actualizar el registro de Hora Extra");
            }
        }
    }
}