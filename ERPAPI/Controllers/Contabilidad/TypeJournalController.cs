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
    [Route("api/TypeJournal")]
    [ApiController]
    public class TypeJournalController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        /*public DimensionsController(ApplicationDbContext context)
        {
            _context = context;
        }*/
        public TypeJournalController(ILogger<TypeJournalController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de TypeJournal paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetTypeJournalPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<TypeJournal> Items = new List<TypeJournal>();
            try
            {
                var query = _context.TypeJournal.AsQueryable();
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
        /// Obtiene los Datos de la TypeJournal en una lista.
        /// </summary>

        // GET: api/TypeJournal
        [HttpGet("[action]")]
        public async Task<IActionResult> GetTypeJournal()

        {
            List<TypeJournal> Items = new List<TypeJournal>();
            try
            {
                Items = await _context.TypeJournal.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
            //return await _context.Dimensions.ToListAsync();
        }
        /// <summary>
        /// Obtiene los Datos de la Journal por medio del Id enviado.
        /// </summary>
        /// <param name="TypeJournalId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{TypeJournalId}")]
        public async Task<IActionResult> GetTypeJournalById(Int64 TypeJournalId)
        {
            TypeJournal Items = new TypeJournal();
            try
            {
                Items = await _context.TypeJournal.Where(q => q.TypeJournalId == TypeJournalId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }
        /// <summary>
        /// Obtiene los Datos de la Journal por medio del Id enviado.
        /// </summary>
        /// <param name="TypeJournalName"></param>
        /// <returns></returns>
        [HttpGet("[action]/{TypeJournalName}")]
        public async Task<IActionResult> GetTypeJournalByName(String TypeJournalName)
        {
            TypeJournal Items = new TypeJournal();
            try
            {
                Items = await _context.TypeJournal.Where(q => q.TypeJournalName == TypeJournalName).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

    /// <summary>
    /// Inserta una nueva Journal
    /// </summary>
    /// <param name="_TypeJournal"></param>
    /// <returns></returns>
    [HttpPost("[action]")]
        public async Task<ActionResult<TypeJournal>> Insert([FromBody]TypeJournal _TypeJournal)
        {
            TypeJournal _TypeJournalq = new TypeJournal();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        _TypeJournalq = _TypeJournal;
                _context.TypeJournal.Add(_TypeJournalq);
                await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _TypeJournalq.TypeJournalId,
                            DocType = "TypeJournal",
                            ClaseInicial =
Newtonsoft.Json.JsonConvert.SerializeObject(_TypeJournalq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _TypeJournalq.CreatedUser,
                            UsuarioModificacion = _TypeJournalq.ModifiedUser,
                            UsuarioEjecucion = _TypeJournalq.ModifiedUser,

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

            return await Task.Run(() => Ok(_TypeJournalq));
        }

        /// <summary>
        /// Actualiza la Journal
        /// </summary>
        /// <param name="_TypeJournal"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<TypeJournal>> Update([FromBody]TypeJournal _TypeJournal)
        {
            TypeJournal _TypeJournalq = _TypeJournal;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        _TypeJournalq = await (from c in _context.TypeJournal
                                 .Where(q => q.TypeJournalId == _TypeJournal.TypeJournalId)
                                       select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_TypeJournalq).CurrentValues.SetValues((_TypeJournal));

                //_context.Bank.Update(_Bankq);
                await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _TypeJournalq.TypeJournalId,
                            DocType = "TypeJournal",
                            ClaseInicial =
Newtonsoft.Json.JsonConvert.SerializeObject(_TypeJournalq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _TypeJournalq.CreatedUser,
                            UsuarioModificacion = _TypeJournalq.ModifiedUser,
                            UsuarioEjecucion = _TypeJournalq.ModifiedUser,

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

            return await Task.Run(() => Ok(_TypeJournalq));
        }

        /// <summary>
        /// Elimina una Journal       
        /// </summary>
        /// <param name="_TypeJournal"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]TypeJournal _TypeJournal)
        {
            TypeJournal _TypeJournalq = new TypeJournal();
            try
            {
                _TypeJournalq = _context.TypeJournal
                .Where(x => x.TypeJournalId == (Int64)_TypeJournal.TypeJournalId)
                .FirstOrDefault();

                _context.TypeJournal.Remove(_TypeJournalq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_TypeJournalq));

        }
    }
}