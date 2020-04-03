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
    public class MatrizRiesgoCustomersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public MatrizRiesgoCustomersController(ILogger<MatrizRiesgoCustomersController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Probabilidad de Riesgo, por paginas
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMatrizRiesgoCustomersPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<MatrizRiesgoCustomers> Items = new List<MatrizRiesgoCustomers>();
            try
            {
                var query = _context.MatrizRiesgoCustomers.AsQueryable();
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
        public async Task<IActionResult> GetMatrizRiesgoCustomers()
        {
            List<MatrizRiesgoCustomers> Items = new List<MatrizRiesgoCustomers>();
            try
            {
                Items = await _context.MatrizRiesgoCustomers.Include("Customer")
                                                            .Include("Product")
                                                            .Include("ContextoRiesgo")
                                                            .Include("TipodeAccionderiesgo")
                                                            .ToListAsync();
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
        /// <param name="IdMatrizRiesgoCustomers"></param>
        /// <returns></returns>
        [HttpGet("[action]/{IdMatrizRiesgoCustomers}")]
        public async Task<IActionResult> GetMatrizRiesgoCustomersById(Int64 IdMatrizRiesgoCustomers)
        {
            MatrizRiesgoCustomers Items = new MatrizRiesgoCustomers();
            try
            {
                Items = await _context.MatrizRiesgoCustomers.Where(q => q.Id == IdMatrizRiesgoCustomers).FirstOrDefaultAsync();
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
        /// <param name="_MatrizRiesgoCustomers"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<MatrizRiesgoCustomers>> Insert([FromBody]MatrizRiesgoCustomers _MatrizRiesgoCustomers)
        {
            MatrizRiesgoCustomers MatrizRiesgoCustomersq = new MatrizRiesgoCustomers();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        MatrizRiesgoCustomersq = _MatrizRiesgoCustomers;
                        _context.MatrizRiesgoCustomers.Add(MatrizRiesgoCustomersq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = MatrizRiesgoCustomersq.Id,
                            DocType = "MatrizRiesgoCustomers",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(MatrizRiesgoCustomersq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = MatrizRiesgoCustomersq.UsuarioCreacion,
                            UsuarioModificacion = MatrizRiesgoCustomersq.UsuarioModificacion,
                            UsuarioEjecucion = MatrizRiesgoCustomersq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(MatrizRiesgoCustomersq));
        }

        /// <summary>
        /// Actualiza la Probabilidad de Riesgo
        /// </summary>
        /// <param name="_MatrizRiesgoCustomers"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<MatrizRiesgoCustomers>> Update([FromBody]MatrizRiesgoCustomers _MatrizRiesgoCustomers)
        {
            MatrizRiesgoCustomers _MatrizRiesgoCustomersq = _MatrizRiesgoCustomers;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _MatrizRiesgoCustomersq = await (from c in _context.MatrizRiesgoCustomers
                        .Where(q => q.Id == _MatrizRiesgoCustomers.Id)
                                                         select c
                        ).FirstOrDefaultAsync();

                        _context.Entry(_MatrizRiesgoCustomersq).CurrentValues.SetValues((_MatrizRiesgoCustomers));

                        //_context.Bank.Update(_Bankq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _MatrizRiesgoCustomersq.Id,
                            DocType = "MatrizRiesgoCustomers",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_MatrizRiesgoCustomersq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _MatrizRiesgoCustomersq.UsuarioCreacion,
                            UsuarioModificacion = _MatrizRiesgoCustomersq.UsuarioModificacion,
                            UsuarioEjecucion = _MatrizRiesgoCustomersq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_MatrizRiesgoCustomersq));
        }

        /// <summary>
        /// Elimina una Probabilidad de Riesgo      
        /// </summary>
        /// <param name="_MatrizRiesgoCustomers"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]MatrizRiesgoCustomers _MatrizRiesgoCustomers)
        {
            MatrizRiesgoCustomers _MatrizRiesgoCustomersq = new MatrizRiesgoCustomers();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _MatrizRiesgoCustomersq = _context.MatrizRiesgoCustomers
                        .Where(x => x.Id == (Int64)_MatrizRiesgoCustomers.Id)
                        .FirstOrDefault();

                        _context.MatrizRiesgoCustomers.Remove(_MatrizRiesgoCustomersq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _MatrizRiesgoCustomersq.Id,
                            DocType = "MatrizRiesgoCustomers",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_MatrizRiesgoCustomersq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Eliminar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _MatrizRiesgoCustomersq.UsuarioCreacion,
                            UsuarioModificacion = _MatrizRiesgoCustomersq.UsuarioModificacion,
                            UsuarioEjecucion = _MatrizRiesgoCustomersq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_MatrizRiesgoCustomersq));

        }
    }
}