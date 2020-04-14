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
    public class CustomerRiskController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CustomerRiskController(ILogger<CustomerRiskController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de CustomerRisk, por paginas
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCustomerRiskPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<CustomerRisk> Items = new List<CustomerRisk>();
            try
            {
                var query = _context.CustomerRisk.AsQueryable();
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
        public async Task<IActionResult> GetCustomerRisk()
        {
            List<CustomerRisk> Items = new List<CustomerRisk>();
            try
            {
                Items = await _context.CustomerRisk.ToListAsync();
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
        /// Obtiene los Datos de la CustomerRisk por medio del Id enviado.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetCustomerRiskById(Int64 Id)
        {
            CustomerRisk Items = new CustomerRisk();
            try
            {
                Items = await _context.CustomerRisk.Where(q => q.Id == Id).FirstOrDefaultAsync();
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
        /// <param name="_CustomerRisk"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<CustomerRisk>> Insert([FromBody]CustomerRisk _CustomerRisk)
        {
            CustomerRisk CustomerRiskq = new CustomerRisk();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        CustomerRiskq = _CustomerRisk;
                        _context.CustomerRisk.Add(CustomerRiskq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = CustomerRiskq.Id,
                            DocType = "CustomerRisk",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(CustomerRiskq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = CustomerRiskq.UsuarioCreacion,
                            UsuarioModificacion = CustomerRiskq.UsuarioModificacion,
                            UsuarioEjecucion = CustomerRiskq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(CustomerRiskq));
        }

        /// <summary>
        /// Actualiza la Severidad Riesgo
        /// </summary>
        /// <param name="_CustomerRisk"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<CustomerRisk>> Update([FromBody]CustomerRisk _CustomerRisk)
        {
            CustomerRisk _CustomerRiskq = _CustomerRisk;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _CustomerRiskq = await (from c in _context.CustomerRisk
                        .Where(q => q.Id == _CustomerRisk.Id)
                                                   select c
                        ).FirstOrDefaultAsync();

                        _context.Entry(_CustomerRiskq).CurrentValues.SetValues((_CustomerRisk));

                        //_context.Bank.Update(_Bankq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _CustomerRiskq.Id,
                            DocType = "CustomerRisk",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_CustomerRiskq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _CustomerRiskq.UsuarioCreacion,
                            UsuarioModificacion = _CustomerRiskq.UsuarioModificacion,
                            UsuarioEjecucion = _CustomerRiskq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_CustomerRiskq));
        }

        /// <summary>
        /// Elimina una Severidad Riesgo      
        /// </summary>
        /// <param name="_CustomerRisk"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]CustomerRisk _CustomerRisk)
        {
            CustomerRisk _CustomerRiskq = new CustomerRisk();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _CustomerRiskq = _context.CustomerRisk
                        .Where(x => x.Id == (Int64)_CustomerRisk.Id)
                        .FirstOrDefault();

                        _context.CustomerRisk.Remove(_CustomerRiskq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _CustomerRiskq.Id,
                            DocType = "CustomerRisk",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_CustomerRiskq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Eliminar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _CustomerRiskq.UsuarioCreacion,
                            UsuarioModificacion = _CustomerRiskq.UsuarioModificacion,
                            UsuarioEjecucion = _CustomerRiskq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_CustomerRiskq));

        }
    }
}
