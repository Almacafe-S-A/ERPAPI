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
    [Route("api/QuotationDetail")]
    [ApiController]
    public class QuotationDetailController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public QuotationDetailController(ILogger<QuotationDetailController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de detalle de cotizacion, con paginacion
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetQuotationDetailPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<QuotationDetail> Items = new List<QuotationDetail>();
            try
            {
                var query = _context.QuotationDetail.AsQueryable();
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
        /// Obtiene el Listado de detalle de cotizacion, ordenado por codigo de Cotizacion y id
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetQuotationDetail()
        {
            List<QuotationDetail> Items = new List<QuotationDetail>();
            try
            {
                Items = await _context.QuotationDetail.OrderBy(b => b.QuotationCode & b.QuotationDetailId).ToListAsync();
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
        /// Obtiene los Datos de detalle de cotizacion por medio del Id enviado.
        /// </summary>
        /// <param name="QuotationDetailId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{QuotationDetailId}")]
        public async Task<IActionResult> GetQuotationDetailById(Int64 QuotationDetailId)
        {
            QuotationDetail Items = new QuotationDetail();
            try
            {
                Items = await _context.QuotationDetail.Where(q => q.QuotationDetailId == QuotationDetailId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene el detalle de cotizacion por el codigo enviado
        /// </summary>
        /// <param name="QuotationDetailCode"></param>
        /// <returns></returns>
        [HttpGet("[action]/{QuotationDetailCode}")]
        public async Task<IActionResult> GetQuotationDetailByCode(Int64 QuotationDetailCode)
        {
            QuotationDetail Items = new QuotationDetail();
            try
            {
                Items = await _context.QuotationDetail.Where(q => q.QuotationCode == QuotationDetailCode).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene el detalle de cotizacion, por medio del id y el codigo de cotizacion
        /// </summary>
        /// <param name="QuotationDetailId"></param>
        /// <param name="QuotationCode"></param>
        /// <returns></returns>
        [HttpGet("[action]/{QuotationDetailDescription}")]
        public async Task<IActionResult> GetQuotationDetailByDescription(Int64 QuotationDetailId, Int64 QuotationCode)
        {
            QuotationDetail Items = new QuotationDetail();
            try
            {
                Items = await _context.QuotationDetail.Where(q => q.QuotationCode == QuotationCode & q.QuotationDetailId == QuotationDetailId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Inserta un nuevo detalle de cotizacion
        /// </summary>
        /// <param name="_QuotationDetail"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<QuotationDetail>> Insert([FromBody]QuotationDetail _QuotationDetail)
        {
            QuotationDetail _QuotationDetailq = new QuotationDetail();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _QuotationDetailq = _QuotationDetail;
                        _context.QuotationDetail.Add(_QuotationDetailq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _QuotationDetailq.QuotationDetailId,
                            DocType = "QuotationDetail",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_QuotationDetailq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _QuotationDetailq.UsuarioCreacion,
                            UsuarioModificacion = _QuotationDetailq.UsuarioModificacion,
                            UsuarioEjecucion = _QuotationDetailq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_QuotationDetailq));
        }

        /// <summary>
        /// Actualiza el Color
        /// </summary>
        /// <param name="_QuotationDetail"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<QuotationDetail>> Update([FromBody]QuotationDetail _QuotationDetail)
        {
            QuotationDetail _QuotationDetailq = _QuotationDetail;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _QuotationDetailq = await (from c in _context.QuotationDetail
                        .Where(q => q.QuotationCode == _QuotationDetail.QuotationCode & q.QuotationDetailId == _QuotationDetail.QuotationDetailId)
                                          select c
                        ).FirstOrDefaultAsync();

                        _context.Entry(_QuotationDetailq).CurrentValues.SetValues((_QuotationDetail));
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _QuotationDetailq.QuotationDetailId,
                            DocType = "QuotationDetail",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_QuotationDetailq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _QuotationDetailq.UsuarioCreacion,
                            UsuarioModificacion = _QuotationDetailq.UsuarioModificacion,
                            UsuarioEjecucion = _QuotationDetailq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_QuotationDetailq));
        }

        /// <summary>
        /// Elimina un Color       
        /// </summary>
        /// <param name="_QuotationDetail"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]QuotationDetail _QuotationDetail)
        {
            QuotationDetail _QuotationDetailq = new QuotationDetail();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _QuotationDetailq = _context.QuotationDetail
                        .Where(x => x.QuotationCode == (Int64)_QuotationDetail.QuotationCode & x.QuotationDetailId == _QuotationDetail.QuotationDetailId)
                        .FirstOrDefault();

                        _context.QuotationDetail.Remove(_QuotationDetailq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _QuotationDetailq.QuotationDetailId,
                            DocType = "QuotationDetail",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_QuotationDetailq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Eliminar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _QuotationDetailq.UsuarioCreacion,
                            UsuarioModificacion = _QuotationDetailq.UsuarioModificacion,
                            UsuarioEjecucion = _QuotationDetailq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_QuotationDetailq));

        }
    }
}
