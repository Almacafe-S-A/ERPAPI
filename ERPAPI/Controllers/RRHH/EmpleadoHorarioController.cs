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
    public class EmpleadoHorarioController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger logger;

        public EmpleadoHorarioController(ApplicationDbContext context, ILogger<EmpleadoHorarioController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Guardar(EmpleadoHorario registro)
        {
            try
            {
                if (registro.Id == 0)
                {
                    var verificador =
                        await context.EmpleadoHorarios.FirstOrDefaultAsync(r => r.EmpleadoId == registro.EmpleadoId);
                    if (verificador != null)
                    {
                        throw new Exception("No se puede asignar más de un horario a un empleado");
                    }

                    context.EmpleadoHorarios.Add(registro);

                    //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                    new appAuditor(context, logger, User.Identity.Name).SetAuditor();

                    await context.SaveChangesAsync();
                    return Ok(registro);
                }
                
                var registroExistente =
                    await context.EmpleadoHorarios.FirstOrDefaultAsync(r => r.Id == registro.Id);
                if (registroExistente.EmpleadoId != registro.EmpleadoId)
                {
                    //Cambio el empleado
                    var verificador = await context.EmpleadoHorarios.FirstOrDefaultAsync(
                        r => r.EmpleadoId == registro.EmpleadoId && r.HorarioId == registro.HorarioId);
                    if (verificador != null)
                    {
                        throw new Exception("No se puede asignar más de un horario a un empleado");
                    }
                }

                if (registroExistente.HorarioId!= registro.HorarioId)
                {
                    //Cambio el horario
                    var verificador = await context.EmpleadoHorarios.FirstOrDefaultAsync(
                        r => r.EmpleadoId == registro.EmpleadoId && r.HorarioId == registro.HorarioId);
                    if (verificador != null)
                    {
                        throw new Exception("No se puede asignar más de un horario a un empleado");
                    }
                }

                context.Entry(registroExistente).CurrentValues.SetValues(registro);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(context, logger, User.Identity.Name).SetAuditor();

                await context.SaveChangesAsync();
                return Ok(registroExistente);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar el registro de horario del empleado");
                return BadRequest(ex);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetHorariosEmpleados()
        {
            try
            {
                var registros = await context.EmpleadoHorarios
                    .Include(e => e.Empleado)
                    .Include(h => h.HorarioEmpleado)
                    .Include(e=>e.Estado)
                    .ToListAsync();
                return Ok(registros);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al cargar los registros de horario del empleado");
                return BadRequest(ex);
            }
        }
    }
}