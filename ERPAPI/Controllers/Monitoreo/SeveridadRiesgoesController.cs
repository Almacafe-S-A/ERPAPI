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
    public class SeveridadRiesgoesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public SeveridadRiesgoesController(ILogger<SeveridadRiesgoesController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de SeveridadRiesgo, por paginas
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetServeridadRiesgoPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<SeveridadRiesgo> Items = new List<SeveridadRiesgo>();
            try
            {
                var query = _context.SeveridadRiesgo.AsQueryable();
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
        public async Task<IActionResult> GetSeveridadRiesgo()
        {
            List<SeveridadRiesgo> Items = new List<SeveridadRiesgo>();
            try
            {
                Items = await _context.SeveridadRiesgo.ToListAsync();
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
        /// Obtiene los Datos de la SeveridadRiesgo por medio del Id enviado.
        /// </summary>
        /// <param name="IdSeveridad"></param>
        /// <returns></returns>
        [HttpGet("[action]/{IdSeveridad}")]
        public async Task<IActionResult> GetSeveridadRiesgoById(Int64 IdSeveridad)
        {
            SeveridadRiesgo Items = new SeveridadRiesgo();
            try
            {
                Items = await _context.SeveridadRiesgo.Where(q => q.IdSeveridad == IdSeveridad).FirstOrDefaultAsync();
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
        /// <param name="_SeveridadRiesgo"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<SeveridadRiesgo>> Insert([FromBody]SeveridadRiesgo _SeveridadRiesgo)
        {
            SeveridadRiesgo SeveridadRiesgoq = new SeveridadRiesgo();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        SeveridadRiesgoq = _SeveridadRiesgo;
                        _context.SeveridadRiesgo.Add(SeveridadRiesgoq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = SeveridadRiesgoq.IdSeveridad,
                            DocType = "SeveridadRiesgo",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(SeveridadRiesgoq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = SeveridadRiesgoq.UsuarioCreacion,
                            UsuarioModificacion = SeveridadRiesgoq.UsuarioModificacion,
                            UsuarioEjecucion = SeveridadRiesgoq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(SeveridadRiesgoq));
        }

        /// <summary>
        /// Actualiza la Severidad Riesgo
        /// </summary>
        /// <param name="_SeveridadRiesgo"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<SeveridadRiesgo>> Update([FromBody]SeveridadRiesgo _SeveridadRiesgo)
        {
            SeveridadRiesgo _SeveridadRiesgoq = _SeveridadRiesgo;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _SeveridadRiesgoq = await (from c in _context.SeveridadRiesgo
                        .Where(q => q.IdSeveridad == _SeveridadRiesgo.IdSeveridad)
                                                   select c
                        ).FirstOrDefaultAsync();

                        _context.Entry(_SeveridadRiesgoq).CurrentValues.SetValues((_SeveridadRiesgo));

                        //_context.Bank.Update(_Bankq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _SeveridadRiesgoq.IdSeveridad,
                            DocType = "SeveridadRiesgo",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_SeveridadRiesgoq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _SeveridadRiesgoq.UsuarioCreacion,
                            UsuarioModificacion = _SeveridadRiesgoq.UsuarioModificacion,
                            UsuarioEjecucion = _SeveridadRiesgoq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_SeveridadRiesgoq));
        }

        /// <summary>
        /// Elimina una Severidad Riesgo      
        /// </summary>
        /// <param name="_SeveridadRiesgo"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]SeveridadRiesgo _SeveridadRiesgo)
        {
            SeveridadRiesgo _SeveridadRiesgoq = new SeveridadRiesgo();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _SeveridadRiesgoq = _context.SeveridadRiesgo
                        .Where(x => x.IdSeveridad == (Int64)_SeveridadRiesgo.IdSeveridad)
                        .FirstOrDefault();

                        _context.SeveridadRiesgo.Remove(_SeveridadRiesgoq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _SeveridadRiesgoq.IdSeveridad,
                            DocType = "SeveridadRiesgo",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_SeveridadRiesgoq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Eliminar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _SeveridadRiesgoq.UsuarioCreacion,
                            UsuarioModificacion = _SeveridadRiesgoq.UsuarioModificacion,
                            UsuarioEjecucion = _SeveridadRiesgoq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_SeveridadRiesgoq));

        }
    }
}
