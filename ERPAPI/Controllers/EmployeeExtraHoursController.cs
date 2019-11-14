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
    [Route("api/EmployeeExtraHours")]
    [ApiController]
    public class EmployeeExtraHoursController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public EmployeeExtraHoursController(ILogger<EmployeeExtraHoursController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de EmployeeExtraHours paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEmployeeExtraHoursPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<EmployeeExtraHours> Items = new List<EmployeeExtraHours>();
            try
            {
                var query = _context.EmployeeExtraHours.AsQueryable();
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
        /// Obtiene el Listado de EmployeeExtraHourses 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEmployeeExtraHours()
        {
            List<EmployeeExtraHours> Items = new List<EmployeeExtraHours>();
            try
            {
                Items = await _context.EmployeeExtraHours.ToListAsync();
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
        /// Obtiene los Datos de la EmployeeExtraHours por medio del Id enviado.
        /// </summary>
        /// <param name="EmployeeExtraHoursId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{EmployeeExtraHoursId}")]
        public async Task<IActionResult> GetEmployeeExtraHoursById(Int64 EmployeeExtraHoursId)
        {
            EmployeeExtraHours Items = new EmployeeExtraHours();
            try
            {
                Items = await _context.EmployeeExtraHours.Where(q => q.EmployeeExtraHoursId == EmployeeExtraHoursId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva EmployeeExtraHours
        /// </summary>
        /// <param name="_EmployeeExtraHours"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<EmployeeExtraHours>> Insert([FromBody]EmployeeExtraHours _EmployeeExtraHours)
        {
            EmployeeExtraHours _EmployeeExtraHoursq = new EmployeeExtraHours();
            try
            {

                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        _EmployeeExtraHoursq = _EmployeeExtraHours;
                        _context.EmployeeExtraHours.Add(_EmployeeExtraHoursq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _EmployeeExtraHoursq.EmployeeExtraHoursId,
                            DocType = "EmployeeExtraHours",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_EmployeeExtraHoursq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _EmployeeExtraHoursq.UsuarioCreacion,
                            UsuarioModificacion = _EmployeeExtraHoursq.UsuarioModificacion,
                            UsuarioEjecucion = _EmployeeExtraHoursq.UsuarioModificacion,
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

            return await Task.Run(() => Ok(_EmployeeExtraHoursq));
        }

        /// <summary>
        /// Actualiza la EmployeeExtraHours
        /// </summary>
        /// <param name="_EmployeeExtraHours"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<EmployeeExtraHours>> Update([FromBody]EmployeeExtraHours _EmployeeExtraHours)
        {
            EmployeeExtraHours _EmployeeExtraHoursq = _EmployeeExtraHours;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _EmployeeExtraHoursq = await (from c in _context.EmployeeExtraHours
                              .Where(q => q.EmployeeExtraHoursId == _EmployeeExtraHours.EmployeeExtraHoursId)
                                                      select c
                             ).FirstOrDefaultAsync();

                        _context.Entry(_EmployeeExtraHoursq).CurrentValues.SetValues((_EmployeeExtraHours));

                        //_context.EmployeeExtraHours.Update(_EmployeeExtraHoursq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _EmployeeExtraHoursq.EmployeeExtraHoursId,
                            DocType = "EmployeeExtraHours",
                            ClaseInicial =
                                    Newtonsoft.Json.JsonConvert.SerializeObject(_EmployeeExtraHoursq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Update",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _EmployeeExtraHoursq.UsuarioCreacion,
                            UsuarioModificacion = _EmployeeExtraHoursq.UsuarioModificacion,
                            UsuarioEjecucion = _EmployeeExtraHoursq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_EmployeeExtraHoursq));
        }

        /// <summary>
        /// Elimina una EmployeeExtraHours       
        /// </summary>
        /// <param name="_EmployeeExtraHours"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]EmployeeExtraHours _EmployeeExtraHours)
        {
            EmployeeExtraHours _EmployeeExtraHoursq = new EmployeeExtraHours();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _EmployeeExtraHoursq = _context.EmployeeExtraHours
                       .Where(x => x.EmployeeExtraHoursId == (Int64)_EmployeeExtraHours.EmployeeExtraHoursId)
                       .FirstOrDefault();

                        _context.EmployeeExtraHours.Remove(_EmployeeExtraHoursq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _EmployeeExtraHoursq.EmployeeExtraHoursId,
                            DocType = "EmployeeExtraHours",
                            ClaseInicial =
                                      Newtonsoft.Json.JsonConvert.SerializeObject(_EmployeeExtraHoursq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Delete",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _EmployeeExtraHoursq.UsuarioCreacion,
                            UsuarioModificacion = _EmployeeExtraHoursq.UsuarioModificacion,
                            UsuarioEjecucion = _EmployeeExtraHoursq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_EmployeeExtraHoursq));

        }







    }
}