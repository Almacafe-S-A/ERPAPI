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
    [Route("api/CustomerContractTerms")]
    [ApiController]
    public class CustomerContractTermsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CustomerContractTermsController(ILogger<CustomerContractTermsController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de CustomerContractTerms paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCustomerContractTermsPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<CustomerContractTerms> Items = new List<CustomerContractTerms>();
            try
            {
                var query = _context.CustomerContractTerms.AsQueryable();
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
        /// Obtiene el Listado de CustomerContractTermses 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCustomerContractTerms()
        {
            List<CustomerContractTerms> Items = new List<CustomerContractTerms>();
            try
            {
                Items = await _context.CustomerContractTerms.OrderByDescending(b => b.Id).ToListAsync();
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
        /// Obtiene los Datos de la CustomerContractTerms por medio del Id enviado.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetCustomerContractTermsById(Int64 Id)
        {
            CustomerContractTerms Items = new CustomerContractTerms();
            try
            {
                Items = await _context.CustomerContractTerms.Where(q => q.Id == Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }
        [HttpGet("[action]/{CustomerContractTermsName}")]
        public async Task<IActionResult> GetCustomerContractTermsByCustomerContractTermsName(String CustomerContractTermsName)
        {
            CustomerContractTerms Items = new CustomerContractTerms();
            try
            {
                Items = await _context.CustomerContractTerms.Where(q => q.TermTitle == CustomerContractTermsName).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva CustomerContractTerms
        /// </summary>
        /// <param name="_CustomerContractTerms"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<CustomerContractTerms>> Insert([FromBody]CustomerContractTerms _CustomerContractTerms)
        {
            CustomerContractTerms _CustomerContractTermsq = new CustomerContractTerms();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    { 
                        if (_CustomerContractTerms.Position == 0)
                        {
                            List<CustomerContractTerms> terminos = _context.CustomerContractTerms
                                .Where(q => q.CustomerContractType == _CustomerContractTerms.CustomerContractType
                                && q.ProductId == _CustomerContractTerms.ProductId
                                && q.TypeInvoiceId == _CustomerContractTerms.TypeInvoiceId).ToList();
                            _CustomerContractTerms.Position = terminos.Count > 0 ? (int)terminos.Max(q => q.Position) + 1 : 1;
                        }
                        _CustomerContractTermsq = _CustomerContractTerms;
                         _context.CustomerContractTerms.Add(_CustomerContractTermsq);
                            await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _CustomerContractTermsq.Id,
                            DocType = "CustomerContractTerms",
                            ClaseInicial =
                           Newtonsoft.Json.JsonConvert.SerializeObject(_CustomerContractTermsq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _CustomerContractTermsq.UsuarioCreacion,
                            UsuarioModificacion = _CustomerContractTermsq.UsuarioModificacion,
                            UsuarioEjecucion = _CustomerContractTermsq.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        throw ex;
                        // return BadRequest($"Ocurrio un error:{ex.Message}");
                    }
                }

            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_CustomerContractTermsq));
        }

        /// <summary>
        /// Actualiza la CustomerContractTerms
        /// </summary>
        /// <param name="_CustomerContractTerms"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<CustomerContractTerms>> Update([FromBody]CustomerContractTerms _CustomerContractTerms)
        {
            CustomerContractTerms _CustomerContractTermsq = _CustomerContractTerms;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        _CustomerContractTermsq = await (from c in _context.CustomerContractTerms
                                 .Where(q => q.Id == _CustomerContractTerms.Id)
                                      select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_CustomerContractTermsq).CurrentValues.SetValues((_CustomerContractTerms));

                //_context.CustomerContractTerms.Update(_CustomerContractTermsq);
                await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _CustomerContractTermsq.Id,
                            DocType = "CustomerContractTerms",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_CustomerContractTermsq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_CustomerContractTermsq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _CustomerContractTermsq.UsuarioCreacion,
                            UsuarioModificacion = _CustomerContractTermsq.UsuarioModificacion,
                            UsuarioEjecucion = _CustomerContractTermsq.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        throw ex;
                        // return BadRequest($"Ocurrio un error:{ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CustomerContractTermsq));
        }

        /// <summary>
        /// Elimina una CustomerContractTerms       
        /// </summary>
        /// <param name="_CustomerContractTerms"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]CustomerContractTerms _CustomerContractTerms)
        {
            CustomerContractTerms _CustomerContractTermsq = new CustomerContractTerms();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        _CustomerContractTermsq = _context.CustomerContractTerms
                .Where(x => x.Id == (Int64)_CustomerContractTerms.Id)
                .FirstOrDefault();

                _context.CustomerContractTerms.Remove(_CustomerContractTermsq);
                await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _CustomerContractTermsq.Id,
                            DocType = "ContactPerson",
                            ClaseInicial =
                             Newtonsoft.Json.JsonConvert.SerializeObject(_CustomerContractTermsq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_CustomerContractTermsq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Eliminar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _CustomerContractTermsq.UsuarioCreacion,
                            UsuarioModificacion = _CustomerContractTermsq.UsuarioModificacion,
                            UsuarioEjecucion = _CustomerContractTermsq.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        throw ex;
                        // return BadRequest($"Ocurrio un error:{ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CustomerContractTermsq));

        }







    }
}