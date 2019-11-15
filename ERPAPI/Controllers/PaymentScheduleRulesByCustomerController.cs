using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/PaymentScheduleRulesByCustomer")]
    [ApiController]
    public class PaymentScheduleRulesByCustomerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public PaymentScheduleRulesByCustomerController(ILogger<PaymentScheduleRulesByCustomerController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de PaymentScheduleRulesByCustomer paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPaymentScheduleRulesByCustomerPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<PaymentScheduleRulesByCustomer> Items = new List<PaymentScheduleRulesByCustomer>();
            try
            {
                var query = _context.PaymentScheduleRulesByCustomer.AsQueryable();
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
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Obtiene el Listado de PaymentScheduleRulesByCustomeres 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPaymentScheduleRulesByCustomer()
        {
            List<PaymentScheduleRulesByCustomer> Items = new List<PaymentScheduleRulesByCustomer>();
            try
            {
                Items = await _context.PaymentScheduleRulesByCustomer.ToListAsync();
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
        /// Obtiene el Listado de PaymentScheduleRulesByCustomeres por Schedule
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{ScheduleSubservicesId}")]
        public async Task<IActionResult> GetByScheduleId(Int64 ScheduleSubservicesId)
        {
            List<PaymentScheduleRulesByCustomer> Items = new List<PaymentScheduleRulesByCustomer>();
            try
            {
                Items = await _context.PaymentScheduleRulesByCustomer
                    .Where(q=>q.ScheduleSubservicesId== ScheduleSubservicesId).ToListAsync();
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
        /// Obtiene los Datos de la PaymentScheduleRulesByCustomer por medio del Id enviado.
        /// </summary>
        /// <param name="PaymentScheduleRulesByCustomerId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{PaymentScheduleRulesByCustomerId}")]
        public async Task<IActionResult> GetPaymentScheduleRulesByCustomerById(Int64 PaymentScheduleRulesByCustomerId)
        {
            PaymentScheduleRulesByCustomer Items = new PaymentScheduleRulesByCustomer();
            try
            {
                Items = await _context.PaymentScheduleRulesByCustomer.Where(q => q.PaymentScheduleRulesByCustomerId == PaymentScheduleRulesByCustomerId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva PaymentScheduleRulesByCustomer
        /// </summary>
        /// <param name="_PaymentScheduleRulesByCustomer"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<PaymentScheduleRulesByCustomer>> Insert([FromBody]PaymentScheduleRulesByCustomer _PaymentScheduleRulesByCustomer)
        {
            PaymentScheduleRulesByCustomer _PaymentScheduleRulesByCustomerq = new PaymentScheduleRulesByCustomer();
            try
            {

                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        _PaymentScheduleRulesByCustomerq = _PaymentScheduleRulesByCustomer;
                        Customer _custo = await _context.Customer
                        .Where(q => q.CustomerId == _PaymentScheduleRulesByCustomer.CustomerId).FirstOrDefaultAsync();

                        _PaymentScheduleRulesByCustomerq.CustomerName = _custo.CustomerName;
                        _context.PaymentScheduleRulesByCustomer.Add(_PaymentScheduleRulesByCustomerq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _PaymentScheduleRulesByCustomerq.PaymentScheduleRulesByCustomerId,
                            DocType = "PaymentScheduleRulesByCustomer",
                            ClaseInicial =
                         Newtonsoft.Json.JsonConvert.SerializeObject(_PaymentScheduleRulesByCustomerq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _PaymentScheduleRulesByCustomerq.UsuarioCreacion,
                            UsuarioModificacion = _PaymentScheduleRulesByCustomerq.UsuarioModificacion,
                            UsuarioEjecucion = _PaymentScheduleRulesByCustomerq.UsuarioModificacion,

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
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_PaymentScheduleRulesByCustomerq));
        }

        /// <summary>
        /// Actualiza la PaymentScheduleRulesByCustomer
        /// </summary>
        /// <param name="_PaymentScheduleRulesByCustomer"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<PaymentScheduleRulesByCustomer>> Update([FromBody]PaymentScheduleRulesByCustomer _PaymentScheduleRulesByCustomer)
        {
            PaymentScheduleRulesByCustomer _PaymentScheduleRulesByCustomerq = _PaymentScheduleRulesByCustomer;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _PaymentScheduleRulesByCustomerq = await (from c in _context.PaymentScheduleRulesByCustomer
                              .Where(q => q.PaymentScheduleRulesByCustomerId == _PaymentScheduleRulesByCustomer.PaymentScheduleRulesByCustomerId)
                                                                  select c
                             ).FirstOrDefaultAsync();

                        Customer _custo = await _context.Customer
                        .Where(q => q.CustomerId == _PaymentScheduleRulesByCustomer.CustomerId).FirstOrDefaultAsync();
                        _PaymentScheduleRulesByCustomer.CustomerName = _custo.CustomerName;

                        _context.Entry(_PaymentScheduleRulesByCustomerq).CurrentValues.SetValues((_PaymentScheduleRulesByCustomer));

                        //_context.PaymentScheduleRulesByCustomer.Update(_PaymentScheduleRulesByCustomerq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _PaymentScheduleRulesByCustomerq.PaymentScheduleRulesByCustomerId,
                            DocType = "PaymentScheduleRulesByCustomer",
                            ClaseInicial =
                                    Newtonsoft.Json.JsonConvert.SerializeObject(_PaymentScheduleRulesByCustomerq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Update",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _PaymentScheduleRulesByCustomerq.UsuarioCreacion,
                            UsuarioModificacion = _PaymentScheduleRulesByCustomerq.UsuarioModificacion,
                            UsuarioEjecucion = _PaymentScheduleRulesByCustomerq.UsuarioModificacion,

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
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_PaymentScheduleRulesByCustomerq));
        }

        /// <summary>
        /// Elimina una PaymentScheduleRulesByCustomer       
        /// </summary>
        /// <param name="_PaymentScheduleRulesByCustomer"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]PaymentScheduleRulesByCustomer _PaymentScheduleRulesByCustomer)
        {
            PaymentScheduleRulesByCustomer _PaymentScheduleRulesByCustomerq = new PaymentScheduleRulesByCustomer();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _PaymentScheduleRulesByCustomerq = _context.PaymentScheduleRulesByCustomer
                       .Where(x => x.PaymentScheduleRulesByCustomerId == (Int64)_PaymentScheduleRulesByCustomer.PaymentScheduleRulesByCustomerId)
                       .FirstOrDefault();

                        _context.PaymentScheduleRulesByCustomer.Remove(_PaymentScheduleRulesByCustomerq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _PaymentScheduleRulesByCustomerq.PaymentScheduleRulesByCustomerId,
                            DocType = "PaymentScheduleRulesByCustomer",
                            ClaseInicial =
                                      Newtonsoft.Json.JsonConvert.SerializeObject(_PaymentScheduleRulesByCustomerq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Delete",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _PaymentScheduleRulesByCustomerq.UsuarioCreacion,
                            UsuarioModificacion = _PaymentScheduleRulesByCustomerq.UsuarioModificacion,
                            UsuarioEjecucion = _PaymentScheduleRulesByCustomerq.UsuarioModificacion,

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
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_PaymentScheduleRulesByCustomerq));

        }







    }
}