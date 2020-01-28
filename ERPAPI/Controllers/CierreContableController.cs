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

                    _context.BitacoraCierreProceso.Add(proceso1);
                    _context.BitacoraCierreProceso.Add(proceso2);
                    _context.BitacoraCierreProceso.Add(proceso3);
                     Paso1(cierre,proceso1); ////HISTORICOS
                     Paso2(cierre,proceso2);/// Valor Maximo de Certificados 
                     Paso3(proceso3);//// POLIZAS VENCIDAS 

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
                            //Paso4(cierre, proceso4);
                            _context.BitacoraCierreProceso.Add(proceso4);
                        }
                         
                        _context.SaveChanges();


            try
            {
                //////Ejecuta el Cierre
               // _context.Database.ExecuteSqlCommand("Cierres @p0", cierre.Id); ////Ejecuta SP Cierres
                //ValidarPasos(cierre);
                _context.SaveChanges();
                return await Task.Run(() => Ok());

            }
            catch (Exception ex)
            {
                return await Task.Run(() => BadRequest(ex.Message));
                throw;
            }

                   


            }


        
        private  void Paso1(BitacoraCierreContable pCierre,BitacoraCierreProcesos pProceso)
        {

            try
            {
                ////////////////Cierre Catalogo
                List<CierresAccounting> cierreCatalogo = new List<CierresAccounting>();
                List<Accounting> catalogo = _context.Accounting.ToList();

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
                        FechaCierre = pCierre.FechaCierre,
                        BitacoraCierreContableId = pCierre.Id,
                        UsuarioCreacion = User.Claims.FirstOrDefault().Value.ToString(),
                        UsuarioModificacion = User.Claims.FirstOrDefault().Value.ToString(),


                    };
                    cierreCatalogo.Add(cuenta);
                }

                _context.CierresAccounting.AddRange(cierreCatalogo);


                /////////Cierre Journal entries
                ///
                var cerradosJE = _context.CierresJournal.Select(x => x.JournalEntryId).ToList();
                List<JournalEntry> cierreJE = _context.JournalEntry.Where(c => !cerradosJE.Any(x => x == c.JournalEntryId)).ToList(); ////Busca los Journal Entry a Cerrar (los no cerrados)
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
                        BitacoraCierreContableId = pCierre.Id,
                        FechaCierre = pCierre.FechaCierre

                    };

                    cierreCuentas.Add(je);

                }

                _context.CierresJournal.AddRange(cierreCuentas);
                /////////Cierre Journal entries Lineas
                ///

                List<JournalEntryLine> cierreJEL = _context.JournalEntryLine.Where(c => cierreJE.Any(x => c.JournalEntryId == x.JournalEntryId)).ToList();
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
                        BitacoraCierreContableId = pCierre.Id,
                        FechaCierre = pCierre.FechaCierre,




                    };

                    cierreLineas.Add(jel);



                }

                _context.CierresJournalEntryLine.AddRange(cierreLineas);

                pProceso.Estatus = "FINALIZADO";
                _context.BitacoraCierreProceso.Update(pProceso);

            }
            catch (Exception ex)
            {

                pProceso.Estatus = "ERROR";
                pProceso.Mensaje = ex.Message;
                _context.BitacoraCierreProceso.Update(pProceso);


            }
            


        }

        private  void Paso2(BitacoraCierreContable pCierre, BitacoraCierreProcesos pProceso)
        {
           
            //////////Actualiza el techo de los certificados mensuales
            double suma = _context.CertificadoDeposito.Sum(c => c.Total);
            ElementoConfiguracion elemento =  _context.ElementoConfiguracion.Where(e => e.Descripcion == "VALOR MAXIMO CERTIFICADOS").FirstOrDefault();
            if (elemento != null)
            {
                elemento.Valordecimal = suma;
                _context.ElementoConfiguracion.Update(elemento);
            }
            else
            {
                pProceso.Estatus = "ERROR";
                pProceso.Mensaje = "NO SE ENCONTRO EL ELEMENTO DE CONFIGURACION";

                _context.BitacoraCierreProceso.Update(pProceso);

                return;

            }


            pProceso.Estatus = "FINALIZADO";

            _context.BitacoraCierreProceso.Update(pProceso);
            return;

        }

        private async void Paso3(BitacoraCierreProcesos pProceso3)
        {

            List<InsurancePolicy> insurancePolicies = _context.InsurancePolicy.Where(i => i.PolicyDueDate < DateTime.Now).ToList();

            double SumaPolizas = _context.InsurancePolicy.Where(i => i.PolicyDueDate < DateTime.Now).ToList().
                Sum(s => s.LpsAmount);

            if (insurancePolicies.Count > 0)
            {


                foreach (var item in insurancePolicies)
                {
                    item.Status = "INACTIVA";
                }
                _context.InsurancePolicy.UpdateRange(insurancePolicies);
                pProceso3.Estatus = "FINALIZADO";

                if (SumaPolizas > 0)
                {
                    TiposDocumento tipoDocumento = _context.TiposDocumento.Where(d => d.Descripcion == "Polizas").FirstOrDefault();
                    JournalEntryConfiguration _journalentryconfiguration = await(_context.JournalEntryConfiguration
                                                               .Where(q => q.TransactionId == tipoDocumento.IdTipoDocumento)
                                                               //.Where(q => q.BranchId == _Invoiceq.BranchId)
                                                               .Where(q => q.EstadoName == "Activo")
                                                               .Include(q => q.JournalEntryConfigurationLine)
                                                               ).FirstOrDefaultAsync();

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
                            DocumentId = pProceso3.IdProceso,
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
                        pProceso3.Mensaje = "Generado Asiendo No. "+_je.JournalEntryId;
                        pProceso3.Estatus = "FINALIZADO";
                    }
                    else
                    {
                        pProceso3.Mensaje = "No se encontro Configuracion del asiento del vencimiento de pólizas";
                        pProceso3.Estatus = "ERROR";
                    }

                }

                //_context.BitacoraCierreProceso.Update(pProceso3);
                return;
            }
            else
            {
                pProceso3.Mensaje = "No se encontraron pólizas vencidas";
                pProceso3.Estatus = "FINALIZADO";
                //_context.BitacoraCierreProceso.Update(pProceso3);
                return;
            }




        }


        private void Paso4(BitacoraCierreContable pProceso)
        {
            var depreciaciongrupos = _context.FixedAsset
                .GroupBy(g => new { 
                    g.FixedAssetGroupId,
                    g.CenterCostId,
                    g.CenterCostName })
                .Select(g => new {
                    CentroCostoName = g.Key.CenterCostName, 
                    CentroCostoID = g.Key.CenterCostId, 
                    Grupo = g.Key.FixedAssetGroupId, 
                    Depreciacion = g.Sum(s => s.ToDepreciate) })
                .ToList();

            if (depreciaciongrupos.Count>0)
            {
                
                foreach (var item in depreciaciongrupos)
                {
                    FixedAssetGroup grupo = _context.FixedAssetGroup.Where(w => w.FixedAssetGroupId == item.Grupo).FirstOrDefault();

                    Accounting cuentaDepreciacion = _context.Accounting.Where(w => w.AccountId == grupo.DepreciationAccountingId).FirstOrDefault();
                    Accounting cuentaActivo = _context.Accounting.Where(w => w.AccountId == grupo.FixedAssetAccountingId).FirstOrDefault();

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
            }
            else
            {
                pProceso.Mensaje = "No se encontraron grupos de Activos";
                pProceso.Estatus = "FINALIZADO";
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
                        Paso3(item);
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


