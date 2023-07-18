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
                Items = await _context.CreditNote
                    .Where(q => q.CreditNoteId == CreditNoteId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Obtiene los Datos de la CreditNote por medio del Id enviado.
        /// </summary>
        /// <param name="CreditnoteId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CreditNoteId}/{estado}")]
        public async Task<IActionResult> ChangeStatus(Int64 CreditnoteId, int estado)
        {
            CreditNote Items = new CreditNote();
            try
            {
                Items = await _context
                    .CreditNote
                    .Where(q => q.CreditNoteId == CreditnoteId).
                    FirstOrDefaultAsync();

                switch (estado)
                {
                    case 1:
                        Items.Estado = "Revisado";
                        Items.RevisadoEl = DateTime.Now;
                        Items.RevisadoPor = User.Identity.Name;
                        break;
                    case 2:
                        Items.Estado = "Aprobado";
                        Items.AprobadoEl = DateTime.Now;
                        Items.AprobadoPor = User.Identity.Name;
                        break;
                    case 3:
                        Items.Estado = "Rechazado";
                        Items.RevisadoEl = DateTime.Now;
                        break;
                    default:
                        break;
                }

                Items.UsuarioModificacion = User.Identity.Name;
                Items.FechaModificacion = DateTime.Now;

                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();


                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: {ex.ToString()}");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Anula la creditnote por medio del Id enviado.
        /// </summary>
        /// <param name="InvoiceId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> Anular(Int64 Id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                CreditNote creditnote = new CreditNote();
                try
                {
                    Periodo periodo = new Periodo();
                    periodo = periodo.PeriodoActivo(_context);

                    creditnote = await _context.CreditNote
                        .Include(i => i.CreditNoteLine)
                        .Include(i => i.JournalEntry)
                        .Where(q => q.CreditNoteId == Id)
                        .FirstOrDefaultAsync();

                    Customer customer = _context.Customer
                        .Where(q => q.CustomerId == creditnote.CustomerId)
                        .FirstOrDefault();


                    JournalEntry asientoFactura = _context.JournalEntry
                        .Include(j => j.JournalEntryLines)
                        .Where(q => q.JournalEntryId == creditnote.JournalEntryId).FirstOrDefault();



                    JournalEntry asientoreversado = new JournalEntry();

                    asientoreversado = new JournalEntry
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
                        Memo = $"Nota de credito #{creditnote.NumeroDEI} anulada a Cliente {creditnote.CustomerName}",
                        Periodo = periodo.Anio.ToString(),
                        Posted = false,
                        TotalCredit = 0,
                        TotalDebit = 0,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = User.Identity.Name,
                        VoucherType = 1,
                        TypeJournalName = "Factura de ventas",
                        PartyTypeId = 1,
                        PartyTypeName = "Cliente",
                        PartyName = creditnote.CustomerName,
                        PartyId = creditnote.CustomerId,




                    };
                    foreach (var item in asientoFactura.JournalEntryLines)
                    {
                        asientoreversado.JournalEntryLines.Add(new JournalEntryLine
                        {
                            AccountId = item.AccountId,
                            CostCenterId = item.CostCenterId,
                            CostCenterName = item.CostCenterName,
                            CreatedDate = DateTime.Now,
                            CreatedUser = User.Identity.Name,
                            Credit = item.Debit,
                            Debit = item.Credit,
                            AccountName = item.AccountName,
                            Description = item.Description,
                            Memo = item.Memo,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = User.Identity.Name,
                            PartyId = item.PartyId,
                            PartyTypeName = item.PartyTypeName,
                            PartyName = item.PartyName,
                            PartyTypeId = item.PartyTypeId,


                        });
                    }


                    asientoreversado.TotalCredit = asientoreversado.JournalEntryLines.Sum(s => s.Credit);
                    asientoreversado.TotalDebit = asientoreversado.JournalEntryLines.Sum(s => s.Debit);

                    asientoreversado.JournalEntryLines = asientoreversado.JournalEntryLines.OrderBy(o => o.Credit).ThenBy(t => t.AccountId).ToList();

                    _context.JournalEntry.Add(asientoreversado);



                    //creditnote. = 0;
                    //creditnote.SaldoImpuesto = 0;
                    creditnote.Estado = "Anulado";

                    

                    creditnote.FechaModificacion = DateTime.Now;

                    new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                    await _context.SaveChangesAsync();

                    _context.CancelledDocuments.Add(new CancelledDocuments
                    {
                        FechaCreacion = DateTime.Now,
                        IdDocumento = creditnote.CreditNoteId,
                        IdTipoDocumento = 9,
                        TipoDocumento = "Nota de Credito",
                        JournalEntryId = asientoreversado.JournalEntryId,
                        UsuarioCreacion = User.Identity.Name,
                        UsuarioModificacion = User.Identity.Name
                    });


                    CustomerAcccountStatus accountstatus = _context.CustomerAcccountStatus.Where(q => q.DocumentoId == Id && q.TipoDocumentoId == 8).FirstOrDefault();

                    accountstatus.Debito = 0;
                    accountstatus.Credito = 0;

                    accountstatus.Sinopsis = "#### A N U L A D O##### " + accountstatus.Sinopsis;

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


                return await Task.Run(() => Ok(creditnote));
            }

        }



        /// <summary>
        /// Obtiene los Datos de la CreditNote por medio del Id enviado.
        /// </summary>
        /// <param name="CreditNoteId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CreditNoteId}/{interna}")]
        public async Task<IActionResult> Generar(Int64 CreditNoteId ,  int interna )
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                CreditNote creditnote = new CreditNote();
                try
                {
                    Periodo periodo = new Periodo();
                    periodo = periodo.PeriodoActivo(_context);

                    creditnote = await _context.CreditNote
                        .Include(i => i.CreditNoteLine)
                        .Where(q => q.CreditNoteId == CreditNoteId)
                        .FirstOrDefaultAsync();

                    Customer customer = _context.Customer
                        .Where(q => q.CustomerId == creditnote.CustomerId)
                        .FirstOrDefault();

                    if(interna== 0)
                    {
                        NumeracionSAR numeracionSAR = new NumeracionSAR();
                        numeracionSAR = numeracionSAR.ObtenerNumeracionSarValida(8, creditnote.BranchId, _context);

                        creditnote.NumeroDEI = numeracionSAR.GetCorrelativo();
                        creditnote.RangoAutorizado = numeracionSAR.getRango();
                        creditnote.CAI = numeracionSAR._cai;
                        creditnote.NoInicio = numeracionSAR.NoInicio.ToString();
                        creditnote.NoFin = numeracionSAR.NoFin.ToString();
                        creditnote.FechaLimiteEmision = numeracionSAR.FechaLimite;
                        _context.NumeracionSAR.Update(numeracionSAR);

                    }
                    else
                    {
                        creditnote.NumeroDEI = "Interna";
                    }


                    creditnote.UsuarioModificacion = User.Identity.Name.ToUpper();
                    creditnote.FechaModificacion = DateTime.Now;
                    //creditnote. = DateTime.Now.AddDays(creditnote.DiasVencimiento);
                    creditnote.Estado = "Emitido";

                    
                    //var alerta = await GeneraAlerta(creditnote);

                    JournalEntry asiento = new JournalEntry();


                    _context.CustomerAcccountStatus.Add(new CustomerAcccountStatus
                    {
                        Debito = 0,
                        Fecha = DateTime.Now,
                        CustomerName = creditnote.CustomerName,
                        Credito = creditnote.Total,
                        Sinopsis = $"Credito por Nota de Credito #{creditnote.NumeroDEI} " ,                        
                        NoDocumento = creditnote.NumeroDEI,
                        CustomerId = creditnote.CustomerId,
                        TipoDocumentoId = 8,
                        TipoDocumento = "Nota de Credito",
                        DocumentoId = creditnote.CreditNoteId,
                    });

                    asiento = GeneraAsiento(creditnote).Result.Value;

                    creditnote.JournalEntryId = asiento.JournalEntryId;

                    Invoice invoice = _context.Invoice
                        .Include(i => i.InvoiceLine)
                        .Where(q => q.InvoiceId == creditnote.InvoiceId).FirstOrDefault();

                    foreach (var item in creditnote.CreditNoteLine)
                    {
                        
                        if (item.SubProductId == 10000)
                        {
                            
                            invoice.SaldoImpuesto = invoice.SaldoImpuesto - item.CreditValue;
                           
                            continue;

                        }
                        InvoiceLine invoiceLine = invoice.InvoiceLine.Where(q => item.SubProductId == q.SubProductId).FirstOrDefault();
                        invoiceLine.Saldo = invoiceLine.Saldo - item.CreditValue;
                        invoiceLine.Invoice.Saldo = invoiceLine.Invoice.Saldo - item.CreditValue;
                        
                    }

                    new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();
                    await _context.SaveChangesAsync();

                    creditnote.FechaModificacion = DateTime.Now;

                    Numalet let;
                    let = new Numalet();
                    let.SeparadorDecimalSalida = "Lempiras";
                    let.MascaraSalidaDecimal = "00/100 ";
                    let.ApocoparUnoParteEntera = true;
                    creditnote.TotalLetras = let.ToCustomCardinal((creditnote.Total)).ToUpper();

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


                return await Task.Run(() => Ok(creditnote));
            }

        }






        /// <summary>
        /// Genera el asiento de la creditnote
        /// </summary>
        /// <param name="creditnote"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ActionResult<JournalEntry>> GeneraAsiento(CreditNote creditnote)
        {
            JournalEntry partida = new JournalEntry();
            ///Impuesto 
            ///
            Tax tax = new Tax();
            tax = _context.Tax.Where(x => x.TaxId == 1).FirstOrDefault();

            if (tax.CuentaContablePorCobrarId == null || tax.CuentaContableIngresosId == null)
            {
                throw new Exception("No se han configurado las cuentas contables para el ISV");
            }
            try
            {
                Periodo periodo = new Periodo();
                periodo = periodo.PeriodoActivo(_context);


                CostCenter centrocosto = _context.CostCenter.Where(x => x.BranchId == creditnote.BranchId).FirstOrDefault();

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
                    Memo = $"Nota de Credito #{creditnote.NumeroDEI} a Cliente {creditnote.CustomerName} por concepto de {creditnote.ProductName}",
                    Periodo = periodo.Anio.ToString(),
                    Posted = false,
                    TotalCredit = 0,
                    TotalDebit = 0,
                    ModifiedDate = DateTime.Now,
                    ModifiedUser = User.Identity.Name,
                    VoucherType = 3,
                    TypeJournalName = "Nota de crédito",
                    PartyTypeId = 1,
                    PartyTypeName = "Cliente",
                    PartyName = creditnote.CustomerName,
                    PartyId = creditnote.CustomerId,




                };



                foreach (var item in creditnote.CreditNoteLine)
                {


                    if (item.SubProductId == 10000 && item.CreditValue > 0)
                    {


                        partida.JournalEntryLines.Add(new JournalEntryLine
                        {
                            AccountId = (long)tax.CuentaContablePorCobrarId,
                            AccountName = tax.CuentaContablePorCobrarNombre,
                            CostCenterId = centrocosto.CostCenterId,
                            CostCenterName = centrocosto.CostCenterName,
                            Debit = 0,
                            Credit = item.CreditValue,
                            CreatedDate = DateTime.Now,
                            CreatedUser = User.Identity.Name,
                            ModifiedUser = User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            PartyTypeId = 1,
                            PartyTypeName = "Cliente",
                            PartyName = creditnote.CustomerName,
                            PartyId = creditnote.CustomerId,



                        });

                        partida.JournalEntryLines.Add(new JournalEntryLine
                        {
                            AccountId = (long)tax.CuentaContableIngresosId,
                            AccountName = tax.CuentaContableIngresosNombre,
                            CostCenterId = centrocosto.CostCenterId,
                            CostCenterName = centrocosto.CostCenterName,
                            Debit = item.CreditValue,
                            Credit = 0,
                            CreatedDate = DateTime.Now,
                            CreatedUser = User.Identity.Name,
                            ModifiedUser = User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            PartyTypeId = 1,
                            PartyTypeName = "Cliente",
                            PartyName = creditnote.CustomerName,
                            PartyId = creditnote.CustomerId,



                        });
                        continue;

                    }

                    ProductRelation relation = new ProductRelation();
                    relation = _context.ProductRelation.Where(x =>
                    x.ProductId == creditnote.ProductId
                    && x.SubProductId == item.SubProductId
                    )
                        .FirstOrDefault();


                    partida.JournalEntryLines.Add(new JournalEntryLine
                    {
                        AccountId = (int)relation.CuentaContableIngresosId,
                        AccountName = relation.CuentaContableIngresosNombre,
                        CostCenterId = centrocosto.CostCenterId,
                        CostCenterName = centrocosto.CostCenterName,
                        Debit = item.CreditValue,
                        Credit = 0,
                        CreatedDate = DateTime.Now,
                        CreatedUser = User.Identity.Name,
                        ModifiedUser = User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        PartyTypeId = 1,
                        PartyTypeName = "Cliente",
                        PartyName = creditnote.CustomerName,
                        PartyId = creditnote.CustomerId,



                    });

                    partida.JournalEntryLines.Add(new JournalEntryLine
                    {
                        AccountId = (int)relation.CuentaContableIdPorCobrar,
                        AccountName = relation.CuentaContablePorCobrarNombre,
                        CostCenterId = centrocosto.CostCenterId,
                        CostCenterName = centrocosto.CostCenterName,
                        Debit = 0,
                        Credit = item.CreditValue,
                        CreatedDate = DateTime.Now,
                        CreatedUser = User.Identity.Name,
                        ModifiedUser = User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        PartyTypeId = 1,
                        PartyTypeName = "Cliente",
                        PartyName = creditnote.CustomerName,
                        PartyId = creditnote.CustomerId,



                    });


                    
                }



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
                        _CreditNoteq.Total = _CreditNoteq.CreditNoteLine.Sum(x => x.CreditValue);
                        _CreditNoteq.TotalLetras = let.ToCustomCardinal((_CreditNoteq.Total)).ToUpper();
                        _CreditNoteq.UsuarioCreacion= User.Identity.Name;
                        _CreditNoteq.UsuarioModificacion = User.Identity.Name;
                        _CreditNoteq.CreditNoteDate = DateTime.Now;
                        _CreditNoteq.Estado = "Revisión";
                        _CreditNoteq.NumeroDEI = "BORRADOR";


                        //CreditNote factura = await _context.CreditNote.Where(q => q.CreditNoteId == _CreditNote.CreditNoteId).FirstOrDefaultAsync();

                        Invoice invoice = await _context.Invoice
                            .Where(q => q.InvoiceId == _CreditNote.InvoiceId)
                            .FirstOrDefaultAsync();
                        if (invoice != null) {
                            _CreditNoteq.ProductName = invoice.ProductName;
                            _CreditNoteq.ProductId = invoice.ProductId;

                        }
                        

                        _context.CreditNote.Add(_CreditNoteq);

                        foreach (var item in _CreditNote.CreditNoteLine)
                        {
                            item.CreditNoteId = _CreditNote.CreditNoteId;
                            _context.CreditNoteLine.Add(item);
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