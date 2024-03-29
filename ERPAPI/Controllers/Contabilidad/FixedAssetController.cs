﻿using System;
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

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();


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

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();


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
        /// Obtiene los Datos de la FixedAsset por medio del Id enviado.
        /// </summary>
        /// <param name="FixedAssetId"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> BajaActivo([FromBody] FixedAssetDTO _FixedAsset)
        {
            FixedAsset _FixedAssetq = new FixedAsset();
            try
            {
                _FixedAssetq = _context.FixedAsset
                    .Include(q => q.FixedAssetGroup.DepreciationFixedAssetAccounting)
                    .Include(q => q.FixedAssetGroup.FixedAssetAccounting)
                    .Include(q => q.FixedAssetGroup.AccumulatedDepreciationAccounting)
                .Where(x => x.FixedAssetId == _FixedAsset.FixedAssetId)
                .FirstOrDefault();

                if (_FixedAsset.FechaBaja == null)
                {
                    return BadRequest("No se recibio la fecha de baja del activo");
                }

                DateTime fechabaja = (DateTime)_FixedAsset.FechaBaja;

                

                bool seaplicodepreciacion = _FixedAssetq.AccumulatedDepreciation > 0;

                //DepreciationFixedAsset depreciationFixedAssets = _context.DepreciationFixedAsset
                //    .Where(q => q.FixedAssetId == _FixedAsset.FixedAssetId  && q.Year == _baja ).FirstOrDefault();

                Periodo periodo = _context.Periodo.Where(q => q.IdEstado == 105).FirstOrDefault();

                BitacoraCierreProcesos procesos = _context.BitacoraCierreProceso
                    .Where(q => q.BitacoraCierresContable.Anio == fechabaja.Year
                    && q.PasoCierre == 3
                    && q.BitacoraCierresContable.Mes >= fechabaja.Month)
                    .Include(i => i.BitacoraCierresContable).FirstOrDefault();


                if (procesos.Estatus == "FINALIZADO")
                {
                    return BadRequest("No se Puede dar de baja el activo en la fecha indicada, se ha ejecutado una depreciacion previamente en el mes de baja seleccionado");
                }


                if (_FixedAssetq.AssetDate >  _FixedAsset.FechaBaja)
                {
                   return BadRequest("No se Puede dar de baja el activo la fecha de baja es previa a la fecha de adquisicion");
                }

                if (_FixedAssetq.IdEstado == 105)
                {
                    return BadRequest("No se Puede dar de baja el activo");
                }


                if (_FixedAssetq.FixedAssetGroup.DepreciationFixedAssetAccounting == null && seaplicodepreciacion)
                {
                    return BadRequest("no se encontro la cuenta de Depreciacion");
                }

                if (_FixedAssetq.FixedAssetGroup.AccumulatedDepreciationAccounting == null && seaplicodepreciacion)
                {
                    return BadRequest("no se encontro la cuenta de Depreciacion Acumulada");
                }

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

                string motivoMensaje = _FixedAsset.MotivoId == 1 ? "Venta" : "Obsolecencia";

                JournalEntry _je = new JournalEntry
                {
                    Date = DateTime.Now,
                    Memo = $"Se dio de baja el Activo {_FixedAssetq.FixedAssetName} por motivo de {motivoMensaje}",
                    DatePosted = fechabaja,
                    ModifiedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    ModifiedUser = User.Identity.Name,
                    CreatedUser = User.Identity.Name,
                    DocumentId = _FixedAssetq.FixedAssetId,
                    TotalDebit = valorasiento,
                    TotalCredit = valorasiento,
                    TypeJournalName = "Activos",
                    VoucherType = 24,
                    EstadoId = 5,
                    EstadoName = "Enviado a Aprobación",
                    TypeOfAdjustmentId = 65,
                    TypeOfAdjustmentName = "Asiento diario"

                };
                _je.JournalEntryLines = new List<JournalEntryLine>();

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

                if (seaplicodepreciacion)
                {
                    ////////Lineas de Asiento por valor LIBROS//////////////
                    _je.JournalEntryLines.Add(new JournalEntryLine()
                    {
                        ModifiedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedUser = User.Identity.Name,
                        CreatedUser = User.Identity.Name,
                        AccountId = Convert.ToInt32(_FixedAssetq.FixedAssetGroup.AccumulatedDepreciationAccountingId),
                        AccountName = _FixedAssetq.FixedAssetGroup.AccumulatedDepreciationAccounting.AccountCode + "--" + _FixedAssetq.FixedAssetGroup.AccumulatedDepreciationAccounting.AccountName,
                        CostCenterId = _FixedAssetq.CenterCostId,
                        CostCenterName = _FixedAssetq.CenterCostName,
                        Debit = depreciacionacumulada
                    });
                }

                

                Accounting cuentaMotivo = new Accounting();
                string codigoCuentamotivo = "";

                if (_FixedAsset.MotivoId == 1) /// por venta 
                {
                    codigoCuentamotivo = "143010309";
                }
                else  /// por obsolecencia
                {
                    codigoCuentamotivo = "65304";
                }
                cuentaMotivo = _context.Accounting.Where(q => q.AccountCode==codigoCuentamotivo).FirstOrDefault();

                if (cuentaMotivo == null) return BadRequest("No se encontro la cuenta relacionoada al motivo de baja");

                _je.JournalEntryLines.Add(new JournalEntryLine()
                {
                    ModifiedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    ModifiedUser = User.Identity.Name,
                    CreatedUser = User.Identity.Name,
                    AccountId = (int)cuentaMotivo.AccountId,
                    AccountName =cuentaMotivo.AccountCode + "--" + cuentaMotivo.AccountName,
                    CostCenterId = _FixedAssetq.CenterCostId,
                    CostCenterName = _FixedAssetq.CenterCostName,
                    Debit = valorlibros
                });


                //Periodo periodo = _context.Periodo.Where(q => q.IdEstado == 105).FirstOrDefault();

                _je.PeriodoId = periodo?.Id;
                _je.Periodo = periodo.Anio.ToString();
                _je.TypeJournalName = "Voucher de Registros";
                _je.VoucherType = 9;




                ///////Actualiza el saldo de las cuentas ///////////
                //ContabilidadHandler.ActualizarSaldoCuentas(_context, _je);
                _context.JournalEntry.Add(_je);
                _FixedAssetq.IdEstado = 110;
                _FixedAssetq.Estado = "Dado de Baja";
                _FixedAssetq.FechaBaja = fechabaja;

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
                ContabilidadHandler.ActualizarSaldoCuentas(_context, _je);
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