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
    [Route("api/BankAccountTransfers")]
    [ApiController]
    public class BankAccountTransfersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public BankAccountTransfersController(ILogger<BankAccountTransfersController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de BankAccountTransfers
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBankAccountTransfersPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<BankAccountTransfers> Items = new List<BankAccountTransfers>();
            try
            {
                var query = _context.BankAccountTransfers.AsQueryable();
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
        /// Obtiene el Listado de BankAccountTransferses 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> Get()
        {
            List<BankAccountTransfers> Items = new List<BankAccountTransfers>();
            try
            {
                Items = await _context.BankAccountTransfers
                    .Include(i => i.Estados)
                    .Include(o => o.DestinationAccountManagement)
                    .Include(o => o.SourceAccountManagement)
                    .ToListAsync();
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
        /// Obtiene los Datos de la BankAccountTransfers por medio del Id enviado.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetBankAccountTransfersById(Int64 Id)
        {
            BankAccountTransfers Items = new BankAccountTransfers();
            try
            {
                Items = await _context.BankAccountTransfers
                    .Include(i => i.DestinationAccountManagement)
                    .Include(i => i.SourceAccountManagement)
                    .Where(q => q.Id == Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

          bool ValidarSaldo(BankAccountTransfers bankAccountTransfers, decimal cantidad) {
            decimal saldo =   _context.Accounting.Where(b => b.AccountId == bankAccountTransfers.SourceAccountManagementId).FirstOrDefault().AccountBalance;
            if (cantidad< saldo)
            {
                return false;
            }
            else
            {
                return true;
            }
        
        
        }




        /// <summary>
        /// Inserta una nueva BankAccountTransfers
        /// </summary>
        /// <param name="_BankAccountTransfers"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<BankAccountTransfers>> Insert([FromBody] BankAccountTransfers _BankAccountTransfers)
        {
            BankAccountTransfers _BankAccountTransfersq = new BankAccountTransfers();
            try
            {
                _BankAccountTransfersq = _BankAccountTransfers;
                _BankAccountTransfersq.UsuarioCreacion = User.Identity.Name;
                _BankAccountTransfersq.FechaCreacion = DateTime.Now;
                
                AccountManagement cuentaortigen = _context.AccountManagement
                    .Include(a => a.Accounting)
                    .Where(w => w.AccountManagementId == _BankAccountTransfersq.SourceAccountManagementId).FirstOrDefault();
                AccountManagement cuentadestino = _context.AccountManagement
                    .Include(a => a.Accounting)
                    .Where(w => w.AccountManagementId == _BankAccountTransfersq.DestinationAccountManagementId).FirstOrDefault();


                decimal cantidad = 0;

                ///////////Conversion a Moneda Local para registros Contables

                if (cuentaortigen.CurrencyId == 1)
                {
                    cantidad = _BankAccountTransfersq.SourceAmount;

                }
                else
                {
                    if (cuentadestino.CurrencyId == 1)
                    {
                        cantidad = _BankAccountTransfersq.DestinationAmount;

                    }
                    else
                    {
                        cantidad = _BankAccountTransfersq.SourceAmount * _BankAccountTransfersq.Rate;
                    }
                }

                if (!ValidarSaldo(_BankAccountTransfersq,cantidad))
                {
                    return BadRequest("La cuenta no tiene saldo suficiente ");
                }

                JournalEntry je = new JournalEntry {
                    Date = DateTime.Now,
                    DatePosted = _BankAccountTransfersq.TransactionDate,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    CreatedUser = User.Identity.Name,
                    ModifiedUser = User.Identity.Name,
                    Memo = _BankAccountTransfersq.Notes,
                    TotalCredit = cantidad,
                    TotalDebit = cantidad,
                    EstadoId = 5,
                    EstadoName = "Enviada a Aprobacion",
                    CurrencyId = 1,
                    TypeOfAdjustmentId = 65,
                    TypeOfAdjustmentName = "Asiento Diario",
                    TypeJournalName = "Transferencias",
                    VoucherType = 17

                    
                };

                je.JournalEntryLines.Add(new JournalEntryLine {
                    AccountId = Convert.ToInt32(cuentadestino.AccountId),
                    AccountName = cuentadestino.Accounting.AccountName,
                    Debit = cantidad,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    CreatedUser = User.Identity.Name,
                    ModifiedUser = User.Identity.Name,

                });

                je.JournalEntryLines.Add(new JournalEntryLine
                {
                    AccountId = Convert.ToInt32(cuentaortigen.AccountId),
                    AccountName = cuentaortigen.Accounting.AccountName,
                    Credit = cantidad,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    CreatedUser = User.Identity.Name,
                    ModifiedUser = User.Identity.Name,

                });

                _BankAccountTransfersq.JournalEntry = je;
                _context.BankAccountTransfers.Add(_BankAccountTransfersq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();


                await _context.SaveChangesAsync();

                BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                {
                    IdOperacion = _BankAccountTransfers.Id,
                    DocType = "BankTransfer",
                    ClaseInicial =
 Newtonsoft.Json.JsonConvert.SerializeObject(_BankAccountTransfersq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                    Accion = "Insertar",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = User.Identity.Name,
                    UsuarioModificacion = User.Identity.Name,
                    UsuarioEjecucion = User.Identity.Name,

                });
                BitacoraWrite _write2 = new BitacoraWrite(_context, new Bitacora
                {
                    IdOperacion = je.JournalEntryId,
                    DocType = "Asiento Contable",
                    ClaseInicial =
Newtonsoft.Json.JsonConvert.SerializeObject(je, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                    Accion = "Insertar",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = User.Identity.Name,
                    UsuarioModificacion = User.Identity.Name,
                    UsuarioEjecucion = User.Identity.Name,

                });
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_BankAccountTransfersq));
        }

        /// <summary>
        /// Metodo que Recibe el id de la transaccion y el valor booleano para aprobarla o rechazarla 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="aprobado"></param>
        /// <returns></returns>

        [HttpGet("[action]/{Id}/{aprobado}")]
        public async Task<ActionResult<BankAccountTransfers>> Aprobar(int Id, bool aprobado) {
            try
            {
                BankAccountTransfers transfers = _context.BankAccountTransfers
                    .Include(i => i.JournalEntry.JournalEntryLines)
                    //.Include(i => )
                    .Where(q => q.Id == Id).FirstOrDefault();
                int estado = 7;
                string estadoname = "Rechazado";
                if (aprobado)
                {
                    estado = 6;
                    estadoname = "Aprobado";
                }
                transfers.EstadoId = estado;
                transfers.UsuarioModificacion = User.Identity.Name;
                transfers.FechaModificacion = DateTime.Now;
                transfers.JournalEntry.EstadoId = estado;
                transfers.JournalEntry.EstadoName = estadoname;

                BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                {
                    IdOperacion = transfers.Id,
                    DocType = "BankTransfer",
                    ClaseInicial =
                    Newtonsoft.Json.JsonConvert.SerializeObject(transfers, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                    Accion = "Aprobar",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = User.Identity.Name,
                    UsuarioModificacion = User.Identity.Name,
                    UsuarioEjecucion = User.Identity.Name,

                });

                _context.SaveChanges();

                ////Actualiza el saldo de  las cuentas del catalogo contable 
                Funciones.ActualizarSaldoCuentas(_context, transfers.JournalEntry);
                return await Task.Run(() => Ok(transfers));
            }
            catch (Exception)
            {

                throw;
            }
        
        
        }


        /// <summary>
        /// Actualiza la BankAccountTransfers
        /// </summary>
        /// <param name="_BankAccountTransfers"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<BankAccountTransfers>> Update([FromBody] BankAccountTransfers _BankAccountTransfers)
        {            
            try
            {

                BankAccountTransfers BankAccountTransfersq = new BankAccountTransfers();
                BankAccountTransfersq = await _context.BankAccountTransfers
                    .Include(i => i.DestinationAccountManagement.Accounting)
                    .Include(i => i.SourceAccountManagement.Accounting)
                    .Include(i => i.JournalEntry)
                    .Include(i =>i.JournalEntry.JournalEntryLines).Where(q => q.Id == _BankAccountTransfers.Id).FirstOrDefaultAsync();


                AccountManagement cuentaortigen = _context.AccountManagement
                    .Include(a => a.Accounting)
                    .Where(w => w.AccountManagementId == BankAccountTransfersq.SourceAccountManagementId).FirstOrDefault();
                AccountManagement cuentadestino = _context.AccountManagement
                    .Include(a => a.Accounting)
                    .Where(w => w.AccountManagementId == BankAccountTransfersq.DestinationAccountManagementId).FirstOrDefault();
                decimal cantidad = 0;

                

                _context.Entry(BankAccountTransfersq).CurrentValues.SetValues((_BankAccountTransfers));

                ///////////Conversion a Moneda Local para registros Contables

                if (cuentaortigen.CurrencyId == 1)
                {
                    cantidad = BankAccountTransfersq.SourceAmount;

                }
                else
                {
                    if (cuentadestino.CurrencyId == 1)
                    {
                        cantidad = BankAccountTransfersq.DestinationAmount;

                    }
                    else
                    {
                        cantidad = BankAccountTransfersq.SourceAmount / BankAccountTransfersq.Rate;
                    }
                }

                _BankAccountTransfers = await _context.BankAccountTransfers
                    .Include(i => i.DestinationAccountManagement.Accounting)
                    .Include(i => i.SourceAccountManagement.Accounting)
                    .Include(i => i.JournalEntry)
                    .Include(i => i.JournalEntry.JournalEntryLines).Where(q => q.Id == _BankAccountTransfers.Id).FirstOrDefaultAsync();

                if (_BankAccountTransfers.EstadoId == 7)
                {
                    _BankAccountTransfers.EstadoId = 5;
                    BankAccountTransfersq.JournalEntry.EstadoId = 5;
                    BankAccountTransfersq.JournalEntry.EstadoName = "Enviado a Aprobacion";

                }

                BankAccountTransfersq.JournalEntry.JournalEntryLines[0].AccountId = Convert.ToInt32(_BankAccountTransfers.DestinationAccountManagement.AccountId);
                BankAccountTransfersq.JournalEntry.JournalEntryLines[0].AccountName = _BankAccountTransfers.DestinationAccountManagement.Accounting.AccountName;
                BankAccountTransfersq.JournalEntry.JournalEntryLines[0].Debit = cantidad;


                BankAccountTransfersq.JournalEntry.JournalEntryLines[1].AccountId = Convert.ToInt32(_BankAccountTransfers.SourceAccountManagement.AccountId);
                BankAccountTransfersq.JournalEntry.JournalEntryLines[1].AccountName = _BankAccountTransfers.DestinationAccountManagement.Accounting.AccountName;
                BankAccountTransfersq.JournalEntry.JournalEntryLines[1].Credit = cantidad;

                _BankAccountTransfers.JournalEntry.Memo = _BankAccountTransfers.Notes;
                _BankAccountTransfers.JournalEntry.TotalCredit = cantidad;
                _BankAccountTransfers.JournalEntry.TotalDebit = cantidad;

                //_context.BankAccountTransfers.Update(_BankAccountTransfersq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();

                BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                {
                    IdOperacion = _BankAccountTransfers.Id,
                    DocType = "BankTransfer",
                    ClaseInicial =
Newtonsoft.Json.JsonConvert.SerializeObject(BankAccountTransfersq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                    Accion = "Modificar",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = User.Identity.Name,
                    UsuarioModificacion = User.Identity.Name,
                    UsuarioEjecucion = User.Identity.Name,

                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_BankAccountTransfers));
        }

        /// <summary>
        /// Elimina una BankAccountTransfers       
        /// </summary>
        /// <param name="_BankAccountTransfers"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody] BankAccountTransfers _BankAccountTransfers)
        {
            BankAccountTransfers _BankAccountTransfersq = new BankAccountTransfers();
            try
            {
                _BankAccountTransfersq = _context.BankAccountTransfers
                .Where(x => x.Id == (Int64)_BankAccountTransfers.Id)
                .FirstOrDefault();

                _context.BankAccountTransfers.Remove(_BankAccountTransfersq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_BankAccountTransfersq));

        }







    }
}