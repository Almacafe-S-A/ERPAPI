using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Contexts;
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
    [Route("api/PhoneLines")]
    [ApiController]
    public class PhoneLinesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public PhoneLinesController(ILogger<PhoneLinesController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Lineas Telefonicas, con paginacion
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPhoneLinesPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<PhoneLines> Items = new List<PhoneLines>();
            try
            {
                var query = _context.PhoneLines.AsQueryable();
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
        /// Obtiene el Listado de Lineas Telefonicas, ordenado por id
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPhoneLines()
        {
            List<PhoneLines> Items = new List<PhoneLines>();
            try
            {
                Items = await _context.PhoneLines.OrderBy(b => b.PhoneLineId).Include("Branch").ToListAsync();
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
        /// Obtiene los Datos de Lineas Telefonicas por medio del Id enviado.
        /// </summary>
        /// <param name="PhoneLinesId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{PhoneLinesId}")]
        public async Task<IActionResult> GetPhoneLinesById(Int64 PhoneLinesId)
        {
            PhoneLines Items = new PhoneLines();
            try
            {
                Items = await _context.PhoneLines.Where(q => q.PhoneLineId == PhoneLinesId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene las lineas telefonicas por el id empleado enviado
        /// </summary>
        /// <param name="IdEmpleado"></param>
        /// <returns></returns>
        [HttpGet("[action]/{PhoneLinesCode}")]
        public async Task<IActionResult> GetPhoneLinesByCode(Int64 IdEmpleado)
        {
            PhoneLines Items = new PhoneLines();
            try
            {
                Items = await _context.PhoneLines.Where(q => q.IdEmpleado == IdEmpleado).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{PhoneLinesId}")]
        public async Task<ActionResult<Int32>> ValidationDelete(Int64 PhoneLinesId)
        {
            try
            {
                var Items = await _context.PhoneLines.CountAsync();
                await _context.PhoneLines.Where(a => a.PhoneLineId == PhoneLinesId)
                                    .CountAsync();
                return await Task.Run(() => Ok(Items));


            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }



        /// <summary>
        /// Inserta una nueva Linea Telefonica
        /// </summary>
        /// <param name="_PhoneLines"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<PhoneLines>> Insert([FromBody]PhoneLines _PhoneLines)
        {
            PhoneLines _PhoneLinesq = new PhoneLines();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _PhoneLinesq = _PhoneLines;
                        _context.PhoneLines.Add(_PhoneLinesq);

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _PhoneLinesq.PhoneLineId,
                            DocType = "PhoneLines",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_PhoneLinesq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _PhoneLinesq.UsuarioCreacion,
                            UsuarioModificacion = _PhoneLinesq.UsuarioModificacion,
                            UsuarioEjecucion = _PhoneLinesq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_PhoneLinesq));
        }

        /// <summary>
        /// Actualiza la linea telefonica
        /// </summary>
        /// <param name="_PhoneLines"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<PhoneLines>> Update([FromBody]PhoneLines _PhoneLines)
        {
            PhoneLines _PhoneLinesq = _PhoneLines;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _PhoneLinesq = await (from c in _context.PhoneLines
                        .Where(q => q.PhoneLineId == _PhoneLines.PhoneLineId)
                                          select c
                        ).FirstOrDefaultAsync();

                        _context.Entry(_PhoneLinesq).CurrentValues.SetValues((_PhoneLines));

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _PhoneLinesq.PhoneLineId,
                            DocType = "PhoneLines",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_PhoneLinesq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _PhoneLinesq.UsuarioCreacion,
                            UsuarioModificacion = _PhoneLinesq.UsuarioModificacion,
                            UsuarioEjecucion = _PhoneLinesq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_PhoneLinesq));
        }

        /// <summary>
        /// Elimina una linea telefonica
        /// </summary>
        /// <param name="_PhoneLines"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]PhoneLines _PhoneLines)
        {
            PhoneLines _PhoneLinesq = new PhoneLines();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _PhoneLinesq = _context.PhoneLines
                        .Where(x => x.PhoneLineId == (Int64)_PhoneLines.PhoneLineId)
                        .FirstOrDefault();

                        _context.PhoneLines.Remove(_PhoneLinesq);

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _PhoneLinesq.PhoneLineId,
                            DocType = "PhoneLines",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_PhoneLinesq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Eliminar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _PhoneLinesq.UsuarioCreacion,
                            UsuarioModificacion = _PhoneLinesq.UsuarioModificacion,
                            UsuarioEjecucion = _PhoneLinesq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_PhoneLinesq));

        }
    }
}
