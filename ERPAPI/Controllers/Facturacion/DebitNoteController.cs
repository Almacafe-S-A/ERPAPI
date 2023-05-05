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
                var user = _context.Users.Where(w => w.UserName == User.Identity.Name.ToString());
                int count = user.Count();
                List<UserBranch> branchlist = await _context.UserBranch.Where(w => w.UserId == user.FirstOrDefault().Id).ToListAsync();
                if (branchlist.Count > 0)
                {
                    Items = await _context.DebitNote.Where(p => branchlist.Any(b => p.BranchId == b.BranchId)).OrderByDescending(b => b.DebitNoteId).ToListAsync();
                }
                else
                {
                    Items = await _context.DebitNote.OrderByDescending(b => b.DebitNoteId).ToListAsync();
                }
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
        /// Obtiene los Datos de la Invoice por medio del Id enviado.
        /// </summary>
        /// <param name="InvoiceId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GenerarNotaDebito(Int64 Id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                DebitNote debitnote = new DebitNote();
                try
                {
                    Periodo periodo = new Periodo();
                    periodo = periodo.PeriodoActivo(_context);

                    debitnote = await _context.DebitNote
                        .Include(i => i.accountManagement)
                        .Where(q => q.DebitNoteId == Id)
                        .FirstOrDefaultAsync();

                    Customer customer = _context.Customer
                        .Where(q => q.CustomerId == debitnote.CustomerId)
                        .FirstOrDefault();

                    
                    NumeracionSAR numeracionSAR = new NumeracionSAR();
                    numeracionSAR = numeracionSAR.ObtenerNumeracionSarValida(9, _context);

                    debitnote.NumeroDEI = numeracionSAR.GetCorrelativo();
                    debitnote.RangoAutorizado = numeracionSAR.getRango();
                    debitnote.CAI = numeracionSAR._cai;
                    debitnote.NoInicio = numeracionSAR.NoInicio.ToString();
                    debitnote.NoFin = numeracionSAR.NoFin.ToString();
                    debitnote.FechaLimiteEmision = numeracionSAR.FechaLimite;
                    debitnote.UsuarioModificacion = User.Identity.Name.ToUpper();
                    debitnote.FechaModificacion = DateTime.Now;
                    debitnote.DebitNoteDueDate = DateTime.Now.AddDays(debitnote.DiasVencimiento);
                    debitnote.Estado = "Emitido";

                    _context.NumeracionSAR.Update(numeracionSAR);

                    //var alerta = await GeneraAlerta(debitnote);

                    JournalEntry asiento;

                    var resppuesta = GeneraAsientoNotaDebito(debitnote).Result.Value;

                    asiento = resppuesta as JournalEntry;

                    debitnote.JournalEntryId = asiento.JournalEntryId;
                    debitnote.Saldo = debitnote.Amount;

                    new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                    await _context.SaveChangesAsync();

                    _context.CustomerAcccountStatus.Add(new CustomerAcccountStatus
                    {
                        Credito = 0,
                        Fecha = DateTime.Now,
                        CustomerName = debitnote.CustomerName,
                        Debito = debitnote.Total,
                        Sinopsis = debitnote.Remarks,
                        InvoiceId = debitnote.InvoiceId,
                        NoDocumento = debitnote.NumeroDEI,
                        CustomerId = debitnote.CustomerId,
                    });


                    debitnote.FechaModificacion = DateTime.Now;

                    new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                    await _context.SaveChangesAsync();


                    transaction.Commit();
                }
                catch (Exception ex)
                {

                    _logger.LogError($"Ocurrio un error: {ex.ToString()}");
                    transaction.Rollback();
                    return BadRequest($"Ocurrio un error:{ex.Message}");
                }


                return await Task.Run(() => Ok(debitnote));
            }

        }

        /// <summary>
        /// Genera el asiento de la debitnote
        /// </summary>
        /// <param name="debitnote"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ActionResult<JournalEntry>> GeneraAsientoNotaDebito(DebitNote debitnote)
        {
            JournalEntry partida = new JournalEntry();
            ///Impuesto 
            ///
          

            try
            {
                Periodo periodo = new Periodo();
                periodo = periodo.PeriodoActivo(_context);

                partida = new JournalEntry
                {
                    Date = DateTime.Now,
                    DatePosted = DateTime.Now,
                    CreatedUser = User.Identity.Name,
                    CreatedDate = DateTime.Now,
                    EstadoId = 5,
                    EstadoName = "Enviada a Aprobación",
                    PeriodoId = periodo.Id,
                    TypeOfAdjustmentId = 65,
                    TypeOfAdjustmentName = "Asiento Diario",
                    JournalEntryLines = new List<JournalEntryLine>(),
                    Memo = $"Nota de Debito #{debitnote.NumeroDEI} a Cliente {debitnote.CustomerName} por concepto de {debitnote.Remarks}",
                    Periodo = periodo.Anio.ToString(),
                    Posted = false,
                    TotalCredit = 0,
                    TotalDebit = 0,
                    ModifiedDate = DateTime.Now,
                    ModifiedUser = User.Identity.Name,
                    VoucherType = 4,



                };

                if (debitnote.SalesTypeId == 2)
                {
                    partida.JournalEntryLines.Add(new JournalEntryLine
                    {
                        AccountId = (long)debitnote.CuentaContableIngresosId,
                        AccountName = debitnote.CuentaContableIngresosNombre,
                        CostCenterId = 1,
                        CostCenterName = "San Pedro Sula",
                        Debit = 0,
                        Credit = debitnote.Amount ,
                        CreatedDate = DateTime.Now,
                        CreatedUser = User.Identity.Name,
                        ModifiedUser = User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                    });
                }
                else
                {
                    Accounting accounting = _context.Accounting
                        .Where(q => q.AccountId == debitnote.accountManagement.AccountId)
                        .FirstOrDefault();
                    partida.JournalEntryLines.Add(new JournalEntryLine
                    {
                        AccountId = accounting.AccountId,
                        AccountName = $"{accounting.AccountCode} - {accounting.AccountName}",
                        CostCenterId = 1,
                        CostCenterName = "San Pedro Sula",
                        Debit = 0,
                        Credit = debitnote.Amount,
                        CreatedDate = DateTime.Now,
                        CreatedUser = User.Identity.Name,
                        ModifiedUser = User.Identity.Name,
                        ModifiedDate = DateTime.Now,



                    });
                }
               

                partida.JournalEntryLines.Add(new JournalEntryLine
                {
                    AccountId = (long)debitnote.CuentaContableDebitoId,
                    AccountName = debitnote.CuentaContableDebitoNombre,
                    CostCenterId = 1,
                    CostCenterName = "San Pedro Sula",
                    Debit = debitnote.Amount,
                    Credit = 0,
                    CreatedDate = DateTime.Now,
                    CreatedUser = User.Identity.Name,
                    ModifiedUser = User.Identity.Name,
                    ModifiedDate = DateTime.Now,



                });



                partida.TotalCredit = partida.JournalEntryLines.Sum(s => s.Credit);
                partida.TotalDebit = partida.JournalEntryLines.Sum(s => s.Debit);

                partida.JournalEntryLines = partida.JournalEntryLines.OrderBy(o => o.Credit).ThenBy(t => t.AccountId).ToList();


                _context.JournalEntry.Add(partida);
            }
            catch (Exception)
            {

                throw new Exception("Falta la configuracion contable en los subservicios utilizados");
            }



            return partida;
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
                        _DebitNoteq.Estado = "Borrador";

                        Numalet let;
                        let = new Numalet();
                        let.SeparadorDecimalSalida = "Lempiras";
                        let.MascaraSalidaDecimal = "00/100 ";
                        let.ApocoparUnoParteEntera = true;
                        _DebitNoteq.TotalLetras = let.ToCustomCardinal((_DebitNoteq.Amount)).ToUpper();

                        _DebitNoteq = _DebitNote;
                        _context.DebitNote.Add(_DebitNoteq);

                        foreach (var item in _DebitNote.DebitNoteLine)
                        {
                            item.DebitNoteId = _DebitNote.DebitNoteId;
                            _context.DebitNoteLine.Add(item);
                        }


                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

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

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

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

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

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