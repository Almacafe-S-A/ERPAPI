/********************************************************************************************************

 -- NAME   :  CRUDSubstratum

 -- PROPOSE:  show records Substratum from company



 REVISIONS:



 version              Date                Author                        Description

 ----------           -------------       ---------------               -------------------------------
 
 2.0                  02/01/2020          Marvin.Guillen                Changes to validation delete records
 1.0                  12/12/2019          Alfredo.Ochoa                 Creation of Controller


 ********************************************************************************************************/
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
    [Route("api/Substratum")]
    [ApiController]
    public class SubstratumController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public SubstratumController(ILogger<SubstratumController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Substratos, por paginas
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSubstratumPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Substratum> Items = new List<Substratum>();
            try
            {
                var query = _context.Substratum.AsQueryable();
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
        /// Obtiene el Listado de subtratos, ordenados por el codigo
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSubstratum()
        {
            List<Substratum> Items = new List<Substratum>();
            try
            {
                Items = await _context.Substratum.OrderBy(b => b.SubstratumCode).ToListAsync();
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
        /// Obtiene los Datos del substrato por medio del Id enviado.
        /// </summary>
        /// <param name="SubstratumId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{SubstratumId}")]
        public async Task<IActionResult> GetSubstratumById(Int64 SubstratumId)
        {
            Substratum Items = new Substratum();
            try
            {
                Items = await _context.Substratum.Where(q => q.SubstratumId == SubstratumId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }
        [HttpGet("[action]/{SubstratumCode}")]
        public async Task<IActionResult> GetSubstratumBySubstratumCode(String SubstratumCode)
        {
            Substratum Items = new Substratum();
            try
            {
                Items = await _context.Substratum.Where(q => q.SubstratumCode == SubstratumCode).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{SubstratumId}")]
        public async Task<ActionResult<Int32>> ValidationDelete(Int64 SubstratumId)
        {
            try
            {
                //var Items = await _context.Product.CountAsync();
                Int32 Items = 0;//await _context.CheckAccount.Where(a => a.BankId == BankId)
                                //    .CountAsync();
                return await Task.Run(() => Ok(Items));


            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }


        /// <summary>
        /// Inserta una nueva Substratum
        /// </summary>
        /// <param name="_Substratum"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Substratum>> Insert([FromBody]Substratum _Substratum)
        {
            Substratum _Substratumq = new Substratum();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Substratumq = _Substratum;
                        _context.Substratum.Add(_Substratumq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Substratumq.SubstratumId,
                            DocType = "Substratum",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Substratumq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Substratumq.UsuarioCreacion,
                            UsuarioModificacion = _Substratumq.UsuarioModificacion,
                            UsuarioEjecucion = _Substratumq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_Substratumq));
        }

        /// <summary>
        /// Actualiza el substrato
        /// </summary>
        /// <param name="_Substratum"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Substratum>> Update([FromBody]Substratum _Substratum)
        {
            Substratum _Substratumq = _Substratum;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Substratumq = await (from c in _context.Substratum
                        .Where(q => q.SubstratumId == _Substratum.SubstratumId)
                                              select c
                        ).FirstOrDefaultAsync();

                        _context.Entry(_Substratumq).CurrentValues.SetValues((_Substratum));

                        //_context.Substratum.Update(_Substratumq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Substratumq.SubstratumId,
                            DocType = "Substratum",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Substratumq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Substratumq.UsuarioCreacion,
                            UsuarioModificacion = _Substratumq.UsuarioModificacion,
                            UsuarioEjecucion = _Substratumq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_Substratumq));
        }

        /// <summary>
        /// Elimina una Substratum       
        /// </summary>
        /// <param name="_Substratum"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Substratum _Substratum)
        {
            Substratum _Substratumq = new Substratum();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Substratumq = _context.Substratum
                        .Where(x => x.SubstratumId == (Int64)_Substratum.SubstratumId)
                        .FirstOrDefault();

                        _context.Substratum.Remove(_Substratumq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Substratumq.SubstratumId,
                            DocType = "Substratum",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Substratumq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Eliminar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Substratumq.UsuarioCreacion,
                            UsuarioModificacion = _Substratumq.UsuarioModificacion,
                            UsuarioEjecucion = _Substratumq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_Substratumq));

        }
    }
}
