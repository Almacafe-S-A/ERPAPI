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
using System.Collections;
using AutoMapper.Configuration.Conventions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;

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
        [HttpGet("[action]")]
        public async Task<IActionResult> UltimoCierre()
        {
            try
            {
                BitacoraCierreContable cierre = new BitacoraCierreContable();
                cierre = await _context.BitacoraCierreContable.Where(w => w.Estatus == "FINALIZADO").OrderByDescending(i => i.FechaCierre).FirstOrDefaultAsync();
                return await Task.Run(() => Ok(cierre)); ;

            }
            catch (Exception ex)
            {

                return await Task.Run(() => BadRequest("Error:" + ex.Message));
            }




        }

        /// <summary>
        /// EjecutarProcesoCierre
        /// </summary>
        /// <param name="ProcesoId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{ProcesoId}")]
        public async Task<IActionResult> EjecutarPasoCierre(int ProcesoId)
        {
            BitacoraCierreProcesos proceso = new BitacoraCierreProcesos();
            proceso = _context.BitacoraCierreProceso.Where(q => q.IdProceso == ProcesoId).FirstOrDefault();

            BitacoraCierreContable cierre = new BitacoraCierreContable();
            cierre = _context.BitacoraCierreContable.Where(q => q.Id == proceso.IdBitacoraCierre).FirstOrDefault(); 


            switch (proceso.PasoCierre)
            {
                case 3:
                    await DepreciacionActivosFijos(proceso, cierre, DateTime.Now);
                    break;

                case 5:
                    await DiferencialesCambiarios(proceso, proceso.IdProceso);
                    break;

                case 4:
                    await EjecucionPresupuestaria(proceso.IdProceso);
                    break;
                    
                default:
                    proceso.Estatus = "ERROR";
                    proceso.Mensaje = "No existe un proceso registrado";
                    break;
            }
            _context.SaveChanges();

            return Ok();


        }

        [HttpGet("[action]")]
        public async Task<IActionResult> EjecutarCierrePartidasCierre(int periodo)
        {

            await _context.Database.ExecuteSqlCommandAsync("Cierres @p0", periodo); ////Ejecuta SP Cierres

            return Ok();


        }


        /// <summary>
        /// Realiza un Cierre Contable
        /// </summary>
        /// <returns></returns>    
        [HttpPost("[action]")]
        public async Task<IActionResult> EjecutarCierreContable([FromBody] BitacoraCierreContable pBitacoraCierre)
        {
            /////obtiene el ultimo cierre
            BitacoraCierreContable cierre = await _context.BitacoraCierreContable.Where(w => w.FechaCierre == pBitacoraCierre.FechaCierre).OrderByDescending(i => i.FechaCierre).FirstOrDefaultAsync();
            ExchangeRate tasacambio = await _context.ExchangeRate
                            //.Where(b => b.DayofRate >= DateTime.Now.AddDays(-1)).FirstOrDefaultAsync();
                            .Where(b => b.DayofRate >= DateTime.Now.AddDays(-1) && b.CurrencyId == 2).FirstOrDefaultAsync();

            BitacoraCierreContable ultimocierre = await _context.BitacoraCierreContable.Where(w => w.Estatus == "FINALIZADO").OrderByDescending(i => i.FechaCierre).FirstOrDefaultAsync();

            if (tasacambio == null)//Revisa la tasa de cambio actualizada
            {
                return await Task.Run(() => BadRequest("Debe de Agregar una tasa de cambio de Dolar Estadounidense actualizada"));
            }

            if (cierre != null && cierre.Estatus != "FINALIZADO")
            {
                cierre.Estatus = await CheckCierre(cierre);
                await _context.SaveChangesAsync();
                return Ok(cierre);

            }
            if (cierre != null && cierre.Estatus == "FINALIZADO")
            {
                return BadRequest("Ya existe un cierre para esta fecha");
            }
            if (ultimocierre != null && ultimocierre.FechaCierre > pBitacoraCierre.FechaCierre)
            {
                return await Task.Run(() => BadRequest($"La fecha no puede anterior al ultimo cierre"));
            }

            ////Si no existe Ciere lo crea
            cierre = new BitacoraCierreContable
            {
                FechaCierre = pBitacoraCierre.FechaCierre,
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
            BitacoraCierreProcesos procesohistoricos = new BitacoraCierreProcesos
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
            BitacoraCierreProcesos procesoValorMaximoCD = new BitacoraCierreProcesos
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
            BitacoraCierreProcesos procesoSegurosVencimineto = new BitacoraCierreProcesos
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

            BitacoraCierreProcesos procesoGarantiasVenc = new BitacoraCierreProcesos
            {
                IdBitacoraCierre = cierre.Id,
                //IdProceso = 1,
                Estatus = "PENDIENTE",
                Proceso = "GARANTIAS BANCARIAS VENCIDAS",
                PasoCierre = 5,
                UsuarioCreacion = User.Claims.FirstOrDefault().Value.ToString(),
                UsuarioModificacion = User.Claims.FirstOrDefault().Value.ToString(),
                FechaModificacion = DateTime.Now,
                FechaCierre = cierre.FechaCierre,
                FechaCreacion = DateTime.Now,

            };

           
            

            _context.BitacoraCierreProceso.Add(procesohistoricos);
            _context.BitacoraCierreProceso.Add(procesoValorMaximoCD);
            _context.BitacoraCierreProceso.Add(procesoSegurosVencimineto);
            _context.BitacoraCierreProceso.Add(procesoGarantiasVenc);
           

            if (Convert.ToDateTime(cierre.FechaCierre).Day == DateTime.DaysInMonth(Convert.ToDateTime(cierre.FechaCierre).Year, Convert.ToDateTime(cierre.FechaCierre).Month)) ///////Se ejecuta solo si es fin de mes
            {
                BitacoraCierreProcesos procesoDepreciacion = new BitacoraCierreProcesos
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
                
                BitacoraCierreProcesos procesoEjecucionPresupuestaria = new BitacoraCierreProcesos
                {
                    IdBitacoraCierre = cierre.Id,
                    //IdProceso = 1,
                    Estatus = "PENDIENTE",
                    Proceso = "Ejecucion Presupuestaria Mensual",
                    PasoCierre = 6,
                    UsuarioCreacion = User.Identity.Name,
                    UsuarioModificacion = User.Identity.Name,
                    FechaModificacion = DateTime.Now,
                    FechaCierre = cierre.FechaCierre,
                    FechaCreacion = DateTime.Now,

                };
                BitacoraCierreProcesos procesoDiferencialesCambiarios = new BitacoraCierreProcesos
                {
                    IdBitacoraCierre = cierre.Id,
                    //IdProceso = 1,
                    Estatus = "PENDIENTE",
                    Proceso = "Diferenciales Cambiarios",
                    PasoCierre = 7,
                    UsuarioCreacion = User.Identity.Name,
                    UsuarioModificacion = User.Identity.Name,
                    FechaModificacion = DateTime.Now,
                    FechaCierre = cierre.FechaCierre,
                    FechaCreacion = DateTime.Now,

                };

                

                _context.BitacoraCierreProceso.Add(procesoDepreciacion);
                _context.BitacoraCierreProceso.Add(procesoDiferencialesCambiarios);
                _context.BitacoraCierreProceso.Add(procesoEjecucionPresupuestaria);

                await DepreciacionActivosFijos(procesoDepreciacion, cierre, (DateTime)cierre.FechaCierre);
                await DiferencialesCambiarios(procesoDiferencialesCambiarios, procesoDiferencialesCambiarios.IdProceso);
                await EjecucionPresupuestaria(procesoEjecucionPresupuestaria.IdProceso);

                await _context.SaveChangesAsync();


            }
            else
            {
                _context.SaveChanges();
            }



            try
            {
                //////Ejecuta el Cierre en db
                //await _context.Database.ExecuteSqlCommandAsync("Cierres @p0", cierre.Id); ////Ejecuta SP Cierres
                //ValidarPasos(cierre);
                
                await VencimientoPolizas(procesoSegurosVencimineto.IdProceso);
                await ValorMaxiomoCD(procesoValorMaximoCD.IdProceso);/// Valor Maximo de Certificados 
                await VencimientoGarantiasBancarias(procesoGarantiasVenc.IdProceso);
                // POLIZAS VENCIDAS 
                await Historicos(cierre.Id, procesohistoricos.IdProceso); ////HISTORICOS

                cierre.Estatus = await ValidarPasos(cierre);
                //_context.Update(cierre);
                await _context.SaveChangesAsync();
                return await Task.Run(() => Ok());

            }
            catch (Exception ex)
            {
                return await Task.Run(() => BadRequest(ex.Message));
                throw;
            }




        }

        private int apariciones()
        {
            int apariciones = 0;
            string numeros = "";
            for (int i = 0; i < 1537; i++)
            {
                numeros += i;
            }

            foreach (var item in numeros)
            {
                if (item == '6')
                {
                    apariciones++;
                }
            }

            return apariciones;

        }





        #region Acciones



        private async Task DiferencialesCambiarios(BitacoraCierreProcesos proceso, int procesoId)
        {

            /////////////Pendiente por definir ///////////////

            ExchangeRate rate = _context.ExchangeRate.Where(p => p.CurrencyId == 2 && p.DayofRate == proceso.FechaCierre).FirstOrDefault();
            if (rate == null)
            {
                proceso.Estatus = "ERROR";
                proceso.Mensaje = "No existe una tasa de cambio actualizada para la fecha seleccionada";
                _context.BitacoraCierreProceso.Update(proceso);
                await _context.SaveChangesAsync();
                return;
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                List<InsurancePolicy> polizas = await _context.InsurancePolicy.Where(p => p.EstadoId == 1 && !p.Propias && p.DollarAmount > 0).ToListAsync();
                decimal montolps = 0;
                foreach (var poliza in polizas)
                {
                    poliza.LpsAmount = poliza.DollarAmount * rate.ExchangeRateValue;
                    List<InsuranceEndorsement> endosos = await _context.InsuranceEndorsement.
                        Where(w => w.InsurancePolicyId == poliza.InsurancePolicyId && w.EstadoId == 1 && poliza.DollarAmount > 0).ToListAsync();
                    foreach (var endoso in endosos)
                    {
                        montolps = (endoso.TotalAmountLp * rate.ExchangeRateValue) - endoso.TotalAmountLp;
                        endoso.TotalAmountLp = endoso.TotalAmountLp * rate.ExchangeRateValue;
                    }

                }

                if (montolps == 0)
                {
                    proceso.Estatus = "ERROR";
                    proceso.Mensaje = "No existe una tasa de cambio actualizada para la fecha seleccionada";
                    _context.BitacoraCierreProceso.Update(proceso);
                    await _context.SaveChangesAsync();
                    return;
                }

                JournalEntryConfiguration _journalentryconfiguration = await (_context.JournalEntryConfiguration
                                                                         //.Where(q => q.TransactionId == 24)
                                                                         .Where(q => q.Transaction == "Diferenciales Cambiarios Seguros Endosados")
                                                                         //.Where(q => q.BranchId == _InsuranceEndorsementq.BranchId)
                                                                         .Where(q => q.EstadoName == "Activo")
                                                                         .Include(q => q.JournalEntryConfigurationLine)
                                                                         ).FirstOrDefaultAsync();
                if (montolps == 0)
                {
                    proceso.Estatus = "ERROR";
                    proceso.Mensaje = "No se encontro una configuracion de asiento automatico para este proceso";
                    _context.BitacoraCierreProceso.Update(proceso);
                    await _context.SaveChangesAsync();
                    return;
                }

                decimal sumacreditos = 0, sumadebitos = 0;
                if (_journalentryconfiguration != null)
                {
                    //Crear el asiento contable configurado
                    //.............................///////
                    JournalEntry _je = new JournalEntry
                    {
                        Date = (DateTime)proceso.FechaCierre,
                        Memo = "Diferenciales Cambiaron en Seguros Endosados",
                        DatePosted = (DateTime)proceso.FechaCierre,
                        ModifiedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedUser = User.Identity.Name,
                        CreatedUser = User.Identity.Name,
                        DocumentId = 0,
                        TypeOfAdjustmentId = 65,
                        VoucherType = 1,

                    };


                    foreach (var item in _journalentryconfiguration.JournalEntryConfigurationLine)
                    {

                        _je.JournalEntryLines.Add(new JournalEntryLine
                        {
                            AccountId = Convert.ToInt32(item.AccountId),
                            AccountName = item.AccountName,
                            Description = item.AccountName,
                            Credit = item.DebitCredit == "Credito" ? montolps : 0,
                            Debit = item.DebitCredit == "Debito" ? montolps : 0,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                            CreatedUser = User.Identity.Name,
                            ModifiedUser = User.Identity.Name,
                            Memo = "",
                        });

                        sumacreditos += item.DebitCredit == "Credito" ? montolps : 0;
                        sumadebitos += item.DebitCredit == "Debito" ? montolps : 0;


                    }

                    _je.TotalCredit = sumacreditos;
                    _je.TotalDebit = sumadebitos;
                    _context.JournalEntry.Add(_je);
                    await _context.SaveChangesAsync();
                    BitacoraWrite _writejec = new BitacoraWrite(_context, new Bitacora
                    {
                        IdOperacion = +_je.JournalEntryId,
                        DocType = "Asiento Configurado para Endosos",
                        ClaseInicial =
                     Newtonsoft.Json.JsonConvert.SerializeObject(_journalentryconfiguration, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                        ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_je, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                        Accion = "Insertado Asiento de Endoso",
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = User.Identity.Name,
                        UsuarioModificacion = User.Identity.Name,
                        UsuarioEjecucion = User.Identity.Name,

                    });
                    await _context.SaveChangesAsync();
                }
            }
        }


        private async Task Historicos(int idCierre, int idproceso)
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
                List<Accounting> catalogo = await _context.Accounting.ToListAsync();

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
                        FechaCierre = (DateTime)proceso.FechaCierre,
                        BitacoraCierreContableId = proceso.IdBitacoraCierre,
                        UsuarioCreacion = User.Claims.FirstOrDefault().Value.ToString(),
                        UsuarioModificacion = User.Claims.FirstOrDefault().Value.ToString(),


                    };
                    cierreCatalogo.Add(cuenta);
                }

                await _context.CierresAccounting.AddRangeAsync(cierreCatalogo);


                /////////Cierre Journal entries
                //var cerradosJE = await _context.CierresJournal.Select(x => x.JournalEntryId).ToListAsync();
                List<JournalEntry> pendientes = await _context.JournalEntry.Include(j => j.JournalEntryLines).Where(c => c.EstadoId == 6 && (c.Posted == false || c.Posted == null)).ToListAsync(); ////Busca los Journal Entry a Cerrar (los no cerrados)

                foreach (var item in pendientes)
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
                        FechaCierre = (DateTime)proceso.FechaCierre,
                        CreatedUser = User.Claims.FirstOrDefault().Value.ToString(),
                        ModifiedUser = User.Claims.FirstOrDefault().Value.ToString(),
                        CreatedDate = item.CreatedDate,
                        ModifiedDate = item.ModifiedDate,

                    };
                    je.CierresJournalEntryLines = new List<CierresJournalEntryLine>();
                    foreach (var detalle in item.JournalEntryLines)
                    {
                        CierresJournalEntryLine jel = new CierresJournalEntryLine
                        {
                            JournalEntryId = detalle.JournalEntryId,
                            JournalEntryLineId = detalle.JournalEntryLineId,
                            AccountId = detalle.AccountId,
                            AccountName = detalle.AccountName,
                            Debit = detalle.Debit,
                            Credit = detalle.Credit,
                            BitacoraCierreContableId = proceso.IdBitacoraCierre,
                            FechaCierre = (DateTime)proceso.FechaCierre,
                            CreatedUser = User.Claims.FirstOrDefault().Value.ToString(),
                            ModifiedUser = User.Claims.FirstOrDefault().Value.ToString(),
                            CreatedDate = detalle.CreatedDate,
                            ModifiedDate = detalle.ModifiedDate,
                        };
                        je.CierresJournalEntryLines.Add(jel);

                    }
                    item.Posted = true;
                    _context.CierresJournal.Add(je);

                }
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

        private async Task ValorMaxiomoCD(int idProceso)
        {
            //////////////
            ///
            BitacoraCierreProcesos proceso = await _context.BitacoraCierreProceso.Where(w => w.IdProceso == idProceso).FirstOrDefaultAsync();
            proceso.Estatus = "EJECUTANDO";
            _context.BitacoraCierreProceso.Update(proceso);
            await _context.SaveChangesAsync();

            ElementoConfiguracion elemento = await _context.ElementoConfiguracion.Where(e => e.Id == 92).FirstOrDefaultAsync();
            if (elemento == null)
            {
                proceso.Estatus = "ERROR";
                proceso.Mensaje = "NO SE ENCONTRO EL ELEMENTO DE CONFIGURACION";

                _context.BitacoraCierreProceso.Update(proceso);
                await _context.SaveChangesAsync();
                return;

            }
            //////////Actualiza el techo de los certificados mensuales
            decimal suma = await _context.CertificadoDeposito.SumAsync(c => c.Total);

            elemento.Valordecimal = (double)suma;
            _context.ElementoConfiguracion.Update(elemento);


            proceso.Estatus = "FINALIZADO";

            _context.BitacoraCierreProceso.Update(proceso);
            return;

        }

        private async Task VencimientoPolizas(int idProceso)
        {
            TypeJournal tipoDocumento = await _context.TypeJournal.Where(d => d.TypeJournalId == 7).FirstOrDefaultAsync();

            BitacoraCierreProcesos proceso = await _context.BitacoraCierreProceso.Where(w => w.IdProceso == idProceso).FirstOrDefaultAsync();
            proceso.Estatus = "EJECUTANDO";
            _context.BitacoraCierreProceso.Update(proceso);
            //await _context.SaveChangesAsync();

            JournalEntryConfiguration _journalentryconfiguration = await (_context.JournalEntryConfiguration
                                                          .Where(q => q.TransactionId == tipoDocumento.TypeJournalId)
                                                          //.Where(q => q.BranchId == _Invoiceq.BranchId)
                                                          .Where(q => q.EstadoId == 1)
                                                          .Include(q => q.JournalEntryConfigurationLine)
                                                          ).FirstOrDefaultAsync();

            if (_journalentryconfiguration == null)
            {
                proceso.Mensaje = "No se encontro Configuracion del asiento del vencimiento de pólizas";
                proceso.Estatus = "ERROR";
                await _context.SaveChangesAsync();
                return;
            }

            List<InsurancePolicy> polizas = await _context.InsurancePolicy.Where(i => i.PolicyDueDate < DateTime.Now && i.EstadoId == 1).ToListAsync();


            if (polizas.Count > 0)
            {
                proceso.Mensaje = "No se encontraron Polizas Vencidas";
                proceso.Estatus = "FINALIZADO";
                await _context.SaveChangesAsync();
                return;
            }

            //double SumaPolizas = await _context.InsurancePolicy.Where(i => i.PolicyDueDate < DateTime.Now && i.Status != "VENCIDA").SumAsync(s => s.LpsAmount);
            foreach (var item in polizas)
            {
                //Crear el asiento contable configurado
                //.............................///////
                JournalEntry _je = new JournalEntry
                {
                    Date = DateTime.Now,
                    Memo = $"Vecimiento de Poliza Id - {item.InsurancePolicyId}, Aseguradora {item.InsurancesName} , Valor {item.LpsAmount}",
                    DatePosted = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    ModifiedUser = User.Claims.FirstOrDefault().Value.ToString(),
                    CreatedUser = User.Claims.FirstOrDefault().Value.ToString(),
                    DocumentId = item.InsurancePolicyId,
                    TypeOfAdjustmentId = 65,
                    VoucherType = 7,
                    TypeJournalName = "Polizas",
                    EstadoId = 6,
                    EstadoName = "Aprobado",

                };
                _je.JournalEntryLines = new List<JournalEntryLine>();
                foreach (var conf in _journalentryconfiguration.JournalEntryConfigurationLine)
                {


                    _je.JournalEntryLines.Add(new JournalEntryLine
                    {
                        JournalEntryId = _je.JournalEntryId,
                        AccountId = Convert.ToInt32(conf.AccountId),
                        AccountName = conf.AccountName,
                        Description = conf.AccountName,
                        Credit = conf.DebitCredit == "Credito" ? item.LpsAmount : 0,
                        Debit = conf.DebitCredit == "Debito" ? item.LpsAmount : 0,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        CreatedUser = User.Claims.FirstOrDefault().Value.ToString(),
                        ModifiedUser = User.Claims.FirstOrDefault().Value.ToString(),
                        Memo = "",
                    }); ;


                }
                if (_je.JournalEntryLines.Sum(s => s.Debit) != _je.JournalEntryLines.Sum(s => s.Credit))
                {
                    proceso.Mensaje = "Error en Configuracion de Asientos contables No coinciden los Creditos y debitos del asiento";
                    proceso.Estatus = "ERROR";
                    await _context.SaveChangesAsync();
                    return;
                }

                item.EstadoId = 2;
                item.Estado = "Vencida";




            }
            proceso.Mensaje = $"Generados {polizas.Count} Asientos ";
            proceso.Estatus = "FINALIZADO";
            await _context.SaveChangesAsync();
            return;

        }


        private async Task DepreciacionActivosFijos(BitacoraCierreProcesos pProceso, BitacoraCierreContable pCierre, DateTime pfecha)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {

                    //    var depreciaciongrupos = await _context.FixedAsset
                    //.GroupBy(g => new
                    //{
                    //    g.FixedAssetGroupId,
                    //    g.CenterCostId,
                    //    g.CenterCostName
                    //})
                    //.Select(g => new
                    //{
                    //    CentroCostoName = g.Key.CenterCostName,
                    //    CentroCostoID = g.Key.CenterCostId,
                    //    Grupo = g.Key.FixedAssetGroupId,
                    //    Depreciacion = g.Sum(s => s.ToDepreciate)
                    //})
                    //.ToListAsync();
                    var depreciaciongrupos = await _context.FixedAssetGroup.Include(q => q.DepreciationFixedAssetAccounting).Include(c => c.FixedAssetAccounting).ToListAsync();


                    JournalEntry _je = new JournalEntry
                    {
                        Date = DateTime.Now,
                        Memo = "Depreciacion de Activos Cierre Mensual" + pCierre.FechaCierre,
                        DatePosted = pfecha,
                        ModifiedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedUser = User.Identity.Name,
                        CreatedUser = User.Identity.Name,
                        //DocumentId = item.FixedAssetId,
                        VoucherType = 21,
                        TypeJournalName = "Depreciacion",
                        EstadoId = 5,
                        EstadoName = "Aprobado Sistema",
                        TypeOfAdjustmentId = 65,

                        //VoucherType = Convert.ToInt32(tipoDocumento.IdTipoDocumento),                      

                    };

                    foreach (var grupo in depreciaciongrupos)
                    {


                        JournalEntryLine jelDepreciacion = new JournalEntryLine
                        {
                            JournalEntryId = _je.JournalEntryId,
                            AccountId = Convert.ToInt32(grupo.DepreciationFixedAssetAccounting.AccountId),
                            AccountName = grupo.DepreciationFixedAssetAccounting.AccountName,
                            Debit = 0,
                            //CostCenterId = item.CenterCostId,
                            //CostCenterName = item.CenterCostName,
                            CreatedUser = "SYSTEM",
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = User.Identity.Name
                        };

                        JournalEntryLine jeActivo = new JournalEntryLine
                        {
                            JournalEntryId = _je.JournalEntryId,
                            AccountId = Convert.ToInt32(grupo.FixedAssetAccounting.AccountId),
                            AccountName = grupo.FixedAssetAccounting.AccountName,
                            Credit = 0,
                            //CostCenterId = item.CenterCostId,
                            //CostCenterName = item.CenterCostName,
                            CreatedUser = "SYSTEM",
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = User.Identity.Name
                        };

                        var activos = await _context.FixedAsset.Where(p => p.IdEstado != 51 && p.FixedAssetGroupId == grupo.FixedAssetGroupId).ToListAsync();
                        foreach (var item in activos)
                        {
                            var adepreciar = item.TotalDepreciated;

                            if (adepreciar > item.ResidualValue)
                            {
                                adepreciar = item.ResidualValue;
                            }
                            if (item.ResidualValue - adepreciar <= 0)
                            {
                                item.IdEstado = 51;
                                item.Estado = "Depreciado";
                            }
                            else
                            {
                                item.IdEstado = 47;
                                item.Estado = "Depreciandose";
                            }

                            item.AccumulatedDepreciation += item.ToDepreciate;
                            item.NetValue -= item.ToDepreciate;
                            var depreciacion = _context.DepreciationFixedAsset.Where(q => q.FixedAssetId == item.FixedAssetId && q.Year == pfecha.Year).FirstOrDefault();
                            if (depreciacion != null)
                            {
                                depreciacion.FixedAssetId = item.FixedAssetId;
                                depreciacion.Year = pfecha.Year;
                                depreciacion.January = pfecha.Month == 1 ? adepreciar : depreciacion.January;
                                depreciacion.February = pfecha.Month == 2 ? adepreciar : depreciacion.February;
                                depreciacion.March = pfecha.Month == 3 ? adepreciar : depreciacion.March;
                                depreciacion.April = pfecha.Month == 4 ? adepreciar : depreciacion.April;
                                depreciacion.May = pfecha.Month == 5 ? adepreciar : depreciacion.May;
                                depreciacion.June = pfecha.Month == 6 ? adepreciar : depreciacion.June;
                                depreciacion.July = pfecha.Month == 7 ? adepreciar : depreciacion.July;
                                depreciacion.August = pfecha.Month == 8 ? adepreciar : depreciacion.August;
                                depreciacion.September = pfecha.Month == 9 ? adepreciar : depreciacion.September;
                                depreciacion.October = pfecha.Month == 10 ? adepreciar : depreciacion.October;
                                depreciacion.November = pfecha.Month == 11 ? adepreciar : depreciacion.November;
                                depreciacion.December = pfecha.Month == 12 ? adepreciar : depreciacion.December;
                                depreciacion.TotalDepreciated = depreciacion.January
                                                                + depreciacion.February
                                                                + depreciacion.March
                                                                + depreciacion.April
                                                                + depreciacion.May
                                                                + depreciacion.June
                                                                + depreciacion.July
                                                                + depreciacion.August
                                                                + depreciacion.September
                                                                + depreciacion.October
                                                                + depreciacion.November
                                                                + depreciacion.December;

                                depreciacion.FechaCreacion = DateTime.Now;
                                depreciacion.FechaModificacion = DateTime.Now;
                                depreciacion.UsuarioCreacion = User.Claims.FirstOrDefault().Value.ToString();
                                depreciacion.UsuarioModificacion = User.Claims.FirstOrDefault().Value.ToString();
                            }
                            else
                            {
                                //////Si el activo se adquirio del 15 en adelante se calcula una depreciacion diaria si se Aquirio el ultimo del mes no se deprecia en el mes actual

                                if (_context.DepreciationFixedAsset.Where(w => w.FixedAssetId == item.FixedAssetId).ToList() != null)
                                {
                                    if (item.AssetDate.Day >= DateTime.DaysInMonth(item.AssetDate.Year, item.AssetDate.Month) - 1
                                        && (item.AssetDate.Month == pfecha.Month && item.AssetDate.Year == pfecha.Year))
                                    {
                                        continue;
                                    }
                                    if (item.AssetDate.Day > 14 && item.AssetDate.Day < 30 && (item.AssetDate.Month == pfecha.Month && item.AssetDate.Year == pfecha.Year))
                                    {
                                        adepreciar = (item.TotalDepreciated / 30) * item.AssetDate.Day;
                                    }
                                }


                                _context.DepreciationFixedAsset.Add(new DepreciationFixedAsset
                                {
                                    FixedAssetId = item.FixedAssetId,
                                    Year = pfecha.Year,
                                    January = pfecha.Month == 1 ? adepreciar : 0,
                                    February = pfecha.Month == 2 ? adepreciar : 0,
                                    March = pfecha.Month == 3 ? adepreciar : 0,
                                    April = pfecha.Month == 4 ? adepreciar : 0,
                                    May = pfecha.Month == 5 ? adepreciar : 0,
                                    June = pfecha.Month == 6 ? adepreciar : 0,
                                    July = pfecha.Month == 7 ? adepreciar : 0,
                                    August = pfecha.Month == 8 ? adepreciar : 0,
                                    September = pfecha.Month == 9 ? adepreciar : 0,
                                    October = pfecha.Month == 10 ? adepreciar : 0,
                                    November = pfecha.Month == 11 ? adepreciar : 0,
                                    December = pfecha.Month == 12 ? adepreciar : 0,
                                    TotalDepreciated = adepreciar,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = User.Claims.FirstOrDefault().Value.ToString(),
                                    UsuarioModificacion = User.Claims.FirstOrDefault().Value.ToString(),
                                });
                            }

                            /////Actualiza los 
                            item.AccumulatedDepreciation += adepreciar;
                            item.NetValue -= adepreciar;

                            jeActivo.Credit += adepreciar;
                            jelDepreciacion.Debit += adepreciar;
                        }
                        _je.JournalEntryLines.Add(jelDepreciacion);
                        _je.JournalEntryLines.Add(jeActivo);
                        //_je.JournalEntryLines = new List<JournalEntryLine>();


                    }

                    _context.JournalEntry.Add(_je);
                    pProceso.Estatus = "FINALIZADO";
                    pProceso.Mensaje = "";
                    transaction.Commit();
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    pProceso.Estatus = "ERROR";
                    //throw;
                    await _context.SaveChangesAsync();
                    return;
                }
            }


        }

        private async Task VencimientoGarantiasBancarias(int pProcesoId)
        {
            //////////////
            ///
            BitacoraCierreProcesos proceso = await _context.BitacoraCierreProceso.Where(w => w.IdProceso == pProcesoId).FirstOrDefaultAsync();
            proceso.Estatus = "EJECUTANDO";
            _context.BitacoraCierreProceso.Update(proceso);
            await _context.SaveChangesAsync();

            TypeJournal tipoDocumento = await _context.TypeJournal.Where(d => d.TypeJournalId == 11).FirstOrDefaultAsync();
            if (tipoDocumento == null)
            {
                proceso.Mensaje = "No se encontro Configuracion de las garantias Bancarias";
                proceso.Estatus = "ERROR";
                await _context.SaveChangesAsync();
                return;
            }

            JournalEntryConfiguration _journalentryconfiguration = await (_context.JournalEntryConfiguration
                                                           .Where(q => q.TransactionId == tipoDocumento.TypeJournalId)
                                                           //.Where(q => q.BranchId == _Invoiceq.BranchId)
                                                           .Where(q => q.EstadoName == "Activo")
                                                           .Include(q => q.JournalEntryConfigurationLine)
                                                           ).FirstOrDefaultAsync();

            if (_journalentryconfiguration == null)
            {
                proceso.Mensaje = "No se encontro Configuracion de las garantias Bancarias";
                proceso.Estatus = "ERROR";
                await _context.SaveChangesAsync();
                return;
            }




            List<GarantiaBancaria> garantias = await _context.GarantiaBancaria.Where(i => i.FechaFianlVigencia < DateTime.Now && i.IdEstado != 2).ToListAsync();

            if (garantias.Count == 0)
            {
                proceso.Mensaje = "No se encontraron garantias Bancarias";
                proceso.Estatus = "FINALIZADO";
                await _context.SaveChangesAsync();
                return;

            }

            foreach (var item in garantias)
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
                    DocumentId = 11,
                    TypeJournalName = "Garantias Bancarias",
                    TypeOfAdjustmentId = 65,
                    VoucherType = 11,


                };
                _je.JournalEntryLines = new List<JournalEntryLine>();
                foreach (var conf in _journalentryconfiguration.JournalEntryConfigurationLine)
                {


                    _je.JournalEntryLines.Add(new JournalEntryLine
                    {
                        JournalEntryId = _je.JournalEntryId,
                        AccountId = Convert.ToInt32(conf.AccountId),
                        AccountName = conf.AccountName,
                        Description = conf.AccountName,
                        Credit = conf.DebitCredit == "Credito" ? item.Monto : 0,
                        Debit = conf.DebitCredit == "Debito" ? item.Monto : 0,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        CreatedUser = User.Claims.FirstOrDefault().Value.ToString(),
                        ModifiedUser = User.Claims.FirstOrDefault().Value.ToString(),
                        Memo = "",
                    }); ;


                }

                _context.JournalEntry.Add(_je);
                item.Estado = "Inactiva";
                item.IdEstado = 2;
            }

            proceso.Mensaje = $"Generado {garantias.Count} Asientos";
            proceso.Estatus = "FINALIZADO";
            await _context.SaveChangesAsync();

            return;


        }
        
        private async Task EjecucionPresupuestaria(int procesoId)
        {
            BitacoraCierreProcesos proceso = await _context.BitacoraCierreProceso.Where(w => w.IdProceso == procesoId).FirstOrDefaultAsync();
            proceso.Estatus = "EJECUTANDO";
            _context.BitacoraCierreProceso.Update(proceso);
            await _context.SaveChangesAsync();

            var periodo = _context.Periodo.Where(q => q.IdEstado == 1).OrderByDescending(q => q.Anio).FirstOrDefaultAsync();
            List<Presupuesto> cuentasPresupuesto = await _context.Presupuesto.Where(q => q.PeriodoId == periodo.Id).ToListAsync();
            foreach (var item in cuentasPresupuesto)
            {
                Accounting cuenta = await _context.Accounting.Where(q => q.AccountId == item.AccountigId).FirstOrDefaultAsync();
                var mes = DateTime.Now.Month;
                switch (mes)
                {
                    case 1:
                        item.EjecucionEnero = Convert.ToDecimal(cuenta.AccountBalance);
                        break;
                    case 2:
                        item.EjecucionFebrero = Convert.ToDecimal(cuenta.AccountBalance);
                        break;
                    case 3:
                        item.EjecucionMarzo = Convert.ToDecimal(cuenta.AccountBalance);
                        break;
                    case 4:
                        item.EjecucionAbril = Convert.ToDecimal(cuenta.AccountBalance);
                        break;
                    case 5:
                        item.EjecucionMayo = Convert.ToDecimal(cuenta.AccountBalance);
                        break;
                    case 6:
                        item.EjecucionJunio = Convert.ToDecimal(cuenta.AccountBalance);
                        break;
                    case 7:
                        item.EjecucionJulio = Convert.ToDecimal(cuenta.AccountBalance);
                        break;
                    case 8:
                        item.EjecucionAgosto = Convert.ToDecimal(cuenta.AccountBalance);
                        break;
                    case 9:
                        item.EjecucionSeptiembre = Convert.ToDecimal(cuenta.AccountBalance);
                        break;
                    case 10:
                        item.EjecucionOctubre = Convert.ToDecimal(cuenta.AccountBalance);
                        break;
                    case 11:
                        item.EjecucionNoviembre = Convert.ToDecimal(cuenta.AccountBalance);
                        break;
                    case 12:
                        item.EjecucionDiciembre = Convert.ToDecimal(cuenta.AccountBalance);
                        break;
                    default:
                        break;
                }
            }


            proceso.Estatus = "FINALIZADO";
            await _context.SaveChangesAsync();


        }

        private async Task ComprobacionSaldosCatalogo(int procesoId)
        {
            BitacoraCierreProcesos proceso = await _context.BitacoraCierreProceso.Where(w => w.IdProceso == procesoId).FirstOrDefaultAsync();
            proceso.Estatus = "EJECUTANDO";
            _context.BitacoraCierreProceso.Update(proceso);
            await _context.SaveChangesAsync();


            List<Accounting> cuentas = await _context.Accounting.Where(q => q.AccountBalance < (decimal)0.00001).ToListAsync();

            foreach (var item in cuentas)
            {
                item.AccountBalance = 0;
            }

            proceso.Estatus = "FINALIZADO";
            await _context.SaveChangesAsync();



        }

        private async Task<string> ValidarPasos(BitacoraCierreContable pCierre)
        {
            List<BitacoraCierreProcesos> procesos = await _context.BitacoraCierreProceso
                           .Where(b => b.IdBitacoraCierre == pCierre.Id)
                           .ToListAsync();
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

                return "Finalizado con Errores";
            }
            else
            {
                pCierre.Estatus = "FINALIZADO";
                return pCierre.Estatus;

            }

        }


        private async Task<string> CheckCierre(BitacoraCierreContable pCierre)
        {
            List<BitacoraCierreProcesos> procesos = _context.BitacoraCierreProceso
                           .Where(b => b.IdBitacoraCierre == pCierre.Id && b.Estatus == "ERROR")
                           .ToList();

            foreach (var item in procesos) ////REVISA QUE ALGUN PASO NO TENGA ERROR EN EL CIERRE AL VOLVER A EJECUTAR
            {
                switch (item.PasoCierre)
                {
                    case 1:
                        await Historicos(item.IdBitacoraCierre, item.IdProceso);
                        break;
                    case 2:
                        await VencimientoPolizas(item.IdProceso);
                        break;
                    case 3:
                        await VencimientoGarantiasBancarias(item.IdProceso);
                        break;
                    case 4:
                        await DepreciacionActivosFijos(item, pCierre, (DateTime)item.FechaCierre);
                        break;
                    case 5:
                        await VencimientoGarantiasBancarias(item.IdProceso);
                        break;
                    case 6:
                        await EjecucionPresupuestaria(item.IdProceso);
                        break;
                    case 7:
                        await ComprobacionSaldosCatalogo(item.IdProceso);
                        break;
                    default:
                        break;
                }
            }
            _context.SaveChanges();
            return await ValidarPasos(pCierre);


            //return await Task.Run(() => Ok());


        }

        #endregion

    }



}


