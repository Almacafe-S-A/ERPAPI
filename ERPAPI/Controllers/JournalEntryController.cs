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
        /// Obtiene el Listado de JournalEntry paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetJournalEntryPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<JournalEntry> Items = new List<JournalEntry>();
            try
            {
                var query = _context.JournalEntry.AsQueryable();
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
                //Items = await _context.JournalEntry.Where(q => q.TypeOfAdjustmentId == 65).ToListAsync();
                Items = await _context.JournalEntry.ToListAsync();
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
                //Items = await _context.JournalEntry.Where(q => q.TypeOfAdjustmentId == 66).ToListAsync();
                Items = await _context.JournalEntry.ToListAsync();
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
        /// Obtiene los Datos de la JournalEntryLine por medio del Conciliacion.
        /// </summary>
        /// 
         /// <returns></returns>
        /*[HttpPost("[action]")]
        public async Task<IActionResult> GetJournalEntryByDateAccount([FromBody]Conciliacion _ConciliacionP)
        {
          //  JournalEntry Items = new JournalEntry();
            List<ConciliacionLinea> LineConciliacionLinea = new List<ConciliacionLinea>();
            try
            {

                var consulta = from journale in _context.JournalEntry
                               join journalel in _context.JournalEntryLine
                               on journale.JournalEntryId equals journalel.JournalEntryId
                               //join Currencyl in _context.Currency
                               // on journale.CurrencyId equals Currencyl.CurrencyId
                               where journalel.AccountId == _ConciliacionP.ConciliacionLinea[0].AccountId &&
                                 journale.Date >= _ConciliacionP.DateBeginReconciled &&
                                 journale.Date <= _ConciliacionP.DateEndReconciled
                               // group journalel
                               //  by journalel.JournalEntryId into journalel

                               select new ConciliacionLinea
                               {
                                   Debit = journalel.Debit,
                                   Credit = journalel.Credit,
                                   Monto = journalel.Debit - journalel.Credit,
                                   AccountId = journalel.AccountId,
                                   JournalEntryId = journalel.JournalEntryId,
                                   JournalEntryLineId = journalel.JournalEntryLineId,
                                   FechaCreacion = DateTime.Now,
                                   FechaModificacion = DateTime.Now,
                                   UsuarioCreacion = _ConciliacionP.UsuarioCreacion,
                                   UsuarioModificacion = _ConciliacionP.UsuarioModificacion,
                                   ConciliacionId = _ConciliacionP.ConciliacionId,
                                   ReferenceTrans = journale.ReferenceNo,
                                   VoucherTypeId = (int)journale.VoucherType,
                                   TransDate = journale.Date,
                                   CurrencyId =journale.CurrencyId,
                                   AccountName=journalel.AccountName,
                                   MonedaName= _context.Currency.Where(
                                       p => p.CurrencyId == journale.CurrencyId
                                       ).FirstOrDefault().CurrencyName,
                               };

                LineConciliacionLinea = consulta.ToList();
                               //   var query = "select sum(debit) as Debito ,SUM(CREDIT) as Credito from dbo.journalentryline jel   "
                               //   + $"inner join  dbo.journalentry je  on je.journalentryid = jel.journalentryid "
                               //   + $"where JE.[DATE] >= '{FechaInicio}' and JE.[DATE] < ='{FechaFinal}' and jel.AccountId = {AccountId}"
                               //  + "  ";







            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(LineConciliacionLinea));
        }*/


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
                        double sumacreditos = 0, sumadebitos = 0;
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

                _context.Entry(_JournalEntryq).CurrentValues.SetValues((_JournalEntry));

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
                var entradas = (from lineas in _context.JournalEntryLine
                    join cabeza in _context.JournalEntry on lineas.JournalEntryId equals cabeza.JournalEntryId
                    where cabeza.Date >= fechaInicial && cabeza.Date <= fechaFinal.AddDays(1).AddTicks(-1) && lineas.AccountId == codigoCuenta
                    select new JournalEntryLineDTO(lineas, cabeza.Date)).ToList();
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