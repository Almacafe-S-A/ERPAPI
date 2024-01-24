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
    public class BonificacionController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger logger;

        public BonificacionController(ApplicationDbContext context, ILogger<BonificacionController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        [HttpGet("[action]/{empleadoId}/{inactivos}")]
        public async Task<ActionResult> GetBonificacionesEmpleado(int empleadoId, bool inactivos)
        {
            try
            {
                List<Bonificacion> bonificaciones = null;
                if(inactivos)
                    bonificaciones = await context.Bonificaciones.Where(r => r.EmpleadoId == empleadoId).ToListAsync();
                else
                    bonificaciones = await context.Bonificaciones.Where(r => r.EmpleadoId == empleadoId).ToListAsync();

                return Ok(bonificaciones);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al cargar las bonificaciones del empleado");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{Periodo}/{Mes}")]
        public async Task<ActionResult> GetBonificacionesMesPeriodo(int Periodo, int Mes)
        {
            try
            {
                // Corregir el orden de los parámetros en fchInicio y fchFin
                DateTime fchInicio = new DateTime(Periodo, Mes, 1);
                DateTime fchFin = new DateTime(Periodo, Mes, 1).AddMonths(1);

                List<Bonificacion> bonificaciones = null;

                bonificaciones = await context.Bonificaciones
                    .Include(e => e.Empleado)
                    .Include(t => t.Tipo)
                    .Include(e => e.Estado)
                    .Where(r => r.FechaBono >= fchInicio && r.FechaBono < fchFin)
                    .OrderBy(b => b.EmpleadoId)
                    .ToListAsync();

                return Ok(bonificaciones);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al cargar las bonificaciones del empleado");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Guardar(Bonificacion registro)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (registro.Id == 0)
                        {
                            await context.Bonificaciones.AddAsync(registro);
                            await context.SaveChangesAsync();
                            transaction.Commit();
                            return Ok(registro);
                        }

                        var registroExistente =
                            await context.Bonificaciones.FirstOrDefaultAsync(r => r.Id == registro.Id);
                        if (registroExistente == null)
                        {
                            throw new Exception("No existe el registro de la bonificación a modificar");
                        }

                        context.Entry(registroExistente).CurrentValues.SetValues(registro);

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(context, logger, User.Identity.Name).SetAuditor();

                        await context.SaveChangesAsync();
                        transaction.Commit();
                        return Ok(registroExistente);
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex,"Error al guardar el registro de la bonificación");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{idBonificacion}/{estado}")]
        public async Task<IActionResult> ChangeStatus(long idBonificacion, int estado)
        {
            Bonificacion Items = new Bonificacion();
            try
            {
                Items = await context
                    .Bonificaciones
                    .Where(q => q.Id == idBonificacion).
                    FirstOrDefaultAsync();
                switch (estado)
                {
                    case 1:
                        Items.EstadoId = 6;
                        break;
                    case 2:
                        Items.EstadoId = 7;
                        break;
                    default:
                        break;
                }
                new appAuditor(context, logger, User.Identity.Name).SetAuditor();
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ocurrio un error: {ex.ToString()}");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }
    }
}