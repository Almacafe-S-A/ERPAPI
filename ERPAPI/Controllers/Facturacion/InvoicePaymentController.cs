using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoreLinq;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/InvoicePayments")]
    [ApiController]
    public class InvoicePaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public InvoicePaymentsController(ILogger<InvoicePaymentsController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }


        /// <summary>
        /// Obtiene el Listado de InvoicePaymentses 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInvoicePayments()
        {
            List<InvoicePayments> Items = new List<InvoicePayments>();
            try
            {
                var user = _context.Users.Where(w => w.UserName == User.Identity.Name.ToString());
                int count = user.Count();
                List<UserBranch> branchlist = await _context.UserBranch.Where(w => w.UserId == user.FirstOrDefault().Id).ToListAsync();
                if (branchlist.Count > 0)
                {
                    Items = await _context.InvoicePayments
                        .Include(b => b.Branch)
                        .Where(p => branchlist.Any(b => p.BranchId == b.BranchId)).OrderByDescending(b => b.Id).ToListAsync();
                }
                else
                {
                    Items = await _context.InvoicePayments.OrderByDescending(b => b.Id).ToListAsync();
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
        /// Obtiene los Datos de la InvoicePayments por medio del Id enviado.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetInvoicePaymentsById(Int64 Id)
        {
            InvoicePayments Items = new InvoicePayments();
            try
            {
                Items = await _context.InvoicePayments.Where(q => q.Id == Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Obtiene los Datos de la InvoicePayments por medio del Id enviado.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> GetDetalletInvoiceById([FromBody]int[] InvoiceIds )
        {
            List<InvoicePaymentsLine> Items = new List<InvoicePaymentsLine>();

            try
            {
                List<InvoiceLine> detallefactura =  _context.InvoiceLine
                    .Include(i => i.Invoice)
                    .Where(q => InvoiceIds.Any(a=> a == q.InvoiceId) && q.Saldo > 0).ToList();

                Items = (from d in detallefactura
                        select new InvoicePaymentsLine {
                            SubProductName= d.SubProductName,
                            SubProductId= d.SubProductId,   
                            MontoAdeudaPrevio = d.Saldo,
                            MontoRestante = 0,
                            MontoPagado= 0,
                            ValorOriginal = d.Total,
                            InvoivceId = d.InvoiceId,
                            NoDocumento= d.Invoice.NumeroDEI,                                                 
                        }).ToList();

                foreach (var facturaid in InvoiceIds)
                {
                    Invoice invoice = await _context.Invoice.Where(q => q.InvoiceId == facturaid).FirstOrDefaultAsync();

                    if (invoice.SaldoImpuesto > 0)
                    {
                        Items.Add(new InvoicePaymentsLine
                        {
                            SubProductName = "Impuesto",
                            SubProductId = null,
                            MontoAdeudaPrevio = invoice.SaldoImpuesto,
                            MontoRestante = invoice.SaldoImpuesto,
                            MontoPagado = 0,
                            ValorOriginal = invoice.SaldoImpuesto,
                            InvoivceId = invoice.InvoiceId,
                            NoDocumento = invoice.NumeroDEI,
                        });
                    }
                }


            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: {ex.ToString()}");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Obtiene los Datos de la InvoicePayments por medio del Id enviado.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetDetalletInvoicePaymentById(Int64 Id)
        {
            List<InvoicePaymentsLine>  Items = new List<InvoicePaymentsLine>();
            try
            {
                Items = _context.InvoicePaymentsLine.Where(q => q.InvoicePaymentId == Id).ToList();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: {ex.ToString()}");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene los Datos de la InvoicePayments por medio del Id enviado.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Id}/{estado}")]
        public async Task<IActionResult> ChangeInvoicePaymentsStatus(Int64 Id, int estado)
        {
            InvoicePayments Items = new InvoicePayments();
            try
            {
                Items = await _context
                    .InvoicePayments
                    .Where(q => q.Id == Id).
                    FirstOrDefaultAsync();

                switch (estado)
                {
                    case 1:
                        Items.Estado = "Revisado";
                        break;
                    case 2:
                        Items.Estado = "Aprobado";
                        break;
                    case 3:
                        Items.Estado = "Rechazado";
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
        /// Obtiene los Datos de la InvoicePayments por medio del Id enviado.
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CustomerId}")]
        public async Task<IActionResult> GetPagosByCustomer(int CustomerId)
        {
            List<InvoicePayments> Items = new List<InvoicePayments>();
            try
            {
                Items = await _context.InvoicePayments
                    .Where(q => q.CustomerId == CustomerId
                    //&& q.NumeroDEI != "PROFORMA"
                    ).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: {ex.ToString()}");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }



        /// <summary>
        /// Inserta una nueva InvoicePayments
        /// </summary>
        /// <param name="_InvoicePayments"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<InvoicePayments>> Insert([FromBody]InvoicePayments _InvoicePayments)
        {
            InvoicePayments _InvoicePaymentsq = new InvoicePayments();
            
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _InvoicePaymentsq = _InvoicePayments;

                        _InvoicePaymentsq = _InvoicePayments;
                        _InvoicePaymentsq.UsuarioCreacion= User.Identity.Name;
                        _InvoicePaymentsq.FechaCreacion = DateTime.Now;                    
                        _InvoicePaymentsq.Estado = "Aprobado";
                        _InvoicePaymentsq.EstadoId = 1;
                        Numalet let;
                        let = new Numalet();
                        let.SeparadorDecimalSalida = "Lempiras";
                        let.MascaraSalidaDecimal = "00/100 ";
                        let.ApocoparUnoParteEntera = true;
                        _InvoicePaymentsq.CantidadenLetras = let.ToCustomCardinal((_InvoicePaymentsq.MontoPagado)).ToUpper();




                        _InvoicePaymentsq.JournalId = null;
                        _InvoicePaymentsq.NoDocumentos = String.Join(", ", _InvoicePaymentsq.InvoicePaymentsLines.DistinctBy(d => d.InvoivceId).Select(s => s.NoDocumento));

                        foreach (var item in _InvoicePaymentsq.InvoicePaymentsLines)
                        {
                            if (item.SubProduct != null)
                            {
                                item.SubProductName = item.SubProduct.SubProductName;
                                item.SubProductId = item.SubProduct.SubproductId;
                                item.SubProduct = null;
                                item.InvoivceId = null;
                            }
                        }


                        _context.InvoicePayments.Add(_InvoicePaymentsq);


                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();                   
                 
                        await _context.SaveChangesAsync();


                        _context.CustomerAcccountStatus.Add(new CustomerAcccountStatus
                        {
                            Credito = _InvoicePaymentsq.MontoPagado,
                            Fecha = DateTime.Now,
                            CustomerName = _InvoicePaymentsq.CustomerName,
                            CustomerId = _InvoicePaymentsq.CustomerId,
                            Debito = 0,
                            Sinopsis = $"Recibo de Pago #{_InvoicePaymentsq.Id} por Facturas # {_InvoicePaymentsq.NoDocumentos} ",
                            InvoiceId = null,
                            NoDocumento = _InvoicePaymentsq.Id.ToString(),
                            DocumentoId = _InvoicePaymentsq.Id,
                            TipoDocumentoId = null,
                            TipoDocumento = "Recibo de Pago",
                            
                        });
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        await _context.SaveChangesAsync();

                        var resppuesta = GeneraAsiento(_InvoicePaymentsq);
                        JournalEntry asiento = resppuesta.Result.Value as JournalEntry;

                        _InvoicePaymentsq.JournalId = asiento.JournalEntryId;


                        foreach (var item in _InvoicePaymentsq.InvoicePaymentsLines)
                        {
                            if (item.SubProductId == null)
                            {
                                Invoice invoice = _context.Invoice.Where(q => q.NumeroDEI == item.NoDocumento ).FirstOrDefault();
                                if (invoice != null)  invoice.SaldoImpuesto = invoice.SaldoImpuesto - item.MontoPagado;
                                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();
                                await _context.SaveChangesAsync();
                                continue;

                            }
                            InvoiceLine invoiceLine = _context.InvoiceLine.Where(q => q.Invoice.NumeroDEI == item.NoDocumento && item.SubProductId == q.SubProductId ).FirstOrDefault();
                            invoiceLine.Saldo = invoiceLine.Saldo - item.MontoPagado;
                            invoiceLine.Invoice.Saldo = invoiceLine.Invoice.Saldo - item.MontoPagado;
                            new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();
                            await _context.SaveChangesAsync();
                        }

                       
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        return BadRequest(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_InvoicePaymentsq));
        }


        /// <summary>
        /// Anula la invoicepayment por medio del Id enviado.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet("[action]/{ID}")]
        public async Task<IActionResult> AnularRecibo(Int64 ID)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                InvoicePayments invoicepayment = new InvoicePayments();
                try
                {
                    Periodo periodo = new Periodo();
                    periodo = periodo.PeriodoActivo(_context);

                    invoicepayment = await _context.InvoicePayments
                        .Include(i => i.InvoicePaymentsLines)
                        .Where(q => q.Id == ID)
                        .FirstOrDefaultAsync();

                    Customer customer = _context.Customer
                        .Where(q => q.CustomerId == invoicepayment.CustomerId)
                        .FirstOrDefault();


                    JournalEntry asientoFactura = _context.JournalEntry
                        .Include(j => j.JournalEntryLines)
                        .Where(q => q.JournalEntryId == invoicepayment.JournalId).FirstOrDefault();
                    /////revesiones de saldos
                    List<InvoiceLine> facturasrever = _context.InvoiceLine
                        .Include(i => i.Invoice)
                        .Where(q => invoicepayment.InvoicePaymentsLines.Any(a => a.InvoivceId == q.InvoiceId))
                        .ToList();

                    foreach (var item in invoicepayment.InvoicePaymentsLines)
                    {
                        InvoiceLine linea = facturasrever.Where(q => q.InvoiceId == item.InvoivceId && q.SubProductId == item.SubProductId).FirstOrDefault();
                        if (linea != null)
                        {
                            linea.Saldo += item.MontoPagado;
                        }
                        else
                        {
                            Invoice factura = _context.Invoice.Where(q => q.InvoiceId == item.InvoivceId).FirstOrDefault();
                            factura.SaldoImpuesto += item.MontoPagado;
                        }
                    }
                    ////////
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
                        Memo = $"Recibo #{invoicepayment.Id} anulado a Cliente {invoicepayment.CustomerName}",
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
                        PartyName = invoicepayment.CustomerName,
                        PartyId = invoicepayment.CustomerId,




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



                    invoicepayment.Estado = "Anulado";

                    invoicepayment.FechaModificacion = DateTime.Now;

                    new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                    await _context.SaveChangesAsync();

                    _context.CancelledDocuments.Add(new CancelledDocuments
                    {
                        FechaCreacion = DateTime.Now,
                        IdDocumento = invoicepayment.Id,
                        IdTipoDocumento = 1,
                        TipoDocumento = "Recibo de Pago",
                        JournalEntryId = asientoreversado.JournalEntryId,
                        UsuarioCreacion = User.Identity.Name,
                        UsuarioModificacion = User.Identity.Name
                    });


                    CustomerAcccountStatus accountstatus = _context.CustomerAcccountStatus.Where(q => q.DocumentoId == ID && q.TipoDocumentoId == null).FirstOrDefault();

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


                return await Task.Run(() => Ok(invoicepayment));
            }

        }


        /// <summary>
        /// Genera el asiento de la pago
        /// </summary>
        /// <param name="pago"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ActionResult<JournalEntry>> GeneraAsiento(InvoicePayments pago) {
            JournalEntry partida = new JournalEntry();
            try
            {
                Periodo periodo = new Periodo();
                periodo = periodo.PeriodoActivo(_context);
                CostCenter centrocosto = _context.CostCenter.Where(x => x.BranchId == pago.BranchId).FirstOrDefault();


                pago.accountManagement = await  _context.AccountManagement
                    .Include(i => i.Accounting)
                    .Where(q => q.AccountManagementId == pago.CuentaBancariaId).FirstOrDefaultAsync();




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
                    Memo = $"Pago de Factura(s) No. {String.Join(", ", pago.InvoicePaymentsLines.DistinctBy(d => d.InvoivceId).Select(s => s.NoDocumento))} con Recibo No. {pago.Id}   ",
                    Periodo = periodo.Anio.ToString(),
                    Posted = false,
                    TotalCredit = 0,
                    TotalDebit = 0,
                    ModifiedDate = DateTime.Now,
                    ModifiedUser = User.Identity.Name,
                    TypeJournalName = "Pagos de Clientes",
                    VoucherType = 12,
                    PartyTypeId = 1,
                    PartyTypeName = "Cliente",
                    PartyName = pago.CustomerName,
                    PartyId = pago.CustomerId,



                };

                partida.JournalEntryLines.Add(new JournalEntryLine
                {
                    AccountId = pago.accountManagement.AccountId,
                    AccountName = $"{pago.accountManagement.Accounting.AccountCode} - {pago.accountManagement.Accounting.AccountName}",
                    CostCenterId = centrocosto.CostCenterId,
                    CostCenterName = centrocosto.CostCenterName,
                    Debit = pago.MontoPagado,
                    Credit = 0,
                    CreatedDate = DateTime.Now,
                    CreatedUser = User.Identity.Name,
                    ModifiedUser = User.Identity.Name,
                    ModifiedDate = DateTime.Now,
                    PartyTypeId = 1,
                    PartyTypeName = "Cliente",
                    PartyName = pago.CustomerName,
                    PartyId = pago.CustomerId,



                });

                foreach (var item in pago.InvoicePaymentsLines)
                {
                    if (item.MontoPagado<= 0 )
                    {
                        continue;
                    }

                    Invoice invoice = _context.Invoice.Where(q => q.InvoiceId == item.InvoivceId).FirstOrDefault();
                    if (item.SubProductId == null && item.MontoPagado > 0 )
                    {
                        Tax tax = new Tax();
                        tax = _context.Tax.Where(x => x.TaxId == 1).FirstOrDefault();

                        partida.JournalEntryLines.Add(new JournalEntryLine
                        {
                            AccountId = (long)tax.CuentaContablePorCobrarId,
                            AccountName = tax.CuentaContablePorCobrarNombre,
                            CostCenterId = centrocosto.CostCenterId,
                            CostCenterName = centrocosto.CostCenterName,
                            Debit = 0,
                            Credit = item.MontoPagado,
                            CreatedDate = DateTime.Now,
                            CreatedUser = User.Identity.Name,
                            ModifiedUser = User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            PartyTypeId = 1,
                            PartyTypeName = "Cliente",
                            PartyName = pago.CustomerName,
                            PartyId = pago.CustomerId,



                        });
                        continue;

                    }
                    ProductRelation relation = new ProductRelation();
                    relation = _context.ProductRelation.Where(x =>
                    x.ProductId == invoice.ProductId
                    && x.SubProductId == item.SubProductId
                    )
                        .FirstOrDefault();


                    partida.JournalEntryLines.Add(new JournalEntryLine
                    {
                        AccountId = (int)relation.CuentaContableIngresosId,
                        AccountName = relation.CuentaContablePorCobrarNombre,
                        CostCenterId = centrocosto.CostCenterId,
                        CostCenterName = centrocosto.CostCenterName,
                        Debit = 0,
                        Credit = item.MontoPagado,
                        CreatedDate = DateTime.Now,
                        CreatedUser = User.Identity.Name,
                        ModifiedUser = User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        PartyTypeId = 1,
                        PartyTypeName = "Cliente",
                        PartyName = pago.CustomerName,
                        PartyId = pago.CustomerId,



                    });
                }

                    partida.TotalCredit= partida.JournalEntryLines.Sum(s => s.Credit);
                partida.TotalDebit= partida.JournalEntryLines.Sum(s => s.Debit);

                partida.JournalEntryLines = partida.JournalEntryLines.OrderBy(o => o.Credit).ThenBy(t => t.AccountId).ToList();


                _context.JournalEntry.Add(partida);
            
            
            }
            catch (Exception)
            {
                
                throw new Exception("Falta la configuracion contable en los subservicios utilizados");
            }
            




            //new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();
            
            // await _context.SaveChangesAsync();

            
            return partida;
        }

        /// <summary>
        /// Actualiza la InvoicePayments
        /// </summary>
        /// <param name="_InvoicePayments"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<InvoicePayments>> Update([FromBody]InvoicePayments _InvoicePayments)
        {
            InvoicePayments _InvoicePaymentsq = _InvoicePayments;
            try
            {
                _InvoicePaymentsq = await (from c in _context.InvoicePayments
                                 .Where(q => q.Id == _InvoicePayments.Id)
                                   select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_InvoicePaymentsq).CurrentValues.SetValues((_InvoicePayments));

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                //_context.InvoicePayments.Update(_InvoicePaymentsq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_InvoicePaymentsq));
        }

        /// <summary>
        /// Elimina una InvoicePayments       
        /// </summary>
        /// <param name="_InvoicePayments"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]InvoicePayments _InvoicePayments)
        {
            InvoicePayments _InvoicePaymentsq = new InvoicePayments();
            try
            {
                _InvoicePaymentsq = _context.InvoicePayments
                .Where(x => x.Id == (Int64)_InvoicePayments.Id)
                .FirstOrDefault();

                _context.InvoicePayments.Remove(_InvoicePaymentsq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_InvoicePaymentsq));

        }







    }
}