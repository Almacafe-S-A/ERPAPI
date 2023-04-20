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
using System.Globalization;
using SQLitePCL;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;
using System.Security.Cryptography;

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
            proceso = _context.BitacoraCierreProceso
                .Where(q => q.IdProceso == ProcesoId)
                .Include(i => i.BitacoraCierresContable)
                .FirstOrDefault();

            if (proceso.Estatus == "FINALIZADO" ) return BadRequest("Este proceso ya ha sido ejecutado previmente el " + proceso.FechaCierre?.ToString("dd/MM/yyyy"));

            if (proceso.Estatus == "PROCESANDO") return BadRequest("Este proceso actualmente se encuentra en EJECUCION");

            proceso.Estatus = "PROCESANDO";

            _context.SaveChanges();


            BitacoraCierreContable cierre = new BitacoraCierreContable();
            cierre = _context.BitacoraCierreContable.Where(q => q.Id == proceso.IdBitacoraCierre).FirstOrDefault();         
            



            switch (proceso.PasoCierre)
            {
                case 1:
                    await VencimientoPolizas(proceso.IdProceso);
                    await VencimientoGarantiasBancarias(proceso.IdProceso);
                    break;

                case 2:
                    
                    await ValorMaxiomoCD(proceso.IdProceso);
                    //await Historicos(cierre.Id, proceso.IdProceso); ////HISTORICOS
                    break;

                case 3:
                    
                    await DepreciacionActivosFijos(proceso, cierre, new DateTime((int)cierre.Anio,(int)cierre.Mes,1));
                    break;

                case 5:
                    await DiferencialesCambiarios(proceso, proceso.IdProceso);
                    break;

                case 4:
                    await EjecucionPresupuestaria(proceso.IdProceso);
                    break;

                case 6:
                    await PartidaCierre(proceso);
                    break;
                case 7:
                    await PartidaApertura(proceso);
                    break;

                    
                default:
                    proceso.Estatus = "ERROR";
                    proceso.Mensaje = "No existe un proceso registrado";
                    _context.SaveChanges();
                    break;
            }
            proceso.FechaModificacion = DateTime.Now;
            proceso.FechaCierre = DateTime.Now;
            
            
            await ValidarPasos(cierre);

            _context.SaveChanges();


            


            
            

            return Ok();


            



        }



        private async Task<IActionResult> PartidaCierre(BitacoraCierreProcesos proceso) {
            using (var transaction = _context.Database.BeginTransaction())
            {


                try
                {


                    int anio = (int)proceso.BitacoraCierresContable.Anio;
                    int mes = 12;
                    int nivel = 12;
                    int centroconsto = 0;

                    List<BalanceSaldos> balanceSaldos = new List<BalanceSaldos>();

                    List<SqlParameter> parms = new List<SqlParameter>
                    {
                        // Create parameter(s)    
                        new SqlParameter { ParameterName = "@MES", Value = mes },
                        new SqlParameter { ParameterName = "@ANIO", Value = anio },
                        new SqlParameter { ParameterName = "@NIVEL", Value = nivel },
                        new SqlParameter { ParameterName = "@CENTROCOSTO", Value = centroconsto },

                    };

                    string sql = @"EXEC	 [dbo].[GenerarBalanceComparativo2]
		                        @MES,
		                        @ANIO,
		                        @NIVEL,
		                        @CENTROCOSTO";

                    balanceSaldos = await _context.BalanceSaldos.FromSql<BalanceSaldos>(sql, parms.ToArray())
                        .ToListAsync();



                    List<BalanceSaldosDTO> cuentaspartida = (from c in balanceSaldos
                                                             join a in _context.Accounting on c.AccountId equals (int)a.AccountId
                                                             select new BalanceSaldosDTO
                                                             {
                                                                 AccountCode = c.AccountCode,
                                                                 AccountId = c.AccountId,
                                                                 AñoActual = c.AñoActual,
                                                                 BloqueoDiarios = a.BlockedInJournal,
                                                                 Debe = c.Debe,
                                                                 Haber = c.Haber,
                                                                 Descripcion = c.Descripcion,
                                                                 DeudoraAcreedora = c.DeudoraAcreedora,
                                                                 Estado = c.Estado,
                                                                 ParentAccountId = c.ParentAccountId,
                                                                 Totaliza = c.Totaliza
                                                             }).Where(q => q.BloqueoDiarios == false && q.AñoActual > 0).OrderBy(o => o.AccountId).ToList();




                    JournalEntry partidaCierre = new JournalEntry
                    {
                        Date = DateTime.Now,
                        DatePosted = new DateTime(anio, mes, 31),
                        CreatedUser = User.Identity.Name,
                        CreatedDate = DateTime.Now,
                        EstadoId = 5,
                        EstadoName = "Enviada a Aprobación",
                        PeriodoId = proceso.BitacoraCierresContable.PeriodoId,
                        TypeOfAdjustmentId = 65,
                        TypeOfAdjustmentName = "Partida Cierre",
                        JournalEntryLines = new List<JournalEntryLine>(),
                        Memo = "Partida de Cierrre " + anio,
                        Periodo = anio.ToString(),
                        Posted = false,
                        TotalCredit = 0,
                        TotalDebit = 0,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = User.Identity.Name,




                    };


                    partidaCierre.JournalEntryLines = (from c in cuentaspartida

                                                       select new JournalEntryLine
                                                       {
                                                           AccountId = (int)c.AccountId,
                                                           AccountName = $"{c.AccountCode}- {c.Descripcion}",
                                                           CostCenterId = 1,
                                                           CostCenterName = "San Pedro Sula",
                                                           Debit = c.DeudoraAcreedora == "1" ? 0 : (decimal)c.AñoActual,
                                                           Credit = c.DeudoraAcreedora == "2" ? 0 : (decimal)c.AñoActual,
                                                           CreatedDate = DateTime.Now,
                                                           CreatedUser = User.Identity.Name,
                                                           ModifiedUser = User.Identity.Name,
                                                           ModifiedDate = DateTime.Now,

                                                       }).ToList();




                    partidaCierre.TotalDebit = partidaCierre.JournalEntryLines.Sum(s => s.Debit);
                    partidaCierre.TotalCredit = partidaCierre.JournalEntryLines.Sum(s => s.Credit);

                    JournalEntryLine jel = partidaCierre.JournalEntryLines.Where(q => q.AccountId == 1279).FirstOrDefault();

                    if (jel != null)
                    {
                        partidaCierre.JournalEntryLines.Remove(jel);
                    }

                    decimal utilidad = (decimal)cuentaspartida.Where(q => q.AccountId == 1279).FirstOrDefault().AñoActual;

                    partidaCierre.JournalEntryLines.Add(new JournalEntryLine
                    {
                        AccountId = 924,
                        AccountName = $"32501--UTILIDAD DEL PERÍODO",
                        CostCenterId = 1,
                        CostCenterName = "San Pedro Sula",
                        Debit = 0,
                        Credit = utilidad,
                        CreatedDate = DateTime.Now,
                        CreatedUser = User.Identity.Name,
                        ModifiedUser = User.Identity.Name,
                        ModifiedDate = DateTime.Now,



                    });

                    partidaCierre.TotalDebit = partidaCierre.JournalEntryLines.Sum(s => s.Debit);
                    partidaCierre.TotalCredit = partidaCierre.JournalEntryLines.Sum(s => s.Credit);




                    JournalClosing journalClosing = new JournalClosing
                    {
                        YearClosed = anio,
                        JournalEntry = partidaCierre,

                    };

                    _context.JournalClosings.Add(journalClosing);

                    await _context.SaveChangesAsync();

                    proceso.Estatus = "FINALIZADO";
                    proceso.Asientos = partidaCierre.JournalEntryId.ToString();
                    proceso.Mensaje = "";

                    await _context.SaveChangesAsync();

                    
                    transaction.Commit();

                    return Ok(partidaCierre);

                }
                catch (Exception ex)
                {

                    transaction.Rollback();
                    proceso.Estatus = "ERROR";
                    _logger.LogError(ex.ToString());
                    //throw;
                    await _context.SaveChangesAsync();
                    return BadRequest("Error en la ejecucion del cierre");
                }

                finally { transaction.Dispose(); }

            }

        }


        private async Task<IActionResult> PartidaApertura(BitacoraCierreProcesos proceso)
        {


            int anio = (int)proceso.BitacoraCierresContable.Anio;
            int mes = 12;
            int nivel = 12;
            int centroconsto = 0;

            List<BalanceSaldos> balanceSaldos= new List<BalanceSaldos>();


            List<SqlParameter> parms = new List<SqlParameter>
                    {
                // Create parameter(s)    
                        new SqlParameter { ParameterName = "@MES", Value = mes },
                        new SqlParameter { ParameterName = "@ANIO", Value = anio },
                        new SqlParameter { ParameterName = "@NIVEL", Value = nivel },
                        new SqlParameter { ParameterName = "@CENTROCOSTO", Value = centroconsto },

                    };

            string sql = @"EXEC	 [dbo].[GenerarBalanceComparativo2]
		                        @MES,
		                        @ANIO,
		                        @NIVEL,
		                        @CENTROCOSTO";

            balanceSaldos = await _context.BalanceSaldos.FromSql<BalanceSaldos>(sql, parms.ToArray())
                .ToListAsync();


            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {

                    int anioapertura = anio + 1;

                    JournalEntry partidaApertura = new JournalEntry
                    {
                        Date = DateTime.Now,
                        DatePosted = new DateTime(anio, mes, 31),
                        CreatedUser = User.Identity.Name,
                        CreatedDate = DateTime.Now,
                        EstadoId = 5,
                        EstadoName = "Enviada a Aprobación",
                        PeriodoId = proceso.BitacoraCierresContable.PeriodoId,
                        TypeOfAdjustmentId = 65,
                        TypeOfAdjustmentName = "Partida Apertura",
                        JournalEntryLines = new List<JournalEntryLine>(),
                        Memo = "Partida de Apertura " + anioapertura,
                        Periodo = (anio+1).ToString(),
                        Posted = false,
                        TotalCredit = 0,
                        TotalDebit = 0,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = User.Identity.Name,

                        


                    };


                    BalanceSaldos utilitdad = balanceSaldos.Where(q => q.AccountId == 1279).FirstOrDefault();

                    if (utilitdad == null)
                    {
                        throw new Exception("Utillitadad nula");
                    }

                    Accounting aniosanteriores = await _context.Accounting.Where(q => q.AccountCode == "32401").FirstOrDefaultAsync();





                    partidaApertura.JournalEntryLines.Add(new JournalEntryLine
                    {
                        AccountId = 924,
                        AccountName = $"32501--UTILIDAD DEL PERÍODO",
                        CostCenterId = 1,
                        CostCenterName = "San Pedro Sula",
                        Debit = (decimal)utilitdad.AñoActual,
                        Credit = 0,
                        CreatedDate = DateTime.Now,
                        CreatedUser = User.Identity.Name,
                        ModifiedUser = User.Identity.Name,
                        ModifiedDate = DateTime.Now,



                    });

                    partidaApertura.JournalEntryLines.Add(new JournalEntryLine
                    {
                        AccountId = (int)aniosanteriores.AccountId,
                        AccountName = $"{aniosanteriores.AccountCode} -- {aniosanteriores.AccountName}",
                        CostCenterId = 1,
                        CostCenterName = "San Pedro Sula",
                        Debit = 0,
                        Credit = (decimal)utilitdad.AñoActual,
                        CreatedDate = DateTime.Now,
                        CreatedUser = User.Identity.Name,
                        ModifiedUser = User.Identity.Name,
                        ModifiedDate = DateTime.Now,



                    });


                    partidaApertura.TotalDebit = partidaApertura.JournalEntryLines.Sum(s => s.Debit);
                    partidaApertura.TotalCredit = partidaApertura.JournalEntryLines.Sum(s => s.Credit);

                    _context.JournalEntry.Add(partidaApertura);



                    _context.SaveChanges();

                    proceso.Estatus = "FINALIZADO";
                    proceso.Asientos = partidaApertura.JournalEntryId.ToString();
                    proceso.Mensaje = "";

                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {

                    transaction.Rollback();
                    proceso.Estatus = "ERROR";
                    _logger.LogError(ex.ToString());
                    //throw;
                    await _context.SaveChangesAsync();
                    return BadRequest("Error en la ejecucion en partidad de Apertura");
                }



            }



            return Ok();



        }




        [HttpGet("[action]")]
        public async Task<IActionResult> ActualizarSaldoCatalogo()
        {

            await _context.Database.ExecuteSqlCommandAsync("[ActualizarSaldoCatalogoContable]"); ////Ejecuta SP Cierres

            return Ok();


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
                            //AccountId = detalle.AccountId,
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
                    var depreciaciongrupos = await _context.FixedAssetGroup
                        .Include(q => q.DepreciationFixedAssetAccounting)
                        .Include(q => q.AccumulatedDepreciationAccounting)
                        .Include(c => c.FixedAssetAccounting).ToListAsync();


                    JournalEntry _je = new JournalEntry
                    {
                        Date = DateTime.Now,
                        Memo = $"Depreciacion de Activos {pfecha.ToString("MMMM", CultureInfo.GetCultureInfo("es-HN"))} {pfecha.ToString("yyyy")}",
                        DatePosted = pfecha,
                        ModifiedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedUser = User.Identity.Name,
                        CreatedUser = User.Identity.Name,
                        //DocumentId = item.FixedAssetId,
                        VoucherType = 21,
                        TypeJournalName = "Depreciacion",
                        EstadoId = 5,
                        EstadoName = "Enviado a Aprobacion",
                        TypeOfAdjustmentName = "Cierre Mensual",
                        TypeOfAdjustmentId = 65,


                        //VoucherType = Convert.ToInt32(tipoDocumento.IdTipoDocumento),                      

                    };

                    List<CostCenter> costCenters = _context.CostCenter.Where(q => q.IdEstado == 1).ToList();
                    foreach (var costCenter in costCenters)
                    {
                        foreach (var grupo in depreciaciongrupos)
                        {
                            if (grupo.DepreciationFixedAssetAccounting == null || grupo.AccumulatedDepreciationAccounting == null)
                            {
                                continue;
                            }

                            JournalEntryLine jelDepreciacion = new JournalEntryLine
                            {
                                JournalEntryId = _je.JournalEntryId,
                                AccountId = Convert.ToInt32(grupo.DepreciationFixedAssetAccounting.AccountId),
                                AccountName = $"{grupo.DepreciationFixedAssetAccounting.AccountCode} - {grupo.DepreciationFixedAssetAccounting.AccountName}",
                                Debit = 0,
                                CostCenterId = costCenter.CostCenterId,
                                CostCenterName = costCenter.CostCenterName,
                                CreatedUser = User.Identity.Name,
                                CreatedDate = DateTime.Now,
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = User.Identity.Name

                            };

                            JournalEntryLine jelDepreciacionMensual = new JournalEntryLine
                            {
                                JournalEntryId = _je.JournalEntryId,
                                AccountId = Convert.ToInt32(grupo.AccumulatedDepreciationAccounting.AccountId),
                                AccountName = $"{grupo.AccumulatedDepreciationAccounting.AccountCode} - {grupo.AccumulatedDepreciationAccounting.AccountName}",
                                Credit = 0,
                                CostCenterId = costCenter.CostCenterId,
                                CostCenterName = costCenter.CostCenterName,
                                CreatedUser = User.Identity.Name,
                                CreatedDate = DateTime.Now,
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = User.Identity.Name
                            };


                            var activos = await _context.FixedAsset
                                .Where(p => p.IdEstado != 109
                                && p.Estado != "Depreciado"
                                && p.FixedAssetGroupId == grupo.FixedAssetGroupId
                                && p.CenterCostId == costCenter.CostCenterId
                                //&& p.AssetDate <= new DateTime(pfecha.Year, pfecha.Month-1, DateTime.DaysInMonth(pfecha.Year, pfecha.Month-1))
                                )
                                .ToListAsync();
                            foreach (var item in activos)
                            {
                                var adepreciar = item.TotalDepreciated;                                

                                if (adepreciar > item.NetValue)
                                {
                                    adepreciar = item.NetValue;
                                    item.IdEstado = 109;
                                    item.Estado = "Depreciado";
                                }
                                else
                                {
                                    item.IdEstado = 47;
                                    item.Estado = "Depreciandose";
                                }



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



                                jelDepreciacionMensual.Credit += adepreciar;
                                jelDepreciacion.Debit += adepreciar;
                            }


                            if (jelDepreciacion.Credit == 0 && jelDepreciacion.Debit == 0)
                            {

                                continue;
                            }
                            _je.JournalEntryLines.Add(jelDepreciacion);
                            _je.JournalEntryLines.Add(jelDepreciacionMensual);

                            //_je.JournalEntryLines = new List<JournalEntryLine>();


                        }
                    }

                    




                    _je.TotalDebit = _je.JournalEntryLines.Sum(s => s.Debit);
                    _je.TotalCredit = _je.JournalEntryLines.Sum(s => s.Credit);
                    
                    Periodo periodo= _context.Periodo.Where(q=>q.Anio == pCierre.Anio).FirstOrDefault();

                    _je.PeriodoId = periodo?.Id;
                    _je.Periodo = periodo.Anio.ToString();
                    _je.TypeJournalName = "Voucher de Registros";
                    _je.VoucherType = 9;
                    _je.DatePosted = new DateTime(pfecha.Year, pfecha.Month, DateTime.DaysInMonth(pfecha.Year, pfecha.Month));

                    _context.JournalEntry.Add(_je);

                    pProceso.Estatus = "FINALIZADO";
                    pProceso.Mensaje = "";
                    pProceso.FechaCierre = System.DateTime.Now;
                    await _context.SaveChangesAsync();
                   
                    transaction.Commit();
                    pProceso.Asientos = _je.JournalEntryId.ToString();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    pProceso.Estatus = "ERROR";
                    _logger.LogError(ex.ToString());
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
            BitacoraCierreProcesos proceso = await _context.BitacoraCierreProceso
                .Where(w => w.IdProceso == procesoId)
                .Include(i => i.BitacoraCierresContable)
                .FirstOrDefaultAsync();

            proceso.Estatus = "EJECUTANDO";
            _context.BitacoraCierreProceso.Update(proceso);
            await _context.SaveChangesAsync();

            Periodo periodo = await _context.Periodo.Where(q => q.Id == proceso.BitacoraCierresContable.PeriodoId).FirstOrDefaultAsync();
            List<Presupuesto> cuentasPresupuesto = await _context.Presupuesto.Where(q => q.PeriodoId == periodo.Id).ToListAsync();
            int mes = (int)proceso.BitacoraCierresContable.Mes;
            int anio = (int) proceso.BitacoraCierresContable.Anio;
            foreach (var cuentaPresupuestada in cuentasPresupuesto)
            {
               

                List<JournalEntryLine> detalleasientos = await _context.JournalEntryLine.Where(j => 
                j.JournalEntry.PeriodoId == periodo.Id
                && j.AccountId == cuentaPresupuestada.AccountigId
                &&j.JournalEntry.DatePosted < new DateTime(anio, mes + 1, 1) 
                && j.JournalEntry.DatePosted >= new DateTime(anio, mes , 1) 
                && j.JournalEntry.EstadoId ==6)
                    .Include(j => j.JournalEntry)
                    .ToListAsync();
                if (detalleasientos.Count == 0)
                {
                    continue;

                }
                decimal credito = detalleasientos.Sum(s => s.Credit);
                decimal debito = detalleasientos.Sum(s => s.Debit);

                decimal balance = 0;

                Accounting cuenta = await _context.Accounting.Where(q => q.AccountId == cuentaPresupuestada.AccountigId).FirstOrDefaultAsync();

                balance = cuenta.DeudoraAcreedora == "D" ? debito - credito : credito - debito;


                switch (mes)
                {
                    case 1:
                        cuentaPresupuestada.EjecucionEnero = Convert.ToDecimal(balance);
                        break;
                    case 2:
                        cuentaPresupuestada.EjecucionFebrero = Convert.ToDecimal(balance);
                        break;
                    case 3:
                        cuentaPresupuestada.EjecucionMarzo = Convert.ToDecimal(balance);
                        break;
                    case 4:
                        cuentaPresupuestada.EjecucionAbril = Convert.ToDecimal(balance);
                        break;
                    case 5:
                        cuentaPresupuestada.EjecucionMayo = Convert.ToDecimal(balance);
                        break;
                    case 6:
                        cuentaPresupuestada.EjecucionJunio = Convert.ToDecimal(balance);
                        break;
                    case 7:
                        cuentaPresupuestada.EjecucionJulio = Convert.ToDecimal(balance);
                        break;
                    case 8:
                        cuentaPresupuestada.EjecucionAgosto = Convert.ToDecimal(balance);
                        break;
                    case 9:
                        cuentaPresupuestada.EjecucionSeptiembre = Convert.ToDecimal(balance);
                        break;
                    case 10:
                        cuentaPresupuestada.EjecucionOctubre = Convert.ToDecimal(balance);
                        break;
                    case 11:
                        cuentaPresupuestada.EjecucionNoviembre = Convert.ToDecimal(balance);
                        break;
                    case 12:
                        cuentaPresupuestada.EjecucionDiciembre = Convert.ToDecimal(balance);
                        break;
                    default:
                        break;
                }
                cuentaPresupuestada.TotalMontoEjecucion = (cuentaPresupuestada.EjecucionEnero + cuentaPresupuestada.EjecucionFebrero +
                           cuentaPresupuestada.EjecucionMarzo + cuentaPresupuestada.EjecucionAbril + cuentaPresupuestada.EjecucionMayo +
                           cuentaPresupuestada.EjecucionJunio + cuentaPresupuestada.EjecucionJulio + cuentaPresupuestada.EjecucionAgosto +
                          cuentaPresupuestada.EjecucionSeptiembre + cuentaPresupuestada.EjecucionOctubre + cuentaPresupuestada.EjecucionNoviembre +
                          cuentaPresupuestada.EjecucionDiciembre);
            }

            proceso.FechaCierre = DateTime.Now;
            proceso.FechaModificacion = DateTime.Now;
            proceso.FechaCreacion= DateTime.Now;
            proceso.FechaCierre = DateTime.Now;
            proceso.Asientos = "Este procceso no genera asientos contables";
            proceso.Mensaje = "Se ha actualizado el saldo del catalogo de cuentas";
            proceso.UsuarioCreacion = User.Identity.Name;
            proceso.UsuarioModificacion = User.Identity.Name;
            proceso.Estatus = "FINALIZADO";
            await _context.SaveChangesAsync();


        }


        private async Task ValidarPasos(BitacoraCierreContable pCierre)
        {
            List<BitacoraCierreProcesos> procesos = await _context.BitacoraCierreProceso
                           .Where(b => b.IdBitacoraCierre == pCierre.Id)
                           .ToListAsync();
            
            foreach (var item in procesos) ////REVISA QUE ALGUN PASO NO TENGA ERROR EN EL CIERRE AL VOLVER A EJECUTAR
            {

                if (item.Estatus != "FINALIZADO"  )
                {

                    pCierre.Estatus = "En PROCESO";
                    pCierre.FechaModificacion = DateTime.Now;
                    _context.SaveChanges();
                    return;

                }

            }

            pCierre.Estatus = "FINALIZADO";
            pCierre.FechaCierre= DateTime.Now;
            await _context.SaveChangesAsync();

        }




        #endregion

    }



}


