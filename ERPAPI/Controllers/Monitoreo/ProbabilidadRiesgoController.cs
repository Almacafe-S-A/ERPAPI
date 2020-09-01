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
    public class ProbabilidadRiesgoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ProbabilidadRiesgoController(ILogger<ProbabilidadRiesgoController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Probabilidad de Riesgo, por paginas
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProbabilidadRiesgoPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<ProbabilidadRiesgo> Items = new List<ProbabilidadRiesgo>();
            try
            {
                var query = _context.ProbabilidadRiesgo.AsQueryable();
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
        /// Obtiene el Listado de Probabilidad de Riesgo
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProbabilidadRiesgo()
        {
            List<ProbabilidadRiesgo> Items = new List<ProbabilidadRiesgo>();
            try
            {
                Items = await _context.ProbabilidadRiesgo.ToListAsync();
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
        /// Obtiene los Datos de Probabilidad de Riesgo por medio del Id enviado.
        /// </summary>
        /// <param name="IdProbabilidadRiesgo"></param>
        /// <returns></returns>
        [HttpGet("[action]/{IdProbabilidadRiesgo}")]
        public async Task<IActionResult> GetProbabilidadRiesgoById(Int64 IdProbabilidadRiesgo)
        {
            ProbabilidadRiesgo Items = new ProbabilidadRiesgo();
            try
            {
                Items = await _context.ProbabilidadRiesgo.Where(q => q.Id == IdProbabilidadRiesgo).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Inserta una nueva Probabilidad de Riesgo 
        /// </summary>
        /// <param name="_ProbabilidadRiesgo"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<ProbabilidadRiesgo>> Insert([FromBody]ProbabilidadRiesgo _ProbabilidadRiesgo)
        {
            ProbabilidadRiesgo ProbabilidadRiesgoq = new ProbabilidadRiesgo();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        ProbabilidadRiesgoq = _ProbabilidadRiesgo;
                        _context.ProbabilidadRiesgo.Add(ProbabilidadRiesgoq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = ProbabilidadRiesgoq.Id,
                            DocType = "ProbabilidadRiesgo",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(ProbabilidadRiesgoq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = ProbabilidadRiesgoq.UsuarioCreacion,
                            UsuarioModificacion = ProbabilidadRiesgoq.UsuarioModificacion,
                            UsuarioEjecucion = ProbabilidadRiesgoq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(ProbabilidadRiesgoq));
        }

        /// <summary>
        /// Actualiza la Probabilidad de Riesgo
        /// </summary>
        /// <param name="_ProbabilidadRiesgo"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<ProbabilidadRiesgo>> Update([FromBody]ProbabilidadRiesgo _ProbabilidadRiesgo)
        {
            ProbabilidadRiesgo _ProbabilidadRiesgoq = _ProbabilidadRiesgo;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _ProbabilidadRiesgoq = await (from c in _context.ProbabilidadRiesgo
                        .Where(q => q.Id == _ProbabilidadRiesgo.Id)
                                                  select c
                        ).FirstOrDefaultAsync();

                        _context.Entry(_ProbabilidadRiesgoq).CurrentValues.SetValues((_ProbabilidadRiesgo));

                        //_context.Bank.Update(_Bankq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _ProbabilidadRiesgoq.Id,
                            DocType = "ProbabilidadRiesgo",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_ProbabilidadRiesgoq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _ProbabilidadRiesgoq.UsuarioCreacion,
                            UsuarioModificacion = _ProbabilidadRiesgoq.UsuarioModificacion,
                            UsuarioEjecucion = _ProbabilidadRiesgoq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_ProbabilidadRiesgoq));
        }

        /// <summary>
        /// Elimina una Probabilidad de Riesgo      
        /// </summary>
        /// <param name="_ProbabilidadRiesgo"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]ProbabilidadRiesgo _ProbabilidadRiesgo)
        {
            ProbabilidadRiesgo _ProbabilidadRiesgoq = new ProbabilidadRiesgo();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _ProbabilidadRiesgoq = _context.ProbabilidadRiesgo
                        .Where(x => x.Id == (Int64)_ProbabilidadRiesgo.Id)
                        .FirstOrDefault();

                        _context.ProbabilidadRiesgo.Remove(_ProbabilidadRiesgoq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _ProbabilidadRiesgoq.Id,
                            DocType = "ProbabilidadRiesgo",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_ProbabilidadRiesgoq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Eliminar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _ProbabilidadRiesgoq.UsuarioCreacion,
                            UsuarioModificacion = _ProbabilidadRiesgoq.UsuarioModificacion,
                            UsuarioEjecucion = _ProbabilidadRiesgoq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_ProbabilidadRiesgoq));

        }
    }
}
