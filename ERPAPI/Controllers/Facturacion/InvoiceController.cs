using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Contexts;
using ERPAPI.Helpers;
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
        /// <param name="InvoiceId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{InvoiceId}")]
        public async Task<IActionResult> GenerarFactura(Int64 InvoiceId)
        {
            Invoice factura = new Invoice();
            try
            {
                factura = await _context.Invoice.Where(q => q.InvoiceId == InvoiceId).FirstOrDefaultAsync();


                NumeracionSAR numeracionSAR = new NumeracionSAR();
                numeracionSAR = numeracionSAR.ObtenerNumeracionSarValida( 1,_context);

                factura.NumeroDEI = numeracionSAR.GetCorrelativo();
                factura.Rango = numeracionSAR.getRango();
                factura.CAI = numeracionSAR._cai;
                factura.NoInicio = numeracionSAR.NoInicio.ToString();
                factura.NoFin = numeracionSAR.NoFin.ToString();
                factura.FechaLimiteEmision = numeracionSAR.FechaLimite;
                factura.UsuarioModificacion = User.Identity.Name.ToUpper();
                factura.FechaModificacion = DateTime.Now;



                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: {ex.ToString()}");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(factura));
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

                        _Invoiceq = _Invoice;
                        _Invoiceq.UsuarioCreacion= User.Identity.Name;
                        _Invoiceq.FechaCreacion = DateTime.Now;
                        _Invoiceq.UsuarioModificacion = User.Identity.Name;
                        _Invoiceq.FechaModificacion= DateTime.Now;
                        _Invoiceq.TotalGravado = _Invoiceq.InvoiceLine.Sum(s =>s.TaxAmount);
                        _Invoiceq.Amount = _Invoiceq.InvoiceLine.Sum(s => s.Amount);
                        _Invoiceq.Discount = _Invoiceq.InvoiceLine.Sum(s => s.DiscountAmount);
                        _Invoiceq.Total = _Invoiceq.InvoiceLine.Sum(s => s.Total);
                        _Invoiceq.SubTotal = _Invoiceq.InvoiceLine.Sum(s => s.Amount);
                        _Invoiceq.NumeroDEI = "PROFORMA";
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
                                item.SubProductName = item.SubProduct.SubProductName;

                            }
                            item.UnitOfMeasure = null;
                            item.SubProduct = null;
                            //item.
                        }


                        _context.Invoice.Add(_Invoiceq);


                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();
                 

                        await _context.SaveChangesAsync();


                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        throw ex;
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
        public async Task<ActionResult<Invoice>> InsertConAlarma([FromBody]Invoice _Invoice)
        {
            Invoice _Invoiceq = new Invoice();
            ElementoConfiguracion _elemento = new ElementoConfiguracion();
            bool GenerateAlert = false;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        _Invoiceq = _Invoice;

                        Invoice _invoice = await _context.Invoice.Where(q => q.BranchId == _Invoice.BranchId)
                                             .Where(q => q.IdPuntoEmision == _Invoice.IdPuntoEmision)
                                             .FirstOrDefaultAsync();
                        if (_invoice != null)
                        {
                            _Invoiceq.NumeroDEI = _context.Invoice.Where(q => q.BranchId == _Invoice.BranchId)
                                                  .Where(q => q.IdPuntoEmision == _Invoice.IdPuntoEmision).Max(q => q.NumeroDEI);
                        }

                        _Invoiceq.NumeroDEI += 1;


                        //  Int64 puntoemision = _context.Users.Where(q=>q.Email==_Invoiceq.UsuarioCreacion).Select(q=>q.)

                        Int64 IdCai = await _context.NumeracionSAR
                                                 .Where(q => q.BranchId == _Invoiceq.BranchId)
                                                 .Where(q => q.IdPuntoEmision == _Invoiceq.IdPuntoEmision)
                                                 .Where(q => q.Estado == "Activo").Select(q => q.IdCAI).FirstOrDefaultAsync();


                        if (IdCai == 0)
                        {
                            return BadRequest("No existe un CAI activo para el punto de emisión");
                        }

                        _Invoiceq.Sucursal = await _context.Branch.Where(q => q.BranchId == _Invoice.BranchId).Select(q => q.BranchCode).FirstOrDefaultAsync();
                        //  _Invoiceq.Caja = await _context.PuntoEmision.Where(q=>q.IdPuntoEmision== _Invoice.IdPuntoEmision).Select(q => q.PuntoEmisionCod).FirstOrDefaultAsync();
                        _Invoiceq.CAI = await _context.CAI.Where(q => q.IdCAI == IdCai).Select(q => q._cai).FirstOrDefaultAsync();

                        Numalet let;
                        let = new Numalet();
                        let.SeparadorDecimalSalida = "Lempiras";
                        let.MascaraSalidaDecimal = "00/100 ";
                        let.ApocoparUnoParteEntera = true;
                        _Invoiceq.TotalLetras = let.ToCustomCardinal((_Invoiceq.Total)).ToUpper();

                        _context.Invoice.Add(_Invoiceq);

                        _elemento = await _context.ElementoConfiguracion.Where(q => q.Id == 76).FirstOrDefaultAsync();
                        if (_elemento != null)
                        {
                            if (_Invoiceq.CurrencyId == 1)
                            {
                                if (_Invoiceq.Total >Convert.ToDecimal(_elemento.Valordecimal))
                                {
                                    GenerateAlert = true;
                                }
                            }
                            else
                            {
                                ExchangeRate rate = new ExchangeRate();
                                rate = await _context.ExchangeRate.Where(q => q.DayofRate == _Invoiceq.InvoiceDate && q.CurrencyId == _Invoiceq.CurrencyId).FirstOrDefaultAsync();
                                if (rate != null)
                                {
                                    if ((rate.ExchangeRateValue * _Invoiceq.Total) >(decimal) _elemento.Valordecimal)
                                    {
                                        GenerateAlert = true;
                                    }
                                }
                            }
                        }

                        if (GenerateAlert)
                        {
                            //se agrega la alerta
                            Alert _alert = new Alert();
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
                        }
                        //await _context.SaveChangesAsync();
                        foreach (var item in _Invoice.InvoiceLine)
                        {
                            item.InvoiceId = _Invoiceq.InvoiceId;
                            _context.InvoiceLine.Add(item);
                        }

                        await _context.SaveChangesAsync();

                        JournalEntryConfiguration _journalentryconfiguration = await (_context.JournalEntryConfiguration
                                                                       .Where(q => q.TransactionId == 1)
                                                                       .Where(q => q.BranchId == _Invoiceq.BranchId)
                                                                       .Where(q => q.EstadoName == "Activo")
                                                                       .Include(q => q.JournalEntryConfigurationLine)
                                                                       ).FirstOrDefaultAsync();

                        BitacoraWrite _writejec = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Invoice.CustomerId,
                            DocType = "JournalEntryConfiguration",
                            ClaseInicial =
                             Newtonsoft.Json.JsonConvert.SerializeObject(_journalentryconfiguration, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_journalentryconfiguration, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "InsertInvoice",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Invoice.UsuarioCreacion,
                            UsuarioModificacion = _Invoice.UsuarioModificacion,
                            UsuarioEjecucion = _Invoice.UsuarioModificacion,

                        });

                        // await _context.SaveChangesAsync();

                        decimal sumacreditos = 0, sumadebitos = 0;
                        if (_journalentryconfiguration != null)
                        {
                            //Crear el asiento contable configurado
                            //.............................///////
                            JournalEntry _je = new JournalEntry
                            {
                                Date = _Invoiceq.InvoiceDate,
                                Memo = "Factura de ventas",
                                DatePosted = _Invoiceq.InvoiceDate,
                                ModifiedDate = DateTime.Now,
                                CreatedDate = DateTime.Now,
                                ModifiedUser = _Invoiceq.UsuarioModificacion,
                                CreatedUser = _Invoiceq.UsuarioCreacion,
                                DocumentId = _Invoiceq.InvoiceId,
                                TypeOfAdjustmentId = 65,
                                VoucherType = 1,

                            };



                            foreach (var item in _journalentryconfiguration.JournalEntryConfigurationLine)
                            {

                                InvoiceLine _iline = new InvoiceLine();
                                _iline = _Invoiceq.InvoiceLine.Where(q => q.SubProductId == item.SubProductId).FirstOrDefault();
                                if (_iline != null || item.SubProductName.ToUpper().Contains(("Impuesto").ToUpper()))
                                {
                                    if (!item.AccountName.ToUpper().Contains(("Impuestos sobre ventas").ToUpper())
                                           && !item.AccountName.ToUpper().Contains(("Sobre Servicios Diversos").ToUpper()))
                                    {

                                        _iline.AccountId = Convert.ToInt32(item.AccountId);
                                        _iline.AccountName = item.AccountName;
                                        _context.Entry(_iline).CurrentValues.SetValues((_iline));

                                        _je.JournalEntryLines.Add(new JournalEntryLine
                                        {
                                            AccountId = Convert.ToInt32(item.AccountId),
                                            AccountName = item.AccountName,
                                            Description = item.AccountName,
                                            Credit = item.DebitCredit == "Credito" ? _iline.SubTotal : 0,
                                            Debit = item.DebitCredit == "Debito" ? _iline.SubTotal : 0,
                                            CreatedDate = DateTime.Now,
                                            ModifiedDate = DateTime.Now,
                                            CreatedUser = _Invoiceq.UsuarioCreacion,
                                            ModifiedUser = _Invoiceq.UsuarioModificacion,
                                            Memo = "",
                                        });

                                        sumacreditos += item.DebitCredit == "Credito" ? _iline.SubTotal : 0;
                                        sumadebitos += item.DebitCredit == "Debito" ? _iline.SubTotal : 0;
                                    }
                                    else
                                    {
                                        _je.JournalEntryLines.Add(new JournalEntryLine
                                        {
                                            AccountId = Convert.ToInt32(item.AccountId),
                                            AccountName = item.AccountName,
                                            Description = item.AccountName,
                                            Credit = item.DebitCredit == "Credito" ? _Invoiceq.Tax + _Invoiceq.Tax18 : 0,
                                            Debit = item.DebitCredit == "Debito" ? _Invoiceq.Tax + _Invoiceq.Tax18 : 0,
                                            CreatedDate = DateTime.Now,
                                            ModifiedDate = DateTime.Now,
                                            CreatedUser = _Invoiceq.UsuarioCreacion,
                                            ModifiedUser = _Invoiceq.UsuarioModificacion,
                                            Memo = "",
                                        });

                                        sumacreditos += item.DebitCredit == "Credito" ? _Invoiceq.Tax + _Invoiceq.Tax18 : 0;
                                        sumadebitos += item.DebitCredit == "Debito" ? _Invoiceq.Tax + _Invoiceq.Tax18 : 0;
                                    }
                                }

                                // _context.JournalEntryLine.Add(_je);

                            }


                            if (sumacreditos != sumadebitos)
                            {
                                transaction.Rollback();
                                _logger.LogError($"Ocurrio un error: No coinciden debitos :{sumadebitos} y creditos{sumacreditos}");
                                return BadRequest($"Ocurrio un error: No coinciden debitos :{sumadebitos} y creditos{sumacreditos}");
                            }

                            _je.TotalCredit = sumacreditos;
                            _je.TotalDebit = sumadebitos;
                            _context.JournalEntry.Add(_je);

                            await _context.SaveChangesAsync();
                        }

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Invoice.CustomerId,
                            DocType = "Invoice",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Invoice, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_Invoice, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Invoice.UsuarioCreacion,
                            UsuarioModificacion = _Invoice.UsuarioModificacion,
                            UsuarioEjecucion = _Invoice.UsuarioModificacion,

                        });



                        await _context.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        throw ex;
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