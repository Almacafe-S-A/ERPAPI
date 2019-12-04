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
    public class PayrollDeductionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public PayrollDeductionController(ILogger<PayrollDeductionController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene las deducciones a la planilla por paginas
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPayrollDeductionPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<PayrollDeduction> Items = new List<PayrollDeduction>();
            try
            {
                var query = _context.PayrollDeduction.AsQueryable();
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
        /// Obtiene las deducciones a la planilla
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPayrollDeduction()
        {
            List<PayrollDeduction> Items = new List<PayrollDeduction>();
            try
            {
                Items = await _context.PayrollDeduction.ToListAsync();
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
        /// Obtiene la deduccion a la planilla del Id enviado.
        /// </summary>
        /// <param name="IdPayrollDeduction"></param>
        /// <returns></returns>
        [HttpGet("[action]/{IdPayrollDeduction}")]
        public async Task<IActionResult> GetSeveridadRiesgoById(Int64 IdPayrollDeduction)
        {
            PayrollDeduction Items = new PayrollDeduction();
            try
            {
                Items = await _context.PayrollDeduction.Where(q => q.PayrollDeductionId == IdPayrollDeduction).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Inserta una nueva deduccion a la planilla
        /// </summary>
        /// <param name="_PayrollDeduction"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<PayrollDeduction>> Insert([FromBody]PayrollDeduction _PayrollDeduction)
        {
            PayrollDeduction PayrollDeductionq = new PayrollDeduction();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        PayrollDeductionq = _PayrollDeduction;
                        _context.PayrollDeduction.Add(PayrollDeductionq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = PayrollDeductionq.PayrollDeductionId,
                            DocType = "PayrollDeduction",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(PayrollDeductionq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = PayrollDeductionq.UsuarioCreacion,
                            UsuarioModificacion = PayrollDeductionq.UsuarioModificacion,
                            UsuarioEjecucion = PayrollDeductionq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(PayrollDeductionq));
        }

        /// <summary>
        /// Actualiza deduccion a la planilla
        /// </summary>
        /// <param name="_PayrollDeduction"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<PayrollDeduction>> Update([FromBody]PayrollDeduction _PayrollDeduction)
        {
            PayrollDeduction PayrollDeductionq = _PayrollDeduction;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        PayrollDeductionq = await (from c in _context.PayrollDeduction
                        .Where(q => q.PayrollDeductionId == _PayrollDeduction.PayrollDeductionId)
                                                  select c
                        ).FirstOrDefaultAsync();

                        _context.Entry(PayrollDeductionq).CurrentValues.SetValues((_PayrollDeduction));

                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = PayrollDeductionq.PayrollDeductionId,
                            DocType = "PayrollDeduction",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(PayrollDeductionq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = PayrollDeductionq.UsuarioCreacion,
                            UsuarioModificacion = PayrollDeductionq.UsuarioModificacion,
                            UsuarioEjecucion = PayrollDeductionq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(PayrollDeductionq));
        }

        /// <summary>
        /// Elimina una Poliza haciendo una contra partida
        /// </summary>
        /// <param name="_PayrollDeduction"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]PayrollDeduction _PayrollDeduction)
        {
            PayrollDeduction PayrollDeductionq = new PayrollDeduction();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        PayrollDeductionq = _context.PayrollDeduction
                        .Where(x => x.PayrollDeductionId == (Int64)_PayrollDeduction.PayrollDeductionId)
                        .FirstOrDefault();

                        _context.PayrollDeduction.Remove(PayrollDeductionq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = PayrollDeductionq.PayrollDeductionId,
                            DocType = "PayrollDeduction",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(PayrollDeductionq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Eliminar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = PayrollDeductionq.UsuarioCreacion,
                            UsuarioModificacion = PayrollDeductionq.UsuarioModificacion,
                            UsuarioEjecucion = PayrollDeductionq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(PayrollDeductionq));

        }
    }
}
