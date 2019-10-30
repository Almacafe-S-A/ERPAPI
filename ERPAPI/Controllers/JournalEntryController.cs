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
                Items = await _context.JournalEntry.Where(q => q.TypeOfAdjustmentId == 65).ToListAsync();
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
        /// Obtiene los Datos de la JournalEntryLine por medio del Id enviado.
        /// </summary>
        /// 
        /// <param name="Date"></param>
        /// <param name="FechaInicio"></param>
        /// <param name="FechaFinal"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Date}")]
        public async Task<IActionResult> GetJournalEntryByDate(string FechaInicio,string FechaFinal)
        {
            //string fecha = Date.ToString("yyyy-MM-dd");
            DateTime fechainicio = Convert.ToDateTime(FechaInicio);
            DateTime fechafinal = Convert.ToDateTime(FechaFinal);
            //List<JournalEntry> Items = new List<JournalEntry>();
            //var Items;
            //List<JournalEntry> Items = new List<JournalEntry>();

            //var Items = new List<double>();

            //var Items = new List<double>();

            List<ConciliacionDTO> Items = new List<ConciliacionDTO>();



            try
            {


                //Items = await (from je in _context.JournalEntry
                //               join jel in _context.JournalEntryLine on je.JournalEntryId equals jel.JournalEntryId

                //               where (je.Date > fechainicio) && (je.Date < fechafinal) && (jel.AccountId == 10050)
                //               group jel by jel.Debit into g
                //               //select sum(debit), sum(credit)).Sum(e => e.Salary).ToListAsync();
                //               select new { credit = g.Sum(x => x.Credit) });




                //string trialbalance = "";
                //string horainicio = " 00:00:00";
                //string horafin = " 23:59:59";

                var query = "select sum(debit) as Debito ,SUM(CREDIT) as Credito from dbo.journalentryline jel   "
                  + $"inner join  dbo.journalentry je  on je.journalentryid = jel.journalentryid "
                  + $"where JE.[DATE] >= '2019-08-01' and JE.[DATE] < ='2019-08-31' and jel.AccountId = 10050"
                 + "  ";


                using (var dr = await _context.Database.ExecuteSqlQueryAsync(query))
                {
                    // Output rows.
                    var reader = dr.DbDataReader;
                    while (reader.Read())
                    {
                        //AccountId = reader["AccountId"] == DBNull.Value ? 0 : Convert.ToInt64(reader["AccountId"]),

                        Items.Add(new ConciliacionDTO
                        {
                            Debit = Convert.ToDouble(reader["Debito"]),
                            Credit = Convert.ToDouble(reader["Credito"])
                        });

                        //Items.Add(Convert.ToDouble(reader["CREDITO"]));
                        //Items.Add(
                        //{
                        //    //AccountId = reader["AccountId"] == DBNull.Value ? 0 : Convert.ToInt64(reader["AccountId"]),
                        //DEBITO = reader["DEBITO"] == DBNull.Value ? 0 : Convert.ToDouble(reader["TotalCredit"]),
                        //CREDITO = reader["CREDITO"] == DBNull.Value ? 0 : Convert.ToDouble(reader["TotalDebit"]),
                        //});

                    }
                }

               

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
        /// <param name="_JournalEntry"></param>
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


                        if (sumacreditos != sumadebitos)
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
        /// <param name="_JournalEntry"></param>
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
                _JournalEntry = JsonConvert.DeserializeObject<JournalEntry>(dto.ToString());
                _JournalEntryq = await (from c in _context.JournalEntry
                                 .Where(q => q.JournalEntryId == _JournalEntry.JournalEntryId)
                                 select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_JournalEntryq).CurrentValues.SetValues((_JournalEntry));

                await _context.SaveChangesAsync();
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
                _JournalEntryq = _context.JournalEntry
                .Where(x => x.JournalEntryId == (Int64)_JournalEntry.JournalEntryId)
                .FirstOrDefault();

                _context.JournalEntry.Remove(_JournalEntryq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_JournalEntryq));

        }
    }
}