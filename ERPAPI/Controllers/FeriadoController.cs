using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
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
    public class FeriadoController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger logger;

        public FeriadoController(ApplicationDbContext context, ILogger<FeriadoController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Guardar(Feriado feriado)
        {
            try
            {
                if (feriado.Id == 0)
                {
                    context.Feriados.Add(feriado);
                    await context.SaveChangesAsync();
                    return Ok(feriado);
                }
                else
                {
                    Feriado feriadoExistente = await context.Feriados.FirstOrDefaultAsync(f => f.Id == feriado.Id);
                    if (feriadoExistente == null)
                    {
                        throw new Exception("Registro de feriado a actualizar no existe");
                    }

                    context.Entry(feriadoExistente).CurrentValues.SetValues(feriado);
                    await context.SaveChangesAsync();
                    return Ok(feriadoExistente);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar el registro del feriado");
                return BadRequest(ex);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Delete(Feriado feriado)
        {
            try
            {
                var vferiado = context.Feriados.Where(q => q.Id == feriado.Id).FirstOrDefault();
                if (vferiado!=null)
                {
                    context.Feriados.Remove(vferiado);
                    await context.SaveChangesAsync();
                    return Ok(feriado);
                }
                else
                {
                    return BadRequest("No se encontro el Feriado");
                }

                
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar el registro del feriado");
                return BadRequest(ex);
            }
        }


        [HttpGet("[action]")]
        public async Task<ActionResult> GetFeriados()
        {
            try
            {
                var feriados = await context.Feriados.Include(e=>e.Estado).ToListAsync();
                return Ok(feriados);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al cargar feriados");
                return BadRequest(ex);
            }
        }

        [HttpGet("[action]/{anio}")]
        public async Task<ActionResult> GetFeriadoAnio(int anio)
        {
            try
            {
                var feriados = await context.Feriados.Include(e=>e.Estado).Where(f=>f.Anio==anio).ToListAsync();
                return Ok(feriados);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al cargar feriados por año");
                return BadRequest(ex);
            }
        }

        [HttpGet("[action]/{fecha}")]
        public async Task<ActionResult> GetFeriadoFecha(DateTime fecha)
        {
            try
            {
                var feriados = await context.Feriados.Include(e => e.Estado).Where(f=> f.FechaInicio <= fecha && f.FechaFin >= fecha).ToListAsync();
                return Ok(feriados);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al cargar feriados");
                return BadRequest(ex);
            }
        }



    }
}
