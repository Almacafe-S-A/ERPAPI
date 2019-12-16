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
    [Route("api/Quotation")]
    [ApiController]
    public class QuotationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public QuotationController(ILogger<QuotationController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Cotizaciones, con paginacion
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetQuotationPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Quotation> Items = new List<Quotation>();
            try
            {
                var query = _context.Quotation.AsQueryable();
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
        /// Obtiene el Listado de Cotizaciones, ordenado por codigo de cotizacion
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetQuotation()
        {
            List<Quotation> Items = new List<Quotation>();
            try
            {
                Items = await _context.Quotation.OrderBy(b => b.QuotationCode).ToListAsync();
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
        /// Obtiene los Datos de Cotizacion por medio del Codigo enviado.
        /// </summary>
        /// <param name="QuotationCode"></param>
        /// <returns></returns>
        [HttpGet("[action]/{QuotationCode}")]
        public async Task<IActionResult> GetQuotationById(Int64 QuotationCode)
        {
            Quotation Items = new Quotation();
            try
            {
                Items = await _context.Quotation.Where(q => q.QuotationCode == QuotationCode).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Inserta una nueva cotizacion
        /// </summary>
        /// <param name="_Quotation"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Quotation>> Insert([FromBody]Quotation _Quotation)
        {
            Quotation _Quotationq = new Quotation();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Quotationq = _Quotation;
                        _context.Quotation.Add(_Quotationq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Quotationq.QuotationCode,
                            DocType = "Quotation",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Quotationq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Quotationq.UsuarioCreacion,
                            UsuarioModificacion = _Quotationq.UsuarioModificacion,
                            UsuarioEjecucion = _Quotationq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_Quotationq));
        }

        /// <summary>
        /// Actualiza la cotizacion
        /// </summary>
        /// <param name="_Quotation"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Quotation>> Update([FromBody]Quotation _Quotation)
        {
            Quotation _Quotationq = _Quotation;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Quotationq = await (from c in _context.Quotation
                        .Where(q => q.QuotationCode == _Quotation.QuotationCode)
                                          select c
                        ).FirstOrDefaultAsync();

                        _context.Entry(_Quotationq).CurrentValues.SetValues((_Quotation));
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Quotationq.QuotationCode,
                            DocType = "Quotation",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Quotationq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Quotationq.UsuarioCreacion,
                            UsuarioModificacion = _Quotationq.UsuarioModificacion,
                            UsuarioEjecucion = _Quotationq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_Quotationq));
        }

        /// <summary>
        /// Elimina un Color       
        /// </summary>
        /// <param name="_Quotation"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Quotation _Quotation)
        {
            Quotation _Quotationq = new Quotation();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Quotationq = _context.Quotation
                        .Where(x => x.QuotationCode == (Int64)_Quotation.QuotationCode)
                        .FirstOrDefault();

                        _context.Quotation.Remove(_Quotationq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Quotationq.QuotationCode,
                            DocType = "Quotation",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Quotationq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Eliminar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Quotationq.UsuarioCreacion,
                            UsuarioModificacion = _Quotationq.UsuarioModificacion,
                            UsuarioEjecucion = _Quotationq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_Quotationq));

        }
    }
}
