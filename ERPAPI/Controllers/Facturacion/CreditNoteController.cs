using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Contexts;
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
    [Route("api/CreditNote")]
    [ApiController]
    public class CreditNoteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CreditNoteController(ILogger<CreditNoteController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de CreditNote paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCreditNotePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<CreditNote> Items = new List<CreditNote>();
            try
            {
                var query = _context.CreditNote.AsQueryable();
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
        /// Obtiene el Listado de CreditNotees 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCreditNote()
        {
            List<CreditNote> Items = new List<CreditNote>();
            try
            {
                var user = _context.Users.Where(w => w.UserName == User.Identity.Name.ToString());
                int count = user.Count();
                List<UserBranch> branchlist = await _context.UserBranch.Where(w => w.UserId == user.FirstOrDefault().Id).ToListAsync();
                if (branchlist.Count > 0)
                {
                    Items = await _context.CreditNote.Where(p => branchlist.Any(b => p.BranchId == b.BranchId)).OrderByDescending(b => b.CreditNoteId).ToListAsync();
                }
                else
                {
                    Items = await _context.CreditNote.OrderByDescending(b => b.CreditNoteId).ToListAsync();
                }
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
        /// Obtiene los Datos de la CreditNote por medio del Id enviado.
        /// </summary>
        /// <param name="CreditNoteId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CreditNoteId}")]
        public async Task<IActionResult> GetCreditNoteById(Int64 CreditNoteId)
        {
            CreditNote Items = new CreditNote();
            try
            {
                Items = await _context.CreditNote.Where(q => q.CreditNoteId == CreditNoteId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva CreditNote
        /// </summary>
        /// <param name="_CreditNote"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<CreditNote>> Insert([FromBody]CreditNote _CreditNote)
        {
            CreditNote _CreditNoteq = new CreditNote();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _CreditNoteq = _CreditNote;


                        Numalet let;
                        let = new Numalet();
                        let.SeparadorDecimalSalida = "Lempiras";
                        let.MascaraSalidaDecimal = "00/100 ";
                        let.ApocoparUnoParteEntera = true;
                        _CreditNoteq.TotalLetras = let.ToCustomCardinal((_CreditNoteq.Total)).ToUpper();
                        _CreditNoteq.Total = _CreditNoteq.CreditNoteLine.Sum(x => x.Total);
                        _CreditNoteq.Tax = _CreditNoteq.CreditNoteLine.Sum(x => x.TaxAmount);
                        _CreditNoteq.Discount = _CreditNoteq.CreditNoteLine.Sum(x => x.DiscountAmount);
                        _CreditNoteq.UsuarioCreacion= User.Identity.Name;
                        _CreditNoteq.UsuarioModificacion = User.Identity.Name;
                        _CreditNoteq.CreditNoteDate = DateTime.Now;
                        _CreditNoteq.NumeroSAR = "PROFORMA";
                        _CreditNoteq.NúmeroDEI = 0;


                        _context.CreditNote.Add(_CreditNoteq);

                        foreach (var item in _CreditNote.CreditNoteLine)
                        {
                            item.CreditNoteId = _CreditNote.CreditNoteId;
                            _context.CreditNoteLine.Add(item);
                        }

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        await _context.SaveChangesAsync();

                      /*
                        JournalEntry _je = new JournalEntry
                        {
                            Date = _CreditNoteq.CreditNoteDate,
                            Memo = "Nota de credito de clientes",
                            DatePosted = _CreditNoteq.CreditNoteDueDate,
                            ModifiedDate = DateTime.Now,
                            CreatedDate = DateTime.Now,
                            ModifiedUser = _CreditNoteq.UsuarioModificacion,
                            CreatedUser = _CreditNoteq.UsuarioCreacion,
                            DocumentId = _CreditNoteq.CreditNoteId,
                        };

                        Accounting account = new Accounting();

                        foreach (var item in _CreditNoteq.CreditNoteLine)
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
                                CreatedUser = _CreditNoteq.UsuarioCreacion,
                                ModifiedUser = _CreditNoteq.UsuarioModificacion,
                                Memo = "Nota de débito",
                            });

                        }

                        _context.JournalEntry.Add(_je);

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        await _context.SaveChangesAsync();*/

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _CreditNote.CreditNoteId,
                            DocType = "CreditNote",
                            ClaseInicial =
                                         Newtonsoft.Json.JsonConvert.SerializeObject(_CreditNote, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_CreditNote, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
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
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CreditNoteq));
        }

        /// <summary>
        /// Actualiza la CreditNote
        /// </summary>
        /// <param name="_CreditNote"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<CreditNote>> Update([FromBody]CreditNote _CreditNote)
        {
            CreditNote _CreditNoteq = _CreditNote;
            try
            {
                _CreditNoteq = await (from c in _context.CreditNote
                                 .Where(q => q.CreditNoteId == _CreditNote.CreditNoteId)
                                      select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_CreditNoteq).CurrentValues.SetValues((_CreditNote));

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                //_context.CreditNote.Update(_CreditNoteq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CreditNoteq));
        }

        /// <summary>
        /// Elimina una CreditNote       
        /// </summary>
        /// <param name="_CreditNote"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]CreditNote _CreditNote)
        {
            CreditNote _CreditNoteq = new CreditNote();
            try
            {
                _CreditNoteq = _context.CreditNote
                .Where(x => x.CreditNoteId == (Int64)_CreditNote.CreditNoteId)
                .FirstOrDefault();

                _context.CreditNote.Remove(_CreditNoteq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CreditNoteq));

        }







    }
}