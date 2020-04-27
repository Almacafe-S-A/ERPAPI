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
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ISRController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ISRController(ILogger<ISRController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Guardar(ISR configuracion)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        if (configuracion.Id == 0)
                        {
                            _context.ISRConfiguracion.Add(configuracion);

                            ISR rango = _context.ISRConfiguracion.Where(w => w.De <= configuracion.De && w.Hasta >= configuracion.De).FirstOrDefault();
                             rango = _context.ISRConfiguracion.Where(w => w.De <= configuracion.Hasta && w.Hasta >= configuracion.Hasta).FirstOrDefault();
                            if (rango!=null)
                            {
                                return BadRequest("Rango debe ser unico");
                            }


                            await _context.SaveChangesAsync();
                            BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                                                                               {
                                                                                   IdOperacion = configuracion.Id,
                                                                                   DocType = "ConfiguracionISR",
                                                                                   ClaseInicial =
                                                                                       Newtonsoft.Json.JsonConvert.SerializeObject(configuracion, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                                                                                   Accion = "Insert",
                                                                                   FechaCreacion = DateTime.Now,
                                                                                   FechaModificacion = DateTime.Now,
                                                                                   UsuarioCreacion = configuracion.UsuarioCreacion,
                                                                                   UsuarioModificacion = configuracion.UsuarioModificacion,
                                                                                   UsuarioEjecucion = configuracion.UsuarioModificacion,

                                                                               });
                            await _context.SaveChangesAsync();
                            transaction.Commit();
                            return Ok(configuracion);
                        }
                        else
                        {
                            ISR rango = _context.ISRConfiguracion.Where(w => w.De <= configuracion.De && w.Hasta >= configuracion.De).FirstOrDefault();
                            rango = _context.ISRConfiguracion.Where(w => w.De <= configuracion.Hasta && w.Hasta >= configuracion.Hasta).FirstOrDefault();
                            if (rango != null&&rango.Id!=configuracion.Id)
                            {
                                return BadRequest("Rango debe ser unico");
                            }
                            var configuracionActual = await _context.ISRConfiguracion.Where(c => c.Id == configuracion.Id)
                                .FirstOrDefaultAsync();
                            _context.Entry(configuracionActual).CurrentValues.SetValues(configuracion);
                            await _context.SaveChangesAsync();

                            BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                                                                               {
                                                                                   IdOperacion = configuracionActual.Id,
                                                                                   DocType = "ConfiguracionISR",
                                                                                   ClaseInicial =
                                                                                       Newtonsoft.Json.JsonConvert.SerializeObject(configuracionActual, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                                                                                   Accion = "Update",
                                                                                   FechaCreacion = DateTime.Now,
                                                                                   FechaModificacion = DateTime.Now,
                                                                                   UsuarioCreacion = configuracionActual.UsuarioCreacion,
                                                                                   UsuarioModificacion = configuracionActual.UsuarioModificacion,
                                                                                   UsuarioEjecucion = configuracionActual.UsuarioModificacion,

                                                                               });
                            await _context.SaveChangesAsync();
                            transaction.Commit();
                            return Ok(configuracionActual);
                        }
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
                _logger.LogError(ex, "Error al guardar el registro de la configuración del ISR");
                return BadRequest(ex);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetISRConfiguracion()
        {
            try
            {
                var configuraciones = await _context.ISRConfiguracion.ToListAsync();
                return Ok(configuraciones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cargando configuración del ISR");
                return BadRequest(ex);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetISRConfiguracionPorId(long id)
        {
            try
            {
                var configuracion = await _context.ISRConfiguracion.FirstOrDefaultAsync(c=>c.Id == id);
                if (configuracion == null)
                {
                    return Ok();
                }
                return Ok(configuracion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cargando configuración del ISR");
                return BadRequest(ex);
            }
        }

        [HttpPost("[action]/{id}")]
        public async Task<ActionResult> Borrar(long id)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        var configuracionActual = await _context.ISRConfiguracion.Where(c => c.Id == id)
                            .FirstOrDefaultAsync();
                        _context.ISRConfiguracion.Remove(configuracionActual);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                                                                           {
                                                                               IdOperacion = configuracionActual.Id,
                                                                               DocType = "ConfiguracionISR",
                                                                               ClaseInicial =
                                                                                   Newtonsoft.Json.JsonConvert
                                                                                       .SerializeObject(
                                                                                           configuracionActual,
                                                                                           new JsonSerializerSettings
                                                                                           {
                                                                                               ReferenceLoopHandling =
                                                                                                   ReferenceLoopHandling
                                                                                                       .Ignore
                                                                                           }),
                                                                               Accion = "Delete",
                                                                               FechaCreacion = DateTime.Now,
                                                                               FechaModificacion = DateTime.Now,
                                                                               UsuarioCreacion =
                                                                                   configuracionActual.UsuarioCreacion,
                                                                               UsuarioModificacion =
                                                                                   configuracionActual
                                                                                       .UsuarioModificacion,
                                                                               UsuarioEjecucion =
                                                                                   configuracionActual
                                                                                       .UsuarioModificacion,

                                                                           });
                        await _context.SaveChangesAsync();
                        transaction.Commit();
                        return Ok();
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
                _logger.LogError(ex, "Error al guardar el registro de la configuración del ISR");
                return BadRequest(ex);
            }
        }
    }
}