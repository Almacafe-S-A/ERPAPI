using System;
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
                    _VendorInvoiceq.PurchaseOrderId = null;
                    //_VendorInvoiceq.Sucursal = await _context.Branch.Where(q => q.BranchId == _VendorInvoiceq.BranchId).Select(q => q.BranchCode).FirstOrDefaultAsync();
                    Numalet let;
                    let = new Numalet();
                    let.SeparadorDecimalSalida = "Lempiras";
                    let.MascaraSalidaDecimal = "00/100 ";
                    let.ApocoparUnoParteEntera = true;
                    _VendorInvoiceq.TotalLetras = let.ToCustomCardinal((_VendorInvoiceq.Total)).ToUpper();

                    _context.VendorInvoice.Add(_VendorInvoiceq);
                    //await _context.SaveChangesAsync();

                    JournalEntry _je = new JournalEntry
                    {
                        Date = _VendorInvoiceq.VendorInvoiceDate,
                        Memo = "Factura de Compra a Proveedores",
                        DatePosted = _VendorInvoiceq.VendorInvoiceDate,
                        ModifiedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedUser = _VendorInvoiceq.UsuarioModificacion,
                        CreatedUser = _VendorInvoiceq.UsuarioCreacion,
                        PartyId = Convert.ToInt32(_VendorInvoiceq.VendorId),
                        PartyTypeName = _VendorInvoiceq.VendorName,
                        TotalDebit = _VendorInvoiceq.Total,
                        TotalCredit = _VendorInvoiceq.Total,                        
                        PartyTypeId = 3,
                        PartyName = "Proveedor",
                        TypeJournalName = "Factura de Compras",
                        VoucherType = 2,
                        EstadoId = 5,
                        EstadoName = "Enviada a Aprobacion",
                        TypeOfAdjustmentId = 65,
                        TypeOfAdjustmentName = "Asiento diario"

                    };

                    Accounting account = await _context.Accounting.Where(acc => acc.AccountId == _VendorInvoiceq.AccountId).FirstOrDefaultAsync();
                    _je.JournalEntryLines.Add(new JournalEntryLine
                    {
                        AccountId = Convert.ToInt32(_VendorInvoiceq.AccountId),
                        //Description = _VendorInvoiceq.Account.AccountName,
                        AccountName = account.AccountCode,
                        Description = account.AccountName,
                        Credit = _VendorInvoiceq.Total,
                        Debit = 0,
                        //CostCenterId = Convert.ToInt64(_VendorInvoiceq.CostCenterId),
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        CreatedUser = _VendorInvoiceq.UsuarioCreacion,
                        ModifiedUser = _VendorInvoiceq.UsuarioModificacion,
                        Memo = "",
                    });
                    foreach (var item in _VendorInvoiceq.VendorInvoiceLine)
                    {
                        account = await _context.Accounting.Where(acc => acc.AccountId == item.AccountId).FirstOrDefaultAsync();
                        item.VendorInvoiceId = _VendorInvoiceq.VendorInvoiceId;
                        _context.VendorInvoiceLine.Add(item);
                        _je.JournalEntryLines.Add(new JournalEntryLine
                        {
                            AccountId = Convert.ToInt32(item.AccountId),
                            AccountName = account.AccountCode,
                            Description = account.AccountName,
                            Credit = 0,
                            Debit = item.Total,
                            CostCenterId = Convert.ToInt64(item.CostCenterId),                            
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                            CreatedUser = _VendorInvoiceq.UsuarioCreacion,
                            ModifiedUser = _VendorInvoiceq.UsuarioModificacion,
                            Memo = "",
                        });
                    }

                    JournalEntryConfiguration jec = _context.JournalEntryConfiguration.Where(w => w.TransactionId == 2).FirstOrDefault();

                    if (jec != null)
                    {
                        JournalEntryConfigurationLine jeclines = _context.JournalEntryConfigurationLine.Where(w => w.JournalEntryConfigurationId == jec.JournalEntryConfigurationId).FirstOrDefault();
                        if (jeclines != null)
                        {                            
                            _je.JournalEntryLines.Add(new JournalEntryLine
                            {
                                AccountId = Convert.ToInt32(jeclines.AccountId),
                                //AccountName = jeclines.AccountCode,
                                Description = jeclines.AccountName,
                                Credit = jeclines.DebitCredit == "Credito" ? _VendorInvoiceq.Tax : 0,
                                Debit = jeclines.DebitCredit == "Debito" ? _VendorInvoiceq.Tax : 0,
                                CostCenterId = Convert.ToInt64(jeclines.CostCenterId),
                                CreatedDate = DateTime.Now,
                                ModifiedDate = DateTime.Now,
                                CreatedUser = _VendorInvoiceq.UsuarioCreacion,
                                ModifiedUser = _VendorInvoiceq.UsuarioModificacion,
                                Memo = "",
                            });
                        }
                    }

                    
                    _context.JournalEntry.Add(_je);

                    await _context.SaveChangesAsync();

                    BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                    {
                        IdOperacion = 4, ///////Falta definir los Id de las Operaciones
                        DocType = "VendorInvoice",
                        ClaseInicial =
                        Newtonsoft.Json.JsonConvert.SerializeObject(_VendorInvoiceq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                        ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_VendorInvoiceq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                        Accion = "Insert",
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = _VendorInvoiceq.UsuarioCreacion,
                        UsuarioModificacion = _VendorInvoiceq.UsuarioModificacion,
                        UsuarioEjecucion = _VendorInvoiceq.UsuarioModificacion,

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
                _VendorInvoiceq = await (from c in _context.VendorInvoice
                                 .Where(q => q.VendorInvoiceId == _VendorInvoice.VendorInvoiceId)
                                         select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_VendorInvoiceq).CurrentValues.SetValues((_VendorInvoice));

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                //_context.VendorInvoice.Update(_VendorInvoiceq);
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
