using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Contexts;
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
    [Route("api/CustomerContractLinesTerms")]
    [ApiController]
    public class CustomerContractLinesTermsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CustomerContractLinesTermsController(ILogger<CustomerContractLinesTermsController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de CustomerContractLinesTerms paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCustomerContractLinesTermsPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<CustomerContractLinesTerms> Items = new List<CustomerContractLinesTerms>();
            try
            {
                var query = _context.CustomerContractLinesTerms.AsQueryable();
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
        /// Obtiene el Listado de CustomerContractLinesTermses 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCustomerContractLinesTerms()
        {
            List<CustomerContractLinesTerms> Items = new List<CustomerContractLinesTerms>();
            try
            {
                Items = await _context.CustomerContractLinesTerms.OrderByDescending(b => b.Id).ToListAsync();
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
        /// Obtiene los Datos de la CustomerContractLinesTerms por medio del Id enviado.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetCustomerContractLinesTermsById(Int64 Id)
        {
            CustomerContractLinesTerms Items = new CustomerContractLinesTerms();
            try
            {
                Items = await _context.CustomerContractLinesTerms.Where(q => q.Id == Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }
        [HttpGet("[action]/{CustomerContractLinesTermsName}")]
        public async Task<IActionResult> GetCustomerContractLinesTermsByCustomerContractLinesTermsName(String CustomerContractLinesTermsName)
        {
            CustomerContractLinesTerms Items = new CustomerContractLinesTerms();
            try
            {
                Items = await _context.CustomerContractLinesTerms.Where(q => q.TermTitle == CustomerContractLinesTermsName).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva CustomerContractLinesTerms
        /// </summary>
        /// <param name="_CustomerContractLinesTerms"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<CustomerContractLinesTerms>> Insert([FromBody]CustomerContractLinesTerms _CustomerContractLinesTerms)
        {
            CustomerContractLinesTerms _CustomerContractLinesTermsq = new CustomerContractLinesTerms();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        _CustomerContractLinesTermsq = _CustomerContractLinesTerms;
                         _context.CustomerContractLinesTerms.Add(_CustomerContractLinesTermsq);

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();


                        await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _CustomerContractLinesTermsq.Id,
                            DocType = "CustomerContractLinesTerms",
                            ClaseInicial =
                           Newtonsoft.Json.JsonConvert.SerializeObject(_CustomerContractLinesTermsq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            //UsuarioCreacion = _CustomerContractLinesTermsq.UsuarioCreacion,
                            //UsuarioModificacion = _CustomerContractLinesTermsq.UsuarioModificacion,
                            //UsuarioEjecucion = _CustomerContractLinesTermsq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_CustomerContractLinesTermsq));
        }

        /// <summary>
        /// Actualiza la CustomerContractLinesTerms
        /// </summary>
        /// <param name="_CustomerContractLinesTerms"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<CustomerContractLinesTerms>> Update([FromBody]CustomerContractLinesTerms _CustomerContractLinesTerms)
        {
            CustomerContractLinesTerms _CustomerContractLinesTermsq = _CustomerContractLinesTerms;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        _CustomerContractLinesTermsq = await (from c in _context.CustomerContractLinesTerms
                                 .Where(q => q.Id == _CustomerContractLinesTerms.Id)
                                      select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_CustomerContractLinesTermsq).CurrentValues.SetValues((_CustomerContractLinesTerms));

                        //_context.CustomerContractLinesTerms.Update(_CustomerContractLinesTermsq);

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();


                        await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _CustomerContractLinesTermsq.Id,
                            DocType = "CustomerContractLinesTerms",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_CustomerContractLinesTermsq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_CustomerContractLinesTermsq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            //UsuarioCreacion = _CustomerContractLinesTermsq.UsuarioCreacion,
                            //UsuarioModificacion = _CustomerContractLinesTermsq.UsuarioModificacion,
                            //UsuarioEjecucion = _CustomerContractLinesTermsq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_CustomerContractLinesTermsq));
        }

        /// <summary>
        /// Elimina una CustomerContractLinesTerms       
        /// </summary>
        /// <param name="_CustomerContractLinesTerms"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]CustomerContractLinesTerms _CustomerContractLinesTerms)
        {
            CustomerContractLinesTerms _CustomerContractLinesTermsq = new CustomerContractLinesTerms();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        _CustomerContractLinesTermsq = _context.CustomerContractLinesTerms
                .Where(x => x.Id == (Int64)_CustomerContractLinesTerms.Id)
                .FirstOrDefault();

                _context.CustomerContractLinesTerms.Remove(_CustomerContractLinesTermsq);

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                       new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();


                        await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _CustomerContractLinesTermsq.Id,
                            DocType = "ContactPerson",
                            ClaseInicial =
                             Newtonsoft.Json.JsonConvert.SerializeObject(_CustomerContractLinesTermsq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_CustomerContractLinesTermsq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Eliminar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            //UsuarioCreacion = _CustomerContractLinesTermsq.UsuarioCreacion,
                            //UsuarioModificacion = _CustomerContractLinesTermsq.UsuarioModificacion,
                            //UsuarioEjecucion = _CustomerContractLinesTermsq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_CustomerContractLinesTermsq));

        }







    }
}