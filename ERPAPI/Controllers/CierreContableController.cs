using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CierreContableController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger _logger;

        public CierreContableController(ILogger<CierreContableController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }


        /// <summary>
        /// Obtiene El ultimo Cierre Ejecutado
        /// </summary>
        /// GET: api/CierreContable
        [HttpGet("[action]")]
        public async Task<IActionResult> UltimoCierre() {
            try
            {
                BitacoraCierreContable cierre = new BitacoraCierreContable();
                cierre = await _context.BitacoraCierreContable.OrderByDescending(i => i.FechaCierre).FirstOrDefaultAsync();
                 return await Task.Run(() => Ok(cierre)); ;

            }
            catch (Exception ex)
            {

                return await Task.Run(() => BadRequest("Error:"+ex.Message));
            }
        
        
        
        
        }

        /// <summary>
        /// Realiza un Cierre Contable
        /// </summary>
        /// <returns></returns>    
        [HttpPost("[action]")]
        public async Task<IActionResult> EjecutarCierreContable([FromBody]BitacoraCierreContable pBitacoraCierre)
            {

             BitacoraCierreContable cierre = await _context.BitacoraCierreContable
                                    .Where(b => b.FechaCierre.Date == pBitacoraCierre.FechaCierre.Date).FirstOrDefaultAsync();
            ExchangeRate tasacambio = await _context.ExchangeRate
                            //.Where(b => b.DayofRate >= DateTime.Now.AddDays(-1)).FirstOrDefaultAsync();
                            .Where(b => b.DayofRate >= DateTime.Now.AddDays(-1)).FirstOrDefaultAsync();

            if (tasacambio == null)//Revisa la tasa de cambio actualizada
            {
                return await Task.Run(() => BadRequest("Debe de Agregar una tasa de cambio actualizada"));
            }

            if (cierre != null)               
            {
                //if (!CheckCierre(cierre))
                //{
                //    return await Task.Run(() =>Ok());
                //}
                //else
                //{
                //    return await Task.Run(() => BadRequest("Ya existe un Cierre Contable para esta Fecha"));
                //}
                return await Task.Run(() => BadRequest("Ya existe un Cierre Contable para esta Fecha"));
            }

                    ////Si no existe Ciere lo crea
                    cierre = new BitacoraCierreContable
                    {
                        FechaCierre = pBitacoraCierre.FechaCierre.Date,
                        FechaCreacion = DateTime.Now,
                        Estatus = "PENDIENTE",
                        EstatusId = 1,
                        UsuarioCreacion = User.Claims.FirstOrDefault().Value.ToString(),
                        UsuarioModificacion = User.Claims.FirstOrDefault().Value.ToString(),
                        FechaModificacion = DateTime.Now,


                    };
                    _context.BitacoraCierreContable.Add(cierre);

                    ///Carga los pasos al Cierre
                    ///
                    //Paso 1
                    BitacoraCierreProcesos proceso1 = new BitacoraCierreProcesos
                    {
                        IdBitacoraCierre = cierre.Id,
                        //IdProceso = 1,
                        Estatus = "PENDIENTE",
                        Proceso = "HISTORICOS",
                        PasoCierre = 1,
                        UsuarioCreacion = User.Claims.FirstOrDefault().Value.ToString(),
                        UsuarioModificacion = User.Claims.FirstOrDefault().Value.ToString(),
                        FechaModificacion = DateTime.Now,
                        FechaCierre = cierre.FechaCierre,
                        FechaCreacion = DateTime.Now,

                    };
                    //Paso2
                    BitacoraCierreProcesos proceso2 = new BitacoraCierreProcesos
                    {
                        IdBitacoraCierre = cierre.Id,
                        //IdProceso = 1,
                        Estatus = "PENDIENTE",
                        Proceso = "VALOR MAXIMO CERTIFICADO DE DEPOSITO",
                        PasoCierre = 2,
                        UsuarioCreacion = User.Claims.FirstOrDefault().Value.ToString(),
                        UsuarioModificacion = User.Claims.FirstOrDefault().Value.ToString(),
                        FechaModificacion = DateTime.Now,
                        FechaCierre = cierre.FechaCierre,
                        FechaCreacion = DateTime.Now,

                    };

                    //Paso3
                    BitacoraCierreProcesos proceso3 = new BitacoraCierreProcesos
                    {
                        IdBitacoraCierre = cierre.Id,
                        //IdProceso = 1,
                        Estatus = "PENDIENTE",
                        Proceso = "POLIZAS DE SEGURO VENCIDAS",
                        PasoCierre = 3,
                        UsuarioCreacion = User.Claims.FirstOrDefault().Value.ToString(),
                        UsuarioModificacion = User.Claims.FirstOrDefault().Value.ToString(),
                        FechaModificacion = DateTime.Now,
                        FechaCierre = cierre.FechaCierre,
                        FechaCreacion = DateTime.Now,

                    };

                    BitacoraCierreProcesos proceso5 = new BitacoraCierreProcesos
                    {
                        IdBitacoraCierre = cierre.Id,
                        //IdProceso = 1,
                        Estatus = "PENDIENTE",
                        Proceso = "GARANTIAS BANCARIAS VENCIDAS",
                        PasoCierre = 3,
                        UsuarioCreacion = User.Claims.FirstOrDefault().Value.ToString(),
                        UsuarioModificacion = User.Claims.FirstOrDefault().Value.ToString(),
                        FechaModificacion = DateTime.Now,
                        FechaCierre = cierre.FechaCierre,
                        FechaCreacion = DateTime.Now,

                    };

                    _context.BitacoraCierreProceso.Add(proceso1);
                    _context.BitacoraCierreProceso.Add(proceso2);
                    _context.BitacoraCierreProceso.Add(proceso3);
                    _context.BitacoraCierreProceso.Add(proceso5);
                    _context.SaveChanges();




                    if (cierre.FechaCierre.Day == DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)) ///////Se ejecuta solo si es fin de mes
                    {
                        //Paso4 Depreciacion de Activos 
                        BitacoraCierreProcesos proceso4 = new BitacoraCierreProcesos
                        {
                            IdBitacoraCierre = cierre.Id,
                            //IdProceso = 1,
                            Estatus = "PENDIENTE",
                            Proceso = "Depreciacion de Activos",
                            PasoCierre = 4,
                            UsuarioCreacion = User.Claims.FirstOrDefault().Value.ToString(),
                            UsuarioModificacion = User.Claims.FirstOrDefault().Value.ToString(),
                            FechaModificacion = DateTime.Now,
                            FechaCierre = cierre.FechaCierre,
                            FechaCreacion = DateTime.Now,

                        };
                        _context.BitacoraCierreProceso.Add(proceso4);

                        _context.SaveChanges();
                        await Paso4(proceso4, cierre);
                    }

            


            try
            {
                //////Ejecuta el Cierre en db
                //await _context.Database.ExecuteSqlCommandAsync("Cierres @p0", cierre.Id); ////Ejecuta SP Cierres
                //ValidarPasos(cierre);
                
                await Paso3(proceso3.IdProceso);
                await Paso2(proceso2.IdProceso);/// Valor Maximo de Certificados 
                await Paso5(proceso5.IdProceso);
                // POLIZAS VENCIDAS 
                await Paso1(cierre.Id, proceso1.IdProceso); ////HISTORICOS
                cierre.Estatus = "Finalizado";
                _context.Update(cierre);
                await _context.SaveChangesAsync();
                return await Task.Run(() => Ok());

            }
            catch (Exception ex)
            {
                return await Task.Run(() => BadRequest(ex.Message));
                throw;
            }

                   


            }


        
        private async Task Paso1(int idCierre,int idproceso)
        {
            //////////////
            ///
            BitacoraCierreProcesos proceso = await _context.BitacoraCierreProceso.Where(w => w.IdProceso == idproceso).FirstOrDefaultAsync();
            proceso.Estatus = "EJECUTANDO";
            _context.BitacoraCierreProceso.Update(proceso);
            await _context.SaveChangesAsync();

            try
            {
                ////////////////Cierre Catalogo
                List<CierresAccounting> cierreCatalogo = new List<CierresAccounting>();
                List<Accounting> catalogo =await  _context.Accounting.ToListAsync();

                foreach (var item in catalogo)
                {
                    CierresAccounting cuenta = new CierresAccounting
                    {
                        AccountId = item.AccountId,
                        AccountCode = item.AccountCode,
                        AccountName = item.AccountName,
                        BlockedInJournal = item.BlockedInJournal,
                        CompanyInfoId = item.CompanyInfoId,
                        Description = item.Description,
                        IsCash = item.IsCash,
                        ParentAccountId = item.ParentAccountId,
                        TypeAccountId = item.TypeAccountId,
                        FechaCierre = proceso.FechaCierre,
                        BitacoraCierreContableId = proceso.IdBitacoraCierre,
                        UsuarioCreacion = User.Claims.FirstOrDefault().Value.ToString(),
                        UsuarioModificacion = User.Claims.FirstOrDefault().Value.ToString(),


                    };
                    cierreCatalogo.Add(cuenta);
                }

                await _context.CierresAccounting.AddRangeAsync(cierreCatalogo);


                /////////Cierre Journal entries
                ///
                var cerradosJE = await _context.CierresJournal.Select(x => x.JournalEntryId).ToListAsync();
                List<JournalEntry> cierreJE = await _context.JournalEntry.Where(c => !cerradosJE.Any(x => x == c.JournalEntryId )&& c.EstadoId ==6 ).ToListAsync(); ////Busca los Journal Entry a Cerrar (los no cerrados)
                List<CierresJournal> cierreCuentas = new List<CierresJournal>();
                foreach (var item in cierreJE)
                {
                    CierresJournal je = new CierresJournal
                    {
                        JournalEntryId = item.JournalEntryId,
                        ApprovedBy = item.ApprovedBy,
                        DatePosted = item.DatePosted,
                        DocumentId = item.DocumentId,
                        EstadoId = item.EstadoId,
                        TotalCredit = item.TotalCredit,
                        TotalDebit = item.TotalDebit,
                        BitacoraCierreContableId = proceso.IdBitacoraCierre,
                        FechaCierre = proceso.FechaCierre,
                        CreatedUser = User.Claims.FirstOrDefault().Value.ToString(),
                        ModifiedUser = User.Claims.FirstOrDefault().Value.ToString(),
                        CreatedDate = item.CreatedDate,
                        ModifiedDate = item.ModifiedDate,


                    };

                    cierreCuentas.Add(je);

                }

                await _context.CierresJournal.AddRangeAsync(cierreCuentas);
                /////////Cierre Journal entries Lineas
                ///

                List<JournalEntryLine> cierreJEL = await _context.JournalEntryLine.Where(c => cierreJE.Any(x => c.JournalEntryId == x.JournalEntryId)).ToListAsync();
                List<CierresJournalEntryLine> cierreLineas = new List<CierresJournalEntryLine>();

                foreach (var item in cierreJEL)
                {
                    CierresJournalEntryLine jel = new CierresJournalEntryLine
                    {
                        JournalEntryId = item.JournalEntryId,
                        JournalEntryLineId = item.JournalEntryLineId,
                        AccountId = item.AccountId,
                        AccountName = item.AccountName,
                        Debit = item.Debit,
                        Credit = item.Credit,
                        BitacoraCierreContableId = proceso.IdBitacoraCierre,
                        FechaCierre = proceso.FechaCierre,
                        CreatedUser = User.Claims.FirstOrDefault().Value.ToString(),
                        ModifiedUser = User.Claims.FirstOrDefault().Value.ToString(),
                        CreatedDate = item.CreatedDate,
                        ModifiedDate = item.ModifiedDate,



                    };

                    cierreLineas.Add(jel);



                }

                await _context.CierresJournalEntryLine.AddRangeAsync(cierreLineas);

                proceso.Estatus = "FINALIZADO";
                _context.BitacoraCierreProceso.Update(proceso);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                proceso.Estatus = "ERROR";
                proceso.Mensaje = ex.Message;
                _context.BitacoraCierreProceso.Update(proceso);
                await _context.SaveChangesAsync();

            }

            

        }

        private  async Task Paso2(int idProceso)
        {
            //////////////
            ///
            BitacoraCierreProcesos proceso = await _context.BitacoraCierreProceso.Where(w => w.IdProceso == idProceso).FirstOrDefaultAsync();
            proceso.Estatus = "EJECUTANDO";
            _context.BitacoraCierreProceso.Update(proceso);
            await _context.SaveChangesAsync();

            //////////Actualiza el techo de los certificados mensuales
            double suma = await _context.CertificadoDeposito.SumAsync(c => c.Total);
            ElementoConfiguracion elemento = await _context.ElementoConfiguracion.Where(e => e.Descripcion == "VALOR MAXIMO CERTIFICADOS").FirstOrDefaultAsync();
            if (elemento != null)
            {
                elemento.Valordecimal = suma;
                _context.ElementoConfiguracion.Update(elemento);
            }
            else
            {
                proceso.Estatus = "ERROR";
                proceso.Mensaje = "NO SE ENCONTRO EL ELEMENTO DE CONFIGURACION";

                _context.BitacoraCierreProceso.Update(proceso);

                return;

            }


            proceso.Estatus = "FINALIZADO";

            _context.BitacoraCierreProceso.Update(proceso);
            return;

        }

        private async Task Paso3(int idProceso)
        {
            TiposDocumento tipoDocumento = await _context.TiposDocumento.Where(d => d.Descripcion == "Polizas").FirstOrDefaultAsync();

            JournalEntryConfiguration _journalentryconfiguration = await (_context.JournalEntryConfiguration
                                                           .Where(q => q.TransactionId == tipoDocumento.IdTipoDocumento)
                                                           //.Where(q => q.BranchId == _Invoiceq.BranchId)
                                                           .Where(q => q.EstadoName == "Activo")
                                                           .Include(q => q.JournalEntryConfigurationLine)
                                                           ).FirstOrDefaultAsync();
            //////////////
            ///
            BitacoraCierreProcesos proceso = await _context.BitacoraCierreProceso.Where(w => w.IdProceso == idProceso).FirstOrDefaultAsync();
            proceso.Estatus = "EJECUTANDO";
            _context.BitacoraCierreProceso.Update(proceso);
            //await _context.SaveChangesAsync();

            List<InsurancePolicy> insurancePolicies = await _context.InsurancePolicy.Where(i => i.PolicyDueDate < DateTime.Now).ToListAsync();

            double SumaPolizas = await _context.InsurancePolicy.Where(i => i.PolicyDueDate < DateTime.Now).SumAsync(s => s.LpsAmount);

            if (insurancePolicies.Count > 0)
            {


                if (_journalentryconfiguration != null)
                {

                    //Crear el asiento contable configurado
                    //.............................///////
                    JournalEntry _je = new JournalEntry
                    {
                        Date = DateTime.Now,
                        Memo = "Vecimiento de Polizas",
                        DatePosted = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedUser = User.Claims.FirstOrDefault().Value.ToString(),
                        CreatedUser = User.Claims.FirstOrDefault().Value.ToString(),
                        DocumentId = proceso.IdProceso,
                        TypeOfAdjustmentId = 65,
                        VoucherType = Convert.ToInt32(tipoDocumento.IdTipoDocumento),

                    };
                    _context.JournalEntry.Add(_je);
                    foreach (var item in _journalentryconfiguration.JournalEntryConfigurationLine)
                    {


                        _je.JournalEntryLines.Add(new JournalEntryLine
                        {
                            JournalEntryId = _je.JournalEntryId,
                            AccountId = Convert.ToInt32(item.AccountId),
                            AccountName = item.AccountName,
                            Description = item.AccountName,
                            Credit = item.DebitCredit == "Credito" ? SumaPolizas : 0,
                            Debit = item.DebitCredit == "Debito" ? SumaPolizas : 0,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                            CreatedUser = User.Claims.FirstOrDefault().Value.ToString(),
                            ModifiedUser = User.Claims.FirstOrDefault().Value.ToString(),
                            Memo = "",
                        }); ;


                    }
                    _context.JournalEntry.Update(_je);
                    proceso.Mensaje = "Generado Asiendo No. " + _je.JournalEntryId;
                    proceso.Estatus = "FINALIZADO";

                    foreach (var item in insurancePolicies)
                    {
                        item.Status = "VENCIDA";
                    }
                    _context.InsurancePolicy.UpdateRange(insurancePolicies);
                    proceso.Estatus = "FINALIZADO";

                }
                else
                {
                    proceso.Mensaje = "No se encontro Configuracion del asiento del vencimiento de pólizas";
                    proceso.Estatus = "ERROR";
                }
                //_context.BitacoraCierreProceso.Update(pProceso3);
                return;
            }

                
            
            else
            {
                proceso.Mensaje = "No se encontraron pólizas vencidas";
                proceso.Estatus = "FINALIZADO";
                //_context.BitacoraCierreProceso.Update(pProceso3);
                return;
            }




        }


        private async Task Paso4(BitacoraCierreProcesos pProceso, BitacoraCierreContable pCierre)
        {
            var depreciaciongrupos = await _context.FixedAsset
                .GroupBy(g => new { 
                    g.FixedAssetGroupId,
                    g.CenterCostId,
                    g.CenterCostName })
                .Select(g => new {
                    CentroCostoName = g.Key.CenterCostName, 
                    CentroCostoID = g.Key.CenterCostId, 
                    Grupo = g.Key.FixedAssetGroupId, 
                    Depreciacion = g.Sum(s => s.ToDepreciate) })
                .ToListAsync();

            var activos = await _context.FixedAsset.ToListAsync();



            if (depreciaciongrupos.Count>0)
            {
                //////////DEPRECIACION /////////////////
                foreach (var item in activos)
                {
                    item.AccumulatedDepreciation += item.ToDepreciate;
                    item.NetValue -= item.ToDepreciate;

                    _context.DepreciationFixedAsset.Add(new DepreciationFixedAsset {
                        FixedAssetId = item.FixedAssetId,
                        Year = DateTime.Now.Year,
                        January = DateTime.Now.ToString("MMMM") == "January" ? item.ToDepreciate : 0,
                        February = DateTime.Now.ToString("MMMM") == "February" ? item.ToDepreciate : 0,
                        March = DateTime.Now.ToString("MMMM") == "March" ? item.ToDepreciate : 0,
                        April = DateTime.Now.ToString("MMMM") == "April" ? item.ToDepreciate : 0,
                        May = DateTime.Now.ToString("MMMM") == "May" ? item.ToDepreciate : 0,
                        June = DateTime.Now.ToString("MMMM") == "June" ? item.ToDepreciate : 0,
                        July = DateTime.Now.ToString("MMMM") == "July" ? item.ToDepreciate : 0,
                        August = DateTime.Now.ToString("MMMM") == "August" ? item.ToDepreciate : 0,
                        September = DateTime.Now.ToString("MMMM") == "September" ? item.ToDepreciate : 0,
                        October = DateTime.Now.ToString("MMMM") == "October" ? item.ToDepreciate : 0,
                        November = DateTime.Now.ToString("MMMM") == "November" ? item.ToDepreciate : 0,
                        December = DateTime.Now.ToString("MMMM") == "December" ? item.ToDepreciate : 0,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = User.Claims.FirstOrDefault().Value.ToString(),
                        UsuarioModificacion = User.Claims.FirstOrDefault().Value.ToString(),
                    });
                }

                //////////////////ASIENTO CONTABLE//////////////////////////
                
                foreach (var item in depreciaciongrupos)
                {
                    FixedAssetGroup grupo = await _context.FixedAssetGroup.Where(w => w.FixedAssetGroupId == item.Grupo).FirstOrDefaultAsync();

                    if (grupo.DepreciationAccountingId == null || grupo.FixedAssetAccountingId == null)
                    {
                        pProceso.Mensaje = "Los grupos de activos deben de tener las cuentas contables asignadas";
                        pProceso.Estatus = "FINALIZADO";
                        return ;
                    }
                    Accounting cuentaDepreciacion = await _context.Accounting.Where(w => w.AccountId == grupo.DepreciationAccountingId).FirstOrDefaultAsync();
                    Accounting cuentaActivo = await _context.Accounting.Where(w => w.AccountId == grupo.FixedAssetAccountingId).FirstOrDefaultAsync();


                    JournalEntry _je = new JournalEntry
                    {
                        Date = DateTime.Now,
                        Memo = "Depreciacion de Activos",
                        DatePosted = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedUser = User.Claims.FirstOrDefault().Value.ToString(),
                        CreatedUser = User.Claims.FirstOrDefault().Value.ToString(),
                        //DocumentId = pProceso3.IdProceso,
                        TypeOfAdjustmentId = 65,
                        //VoucherType = Convert.ToInt32(tipoDocumento.IdTipoDocumento),                      

                    };                    
                    _je.JournalEntryLines.Add(new JournalEntryLine {
                           JournalEntryId = _je.JournalEntryId,
                           AccountId = Convert.ToInt32(cuentaDepreciacion.AccountId),
                           AccountName = cuentaDepreciacion.AccountName,
                           Debit = item.Depreciacion,
                           DebitME = item.Depreciacion,
                           CostCenterId = item.CentroCostoID,
                           CostCenterName = item.CentroCostoName,
                           CreatedUser = "SYSTEM",
                           CreatedDate = DateTime.Now,
                    });

                    _je.JournalEntryLines.Add(new JournalEntryLine
                    {
                        JournalEntryId = _je.JournalEntryId,
                        AccountId = Convert.ToInt32(cuentaActivo.AccountId),
                        AccountName = cuentaActivo.AccountName,
                        Credit = item.Depreciacion,
                        CreditME = item.Depreciacion,
                        CostCenterId = item.CentroCostoID,
                        CostCenterName = item.CentroCostoName,
                        CreatedUser = "SYSTEM",
                        CreatedDate = DateTime.Now,
                    });
                }
                pProceso.Estatus = "FINALIZADO";
                return;
            }
            else
            {
                pProceso.Mensaje = "No se encontraron grupos de Activos";
                pProceso.Estatus = "FINALIZADO";
                return;

            }
                
            
        }

        private async Task Paso5(int pProcesoId)
        {
            TiposDocumento tipoDocumento = await _context.TiposDocumento.Where(d => d.Descripcion == "Garantias Bancarias").FirstOrDefaultAsync();

            JournalEntryConfiguration _journalentryconfiguration = await (_context.JournalEntryConfiguration
                                                           .Where(q => q.TransactionId == tipoDocumento.IdTipoDocumento)
                                                           //.Where(q => q.BranchId == _Invoiceq.BranchId)
                                                           .Where(q => q.EstadoName == "Activo")
                                                           .Include(q => q.JournalEntryConfigurationLine)
                                                           ).FirstOrDefaultAsync();
            //////////////
            ///
            BitacoraCierreProcesos proceso = await _context.BitacoraCierreProceso.Where(w => w.IdProceso == pProcesoId).FirstOrDefaultAsync();
            proceso.Estatus = "EJECUTANDO";
            _context.BitacoraCierreProceso.Update(proceso);
            //await _context.SaveChangesAsync();

            List<GarantiaBancaria> garantias = await _context.GarantiaBancaria.Where(i => i.FechaFianlVigencia < DateTime.Now).ToListAsync();

            double SumaGarantias = await _context.GarantiaBancaria.Where(i => i.FechaFianlVigencia < DateTime.Now).SumAsync(s => s.Monto);

            if (garantias.Count > 0)
            {


                if (_journalentryconfiguration != null)
                {

                    //Crear el asiento contable configurado
                    //.............................///////
                    JournalEntry _je = new JournalEntry
                    {
                        Date = DateTime.Now,
                        Memo = "Vecimiento de Garantias Bancarias",
                        DatePosted = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedUser = User.Claims.FirstOrDefault().Value.ToString(),
                        CreatedUser = User.Claims.FirstOrDefault().Value.ToString(),
                        DocumentId = proceso.IdProceso,
                        TypeOfAdjustmentId = 65,
                        VoucherType = Convert.ToInt32(tipoDocumento.IdTipoDocumento),

                    };
                    _context.JournalEntry.Add(_je);
                    foreach (var item in _journalentryconfiguration.JournalEntryConfigurationLine)
                    {


                        _je.JournalEntryLines.Add(new JournalEntryLine
                        {
                            JournalEntryId = _je.JournalEntryId,
                            AccountId = Convert.ToInt32(item.AccountId),
                            AccountName = item.AccountName,
                            Description = item.AccountName,
                            Credit = item.DebitCredit == "Credito" ? SumaGarantias : 0,
                            Debit = item.DebitCredit == "Debito" ? SumaGarantias : 0,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                            CreatedUser = User.Claims.FirstOrDefault().Value.ToString(),
                            ModifiedUser = User.Claims.FirstOrDefault().Value.ToString(),
                            Memo = "",
                        }); ;


                    }
                    _context.JournalEntry.Update(_je);
                    proceso.Mensaje = "Generado Asiendo No. " + _je.JournalEntryId;
                    proceso.Estatus = "FINALIZADO";

                    foreach (var item in garantias)
                    {
                        item.Estado = "Inactiva";
                        item.IdEstado = 2;
                    }
                    _context.GarantiaBancaria.UpdateRange(garantias);
                    proceso.Estatus = "FINALIZADO";

                }
                else
                {
                    proceso.Mensaje = "No se encontro Configuracion de las garantias Bancarias";
                    proceso.Estatus = "ERROR";
                }
                //_context.BitacoraCierreProceso.Update(pProceso3);
                return;
            }



            else
            {
                proceso.Mensaje = "No se encontraron Garantías Bancarias";
                proceso.Estatus = "FINALIZADO";
                //_context.BitacoraCierreProceso.Update(pProceso3);
                return;
            }


        }

        private  void ValidarPasos(BitacoraCierreContable pCierre )
        {
            List<BitacoraCierreProcesos> procesos =  _context.BitacoraCierreProceso
                           .Where(b => b.IdBitacoraCierre == pCierre.Id)
                           .ToList();
            string mensaje = "";
            foreach (var item in procesos) ////REVISA QUE ALGUN PASO NO TENGA ERROR EN EL CIERRE AL VOLVER A EJECUTAR
            {
                if (item.Estatus == "ERROR" || item.Estatus == "PENDIENTE")
                {
                    //mensaje += " Paso " + item.PasoCierre;
                    mensaje += " " + item.PasoCierre;
                }
            }
            if (mensaje != "")
            {
                mensaje += "con Errores ";
                pCierre.Mensaje = mensaje;
                
                return;
            }
            else
            {
                pCierre.Estatus = "FINALIZADO";                
                return ;

            }

        }


        private bool CheckCierre(BitacoraCierreContable pCierre)
        {
            List<BitacoraCierreProcesos> procesos =  _context.BitacoraCierreProceso
                           .Where(b => b.IdBitacoraCierre == pCierre.Id)
                           .ToList();
            bool repetido = true;
            foreach (var item in procesos) ////REVISA QUE ALGUN PASO NO TENGA ERROR EN EL CIERRE AL VOLVER A EJECUTAR
            {
                if (item.Estatus == "ERROR" || item.Estatus == "PENDIENTE")
                {
                    repetido = false;
                    if (item.PasoCierre == 3)
                    {
                        //Paso3(item);
                        _context.SaveChanges();
                        break;
                    }
                    else
                    {
                        _context.Database.ExecuteSqlCommand("Cierres @p0", pCierre.Id); ////Ejecuta SP para ejecutar pasos con errores
                        break;
                    }

                }
            }
            //if (repetido)
            //{
            //    return await Task.Run(() => BadRequest("Ya existe un Cierre Contable para esta Fecha"));

            //}
           // ValidarPasos(pCierre);
            _context.SaveChanges();
            return repetido;
           
            //return await Task.Run(() => Ok());


        }

    }



}


