using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
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

        [HttpPost("[action]/{empleadoId}")]
        public async Task<ActionResult> GetBonificacionesEmpleado(int empleadoId)
        {
            try
            {
                var bonificaciones = await context.Bonificaciones.Where(r => r.EmpleadoId == empleadoId).ToListAsync();
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
    }
}