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
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

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
                    Feriado f =  context.Feriados
                        .Where(q => q.Nombre == feriado.Nombre && q.PeriodoId == feriado.PeriodoId 
                        && ((q.FechaInicio >= feriado.FechaInicio &&  feriado.FechaInicio<=q.FechaFin) ||
                        (q.FechaFin >= feriado.FechaFin && feriado.FechaFin<=q.FechaFin ))).FirstOrDefault();
                    if (f != null)
                    {
                        return BadRequest("Ya existe un Feriado con esta configuracion");
                    }
                    context.Feriados.Add(feriado);

                    //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                    new appAuditor(context, logger, User.Identity.Name).SetAuditor();

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

                    //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                    new appAuditor(context, logger, User.Identity.Name).SetAuditor();

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

                    //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                    new appAuditor(context, logger, User.Identity.Name).SetAuditor();

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

    /*    [HttpGet("[action]/{anio}")]
        public async Task<ActionResult> GetFeriadoAnio(int P)
        {
            try
            {
                var feriados = await context.Feriados.Include(e=>e.Estado).Where(f=>f.PeriodoId==anio).ToListAsync();
                return Ok(feriados);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al cargar feriados por año");
                return BadRequest(ex);
            }
        }*/

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

        /// <summary>
        /// Obtiene el Listado de Feriados 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{periodoId}")]
        public async Task<ActionResult<List<Feriado>>> GetFeriadosByPeriodo(int periodoId)
        {
            List<Feriado> Items = new List<Feriado>();
            try
            {
                Items = await context.Feriados.Where(q => q.PeriodoId == periodoId).ToListAsync();
            }
            catch (Exception ex)
            {

                logger.LogError($"Ocurrio un error: {ex.ToString()}");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<Feriado>> Update(Feriado feriado)
        {
            try
            {
                Feriado feriadoExistente = await context.Feriados.FirstOrDefaultAsync(f => f.Id == feriado.Id);
                if (feriadoExistente == null)
                {
                    return NotFound("Registro de feriado a actualizar no existe");
                }

                // Verificar si existe otro feriado con el mismo nombre en el mismo PeriodoId
                Feriado feriadoDuplicado = await context.Feriados
                    .FirstOrDefaultAsync(f => f.Nombre == feriado.Nombre && f.PeriodoId == feriado.PeriodoId && f.Id != feriado.Id);
                if (feriadoDuplicado != null)
                {
                    return BadRequest("Ya existe un Feriado con este nombre en el mismo PeriodoId");
                }

                // Actualizar las propiedades del feriado existente con los valores del feriado recibido
                context.Entry(feriadoExistente).CurrentValues.SetValues(feriado);

                // Actualizar los datos de auditoría
                new appAuditor(context, logger, User.Identity.Name).SetAuditor();

                await context.SaveChangesAsync();
                return Ok(feriadoExistente);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualizar el registro del feriado");
                return BadRequest("Error al actualizar el registro del feriado");
            }
        }



    }
}
