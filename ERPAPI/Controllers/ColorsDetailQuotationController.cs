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
    [Route("api/ColorsDetailQuotation")]
    [ApiController]
    public class ColorsDetailQuotationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ColorsDetailQuotationController(ILogger<ColorsDetailQuotationController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Colores detalle cotizacion, con paginacion
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetColorsDetailQuotationPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<ColorsDetailQuotation> Items = new List<ColorsDetailQuotation>();
            try
            {
                var query = _context.ColorsDetailQuotation.AsQueryable();
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
        /// Obtiene el Listado de Colores detalle cotizacion, ordenado por id de colores
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetColorsDetailQuotation()
        {
            List<ColorsDetailQuotation> Items = new List<ColorsDetailQuotation>();
            try
            {
                Items = await _context.ColorsDetailQuotation.OrderBy(b => b.ColorId).ToListAsync();
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
        /// Obtiene los Datos de Colores detalle cotizacion por medio del Id enviado.
        /// </summary>
        /// <param name="ColorsDetailQuotationId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{ColorsDetailQuotationId}")]
        public async Task<IActionResult> GetColorsDetailQuotationById(Int64 ColorsDetailQuotationId)
        {
            ColorsDetailQuotation Items = new ColorsDetailQuotation();
            try
            {
                Items = await _context.ColorsDetailQuotation.Where(q => q.ColorsDetailQuotationId == ColorsDetailQuotationId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene el color por el codigo de cotizacion enviado
        /// </summary>
        /// <param name="ColorsDetailQuotationCode"></param>
        /// <returns></returns>
        [HttpGet("[action]/{ColorsDetailQuotationCode}")]
        public async Task<IActionResult> GetColorsDetailQuotationByCode(Int64 ColorsDetailQuotationCode)
        {
            ColorsDetailQuotation Items = new ColorsDetailQuotation();
            try
            {
                Items = await _context.ColorsDetailQuotation.Where(q => q.QuotationCode == ColorsDetailQuotationCode).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene el Colores detalle cotizacion por el id del color
        /// </summary>
        /// <param name="_ColorId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{ColorsDetailQuotationDescription}")]
        public async Task<IActionResult> GetColorsDetailQuotationByDescription(Int64 _ColorId)
        {
            ColorsDetailQuotation Items = new ColorsDetailQuotation();
            try
            {
                Items = await _context.ColorsDetailQuotation.Where(q => q.ColorId == _ColorId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Inserta un nuevo Colores detalle cotizacion
        /// </summary>
        /// <param name="_ColorsDetailQuotation"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<ColorsDetailQuotation>> Insert([FromBody]ColorsDetailQuotation _ColorsDetailQuotation)
        {
            ColorsDetailQuotation _ColorsDetailQuotationq = new ColorsDetailQuotation();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _ColorsDetailQuotationq = _ColorsDetailQuotation;
                        _context.ColorsDetailQuotation.Add(_ColorsDetailQuotationq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _ColorsDetailQuotationq.ColorsDetailQuotationId,
                            DocType = "ColorsDetailQuotation",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_ColorsDetailQuotationq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _ColorsDetailQuotationq.UsuarioCreacion,
                            UsuarioModificacion = _ColorsDetailQuotationq.UsuarioModificacion,
                            UsuarioEjecucion = _ColorsDetailQuotationq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_ColorsDetailQuotationq));
        }

        /// <summary>
        /// Actualiza el Colores detalle cotizacion
        /// </summary>
        /// <param name="_ColorsDetailQuotation"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<ColorsDetailQuotation>> Update([FromBody]ColorsDetailQuotation _ColorsDetailQuotation)
        {
            ColorsDetailQuotation _ColorsDetailQuotationq = _ColorsDetailQuotation;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _ColorsDetailQuotationq = await (from c in _context.ColorsDetailQuotation
                        .Where(q => q.ColorsDetailQuotationId == _ColorsDetailQuotation.ColorsDetailQuotationId)
                                          select c
                        ).FirstOrDefaultAsync();

                        _context.Entry(_ColorsDetailQuotationq).CurrentValues.SetValues((_ColorsDetailQuotation));
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _ColorsDetailQuotationq.ColorsDetailQuotationId,
                            DocType = "ColorsDetailQuotation",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_ColorsDetailQuotationq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _ColorsDetailQuotationq.UsuarioCreacion,
                            UsuarioModificacion = _ColorsDetailQuotationq.UsuarioModificacion,
                            UsuarioEjecucion = _ColorsDetailQuotationq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_ColorsDetailQuotationq));
        }

        /// <summary>
        /// Elimina un Color       
        /// </summary>
        /// <param name="_ColorsDetailQuotation"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]ColorsDetailQuotation _ColorsDetailQuotation)
        {
            ColorsDetailQuotation _ColorsDetailQuotationq = new ColorsDetailQuotation();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _ColorsDetailQuotationq = _context.ColorsDetailQuotation
                        .Where(x => x.ColorsDetailQuotationId == (Int64)_ColorsDetailQuotation.ColorsDetailQuotationId)
                        .FirstOrDefault();

                        _context.ColorsDetailQuotation.Remove(_ColorsDetailQuotationq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _ColorsDetailQuotationq.ColorsDetailQuotationId,
                            DocType = "ColorsDetailQuotation",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_ColorsDetailQuotationq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Eliminar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _ColorsDetailQuotationq.UsuarioCreacion,
                            UsuarioModificacion = _ColorsDetailQuotationq.UsuarioModificacion,
                            UsuarioEjecucion = _ColorsDetailQuotationq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_ColorsDetailQuotationq));

        }
    }
}
