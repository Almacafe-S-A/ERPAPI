using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Helpers;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/FixedAsset")]
    [ApiController]
    public class FixedAssetController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public FixedAssetController(ILogger<FixedAssetController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de FixedAsset paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetFixedAssetPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<FixedAsset> Items = new List<FixedAsset>();
            try
            {
                var query = _context.FixedAsset.AsQueryable();
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
        /// Obtiene el Listado de FixedAssetes 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetFixedAsset()
        {
            List<FixedAsset> Items = new List<FixedAsset>();
            try
            {
                var user = _context.Users.Where(w => w.UserName == User.Identity.Name.ToString());
                int count = user.Count();
                List<UserBranch> branchlist = await _context.UserBranch.Where(w => w.UserId == user.FirstOrDefault().Id).ToListAsync();
                if (branchlist.Count > 0)
                {
                    Items = await _context.FixedAsset.Where(p => branchlist.Any(b => p.BranchId == b.BranchId)).OrderByDescending(b => b.FixedAssetId).ToListAsync();
                }
                else
                {
                    Items = await _context.FixedAsset.OrderByDescending(b => b.FixedAssetId).ToListAsync();
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
        /// Obtiene los Datos de la FixedAsset por medio del Id enviado.
        /// </summary>
        /// <param name="FixedAssetId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{FixedAssetId}")]
        public async Task<IActionResult> GetFixedAssetById(Int64 FixedAssetId)
        {
            FixedAsset Items = new FixedAsset();
            try
            {
                Items = await _context.FixedAsset.Where(q => q.FixedAssetId == FixedAssetId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva FixedAsset
        /// </summary>
        /// <param name="_FixedAsset"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<FixedAsset>> Insert([FromBody]FixedAsset _FixedAsset)
        {
            FixedAsset _FixedAssetq = new FixedAsset();
            try
            {
                _FixedAssetq = _FixedAsset;
                _context.FixedAsset.Add(_FixedAssetq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_FixedAssetq));
        }

        /// <summary>
        /// Actualiza la FixedAsset
        /// </summary>
        /// <param name="_FixedAsset"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<FixedAsset>> Update([FromBody]FixedAsset _FixedAsset)
        {
            FixedAsset _FixedAssetq = _FixedAsset;
            try
            {
                _FixedAssetq = await (from c in _context.FixedAsset
                                 .Where(q => q.FixedAssetId == _FixedAsset.FixedAssetId)
                                      select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_FixedAssetq).CurrentValues.SetValues((_FixedAsset));

                //_context.FixedAsset.Update(_FixedAssetq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_FixedAssetq));
        }

        /// <summary>
        /// Da de baja un activo y genera un asiento contable de la accion     
        /// </summary>
        /// <param name="_FixedAsset"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]FixedAsset _FixedAsset)
        {
            FixedAsset _FixedAssetq = new FixedAsset();
            try
            {
                _FixedAssetq = _context.FixedAsset
                    .Include(q => q.FixedAssetGroup.DepreciationFixedAssetAccounting)
                    .Include(q => q.FixedAssetGroup.FixedAssetAccounting)
                    .Include(q => q.FixedAssetGroup.AccumulatedDepreciationAccounting)
                .Where(x => x.FixedAssetId == (Int64)_FixedAsset.FixedAssetId)
                .FirstOrDefault();

                if (_FixedAssetq.IdEstado == 105)
                {
                    return BadRequest("No se Puede dar de baja el activo");
                }
                if (_FixedAssetq.FixedAssetGroup.DepreciationFixedAssetAccounting == null)
                {
                    return BadRequest("no se encontro la cuenta de Depreciacion");
                }

                if (_FixedAssetq.FixedAssetGroup.AccumulatedDepreciationAccounting == null)
                {
                    return BadRequest("no se encontro la cuenta de Depreciacion Acumulada");
                }

                ////////Colocar valores en cero para el activo///////


                decimal valorlibros = _FixedAssetq.NetValue;
                decimal depreciacionacumulada = _FixedAssetq.AccumulatedDepreciation;
                decimal valorresidual = _FixedAssetq.ResidualValue;
                decimal valoractivo = _FixedAssetq.ActiveTotalCost;
                //decimal valorasiento = 0;
                //valorasiento = valordepreciado;

                _FixedAssetq.ResidualValue = 0;
                _FixedAssetq.AccumulatedDepreciation = 0;
                _FixedAssetq.NetValue = 0;
                 var valorasiento = valoractivo;

                

                JournalEntry _je = new JournalEntry
                {
                    Date = DateTime.Now,
                    //Memo = $"Cheque Numero {check.CheckNumber} ",
                    Memo = $"Se dio de baja el Activo {_FixedAssetq.FixedAssetName}",
                    DatePosted = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    ModifiedUser = User.Identity.Name,
                    CreatedUser = User.Identity.Name,
                    //PartyId = Convert.ToInt32(_VendorInvoiceq.VendorId),
                    //PartyName = check.PaytoOrderOf,
                    DocumentId = _FixedAssetq.FixedAssetId,
                    TotalDebit = valorasiento,
                    TotalCredit = valorasiento,
                    //PartyTypeId = 3,
                    //PartyName = "Proveedor",
                    TypeJournalName = "Activos",
                    VoucherType = 24,
                    EstadoId = 6,
                    EstadoName = "Aprobado",
                    TypeOfAdjustmentId = 65,
                    TypeOfAdjustmentName = "Asiento diario"

                };
                _je.JournalEntryLines = new List<JournalEntryLine>();


               
                ////////Lineas de Asiento por valor residual//////////////
                _je.JournalEntryLines.Add(new JournalEntryLine(){
                    ModifiedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    ModifiedUser = User.Identity.Name,
                    CreatedUser = User.Identity.Name,
                    AccountId = Convert.ToInt32(_FixedAssetq.FixedAssetGroup.AccumulatedDepreciationAccountingId),
                    AccountName = _FixedAssetq.FixedAssetGroup.AccumulatedDepreciationAccounting.AccountCode + "--" + _FixedAssetq.FixedAssetGroup.AccumulatedDepreciationAccounting.AccountName,
                    CostCenterId = _FixedAssetq.CenterCostId,
                    CostCenterName = _FixedAssetq.CenterCostName,
                    Debit = depreciacionacumulada
                }) ;
                _je.JournalEntryLines.Add(new JournalEntryLine(){
                    ModifiedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    ModifiedUser = User.Identity.Name,
                    CreatedUser = User.Identity.Name,
                    AccountId = Convert.ToInt32(_FixedAssetq.FixedAssetGroup.DepreciationAccountingId),
                    AccountName = _FixedAssetq.FixedAssetGroup.DepreciationFixedAssetAccounting.AccountCode + "--" +  _FixedAssetq.FixedAssetGroup.DepreciationFixedAssetAccounting.AccountName,
                    CostCenterId = _FixedAssetq.CenterCostId,
                    CostCenterName = _FixedAssetq.CenterCostName,
                    Debit = valorlibros
                });

                
                ////////Valor del Activo
                _je.JournalEntryLines.Add(new JournalEntryLine()
                {
                    ModifiedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    ModifiedUser = User.Identity.Name,
                    CreatedUser = User.Identity.Name,
                    AccountId = Convert.ToInt32(_FixedAssetq.FixedAssetGroup.FixedAssetAccountingId),
                    AccountName = _FixedAssetq.FixedAssetGroup.FixedAssetAccounting.AccountCode + "--" + _FixedAssetq.FixedAssetGroup.FixedAssetAccounting.AccountName,
                    CostCenterId = _FixedAssetq.CenterCostId,
                    CostCenterName = _FixedAssetq.CenterCostName,
                    Credit = valoractivo

                });


                ///////Actualiza el saldo de las cuentas ///////////
                Funciones.ActualizarSaldoCuentas(_context, _je);
                _context.JournalEntry.Add(_je);
                _FixedAssetq.IdEstado = 105;
                _FixedAssetq.Estado = "Dado de Baja";

                await _context.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_FixedAssetq));

        }







    }
}