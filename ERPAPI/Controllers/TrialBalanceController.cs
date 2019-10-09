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
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/TrialBalance")]
    [ApiController]
    public class TrialBalanceController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public TrialBalanceController(ILogger<TrialBalanceController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> TrialBalanceRes()
        {

            List<AccountingDTO> _categoria = new List<AccountingDTO>();

            _categoria= ObtenerCategoriarJerarquia(null);


            return await Task.Run(() => Json(_categoria));

        }

        private List<AccountingDTO> ObtenerCategoriarJerarquia(Int32? idCurso)
        {

            List<AccountingDTO> _acclistdto = ObtenerCategorias(idCurso);
            List<Accounting> categoriasList = (from c in _acclistdto
                                               select new Accounting
                                               {
                                                    AccountId = c.AccountId,
                                                    AccountBalance = c.AccountBalance,
                                                    AccountCode = c.AccountCode,
                                                    AccountName = c.AccountName,
                                                    ParentAccountId = c.ParentAccountId,
                                               }

                                                ).ToList();

            List<AccountingDTO> query = (from c in categoriasList
                                      where c.ParentAccountId == null || c.ParentAccountId == 0
                                      select new AccountingDTO
                                      {
                                          AccountId = c.AccountId,
                                          AccountName = c.AccountName,
                                          AccountBalance = c.AccountBalance,
                                          AccountCode = c.AccountCode,
                                          ParentAccountId = c.ParentAccountId,
                                          // ChildAccounts = ObtenerHijos(c.AccountId, categoriasList)
                                          Children = ObtenerHijos(c.AccountId, categoriasList)
                                      }).ToList();

            return query;
        }

        private List<AccountingDTO> ObtenerHijos(Int64 idCategoria, List<Accounting> categoriasList)
        {
           List<Accounting>  categoriasList2 =  _context.Accounting.ToList();
            List<AccountingDTO> query = (from c in categoriasList2
                                      let tieneHijos = categoriasList.Where(o => o.ParentAccountId == c.AccountId).Any()
                                      where c.ParentAccountId == idCategoria
                                      select new AccountingDTO
                                      {
                                          AccountId = c.AccountId,
                                          AccountName = c.AccountName,
                                          AccountBalance = c.AccountBalance,
                                          AccountCode = c.AccountCode,
                                          ParentAccountId = c.ParentAccountId,
                                          // ChildAccounts = tieneHijos ? ObtenerHijos(c.AccountId, categoriasList) : null,
                                          Children = ObtenerHijos(c.AccountId, categoriasList),
                                         // Debit = Debit(c.AccountId),
                                         // Credit = Credit(c.AccountId),

                                      }).ToList();



            return query;

        }


        private List<AccountingDTO> ObtenerCategorias(Int32? idCurso)
        {
            List<AccountingDTO> material = new List<AccountingDTO>();
            //using (var db = new _conte())
            //{
                try
                {
                    material = (from c in _context.Accounting
                                 .Where(q => q.ParentAccountId == null)
                                select new AccountingDTO
                                {
                                    AccountId = c.AccountId,
                                    AccountName = c.AccountName,
                                    AccountBalance = c.AccountBalance,
                                    AccountCode = c.AccountCode,
                                    ParentAccountId = c.ParentAccountId,
                                }
                                   ).ToList();

                        
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            //}

            return material;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetAccounting([FromBody]Fechas _Fecha)

        {
            List<AccountingDTO> Items = new List<AccountingDTO>();
            try
            {
                string trialbalance = "";
                string horainicio = " 00:00:00";
                string horafin = " 23:59:59";
                  trialbalance = "SELECT a.AccountId,a.AccountName,a.ParentAccountId,a.AccountCode "
                    + $" , dbo.[SumaCredito]('{_Fecha.FechaInicio.ToString("yyyy-MM-dd")}{horainicio}','{_Fecha.FechaFin.ToString("yyyy-MM-dd")}{horafin}',a.AccountId) as Credit"
                    + $" , dbo.[SumaDebito]('{_Fecha.FechaInicio.ToString("yyyy-MM-dd")}{horainicio}','{_Fecha.FechaFin.ToString("yyyy-MM-dd")}{horafin}',a.AccountId) as Debit"              
                    + $", dbo.[SumaDebito]('{_Fecha.FechaInicio.ToString("yyyy-MM-dd")}{horainicio}','{_Fecha.FechaFin.ToString("yyyy-MM-dd")}{horafin}',a.AccountId) -  "
                    + $" dbo.[SumaCredito]('{_Fecha.FechaInicio.ToString("yyyy-MM-dd")}{horainicio}','{_Fecha.FechaFin.ToString("yyyy-MM-dd")}{horafin}',a.AccountId) AccountBalance "
                    + $" FROM Accounting a       "
                   + " GROUP BY a.AccountId, a.AccountName,a.ParentAccountId,a.AccountCode ";


                using (var dr = await _context.Database.ExecuteSqlQueryAsync(trialbalance))
                {
                    // Output rows.
                    var reader = dr.DbDataReader;
                    while (reader.Read())
                    {
                        Items.Add(new AccountingDTO
                        {
                            AccountId = reader["AccountId"]== DBNull.Value ?0: Convert.ToInt64(reader["AccountId"]),
                            AccountName = reader["AccountName"]==DBNull.Value?"": Convert.ToString(reader["AccountName"]),
                            AccountCode = reader["AccountCode"] == DBNull.Value ? "" : Convert.ToString(reader["AccountCode"]),
                            ParentAccountId = reader["ParentAccountId"]==DBNull.Value ? 0 : Convert.ToInt32(reader["ParentAccountId"]),
                            Credit = reader["Credit"]==DBNull.Value?0 : Convert.ToDouble(reader["Credit"]),
                            Debit = reader["Debit"]==DBNull.Value ? 0: Convert.ToDouble(reader["Debit"]),
                            AccountBalance = reader["AccountBalance"]==DBNull.Value?0: Convert.ToDouble(reader["AccountBalance"]),
                        });
                       //Console.Write("{0}\t{1}\t{2} \n", reader[0], reader[1], reader[2]);
                    }
                }


                Items = (from c in Items
                       
                         select new AccountingDTO

                         {
                             AccountId = c.AccountId,
                             AccountName = c.AccountCode + "--" + c.AccountName,
                             ParentAccountId = c.ParentAccountId==0? null : c.ParentAccountId,
                             Credit = c.Credit,
                             Debit = c.Debit,
                             AccountBalance = c.AccountBalance

                         }).ToList();

             

                //List<Accounting> _cuentas = await _context.Accounting.ToListAsync();
                //Items =  (from c in _cuentas
                //          let tieneSaldo = _cuentas.Where(o => o.IsCash == true).Any()
                //          select new AccountingDTO
                //               {
                //                   AccountId = c.AccountId,
                //                   AccountName = c.AccountCode + "--" + c.AccountName,
                //                   ParentAccountId = c.ParentAccountId,
                //                   Credit = tieneSaldo? Credit(c.AccountId,_Fecha):0,
                //                   Debit = tieneSaldo?Debit(c.AccountId, _Fecha):0,
                //                   AccountBalance = Balance(c.AccountId,_Fecha)
                //               }
                //          )
                //               .ToList();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Totales([FromBody]Fechas _Fecha)

        {
            List<AccountingDTO> Items = new List<AccountingDTO>();
            try
            {
                string trialbalance = "";
                string horainicio = " 00:00:00";
                string horafin = " 23:59:59";

                trialbalance = "SELECT  "
                  + $"  COALESCE(dbo.[TotalCredito]('{_Fecha.FechaInicio.ToString("yyyy-MM-dd")}{horainicio}','{_Fecha.FechaFin.ToString("yyyy-MM-dd")}{horafin}'),0) as TotalCredit"
                  + $" , COALESCE(dbo.[TotalDebito]('{_Fecha.FechaInicio.ToString("yyyy-MM-dd")}{horainicio}','{_Fecha.FechaFin.ToString("yyyy-MM-dd")}{horafin}'),0) as TotalDebit"
                  + $", COALESCE(dbo.[TotalDebito]('{_Fecha.FechaInicio.ToString("yyyy-MM-dd")}{horainicio}','{_Fecha.FechaFin.ToString("yyyy-MM-dd")}{horafin}'),0) -  "
                  + $" COALESCE(dbo.[TotalCredito]('{_Fecha.FechaInicio.ToString("yyyy-MM-dd")}{horainicio}','{_Fecha.FechaFin.ToString("yyyy-MM-dd")}{horafin}'),0) AccountBalance "
                  + $"       "
                 + "  ";


                using (var dr = await _context.Database.ExecuteSqlQueryAsync(trialbalance))
                {
                    // Output rows.
                    var reader = dr.DbDataReader;
                    while (reader.Read())
                    {
                        Items.Add(new AccountingDTO
                        {
                            //AccountId = reader["AccountId"] == DBNull.Value ? 0 : Convert.ToInt64(reader["AccountId"]),
                            TotalCredit = reader["TotalCredit"] == DBNull.Value ? 0 : Convert.ToDouble(reader["TotalCredit"]),
                            TotalDebit = reader["TotalDebit"] == DBNull.Value ? 0 : Convert.ToDouble(reader["TotalDebit"]),                      
                            AccountBalance = reader["AccountBalance"] == DBNull.Value ? 0 : Convert.ToDouble(reader["AccountBalance"]),
                        });
                       
                    }
                }        

         
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));

        }



        private double Debit(Int64 AccountId,Fechas fechas)
        {

            //return _context.JournalEntryLine
            //        .Where(q => q.AccountId == AccountId).Sum(q => q.Debit);
            double debito = 0;
            debito = (from c in _context.JournalEntryLine
                     join d in _context.JournalEntry on c.JournalEntryId equals d.JournalEntryId
                     where c.AccountId == AccountId && (Convert.ToDateTime(d.Date.ToString("dd/MM/yyyy")) >= Convert.ToDateTime(fechas.FechaInicio.ToString("dd/MM/yyyy")) 
                      && Convert.ToDateTime(d.Date.ToString("dd/MM/yyyy")) <= Convert.ToDateTime(fechas.FechaInicio.ToString("dd/MM/yyyy")))
                     select c.Debit
                    ).Sum();

            return debito; 
                    
                   
        }

        private double Credit(Int64 AccountId, Fechas fechas)
        {
            //return _context.JournalEntryLine
            //        .Where(q => q.AccountId == AccountId).Sum(q => q.Credit);
            double credito = 0;
            credito = (from c in _context.JournalEntryLine
             join d in _context.JournalEntry on c.JournalEntryId equals d.JournalEntryId
                       where c.AccountId == AccountId && (Convert.ToDateTime(d.Date.ToString("dd/MM/yyyy")) >= Convert.ToDateTime(fechas.FechaInicio.ToString("dd/MM/yyyy"))
                                && Convert.ToDateTime(d.Date.ToString("dd/MM/yyyy")) <= Convert.ToDateTime(fechas.FechaInicio.ToString("dd/MM/yyyy")))
                       select c.Credit
                    ).Sum();
            return credito;
        }

        private double Balance(Int64 AccountId, Fechas fechas)
        {
            //return _context.JournalEntryLine
            //        .Where(q => q.AccountId == AccountId).Sum(q => q.Credit);

            return (from c in _context.JournalEntryLine
                    join d in _context.JournalEntry on c.JournalEntryId equals d.JournalEntryId
                    where c.AccountId == AccountId && (Convert.ToDateTime(d.Date.ToString("dd/MM/yyyy")) >= Convert.ToDateTime(fechas.FechaInicio.ToString("dd/MM/yyyy"))
                              && Convert.ToDateTime(d.Date.ToString("dd/MM/yyyy")) <= Convert.ToDateTime(fechas.FechaInicio.ToString("dd/MM/yyyy")))
                    select c.Debit - c.Credit
                    ).Sum();
        }





    }


 

}