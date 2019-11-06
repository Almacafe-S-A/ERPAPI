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
    [Route("api/DebitNote")]
    [ApiController]
    public class DebitNoteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public DebitNoteController(ILogger<DebitNoteController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de DebitNote paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetDebitNotePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<DebitNote> Items = new List<DebitNote>();
            try
            {
                var query = _context.DebitNote.AsQueryable();
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
        /// Obtiene el Listado de DebitNotees 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetDebitNote()
        {
            List<DebitNote> Items = new List<DebitNote>();
            try
            {
                Items = await _context.DebitNote.ToListAsync();
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
        /// Obtiene los Datos de la DebitNote por medio del Id enviado.
        /// </summary>
        /// <param name="DebitNoteId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{DebitNoteId}")]
        public async Task<IActionResult> GetDebitNoteById(Int64 DebitNoteId)
        {
            DebitNote Items = new DebitNote();
            try
            {
                Items = await _context.DebitNote.Where(q => q.DebitNoteId == DebitNoteId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva DebitNote
        /// </summary>
        /// <param name="_DebitNote"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<DebitNote>> Insert([FromBody]DebitNote _DebitNote)
        {
            DebitNote _DebitNoteq = new DebitNote();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _DebitNoteq = _DebitNote;
                        _context.DebitNote.Add(_DebitNoteq);

                        foreach (var item in _DebitNote.DebitNoteLine)
                        {
                            item.DebitNoteId = _DebitNote.DebitNoteId;
                            _context.DebitNoteLine.Add(item);
                        }


                        await _context.SaveChangesAsync();

                        JournalEntry _je = new JournalEntry
                        {
                            Date = _DebitNoteq.DebitNoteDate,
                            Memo = "Nota de débito de clientes",
                            DatePosted = _DebitNoteq.DebitNoteDueDate,
                            ModifiedDate = DateTime.Now,
                            CreatedDate = DateTime.Now,
                            ModifiedUser = _DebitNoteq.UsuarioModificacion,
                            CreatedUser = _DebitNoteq.UsuarioCreacion,
                            DocumentId = _DebitNoteq.DebitNoteId,
                            VoucherType = 4,
                    };

                        Accounting account = new Accounting();


                        foreach (var item in _DebitNoteq.DebitNoteLine)
                        {
                            account = await _context.Accounting.Where(acc => acc.AccountId == item.AccountId).FirstOrDefaultAsync();

                            _je.JournalEntryLines.Add(new JournalEntryLine
                            {
                                AccountId = Convert.ToInt32(item.AccountId),
                                AccountName = account.AccountName,
                                Description = account.AccountName,
                                Credit = item.Total,
                                Debit = 0,
                                CreatedDate = DateTime.Now,
                                ModifiedDate = DateTime.Now,
                                CreatedUser = _DebitNoteq.UsuarioCreacion,
                                ModifiedUser = _DebitNoteq.UsuarioModificacion,
                                Memo = "Nota de débito",
                            });

                        }

                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _DebitNote.DebitNoteId,
                            DocType = "DebitNote",
                            ClaseInicial =
                                         Newtonsoft.Json.JsonConvert.SerializeObject(_DebitNote, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_DebitNote, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,

                        });

                        await _context.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {

                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        throw ex;
                    }
                }

            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(()=> BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_DebitNoteq));
        }

        /// <summary>
        /// Actualiza la DebitNote
        /// </summary>
        /// <param name="_DebitNote"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<DebitNote>> Update([FromBody]DebitNote _DebitNote)
        {
            DebitNote _DebitNoteq = _DebitNote;
            try
            {
                _DebitNoteq = await (from c in _context.DebitNote
                                 .Where(q => q.DebitNoteId == _DebitNote.DebitNoteId)
                                     select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_DebitNoteq).CurrentValues.SetValues((_DebitNote));

                //_context.DebitNote.Update(_DebitNoteq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_DebitNoteq));
        }

        /// <summary>
        /// Elimina una DebitNote       
        /// </summary>
        /// <param name="_DebitNote"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]DebitNote _DebitNote)
        {
            DebitNote _DebitNoteq = new DebitNote();
            try
            {
                _DebitNoteq = _context.DebitNote
                .Where(x => x.DebitNoteId == (Int64)_DebitNote.DebitNoteId)
                .FirstOrDefault();

                _context.DebitNote.Remove(_DebitNoteq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_DebitNoteq));

        }







    }
}