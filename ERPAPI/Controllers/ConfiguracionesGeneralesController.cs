using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ConfiguracionesGeneralesesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ConfiguracionesGeneralesesController(ILogger<ConfiguracionesGeneralesesController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de ConfiguracionesGenerales, por paginas
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetServeridadRiesgoPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<ConfiguracionesGenerales> Items = new List<ConfiguracionesGenerales>();
            try
            {
                var query = _context.ConfiguracionesGenerales.AsQueryable();
                var totalRegistro = query.Count();

                Items = await query
                   .Skip(cantidadDeRegistros * (numeroDePagina - 1))
                   .Take(cantidadDeRegistros)
                    .ToListAsync();

                Response.Headers["X-Total-Registros"] = totalRegistro.ToString();
                Response.Headers["X-Cantidad-Paginas"] = ((Int64)Math.Ceiling((double)totalRegistro / cantidadDeRegistros)).ToString();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene el Listado de Severidad Riesgo 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetConfiguracionesGenerales()
        {
            List<ConfiguracionesGenerales> Items = new List<ConfiguracionesGenerales>();
            try
            {
                Items = await _context.ConfiguracionesGenerales.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene los Datos de la ConfiguracionesGenerales por medio del Id enviado.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetConfiguracionesGeneralesById(Int64 Id)
        {
            ConfiguracionesGenerales Items = new ConfiguracionesGenerales();
            try
            {
                Items = await _context.ConfiguracionesGenerales.Where(q => q.Id == Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Inserta una nueva severidad riesgo
        /// </summary>
        /// <param name="_ConfiguracionesGenerales"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<ConfiguracionesGenerales>> Insert([FromBody]ConfiguracionesGenerales _ConfiguracionesGenerales)
        {
            ConfiguracionesGenerales ConfiguracionesGeneralesq = new ConfiguracionesGenerales();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        ConfiguracionesGeneralesq = _ConfiguracionesGenerales;
                        _context.ConfiguracionesGenerales.Add(ConfiguracionesGeneralesq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = ConfiguracionesGeneralesq.Id,
                            DocType = "ConfiguracionesGenerales",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(ConfiguracionesGeneralesq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = ConfiguracionesGeneralesq.UsuarioCreacion,
                            UsuarioModificacion = ConfiguracionesGeneralesq.UsuarioModificacion,
                            UsuarioEjecucion = ConfiguracionesGeneralesq.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(ConfiguracionesGeneralesq));
        }

        /// <summary>
        /// Actualiza la Severidad Riesgo
        /// </summary>
        /// <param name="_ConfiguracionesGenerales"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<ConfiguracionesGenerales>> Update([FromBody]ConfiguracionesGenerales _ConfiguracionesGenerales)
        {
            ConfiguracionesGenerales _ConfiguracionesGeneralesq = _ConfiguracionesGenerales;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _ConfiguracionesGeneralesq = await (from c in _context.ConfiguracionesGenerales
                        .Where(q => q.Id == _ConfiguracionesGenerales.Id)
                                                   select c
                        ).FirstOrDefaultAsync();

                        _context.Entry(_ConfiguracionesGeneralesq).CurrentValues.SetValues((_ConfiguracionesGenerales));

                        //_context.Bank.Update(_Bankq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _ConfiguracionesGeneralesq.Id,
                            DocType = "ConfiguracionesGenerales",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_ConfiguracionesGeneralesq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _ConfiguracionesGeneralesq.UsuarioCreacion,
                            UsuarioModificacion = _ConfiguracionesGeneralesq.UsuarioModificacion,
                            UsuarioEjecucion = _ConfiguracionesGeneralesq.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_ConfiguracionesGeneralesq));
        }

        /// <summary>
        /// Elimina una Severidad Riesgo      
        /// </summary>
        /// <param name="_ConfiguracionesGenerales"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]ConfiguracionesGenerales _ConfiguracionesGenerales)
        {
            ConfiguracionesGenerales _ConfiguracionesGeneralesq = new ConfiguracionesGenerales();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _ConfiguracionesGeneralesq = _context.ConfiguracionesGenerales
                        .Where(x => x.Id == (Int64)_ConfiguracionesGenerales.Id)
                        .FirstOrDefault();

                        _context.ConfiguracionesGenerales.Remove(_ConfiguracionesGeneralesq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _ConfiguracionesGeneralesq.Id,
                            DocType = "ConfiguracionesGenerales",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_ConfiguracionesGeneralesq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Eliminar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _ConfiguracionesGeneralesq.UsuarioCreacion,
                            UsuarioModificacion = _ConfiguracionesGeneralesq.UsuarioModificacion,
                            UsuarioEjecucion = _ConfiguracionesGeneralesq.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_ConfiguracionesGeneralesq));

        }
    }
}
