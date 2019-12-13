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
        /// Realiza un Cierre Contable
        /// </summary>
        /// <returns></returns>    
        [HttpPost("[action]")]
        public async Task<IActionResult> EjecutarCierreContable([FromBody]BitacoraCierreContable pBitacoraCierre)
        {

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    BitacoraCierreContable existeCierre = await _context.BitacoraCierreContable.Where(b => b.FechaCierre.Date == pBitacoraCierre.FechaCierre.Date).FirstOrDefaultAsync();
                    if (existeCierre != null)
                    {
                        return await Task.Run(() => BadRequest("Ya existe un Cierre Contable para esta Fecha"));
                    }
                    BitacoraCierreContable cierre = new BitacoraCierreContable
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
                        FechaCierre = DateTime.Now,
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
                        FechaCierre = DateTime.Now,
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
                        FechaCierre = DateTime.Now,
                        FechaCreacion = DateTime.Now,

                    };
                    _context.BitacoraCierreProceso.Add(proceso1);
                    _context.BitacoraCierreProceso.Add(proceso2);
                    _context.BitacoraCierreProceso.Add(proceso3);

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
                        proceso3.Estatus = "FINALIZADO";

                        if (SumaPolizas > 0)
                        {
                            TiposDocumento tipoDocumento = _context.TiposDocumento.Where(d => d.Descripcion == "Polizas").FirstOrDefault();
                            JournalEntryConfiguration _journalentryconfiguration = await (_context.JournalEntryConfiguration
                                                                       .Where(q => q.TransactionId == tipoDocumento.IdTipoDocumento)
                                                                       //.Where(q => q.BranchId == _Invoiceq.BranchId)
                                                                       .Where(q => q.EstadoName == "Activo")
                                                                       .Include(q => q.JournalEntryConfigurationLine)
                                                                       ).FirstOrDefaultAsync();


                            double sumacreditos = 0, sumadebitos = 0;
                            if (_journalentryconfiguration != null)
                            {
                                //Crear el asiento contable configurado
                                //.............................///////
                                JournalEntry _je = new JournalEntry
                                {
                                    Date = pBitacoraCierre.FechaCierre,
                                    Memo = "Vecimiento de Polizas",
                                    DatePosted = pBitacoraCierre.FechaCierre,
                                    ModifiedDate = DateTime.Now,
                                    CreatedDate = DateTime.Now,
                                    ModifiedUser = pBitacoraCierre.UsuarioCreacion,
                                    CreatedUser = pBitacoraCierre.UsuarioCreacion,
                                    DocumentId = pBitacoraCierre.Id,
                                    TypeOfAdjustmentId = 65,
                                    VoucherType = Convert.ToInt32(tipoDocumento.IdTipoDocumento),

                                };



                                foreach (var item in _journalentryconfiguration.JournalEntryConfigurationLine)
                                {


                                    _je.JournalEntryLines.Add(new JournalEntryLine
                                    {
                                        AccountId = Convert.ToInt32(item.AccountId),
                                        AccountName = item.AccountName,
                                        Description = item.AccountName,
                                        Credit = item.DebitCredit == "Credito" ? SumaPolizas : 0,
                                        Debit = item.DebitCredit == "Debito" ? SumaPolizas : 0,
                                        CreatedDate = DateTime.Now,
                                        ModifiedDate = DateTime.Now,
                                        CreatedUser = pBitacoraCierre.UsuarioCreacion,
                                        ModifiedUser = pBitacoraCierre.UsuarioModificacion,
                                        Memo = "",
                                    });

                                    // sumacreditos += item.DebitCredit == "Credito" ? _Invoiceq.Tax + _Invoiceq.Tax18 : 0;
                                    //sumadebitos += item.DebitCredit == "Debito" ? _Invoiceq.Tax + _Invoiceq.Tax18 : 0;

                                }
                            }
                        }
                    }
                    else
                    {
                        proceso3.Estatus = "FINALIZADO";
                        //proceso3.Mensaje = "FINALIZADO No se encontraron Polizas Vencidas";
                    }

                    /////////////Fin del Paso 3

                    _context.SaveChanges();

                    //List< BitacoraCierreProcesos> spCierre = await _context.BitacoraCierreProceso.FromSql("Cierres @p0, @p1, @p2", pBitacoraCierre.FechaCierre, cierre.Id).ToListAsync();
                    _context.Database.ExecuteSqlCommand("Cierres @p0, @p1", pBitacoraCierre.FechaCierre, cierre.Id);

                    transaction.Commit();
                    return await Task.Run(() => Ok());
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return await Task.Run(() => BadRequest(ex.Message));
                }




            }

        }


    }


}