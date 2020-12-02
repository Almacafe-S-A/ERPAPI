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
    public class ContextoRiesgoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ContextoRiesgoController(ILogger<ContextoRiesgoController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de ContextoRiesgo, por paginas
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetContextoRiesgoPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<ContextoRiesgo> Items = new List<ContextoRiesgo>();
            try
            {
                var query = _context.ContextoRiesgo.AsQueryable();
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
        /// Obtiene el Listado de Contexto Riesgo 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetContextoRiesgo()
        {
            List<ContextoRiesgo> Items = new List<ContextoRiesgo>();
            try
            {
                Items = await _context.ContextoRiesgo.ToListAsync();
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
        /// Obtiene los Datos de Contexto Riesgo por medio del Id enviado.
        /// </summary>
        /// <param name="IdContextoRiesgo"></param>
        /// <returns></returns>
        [HttpGet("[action]/{IdContextoRiesgo}")]
        public async Task<IActionResult> GetContextoRiesgoById(Int64 IdContextoRiesgo)
        {
            ContextoRiesgo Items = new ContextoRiesgo();
            try
            {
                Items = await _context.ContextoRiesgo.Where(q => q.IdContextoRiesgo == IdContextoRiesgo).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Inserta una nuevo contexto riesgo
        /// </summary>
        /// <param name="_ContextoRiesgo"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<ContextoRiesgo>> Insert([FromBody]ContextoRiesgo _ContextoRiesgo)
        {
            ContextoRiesgo ContextoRiesgoq = new ContextoRiesgo();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        ContextoRiesgoq = _ContextoRiesgo;
                        _context.ContextoRiesgo.Add(ContextoRiesgoq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = ContextoRiesgoq.IdContextoRiesgo,
                            DocType = "ContextoRiesgo",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(ContextoRiesgoq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = ContextoRiesgoq.UsuarioCreacion,
                            UsuarioModificacion = ContextoRiesgoq.UsuarioModificacion,
                            UsuarioEjecucion = ContextoRiesgoq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(ContextoRiesgoq));
        }

        /// <summary>
        /// Actualiza Contexto Riesgo
        /// </summary>
        /// <param name="_ContextoRiesgo"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<ContextoRiesgo>> Update([FromBody]ContextoRiesgo _ContextoRiesgo)
        {
            ContextoRiesgo _ContextoRiesgoq = _ContextoRiesgo;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _ContextoRiesgoq = await (from c in _context.ContextoRiesgo
                        .Where(q => q.IdContextoRiesgo == _ContextoRiesgo.IdContextoRiesgo)
                                                  select c
                        ).FirstOrDefaultAsync();

                        _context.Entry(_ContextoRiesgoq).CurrentValues.SetValues((_ContextoRiesgo));

                        //_context.Bank.Update(_Bankq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _ContextoRiesgoq.IdContextoRiesgo,
                            DocType = "ContextoRiesgo",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_ContextoRiesgoq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _ContextoRiesgoq.UsuarioCreacion,
                            UsuarioModificacion = _ContextoRiesgoq.UsuarioModificacion,
                            UsuarioEjecucion = _ContextoRiesgoq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_ContextoRiesgoq));
        }

        /// <summary>
        /// Elimina un Contexto Riesgo      
        /// </summary>
        /// <param name="_ContextoRiesgo"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]ContextoRiesgo _ContextoRiesgo)
        {
            ContextoRiesgo _ContextoRiesgoq = new ContextoRiesgo();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _ContextoRiesgoq = _context.ContextoRiesgo
                        .Where(x => x.IdContextoRiesgo == (Int64)_ContextoRiesgo.IdContextoRiesgo)
                        .FirstOrDefault();

                        _context.ContextoRiesgo.Remove(_ContextoRiesgoq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _ContextoRiesgoq.IdContextoRiesgo,
                            DocType = "ContextoRiesgo",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_ContextoRiesgoq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Eliminar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _ContextoRiesgoq.UsuarioCreacion,
                            UsuarioModificacion = _ContextoRiesgoq.UsuarioModificacion,
                            UsuarioEjecucion = _ContextoRiesgoq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_ContextoRiesgoq));

        }
    }
}
