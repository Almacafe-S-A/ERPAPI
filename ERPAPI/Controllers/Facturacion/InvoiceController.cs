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

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Invoice")]
    [ApiController]
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public InvoiceController(ILogger<InvoiceController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Invoice paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInvoicePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Invoice> Items = new List<Invoice>();
            try
            {
                var query = _context.Invoice.AsQueryable();
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
        /// Obtiene el Listado de Invoicees 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInvoice()
        {
            List<Invoice> Items = new List<Invoice>();
            try
            {
                var user = _context.Users.Where(w => w.UserName == User.Identity.Name.ToString());
                int count = user.Count();
                List<UserBranch> branchlist = await _context.UserBranch.Where(w => w.UserId == user.FirstOrDefault().Id).ToListAsync();
                if (branchlist.Count > 0)
                {
                    Items = await _context.Invoice.Where(p => branchlist.Any(b => p.BranchId == b.BranchId)).OrderByDescending(b => b.InvoiceId).ToListAsync();
                }
                else
                {
                    Items = await _context.Invoice.OrderByDescending(b => b.InvoiceId).ToListAsync();
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
        /// Obtiene los Datos de la Invoice por medio del Id enviado.
        /// </summary>
        /// <param name="InvoiceId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{InvoiceId}")]
        public async Task<IActionResult> GetInvoiceById(Int64 InvoiceId)
        {
            Invoice Items = new Invoice();
            try
            {
                Items = await _context.Invoice.Where(q => q.InvoiceId == InvoiceId).FirstOrDefaultAsync();
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
        [HttpGet("[action]/{InvoiceId}/{estado}")]
        public async Task<IActionResult> ChangeInvoiceStatus(Int64 InvoiceId, int estado)
        {
            Invoice Items = new Invoice();
            try
            {
                Items = await _context
                    .Invoice
                    .Where(q => q.InvoiceId == InvoiceId).
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
        /// Obtiene los Datos de la Invoice por medio del Id enviado.
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CustomerId}")]
        public async Task<IActionResult> GetFacturasByCustomer(int CustomerId)
        {
            List<Invoice> Items = new List<Invoice>();
            try
            {
                Items = await _context.Invoice
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
        /// Obtiene los Datos de la Invoice por medio del Id enviado.
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CustomerId}")]
        public async Task<IActionResult> GetFacturasPendientesPagoByCustomer(int CustomerId)
        {
            List<Invoice> Items = new List<Invoice>();
            try
            {
                Items = await _context.Invoice
                    .Where(q => q.CustomerId == CustomerId
                    && q.Saldo > 0 
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
        /// Obtiene los Datos de la Invoice por medio del Id enviado.
        /// </summary>
        /// <param name="InvoiceId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{InvoiceId}")]
        public async Task<IActionResult> GenerarFactura(Int64 InvoiceId)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                Invoice factura = new Invoice();
                try
                {
                    Periodo periodo = new Periodo();
                    periodo = periodo.PeriodoActivo(_context);

                    factura = await _context.Invoice
                        .Include(i => i.InvoiceLine)
                        .Where(q => q.InvoiceId == InvoiceId)
                        .FirstOrDefaultAsync();

                    Customer customer = _context.Customer
                        .Where(q => q.CustomerId == factura.CustomerId)
                        .FirstOrDefault();

                    if (customer != null && customer.Exonerado == true  )
                    {
                        if (factura.NoConstanciadeRegistro == String.Empty || factura.NoOCExenta == String.Empty)
                        {
                            throw new Exception("Para generar la factura debe ingresar el numero de Compra excenta y Constancia de registro");
                        }
                    }

                    NumeracionSAR numeracionSAR = new NumeracionSAR();
                    numeracionSAR = numeracionSAR.ObtenerNumeracionSarValida(1, _context);

                    factura.NumeroDEI = numeracionSAR.GetCorrelativo();
                    factura.Rango = numeracionSAR.getRango();
                    factura.CAI = numeracionSAR._cai;
                    factura.NoInicio = numeracionSAR.NoInicio.ToString();
                    factura.NoFin = numeracionSAR.NoFin.ToString();
                    factura.FechaLimiteEmision = numeracionSAR.FechaLimite;
                    factura.UsuarioModificacion = User.Identity.Name.ToUpper();
                    factura.FechaModificacion = DateTime.Now;
                    factura.ExpirationDate = DateTime.Now.AddDays(factura.DiasVencimiento);
                    factura.Estado = "Emitido";

                    _context.NumeracionSAR.Update(numeracionSAR);

                    //var alerta = await GeneraAlerta(factura);

                    JournalEntry asiento;

                    var resppuesta = GeneraAsientoFactura(factura).Result.Value;

                    asiento = resppuesta as JournalEntry;

                    factura.JournalEntryId = asiento.JournalEntryId;
                    factura.Saldo = factura.SubTotal;
                    factura.SaldoImpuesto = factura.Tax;
                    foreach (var item in factura.InvoiceLine)
                    {
                        item.Saldo = item.SubTotal;
                    }

                    new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                    await _context.SaveChangesAsync();

                    _context.CustomerAcccountStatus.Add(new CustomerAcccountStatus
                    {
                        Credito = 0,
                        Fecha = DateTime.Now,
                        CustomerName = factura.CustomerName,
                        Debito = factura.Total,
                        Sinopsis = factura.Sinopsis,
                        InvoiceId = factura.InvoiceId,
                        NoDocumento = factura.NumeroDEI,
                        CustomerId = factura.CustomerId,
                    });

                   
                    factura.FechaModificacion = DateTime.Now;

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


                return await Task.Run(() => Ok(factura));
            }
           
        }





        [HttpPost("[action]")]
        public async Task<IActionResult> GetInvoiceLineById([FromBody]Invoice _Invoice)
        {
            Invoice Items = new Invoice();
            try
            {
                    Items = await _context.Invoice.Include(q => q.InvoiceLine).Where(q => q.Sucursal==_Invoice.Sucursal && q.Caja==_Invoice.Caja && q.NumeroDEI==_Invoice.NumeroDEI).FirstOrDefaultAsync();
                
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Inserta una nueva Invoice
        /// </summary>
        /// <param name="_Invoice"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Invoice>> Insert([FromBody]Invoice _Invoice)
        {
            Invoice _Invoiceq = new Invoice();
            
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        if (_Invoice.Exonerado )
                        {
                            if (_Invoice.NoOCExenta == String.Empty || _Invoice.NoConstanciadeRegistro == String.Empty)
                            {
                                throw new Exception("Numero de OC y No contancia son requeridos para clientes exonerados");
                            }
                        }

                        _Invoiceq = _Invoice;
                        _Invoiceq.UsuarioCreacion= User.Identity.Name;
                        _Invoiceq.FechaCreacion = DateTime.Now;
                        _Invoiceq.UsuarioModificacion = User.Identity.Name;
                        _Invoiceq.FechaModificacion= DateTime.Now;                       
                        _Invoiceq.Estado = "Revisión";

                        _Invoiceq.NumeroDEI = "PROFORMA";

                        Numalet let;
                        let = new Numalet();
                        let.SeparadorDecimalSalida = "Lempiras";
                        let.MascaraSalidaDecimal = "00/100 ";
                        let.ApocoparUnoParteEntera = true;
                        _Invoiceq.TotalLetras = let.ToCustomCardinal((_Invoiceq.Total)).ToUpper();


                        foreach (var item in _Invoiceq.InvoiceLine)
                        {
                            if (item.UnitOfMeasure !=null)
                            {
                                item.UnitOfMeasureId = item.UnitOfMeasure.UnitOfMeasureId;
                                item.UnitOfMeasureName= item.UnitOfMeasure.UnitOfMeasureName;
                                
                            }
                            if (item.SubProduct != null)
                            {
                                item.SubProductId = item.SubProduct.SubproductId;
                                item.SubProductName = item.SubProduct.ProductName;

                            }
                            item.ProductId = _Invoiceq.ProductId;
                            item.ProductName= _Invoiceq.ProductName;
                            item.UnitOfMeasure = null;
                            item.SubProduct = null;
                            //item.

                            item.SubTotal = item.Amount - item.DiscountAmount;
                            item.Total = item.SubTotal + item.TaxAmount;
                        }


                        _Invoiceq.Tax = _Invoiceq.InvoiceLine.Sum(s => s.TaxAmount);
                        _Invoiceq.Amount = _Invoiceq.InvoiceLine.Sum(s => s.Amount);
                        _Invoiceq.Discount = _Invoiceq.InvoiceLine.Sum(s => s.DiscountAmount);                        
                        _Invoiceq.SubTotal = _Invoiceq.InvoiceLine.Sum(s => s.SubTotal);
                        _Invoiceq.TotalGravado = _Invoiceq.InvoiceLine.Where(q => q.TaxAmount> 0).Sum(s => s.SubTotal);
                        _Invoiceq.Total = _Invoiceq.InvoiceLine.Sum(s => s.Total);


                        _context.Invoice.Add(_Invoiceq);


                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                       
                 

                        await _context.SaveChangesAsync();



                        //Servicios Utilizados
                        List<SubServicesWareHouse> subServicesWareHouses = new List<SubServicesWareHouse>();

                        subServicesWareHouses = await _context.SubServicesWareHouse
                            .Where(q => q.InvoiceLineId == null
                            && q.CustomerId == _Invoiceq.CustomerId
                            && q.ServiceId == _Invoiceq.ProductId
                            && q.InvoiceId == null

                            ).ToListAsync();

                        foreach (var item in subServicesWareHouses)
                        {
                            item.InvoiceId = _Invoiceq.InvoiceId;
                            item.Estado = "Facturado";
                        }

                        CustomerArea customerArea = await _context.CustomerArea
                           .Where(q =>
                            q.CustomerId == (long)_Invoiceq.CustomerId
                           && q.InvoiceId == null
                           && q.ProductId == _Invoiceq.ProductId
                           && q.TypeId == 4

                           )
                           .LastOrDefaultAsync();
                        if (customerArea !=null)
                        {

                        customerArea.InvoiceId= _Invoiceq.InvoiceId;
                            customerArea.Cerrado = true;
                            
                        }


                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();
                        await _context.SaveChangesAsync();


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

            return await Task.Run(() => Ok(_Invoiceq));
        }

        /// <summary>
        /// Inserta una nueva Invoice con alerta
        /// </summary>
        /// <param name="_Invoice"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Alert>> GeneraAlerta(Invoice _invoice)
        {
            ElementoConfiguracion _elemento = new ElementoConfiguracion();

            Alert _alert = new Alert();
            try
            {
                try
                {
                    _elemento = await _context.ElementoConfiguracion.Where(q => q.Id == 76).FirstOrDefaultAsync();
                    if (_elemento != null)
                    {
                        return BadRequest("No se encontro configuracion para generar alerta en el elemeento configuracion  76");
                    }

                    if (_invoice.Total < Convert.ToDecimal(_elemento.Valordecimal))
                    {
                        return Ok("No se genero Alerta");
                    }

                    //se agrega la alerta

                    _alert.DocumentId = _invoice.InvoiceId;
                    _alert.DocumentName = "FACTURA";
                    _alert.AlertName = "Sancionados";
                    _alert.Code = "PERSON004";
                    _alert.ActionTakenId = 0;
                    _alert.ActionTakenName = "";
                    _alert.IdEstado = 0;
                    _alert.SujetaARos = false;
                    _alert.FalsoPositivo = false;
                    _alert.CloseDate = DateTime.MinValue;
                    _alert.DescriptionAlert = _invoice.InvoiceId.ToString() + " / " + _invoice.CustomerName + " / " + _invoice.Total.ToString();
                    _alert.FechaCreacion = DateTime.Now;
                    _alert.FechaModificacion = DateTime.Now;
                    _alert.UsuarioCreacion = _invoice.UsuarioCreacion;
                    _alert.UsuarioModificacion = _invoice.UsuarioModificacion;
                    _alert.PersonName = _invoice.CustomerName;
                    _alert.Description = $"Factura {_invoice.InvoiceName}";
                    _alert.DescriptionAlert = $"Factura {_invoice.InvoiceName}";
                    _alert.Type = "170";
                    _alert.DescriptionAlert = _context.ElementoConfiguracion.Where(p => p.Id == 170).FirstOrDefault().Nombre;
                    _context.Alert.Add(_alert);

                    //se agrega la informacion a la tabla InvoiceTransReport
                    InvoiceTransReport _report = new InvoiceTransReport();
                    _report.Amount = _invoice.Total;
                    _report.CustomerId = _invoice.CustomerId;
                    _report.InvoiceDate = _invoice.InvoiceDate;
                    _report.FechaCreacion = DateTime.Now;
                    _report.FechaModificacion = DateTime.Now;
                    _report.UsuarioCreacion = _invoice.UsuarioCreacion;
                    _report.UsuarioModificacion = _invoice.UsuarioModificacion;
                    _context.InvoiceTransReport.Add(_report);

                    new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();
                    await _context.SaveChangesAsync();

                   // transaction.Commit();
                }
                catch (Exception ex)
                {
                    //transaction.Rollback();
                    _logger.LogError($"Ocurrio un error: {ex.ToString()}");
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_alert));
        }

        /// <summary>
        /// Genera el asiento de la factura
        /// </summary>
        /// <param name="factura"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ActionResult<JournalEntry>> GeneraAsientoFactura(Invoice factura) {
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
                    Memo = $"Factura #{factura.NumeroDEI} a Cliente {factura.CustomerName} por concepto de {factura.ProductName}",
                    Periodo = periodo.Anio.ToString(),
                    Posted = false,
                    TotalCredit = 0,
                    TotalDebit = 0,
                    ModifiedDate = DateTime.Now,
                    ModifiedUser = User.Identity.Name,




                };




                

                partida.JournalEntryLines.Add(new JournalEntryLine
                {
                    AccountId = (long)tax.CuentaContablePorCobrarId,
                    AccountName = tax.CuentaContablePorCobrarNombre,
                    CostCenterId = 1,
                    CostCenterName = "San Pedro Sula",
                    Debit = factura.Tax,
                    Credit = 0,
                    CreatedDate = DateTime.Now,
                    CreatedUser = User.Identity.Name,
                    ModifiedUser = User.Identity.Name,
                    ModifiedDate = DateTime.Now,



                });


                foreach (var item in factura.InvoiceLine)
                {
                    ProductRelation relation = new ProductRelation();
                    relation = _context.ProductRelation.Where(x =>
                    x.ProductId == factura.ProductId
                    && x.SubProductId == item.SubProductId
                    )
                        .FirstOrDefault();
                    partida.JournalEntryLines.Add(new JournalEntryLine
                    {
                        AccountId = (long)relation.CuentaContableIdPorCobrar,
                        AccountName = relation.CuentaContableIngresosNombre,
                        CostCenterId = 1,
                        CostCenterName = "San Pedro Sula",
                        Debit = item.SubTotal,
                        Credit = 0,
                        CreatedDate = DateTime.Now,
                        CreatedUser = User.Identity.Name,
                        ModifiedUser = User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                    });

                    partida.JournalEntryLines.Add(new JournalEntryLine
                    {
                        AccountId = (int)relation.CuentaContableIngresosId,
                        AccountName = relation.CuentaContablePorCobrarNombre,
                        CostCenterId = 1,
                        CostCenterName = "San Pedro Sula",
                        Debit = 0,
                        Credit = item.Total,
                        CreatedDate = DateTime.Now,
                        CreatedUser = User.Identity.Name,
                        ModifiedUser = User.Identity.Name,
                        ModifiedDate = DateTime.Now,



                    });
                }

                partida.TotalCredit= partida.JournalEntryLines.Sum(s => s.Credit);
                partida.TotalDebit= partida.JournalEntryLines.Sum(s => s.Debit);

                partida.JournalEntryLines = partida.JournalEntryLines.OrderBy(o => o.Credit).ThenBy(t => t.AccountId).ToList();


                _context.JournalEntry.Add(partida);
            }
            catch (Exception )
            {
                
                throw new Exception("Falta la configuracion contable en los subservicios utilizados");
            }
            

            
            return partida;
        }

        /// <summary>
        /// Actualiza la Invoice
        /// </summary>
        /// <param name="_Invoice"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Invoice>> Update([FromBody]Invoice _Invoice)
        {
            Invoice _Invoiceq = _Invoice;
            try
            {
                _Invoiceq = await (from c in _context.Invoice
                                 .Where(q => q.InvoiceId == _Invoice.InvoiceId)
                                   select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_Invoiceq).CurrentValues.SetValues((_Invoice));

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                //_context.Invoice.Update(_Invoiceq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Invoiceq));
        }

        /// <summary>
        /// Elimina una Invoice       
        /// </summary>
        /// <param name="_Invoice"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Invoice _Invoice)
        {
            Invoice _Invoiceq = new Invoice();
            try
            {
                _Invoiceq = _context.Invoice
                .Where(x => x.InvoiceId == (Int64)_Invoice.InvoiceId)
                .FirstOrDefault();

                _context.Invoice.Remove(_Invoiceq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Invoiceq));

        }







    }
}