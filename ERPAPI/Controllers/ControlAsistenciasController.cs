using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/ControlAsistencias")]
    //[ApiController]
    public class ControlAsistenciasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ControlAsistenciasController(ILogger<ControlAsistenciasController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de ControlAsistencias paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetControlAsistenciasPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<ControlAsistencias> Items = new List<ControlAsistencias>();
            try
            {
                var query = _context.ControlAsistencias.AsQueryable();
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

            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene el Listado de ControlAsistencias.
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetControlAsistencias()
        {
            List<ControlAsistencias> Items = new List<ControlAsistencias>();
            try
            {
                Items = await _context.ControlAsistencias.ToListAsync();
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
        /// Obtiene la sucursal mediante el Id enviado.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetControlAsistenciasById(int Id)
        {
            ControlAsistencias Items = new ControlAsistencias();
            try
            {
                Items = await _context.ControlAsistencias.Where(q => q.Id == Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene el control de asitencia por empleado mediante el Id enviado.
        /// Y la fecha se envia en el campo FechaCreacion y la FechaModificacion 
        /// </summary>
        /// <param name="_ControlAsistenciasP"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetControlAsistenciasByEmployeeId([FromBody]ControlAsistencias _ControlAsistenciasP)
        {
            List<ControlAsistencias> Items = new List<ControlAsistencias>();
            try
            {
                Items = await _context.ControlAsistencias.Where(
                                q => q.Empleado.IdEmpleado == _ControlAsistenciasP.Empleado.IdEmpleado &&
                                    q.Fecha >= _ControlAsistenciasP.FechaCreacion &&
                                    q.Fecha <= _ControlAsistenciasP.FechaModificacion
                                ).ToListAsync();
                    ;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }
        /// <summary>
        /// Devuelve la Asistencia deacuerdo al tipo un Control de Asistencia
        /// </summary>
        /// <param name="_ControlAsistenciasP"></param>
        /// <returns></returns>

        [HttpGet("[action]")]
        public async Task<ActionResult<Int32>> GetSumControlAsistenciasByEmployeeId([FromBody]ControlAsistencias _ControlAsistenciasP)
        {
           // ControlAsistencias Items = new ControlAsistencias();

            try
            {
               var  Items = await _context.ControlAsistencias.Where(
                                q => q.Empleado.IdEmpleado == _ControlAsistenciasP.Empleado.IdEmpleado &&
                                    q.Fecha >= _ControlAsistenciasP.FechaCreacion &&
                                    q.Fecha <= _ControlAsistenciasP.FechaModificacion &&
                                    q.TipoAsistencia == _ControlAsistenciasP.TipoAsistencia
                                ).CountAsync();
                return await Task.Run(() => Ok(Items));
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            
        }

        /// <summary>
        /// Inserta un Control de Asistencia
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([FromBody]ControlAsistencias payload)
        {
            ControlAsistencias ControlAsistencias = new ControlAsistencias();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        ControlAsistencias = payload;
                _context.ControlAsistencias.Add(ControlAsistencias);
                await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = ControlAsistencias.Id,
                            DocType = "ControlAsistencias",
                            ClaseInicial =
Newtonsoft.Json.JsonConvert.SerializeObject(ControlAsistencias, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = ControlAsistencias.UsuarioCreacion,
                            UsuarioModificacion = ControlAsistencias.UsuarioModificacion,
                            UsuarioEjecucion = ControlAsistencias.UsuarioModificacion,

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

            return await Task.Run(() => Ok(ControlAsistencias));
        }

        /// <summary>
        /// Actualiza una sucursal
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody]ControlAsistencias payload)
        {
            ControlAsistencias ControlAsistencias = payload;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        ControlAsistencias = (from c in _context.ControlAsistencias
                                    .Where(q => q.Id == payload.Id)
                          select c
                                    ).FirstOrDefault();

                payload.FechaCreacion = ControlAsistencias.FechaCreacion;
                payload.UsuarioCreacion = ControlAsistencias.UsuarioCreacion;

                _context.Entry(ControlAsistencias).CurrentValues.SetValues(payload);
                // _context.ControlAsistencias.Update(payload);
                await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = ControlAsistencias.Id,
                            DocType = "ControlAsistencias",
                            ClaseInicial =
Newtonsoft.Json.JsonConvert.SerializeObject(ControlAsistencias, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = ControlAsistencias.UsuarioCreacion,
                            UsuarioModificacion = ControlAsistencias.UsuarioModificacion,
                            UsuarioEjecucion = ControlAsistencias.UsuarioModificacion,

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

            return await Task.Run(() => Ok(ControlAsistencias));
        }


        /// <summary>
        /// Elimina una sucursal
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]ControlAsistencias payload)
        {
            ControlAsistencias ControlAsistencias = new ControlAsistencias();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        ControlAsistencias = _context.ControlAsistencias
               .Where(x => x.Id == (int)payload.Id)
               .FirstOrDefault();
                _context.ControlAsistencias.Remove(ControlAsistencias);
                await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = ControlAsistencias.Id,
                            DocType = "ControlAsistencias",
                            ClaseInicial =
Newtonsoft.Json.JsonConvert.SerializeObject(ControlAsistencias, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Eliminar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = ControlAsistencias.UsuarioCreacion,
                            UsuarioModificacion = ControlAsistencias.UsuarioModificacion,
                            UsuarioEjecucion = ControlAsistencias.UsuarioModificacion,

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

            return await Task.Run(() => Ok(ControlAsistencias));

        }


    }
}
