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
    [Route("api/EmployeeExtraHoursDetail")]
    [ApiController]
    public class EmployeeExtraHoursDetailController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public EmployeeExtraHoursDetailController(ILogger<EmployeeExtraHoursDetailController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de EmployeeExtraHoursDetail paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEmployeeExtraHoursDetailPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<EmployeeExtraHoursDetail> Items = new List<EmployeeExtraHoursDetail>();
            try
            {
                var query = _context.EmployeeExtraHoursDetail.AsQueryable();
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
        /// Obtiene el Listado de EmployeeExtraHoursDetailes 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEmployeeExtraHoursDetail()
        {
            List<EmployeeExtraHoursDetail> Items = new List<EmployeeExtraHoursDetail>();
            try
            {
                Items = await _context.EmployeeExtraHoursDetail.ToListAsync();
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
        /// Obtiene el Listado de EmployeeExtraHoursDetailes 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{EmployeeExtraHoursId}")]
        public async Task<IActionResult> GetEmployeeExtraHoursDetailByEmployeeExtraHoursId(Int64 EmployeeExtraHoursId)
        {
            List<EmployeeExtraHoursDetail> Items = new List<EmployeeExtraHoursDetail>();
            try
            {
                Items = await _context.EmployeeExtraHoursDetail
                    .Where(q=>q.EmployeeExtraHoursId== EmployeeExtraHoursId).ToListAsync();
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
        /// Obtiene los Datos de la EmployeeExtraHoursDetail por medio del Id enviado.
        /// </summary>
        /// <param name="EmployeeExtraHoursDetailId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{EmployeeExtraHoursDetailId}")]
        public async Task<IActionResult> GetEmployeeExtraHoursDetailById(Int64 EmployeeExtraHoursDetailId)
        {
            EmployeeExtraHoursDetail Items = new EmployeeExtraHoursDetail();
            try
            {
                Items = await _context.EmployeeExtraHoursDetail.Where(q => q.EmployeeExtraHoursDetailId == EmployeeExtraHoursDetailId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva EmployeeExtraHoursDetail
        /// </summary>
        /// <param name="_EmployeeExtraHoursDetail"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<EmployeeExtraHoursDetail>> Insert([FromBody]EmployeeExtraHoursDetail _EmployeeExtraHoursDetail)
        {
            EmployeeExtraHoursDetail _EmployeeExtraHoursDetailq = new EmployeeExtraHoursDetail();
            try
            {

                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        _EmployeeExtraHoursDetailq = _EmployeeExtraHoursDetail;
                        Customer _custo = new Customer();
                        _custo = await _context.Customer
                            .Where(q => q.CustomerId == _EmployeeExtraHoursDetail.CustomerId).FirstOrDefaultAsync();
                        _EmployeeExtraHoursDetailq.CustomerName = _custo.CustomerName;
                        _context.EmployeeExtraHoursDetail.Add(_EmployeeExtraHoursDetailq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _EmployeeExtraHoursDetailq.EmployeeExtraHoursDetailId,
                            DocType = "EmployeeExtraHoursDetail",
                            ClaseInicial =
                             Newtonsoft.Json.JsonConvert.SerializeObject(_EmployeeExtraHoursDetailq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _EmployeeExtraHoursDetailq.UsuarioCreacion,
                            UsuarioModificacion = _EmployeeExtraHoursDetailq.UsuarioModificacion,
                            UsuarioEjecucion = _EmployeeExtraHoursDetailq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_EmployeeExtraHoursDetailq));
        }

        /// <summary>
        /// Actualiza la EmployeeExtraHoursDetail
        /// </summary>
        /// <param name="_EmployeeExtraHoursDetail"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<EmployeeExtraHoursDetail>> Update([FromBody]EmployeeExtraHoursDetail _EmployeeExtraHoursDetail)
        {
            EmployeeExtraHoursDetail _EmployeeExtraHoursDetailq = _EmployeeExtraHoursDetail;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _EmployeeExtraHoursDetailq = await (from c in _context.EmployeeExtraHoursDetail
                              .Where(q => q.EmployeeExtraHoursDetailId == _EmployeeExtraHoursDetail.EmployeeExtraHoursDetailId)
                                                            select c
                             ).FirstOrDefaultAsync();

                        Customer _custo = new Customer();
                        _custo = await _context.Customer
                            .Where(q=>q.CustomerId==_EmployeeExtraHoursDetail.CustomerId).FirstOrDefaultAsync();

                        _EmployeeExtraHoursDetail.CustomerName = _custo.CustomerName;

                        _EmployeeExtraHoursDetail.FechaCreacion = _EmployeeExtraHoursDetailq.FechaCreacion;
                        _EmployeeExtraHoursDetail.UsuarioCreacion = _EmployeeExtraHoursDetailq.UsuarioCreacion;

                        _context.Entry(_EmployeeExtraHoursDetailq).CurrentValues.SetValues((_EmployeeExtraHoursDetail));

                        //_context.EmployeeExtraHoursDetail.Update(_EmployeeExtraHoursDetailq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _EmployeeExtraHoursDetailq.EmployeeExtraHoursDetailId,
                            DocType = "EmployeeExtraHoursDetail",
                            ClaseInicial =
                                    Newtonsoft.Json.JsonConvert.SerializeObject(_EmployeeExtraHoursDetailq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Update",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _EmployeeExtraHoursDetailq.UsuarioCreacion,
                            UsuarioModificacion = _EmployeeExtraHoursDetailq.UsuarioModificacion,
                            UsuarioEjecucion = _EmployeeExtraHoursDetailq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_EmployeeExtraHoursDetailq));
        }

        /// <summary>
        /// Elimina una EmployeeExtraHoursDetail       
        /// </summary>
        /// <param name="_EmployeeExtraHoursDetail"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]EmployeeExtraHoursDetail _EmployeeExtraHoursDetail)
        {
            EmployeeExtraHoursDetail _EmployeeExtraHoursDetailq = new EmployeeExtraHoursDetail();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _EmployeeExtraHoursDetailq = _context.EmployeeExtraHoursDetail
                       .Where(x => x.EmployeeExtraHoursDetailId == (Int64)_EmployeeExtraHoursDetail.EmployeeExtraHoursDetailId)
                       .FirstOrDefault();

                        _context.EmployeeExtraHoursDetail.Remove(_EmployeeExtraHoursDetailq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _EmployeeExtraHoursDetailq.EmployeeExtraHoursDetailId,
                            DocType = "EmployeeExtraHoursDetail",
                            ClaseInicial =
                                      Newtonsoft.Json.JsonConvert.SerializeObject(_EmployeeExtraHoursDetailq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Delete",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _EmployeeExtraHoursDetailq.UsuarioCreacion,
                            UsuarioModificacion = _EmployeeExtraHoursDetailq.UsuarioModificacion,
                            UsuarioEjecucion = _EmployeeExtraHoursDetailq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_EmployeeExtraHoursDetailq));

        }







    }
}