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
                Items = await _context.Invoice.ToListAsync();
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

        [HttpGet("[action]/{InvoiceId}")]
        public async Task<IActionResult> GetInvoiceLineById(Int64 InvoiceId)
        {
            Invoice Items = new Invoice();
            try
            {
                Items = await _context.Invoice.Include(q=>q.InvoiceLine).Where(q => q.InvoiceId == InvoiceId).FirstOrDefaultAsync();
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

                        Invoice _invoice = await      _context.Invoice.Where(q => q.BranchId == _Invoice.BranchId)
                                             .Where(q => q.IdPuntoEmision == _Invoice.IdPuntoEmision)
                                             .FirstOrDefaultAsync();
                        if (_invoice != null)
                        {
                            _Invoiceq.NumeroDEI = _context.Invoice.Where(q => q.BranchId == _Invoice.BranchId)
                                                  .Where(q => q.IdPuntoEmision == _Invoice.IdPuntoEmision).Max(q => q.NumeroDEI);
                        }

                        _Invoiceq.NumeroDEI += 1;

                        
                      //  Int64 puntoemision = _context.Users.Where(q=>q.Email==_Invoiceq.UsuarioCreacion).Select(q=>q.)

                        Int64 IdCai =await  _context.NumeracionSAR
                                                 .Where(q=>q.BranchId==_Invoiceq.BranchId)
                                                 .Where(q=>q.IdPuntoEmision==_Invoiceq.IdPuntoEmision)                                           
                                                 .Where(q => q.Estado == "Activo").Select(q => q.IdCAI).FirstOrDefaultAsync();

                         
                        if(IdCai==0)
                        {
                            return BadRequest("No existe un CAI activo para el punto de emisión");
                        }

                        _Invoiceq.Sucursal =  await _context.Branch.Where(q => q.BranchId == _Invoice.BranchId).Select(q => q.BranchCode).FirstOrDefaultAsync();
                        //  _Invoiceq.Caja = await _context.PuntoEmision.Where(q=>q.IdPuntoEmision== _Invoice.IdPuntoEmision).Select(q => q.PuntoEmisionCod).FirstOrDefaultAsync();
                        _Invoiceq.CAI = await _context.CAI.Where(q => q.IdCAI == IdCai).Select(q => q._cai).FirstOrDefaultAsync();

                        Numalet let;
                        let = new Numalet();
                        let.SeparadorDecimalSalida = "Lempiras";
                        let.MascaraSalidaDecimal = "00/100 ";
                        let.ApocoparUnoParteEntera = true;
                        _Invoiceq.TotalLetras = let.ToCustomCardinal((_Invoiceq.Total)).ToUpper();

                        _context.Invoice.Add(_Invoiceq);
                        //await _context.SaveChangesAsync();
                        foreach (var item in _Invoice.InvoiceLine)
                        {
                            item.InvoiceId = _Invoiceq.InvoiceId;
                            _context.InvoiceLine.Add(item);
                        }                       

                        await _context.SaveChangesAsync();

                        JournalEntryConfiguration _journalentryconfiguration = await (_context.JournalEntryConfiguration
                                                                       .Where(q => q.TransactionId == 1)
                                                                       .Where(q=>q.BranchId==_Invoiceq.BranchId)
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

                        double sumacreditos=0, sumadebitos = 0;
                        if (_journalentryconfiguration!=null)
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

                                        sumacreditos +=  item.DebitCredit == "Credito" ? _iline.SubTotal : 0;
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

                                        sumacreditos +=  item.DebitCredit == "Credito" ? _Invoiceq.Tax + _Invoiceq.Tax18 : 0;
                                        sumadebitos += item.DebitCredit == "Debito" ? _Invoiceq.Tax + _Invoiceq.Tax18 : 0;
                                    }
                                }

                               // _context.JournalEntryLine.Add(_je);

                            }


                            if(sumacreditos!=sumadebitos)
                            {
                                transaction.Rollback();
                                _logger.LogError($"Ocurrio un error: No coinciden debitos :{sumadebitos} y creditos{sumacreditos}");
                                return BadRequest($"Ocurrio un error: No coinciden debitos :{sumadebitos} y creditos{sumacreditos}");
                            }

                            _je.TotalCredit = sumacreditos;
                            _je.TotalDebit = sumadebitos;
                            _context.JournalEntry.Add(_je);

                            await  _context.SaveChangesAsync();
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