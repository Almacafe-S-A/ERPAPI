using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Contexts;
using ERPAPI.Filters;
using ERPAPI.Helpers;
using ERPAPI.Helpers;
using ERPAPI.Models;
using ERPAPI.Wrappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/JournalEntry")]
    [ApiController]
    public class JournalEntryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        /*public DimensionsController(ApplicationDbContext context)
        {
            _context = context;
        }*/
        public JournalEntryController(ILogger<JournalEntryController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// btiene el Listado de JournalEntry paginado
        /// </summary>
        /// <param name="pageNumer"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>

        [HttpGet("[action]/{pageNumer}/{pageSize}")]
        public async Task<IActionResult> GetJournalEntryPag(int pageNumer, int pageSize)
        {
            var validFilter = new PaginationFilter(pageNumer, pageSize);
            var pagedData = await _context.JournalEntry
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            var totalRegistros = await _context.JournalEntry.CountAsync();
            return Ok(new PagedResponse<List<JournalEntry>>(pagedData, validFilter.PageNumber, validFilter.PageSize));
           
        }

        /// <summary>
        /// Obtiene los Datos de la Diarios en una lista.
        /// </summary>

        // GET: api/JournalEntry
        [HttpGet("[action]")]
        public async Task<IActionResult> GetJournalEntry()

        {
            List<JournalEntry> Items = new List<JournalEntry>();
            try
            {
                Items = await _context.JournalEntry.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }


        // GET: api/JournalEntry
        [HttpGet("[action]")]
        public async Task<IActionResult> GetJournalEntryAsientos()
        {
            List<JournalEntry> Items = new List<JournalEntry>();
            try
            {
                Items = await _context.JournalEntry.Where(q => q.TypeOfAdjustmentId == 65).ToListAsync();
                //Items = await _context.JournalEntry.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }


        // GET: api/JournalEntry
        [HttpGet("[action]")]
        public async Task<IActionResult> GetJournalEntryAjustes()

        {
            List<JournalEntry> Items = new List<JournalEntry>();
            try
            {
                Items = await _context.JournalEntry.Where(q => q.TypeOfAdjustmentId == 66).ToListAsync();
                //Items = await _context.JournalEntry.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Obtiene los Datos de la JournalEntry por medio del Id enviado.
        /// </summary>
        /// <param name="JournalEntryId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{JournalEntryId}")]
        public async Task<IActionResult> GetJournalEntryById(Int64 JournalEntryId)
        {
            JournalEntry Items = new JournalEntry();
            try
            {
                Items = await _context.JournalEntry.Where(q => q.JournalEntryId == JournalEntryId).Include(q=>q.JournalEntryLines).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }
        


        /// <summary>
        /// Inserta una nueva JournalEntry
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<JournalEntry>> Insert([FromBody]dynamic dto)
        //public async Task<ActionResult<JournalEntry>> Insert([FromBody]JournalEntry _JournalEntry)
        {
           JournalEntry _JournalEntry = new JournalEntry();
            JournalEntry _JournalEntryq = new JournalEntry();
            try
            {
                _JournalEntry = JsonConvert.DeserializeObject<JournalEntry>(dto.ToString());
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _JournalEntryq = _JournalEntry;
                        _context.JournalEntry.Add(_JournalEntryq);
                        // await _context.SaveChangesAsync();
                        decimal sumacreditos = 0, sumadebitos = 0;
                        foreach (var item in _JournalEntryq.JournalEntryLines)
                        {
                            item.JournalEntryId = _JournalEntryq.JournalEntryId;
                           // item.JournalEntryLineId = 0;
                            _context.JournalEntryLine.Add(item);
                            sumacreditos += item.Credit > 0 ? item.Credit : 0;
                            sumadebitos += item.Debit>0 ? item.Debit : 0;
                        }


                        if (sumacreditos.ToString("N2") != sumadebitos.ToString("N2"))
                        {
                            transaction.Rollback();
                            _logger.LogError($"Ocurrio un error: No coinciden debitos :{sumadebitos} y creditos:{sumacreditos}");
                            return BadRequest($"Ocurrio un error: No coinciden debitos :{sumadebitos} y creditos:{sumacreditos}");
                        }

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        await _context.SaveChangesAsync();



                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _JournalEntry.JournalEntryId,
                            DocType = "JournalEntry",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_JournalEntry, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _JournalEntry.CreatedUser,
                            UsuarioModificacion = _JournalEntry.ModifiedUser,
                            UsuarioEjecucion = _JournalEntry.ModifiedUser,

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

            return await Task.Run(() => Ok(_JournalEntryq));
        }

        /// <summary>
        /// Actualiza la JournalEntry
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
       // public async Task<ActionResult<JournalEntry>> Update([FromBody]JournalEntry _JournalEntry)
        public async Task<ActionResult<JournalEntry>> Update([FromBody]dynamic dto)
        {
            bool isapproved = new bool();
            //JournalEntry _JournalEntryq = _JournalEntry;
            JournalEntry _JournalEntry = new JournalEntry();
            JournalEntry _JournalEntryq = new JournalEntry();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        _JournalEntry = JsonConvert.DeserializeObject<JournalEntry>(dto.ToString());
                _JournalEntryq = await (from c in _context.JournalEntry
                                 .Where(q => q.JournalEntryId == _JournalEntry.JournalEntryId)
                                 select c
                                ).FirstOrDefaultAsync();

                        if(_JournalEntryq.EstadoId == 6)
                        {
                            isapproved = true;
                        }
                _context.Entry(_JournalEntryq).CurrentValues.SetValues((_JournalEntry));

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();


                        await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _JournalEntry.JournalEntryId,
                            DocType = "JournalEntry",
                            ClaseInicial =
                          Newtonsoft.Json.JsonConvert.SerializeObject(_JournalEntry, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _JournalEntry.CreatedUser,
                            UsuarioModificacion = _JournalEntry.ModifiedUser,
                            UsuarioEjecucion = _JournalEntry.ModifiedUser,

                        });

                        await _context.SaveChangesAsync();

                        if(!isapproved)
                        {
                            CheckAccountLines check = _context.CheckAccountLines.Where(w => w.JournalEntrId == _JournalEntryq.JournalEntryId).FirstOrDefault();
                            if (check != null )
                            {
                                check.Estado = "Autorizado";
                                check.IdEstado = 98;
                            }
                            
                        }
                        else
                        {
                            CheckAccountLines check = _context.CheckAccountLines.Where(w => w.JournalEntrId == _JournalEntryq.JournalEntryId).FirstOrDefault();
                            if (check != null)
                            {
                                check.Estado = "Rechazado";
                                check.IdEstado = 99;
                            }
                        }

                        transaction.Commit();
                        
                        await _context.SaveChangesAsync();
                        ///Actualiza el Saldo del Catalogo contable                       
                        ContabilidadHandler.ActualizarSaldoCuentas(_context, _JournalEntry);
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

            return await Task.Run(() => Ok(_JournalEntryq));
        }

        /// <summary>
        /// Elimina una JournalEntry       
        /// </summary>
        /// <param name="_JournalEntry"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]JournalEntry _JournalEntry)
        {
            JournalEntry _JournalEntryq = new JournalEntry();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        _JournalEntryq = _context.JournalEntry
                .Where(x => x.JournalEntryId == (Int64)_JournalEntry.JournalEntryId)
                .FirstOrDefault();

                _context.JournalEntry.Remove(_JournalEntryq);
                await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _JournalEntry.JournalEntryId,
                            DocType = "JournalEntry",
                            ClaseInicial =
                      Newtonsoft.Json.JsonConvert.SerializeObject(_JournalEntry, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Eliminar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _JournalEntry.CreatedUser,
                            UsuarioModificacion = _JournalEntry.ModifiedUser,
                            UsuarioEjecucion = _JournalEntry.ModifiedUser,

                        });


                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();


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

            return await Task.Run(() => Ok(_JournalEntryq));

        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<JournalEntryLineDTO>>> GetLineasAsientoContableCuentaRangoFechas([FromQuery(Name = "CodigoCuenta")]Int64 codigoCuenta,
            [FromQuery(Name = "FechaInicial")]DateTime fechaInicial, [FromQuery(Name = "FechaFinal")]DateTime fechaFinal)
        {
            try
            {
              /*  var entradas = (from lineas in _context.JournalEntryLine
                    join cabeza in _context.JournalEntry on lineas.JournalEntryId equals cabeza.JournalEntryId
                    where cabeza.Date >= fechaInicial && cabeza.Date <= fechaFinal.AddDays(1).AddTicks(-1) && lineas.AccountId == codigoCuenta && cabeza.EstadoId == 6 
                          && !_context.ConciliacionLinea.Any(l=> l.JournalEntryId == lineas.JournalEntryId && l.JournalEntryLineId == lineas.JournalEntryLineId)
                    select new JournalEntryLineDTO(lineas, cabeza.Date, cabeza.TypeJournalName)).ToList();*/
                var entradas = (from lineas in _context.JournalEntryLine
                                join cabeza in _context.JournalEntry on lineas.JournalEntryId equals cabeza.JournalEntryId
                                where
                                //cabeza.Date >= fechaInicial && cabeza.Date <= fechaFinal.AddDays(1).AddTicks(-1) && 
                                 cabeza.DatePosted <= fechaFinal.AddDays(1).AddTicks(-1) &&
                                lineas.AccountId == codigoCuenta && cabeza.EstadoId == 6
                                      && !_context.ConciliacionLinea.Any(l => l.JournalEntryId == lineas.JournalEntryId && l.JournalEntryLineId == lineas.JournalEntryLineId)
                                select new JournalEntryLineDTO(lineas, cabeza.DatePosted, cabeza.TypeJournalName,cabeza.DocumentId)).ToList();
                return await Task.Run(() => Ok(entradas));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en GetLineasAsientoContableCuentaRangoFechas: {ex}");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
        }
    }
}