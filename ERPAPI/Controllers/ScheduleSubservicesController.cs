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
    [Route("api/ScheduleSubservices")]
    [ApiController]
    public class ScheduleSubservicesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ScheduleSubservicesController(ILogger<ScheduleSubservicesController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de ScheduleSubservices paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetScheduleSubservicesPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<ScheduleSubservices> Items = new List<ScheduleSubservices>();
            try
            {
                var query = _context.ScheduleSubservices.AsQueryable();
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
        /// Obtiene el Listado de ScheduleSubserviceses 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetScheduleSubservices()
        {
            List<ScheduleSubservices> Items = new List<ScheduleSubservices>();
            try
            {
                Items = await _context.ScheduleSubservices.ToListAsync();
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
        /// Obtiene los Datos de la ScheduleSubservices por medio del Id enviado.
        /// </summary>
        /// <param name="ScheduleSubservicesId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{ScheduleSubservicesId}")]
        public async Task<IActionResult> GetScheduleSubservicesById(Int64 ScheduleSubservicesId)
        {
            ScheduleSubservices Items = new ScheduleSubservices();
            try
            {
                Items = await _context.ScheduleSubservices.Where(q => q.ScheduleSubservicesId == ScheduleSubservicesId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva ScheduleSubservices
        /// </summary>
        /// <param name="_ScheduleSubservices"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<ScheduleSubservices>> Insert([FromBody]ScheduleSubservices _ScheduleSubservices)
        {
            ScheduleSubservices _ScheduleSubservicesq = new ScheduleSubservices();
            try
            {

                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        _ScheduleSubservicesq = _ScheduleSubservices;
                        _context.ScheduleSubservices.Add(_ScheduleSubservicesq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _ScheduleSubservicesq.ScheduleSubservicesId,
                            DocType = "ScheduleSubservices",
                            ClaseInicial =
                         Newtonsoft.Json.JsonConvert.SerializeObject(_ScheduleSubservicesq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _ScheduleSubservicesq.UsuarioCreacion,
                            UsuarioModificacion = _ScheduleSubservicesq.UsuarioModificacion,
                            UsuarioEjecucion = _ScheduleSubservicesq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_ScheduleSubservicesq));
        }

        /// <summary>
        /// Actualiza la ScheduleSubservices
        /// </summary>
        /// <param name="_ScheduleSubservices"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<ScheduleSubservices>> Update([FromBody]ScheduleSubservices _ScheduleSubservices)
        {
            ScheduleSubservices _ScheduleSubservicesq = _ScheduleSubservices;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _ScheduleSubservicesq = await (from c in _context.ScheduleSubservices
                              .Where(q => q.ScheduleSubservicesId == _ScheduleSubservices.ScheduleSubservicesId)
                                                       select c
                             ).FirstOrDefaultAsync();

                        _context.Entry(_ScheduleSubservicesq).CurrentValues.SetValues((_ScheduleSubservices));

                        //_context.ScheduleSubservices.Update(_ScheduleSubservicesq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _ScheduleSubservicesq.ScheduleSubservicesId,
                            DocType = "ScheduleSubservices",
                            ClaseInicial =
                                    Newtonsoft.Json.JsonConvert.SerializeObject(_ScheduleSubservicesq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Update",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _ScheduleSubservicesq.UsuarioCreacion,
                            UsuarioModificacion = _ScheduleSubservicesq.UsuarioModificacion,
                            UsuarioEjecucion = _ScheduleSubservicesq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_ScheduleSubservicesq));
        }

        /// <summary>
        /// Elimina una ScheduleSubservices       
        /// </summary>
        /// <param name="_ScheduleSubservices"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]ScheduleSubservices _ScheduleSubservices)
        {
            ScheduleSubservices _ScheduleSubservicesq = new ScheduleSubservices();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _ScheduleSubservicesq = _context.ScheduleSubservices
                       .Where(x => x.ScheduleSubservicesId == (Int64)_ScheduleSubservices.ScheduleSubservicesId)
                       .FirstOrDefault();

                        _context.ScheduleSubservices.Remove(_ScheduleSubservicesq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _ScheduleSubservicesq.ScheduleSubservicesId,
                            DocType = "ScheduleSubservices",
                            ClaseInicial =
                                      Newtonsoft.Json.JsonConvert.SerializeObject(_ScheduleSubservicesq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Delete",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _ScheduleSubservicesq.UsuarioCreacion,
                            UsuarioModificacion = _ScheduleSubservicesq.UsuarioModificacion,
                            UsuarioEjecucion = _ScheduleSubservicesq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_ScheduleSubservicesq));

        }







    }
}