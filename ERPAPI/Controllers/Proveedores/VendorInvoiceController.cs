﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ERPAPI.Contexts;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class VendorInvoiceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public VendorInvoiceController(ILogger<VendorInvoiceController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de VendorInvoice paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetVendorInvoicePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<VendorInvoice> Items = new List<VendorInvoice>();
            try
            {
                var query = _context.VendorInvoice.AsQueryable();
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
        /// Obtiene el Listado de VendorInvoicees 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetVendorInvoice()
        {
            List<VendorInvoice> Items = new List<VendorInvoice>();
            try
            {
                var user = _context.Users.Where(w => w.UserName == User.Identity.Name.ToString());
                //string correo = User.Identity.Name.ToString();
                int count = user.Count();
                List<UserBranch> branchlist = await _context.UserBranch.Where(w => w.UserId == user.FirstOrDefault().Id).ToListAsync();
                if (branchlist.Count > 0)
                {
                    Items = await _context.VendorInvoice.Where(p => branchlist.Any(b => p.BranchId == b.BranchId)).OrderByDescending(b => b.VendorInvoiceId).ToListAsync();
                }
                else
                {
                    Items = await _context.VendorInvoice.OrderByDescending(b => b.VendorInvoiceId).ToListAsync();
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
        /// Obtiene el Listado de VendorInvoicees PENDIENTES DE RETECION
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetVendorInvoicePendienteRetencion()
        {
            List<VendorInvoice> Items = new List<VendorInvoice>();
            try
            {
                var user = _context.Users.Where(w => w.UserName == User.Identity.Name.ToString());
                //string correo = User.Identity.Name.ToString();
                int count = user.Count();
                List<UserBranch> branchlist = await _context.UserBranch.Where(w => w.UserId == user.FirstOrDefault().Id).ToListAsync();
                if (branchlist.Count > 0)
                {
                    Items = await _context.VendorInvoice.Where(p => branchlist.Any(b => p.BranchId == b.BranchId) 
                    && p.RetecionPendiente && p.AplicaRetencion )
                        .OrderByDescending(b => b.VendorInvoiceId).ToListAsync();
                }
                else
                {
                    Items = await _context.VendorInvoice.OrderByDescending(b => b.VendorInvoiceId).ToListAsync();
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: {ex.ToString()}");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene los Datos de la VendorInvoice por medio del Id enviado.
        /// </summary>
        /// <param name="VendorInvoiceId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{VendorInvoiceId}")]
        public async Task<IActionResult> GetVendorInvoiceById(Int64 VendorInvoiceId)
        {
            VendorInvoice Items = new VendorInvoice();
            try
            {
                Items = await _context.VendorInvoice.Where(q => q.VendorInvoiceId == VendorInvoiceId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Anula una factura de proveedores
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{VendorInvoiceId}")]
        public async Task<ActionResult<JournalEntry>> Anular(int vendorInvoiceId)
        {
            try
            {
                Periodo periodo = new Periodo();
                periodo = periodo.PeriodoActivo(_context);

                VendorInvoice vendorInvoice = new VendorInvoice();
                vendorInvoice = await _context.VendorInvoice
                    .Where(q => q.VendorInvoiceId == vendorInvoiceId).FirstOrDefaultAsync();
                if (vendorInvoice == null)
                {
                    return NotFound();
                }

                vendorInvoice.IdEstado = 7;
                vendorInvoice.Estado = "Anulado";
                vendorInvoice.UsuarioModificacion = User.Identity.Name;
                vendorInvoice.FechaModificacion = DateTime.Now;


                JournalEntry _je = new JournalEntry();
                _je = _context.JournalEntry.Where(q => q.JournalEntryId == vendorInvoice.JournalEntryId ).FirstOrDefault();

                if (_je == null) {
                    return BadRequest("No se encontro el asiento contable para esta factura de proveedores");
                }

                _je.EstadoId = 7;
                _je.EstadoName = "Rechazado";

                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                

                await _context.SaveChangesAsync();


                return await Task.Run(() => Ok(_je));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
                throw;
            }




        }






        /// <summary>
        /// Aprueba una factura de proveedores
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{VendorInvoiceId}")]
        public async Task<ActionResult<JournalEntry>> Aprobar(int vendorInvoiceId) {
            try
            {
                Periodo periodo = new Periodo();
                periodo = periodo.PeriodoActivo(_context);

                VendorInvoice vendorInvoice = new VendorInvoice();
                vendorInvoice = await _context.VendorInvoice
                    .Where(q => q.VendorInvoiceId == vendorInvoiceId).FirstOrDefaultAsync();
                if (vendorInvoice == null)
                {
                    return NotFound();
                }

                vendorInvoice.IdEstado = 6;
                vendorInvoice.Estado = "Aprobado";


                JournalEntry _je = new JournalEntry
                {
                    Date = vendorInvoice.VendorInvoiceDate,
                    Memo = $"{vendorInvoice.VendorInvoiceName} del proveedor {vendorInvoice.VendorName}, Factura #{vendorInvoice.NumeroDEI}",
                    DatePosted = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    ModifiedUser = User.Identity.Name,
                    CreatedUser = User.Identity.Name,
                    PartyId = Convert.ToInt32(vendorInvoice.VendorId),
                    PartyTypeName = vendorInvoice.VendorName,
                    TotalDebit = vendorInvoice.Total,
                    TotalCredit = vendorInvoice.Total,
                    PartyTypeId = 3,
                    PartyName = "Proveedor",
                    TypeJournalName = "Factura de Compras",
                    VoucherType = 2,
                    EstadoId = 6,
                    EstadoName = "Aprobado",
                    TypeOfAdjustmentId = 65,
                    TypeOfAdjustmentName = "Asiento diario",
                    Periodo = periodo.Anio.ToString(),
                    PeriodoId = periodo.Id,
                    Posted = false,

                };



                Accounting account = new Accounting();

                account = await _context.Accounting.Where(acc => acc.AccountId == vendorInvoice.AccountIdGasto).FirstOrDefaultAsync();
                _je.JournalEntryLines.Add(new JournalEntryLine
                {
                    AccountId = Convert.ToInt32(vendorInvoice.AccountIdGasto),
                    AccountName = $"{account.AccountCode} - {account.AccountName} ",
                    Description = account.AccountName,
                    Credit = 0,
                    Debit = vendorInvoice.TotalExento + vendorInvoice.TotalGravado,
                    CostCenterId = Convert.ToInt64(vendorInvoice.CostCenterId),
                    CostCenterName = vendorInvoice.CostCenterName,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    CreatedUser = _je.CreatedUser,
                    ModifiedUser = _je.ModifiedUser,
                    Memo = "",
                    PartyId = (int)vendorInvoice.VendorId,
                    PartyName = vendorInvoice.VendorName,
                    PartyTypeName = "Proveedor",
                    PartyTypeId = 2,
                });


                if (vendorInvoice.Tax > 0)
                {
                    Tax tax = await _context.Tax.Where(q => q.TaxId == 1).FirstAsync();
                    Accounting cuentaimpuesto = await _context.Accounting
                        .Where(acc => acc.AccountId == tax.CuentaImpuestoPagadoId).FirstOrDefaultAsync();
                    if (cuentaimpuesto == null)
                    {
                        return BadRequest("No se ha configurado una cuenta contable para el Impuesto");
                    }
                    _je.JournalEntryLines.Add(new JournalEntryLine
                    {
                        AccountId = Convert.ToInt32(vendorInvoice.AccountIdCredito),
                        AccountName = $"{cuentaimpuesto.AccountCode} - {cuentaimpuesto.AccountName} ",
                        Description = cuentaimpuesto.AccountName,
                        Credit = 0 ,
                        Debit = vendorInvoice.Tax,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        CreatedUser = _je.CreatedUser,
                        ModifiedUser = _je.ModifiedUser,
                        Memo = "",
                        PartyId = (int)vendorInvoice.VendorId,
                        PartyName = vendorInvoice.VendorName,
                        PartyTypeName = "Proveedor",
                        PartyTypeId = 2,
                    });

                }


                
                

                account = await _context.Accounting.Where(acc => acc.AccountId == vendorInvoice.AccountIdCredito).FirstOrDefaultAsync();
                _je.JournalEntryLines.Add(new JournalEntryLine
                {
                    AccountId = Convert.ToInt32(vendorInvoice.AccountIdCredito),
                    AccountName = $"{account.AccountCode} - {account.AccountName} ",
                    Description = account.AccountName,
                    Credit = vendorInvoice.Total,
                    Debit = 0,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    CreatedUser = _je.CreatedUser,
                    ModifiedUser = _je.ModifiedUser,
                    Memo = "",
                    PartyId = (int)vendorInvoice.VendorId,
                    PartyName = vendorInvoice.VendorName,
                    PartyTypeName = "Proveedor",
                    PartyTypeId = 2,
                }); ;

                _je.TotalCredit = _je.JournalEntryLines.Sum(s => s.Credit);
                _je.TotalDebit = _je.JournalEntryLines.Sum(s => s.Debit); ;
                

                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                _context.JournalEntry.Add(_je);

                

                await _context.SaveChangesAsync();

                vendorInvoice.JournalEntryId= _je.JournalEntryId;

                await _context.SaveChangesAsync();


                return await Task.Run(() => Ok(_je));

            }
            catch (Exception ex)
            {
                return BadRequest( ex.ToString());
                throw;
            }
            
            


        }


        /// <summary>
        /// Inserta una nueva VendorInvoice
        /// </summary>
        /// <param name="pVendorInvoice"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<VendorInvoice>> Insert([FromBody]VendorInvoice pVendorInvoice)
        {
            VendorInvoice _VendorInvoiceq = new VendorInvoice();
            _VendorInvoiceq = pVendorInvoice;
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {

                    if (_VendorInvoiceq.ExpirationDate< _VendorInvoiceq.VendorInvoiceDate)
                    {
                        return BadRequest("La fecha de vencimiento no puede ser menor a la de emisión");
                    }


                    _VendorInvoiceq.UsuarioCreacion = User.Identity.Name;
                    _VendorInvoiceq.FechaCreacion = DateTime.Now;
                    _VendorInvoiceq.IdEstado = 5;
                    _VendorInvoiceq.Estado = "Enviado a Aprobación";

                    _VendorInvoiceq.RetecionPendiente = _VendorInvoiceq.AplicaRetencion;
                    //obtener proveedor
                    Vendor vendor = _context.Vendor.Where(v => v.VendorId == _VendorInvoiceq.VendorId).FirstOrDefault();

                    if (vendor!=null)
                    {
                        _VendorInvoiceq.VendorRTN = vendor.RTN;
                        _VendorInvoiceq.VendorName = vendor.VendorName;
                    }    

                    _context.VendorInvoice.Add(_VendorInvoiceq);

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


            return await Task.Run(() => Ok(_VendorInvoiceq));
        }




        /// <summary>
        /// Actualiza la VendorInvoice
        /// </summary>
        /// <param name="_VendorInvoice"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<VendorInvoice>> Update([FromBody]VendorInvoice _VendorInvoice)
        {
            VendorInvoice _VendorInvoiceq = _VendorInvoice;
            try
            {
                if (_VendorInvoice.ExpirationDate < _VendorInvoice.VendorInvoiceDate)
                {
                    return BadRequest("La fecha de vencimiento no puede ser menor a la de emisión");
                }

                _VendorInvoiceq = await (from c in _context.VendorInvoice
                                 .Where(q => q.VendorInvoiceId == _VendorInvoice.VendorInvoiceId)
                                         select c
                                ).FirstOrDefaultAsync();

                _VendorInvoice.UsuarioCreacion = _VendorInvoiceq.UsuarioCreacion;
                _VendorInvoice.FechaCreacion = _VendorInvoiceq.FechaCreacion;

                _context.Entry(_VendorInvoiceq).CurrentValues.SetValues((_VendorInvoice));

                _VendorInvoice.UsuarioModificacion = User.Identity.Name;
                _VendorInvoice.FechaModificacion = DateTime.Now;

                _VendorInvoiceq.IdEstado = 5;
                _VendorInvoiceq.Estado = "Enviado a Aprobación";

                _VendorInvoiceq.RetecionPendiente = _VendorInvoiceq.AplicaRetencion;
                //obtener proveedor
                Vendor vendor = _context.Vendor.Where(v => v.VendorId == _VendorInvoiceq.VendorId).FirstOrDefault();

                if (vendor != null)
                {
                    _VendorInvoiceq.VendorRTN = vendor.RTN;
                    _VendorInvoiceq.VendorName = vendor.VendorName;
                }

                

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                //_context.VendorInvoice.Update(vendorInvoice);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_VendorInvoiceq));
        }

        /// <summary>
        /// Elimina una VendorInvoice       
        /// </summary>
        /// <param name="_VendorInvoice"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]VendorInvoice _VendorInvoice)
        {
            VendorInvoice _VendorInvoiceq = new VendorInvoice();
            try
            {
                _VendorInvoiceq = _context.VendorInvoice
                .Where(x => x.VendorInvoiceId == (Int64)_VendorInvoice.VendorInvoiceId)
                .FirstOrDefault();

                _context.VendorInvoice.Remove(_VendorInvoiceq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_VendorInvoiceq));

        }







    }
}
