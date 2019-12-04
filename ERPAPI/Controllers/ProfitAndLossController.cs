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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/ProfitAndLoss")]
    [ApiController]
    public class ProfitAndLossController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ProfitAndLossController(ILogger<ProfitAndLossController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> GetProfitAndLoss([FromBody]Fechas _Fecha)
        {
            List<AccountingDTO> Items = new List<AccountingDTO>();
            List<AccountingDTO> ParentsValues = new List<AccountingDTO>();
            try
            {
                string profitandloss = "";
                string horainicio = " 00:00:00";
                string horafin = " 23:59:59";
                string typeaccountsprofitandloss = "5,6";
                                                                                                                                       
              profitandloss =$"  SELECT T1.AccountId, T2.AccountName , T2.ParentAccountId , T2.AccountCode                                  " +
                    $" , (SELECT ta.TypeAccountName FROM TypeAccount ta WHERE ta.TypeAccountId = t2.TypeAccountId) as 'Tipo de cuenta'    " +
                    $" , Isnull((SELECT SUM(T3.Debit - T3.Credit) FROM JournalEntry T2                                                    " +
                    $"   INNER JOIN JournalEntryLine  T3 ON T2.JournalEntryId = T3.JournalEntryId                                       " +
                    $"      WHERE DateDiff(dd, T2.Date, '{_Fecha.FechaInicio.ToString("yyyy-MM-dd")}{horainicio}') > 0 AND T3.AccountId LIKE T1.AccountId   " +
                    $"       GROUP BY T3.AccountId),0) 'Opening balance',                                                               " +
                    $" SUM(T1.Debit) 'Debit', SUM(T1.Credit) 'Credit',                                                                    " +
                    $" SUM(T1.Debit - T1.Credit) AS 'AccountBalance'                                                                             " +
                    $" FROM JournalEntry  T0                                                                                              " +
                    $" INNER JOIN JournalEntryLine T1 ON T0.JournalEntryId = T1.JournalEntryId                                            " +
                    $" INNER JOIN Accounting T2 ON T1.AccountId = T2.AccountId                                                            " +
                    $" WHERE T0.Date >= '{_Fecha.FechaInicio.ToString("yyyy-MM-dd")}{horainicio}' AND T0.Date <= '{_Fecha.FechaFin.ToString("yyyy-MM-dd")}{horafin}'     " +
                    $" and t2.TypeAccountId in ({typeaccountsprofitandloss})                                                                                     " +
                    $" GROUP BY                                                                                                           " +
                    $" T1.AccountId, T2.AccountName, t2.TypeAccountId ,T2.ParentAccountId  ,T2.AccountCode                     " +
                    $" Having SUM(T1.Debit - T1.Credit) != 0                                                                              " +
                    $"                                                                                                                    " +
                    $"                                                                                                                    ";


                List<Int64> Parents = new List<long>();
                double ingresos = 0 , gastos = 0;
                using (var dr = await _context.Database.ExecuteSqlQueryAsync(profitandloss))
                {                   
                    var reader = dr.DbDataReader;
                    while (reader.Read())
                    {
                        string _ParentAccountId = reader["AccountCode"] == DBNull.Value ? "" : Convert.ToString(reader["AccountCode"]);
                        string parentaccount = _ParentAccountId.Substring(0, 1);
                        if (parentaccount == "5")
                        {
                            ingresos += (reader["AccountBalance"] == DBNull.Value ? 0 : Convert.ToDouble(reader["AccountBalance"]));
                        }
                        else if (parentaccount == "6")
                        {
                            gastos += (reader["AccountBalance"] == DBNull.Value ? 0 : Convert.ToDouble(reader["AccountBalance"]));
                        }

                            Items.Add(new AccountingDTO
                        {
                            AccountId = reader["AccountId"] == DBNull.Value ? 0 : Convert.ToInt64(reader["AccountId"]),
                            AccountName = reader["AccountName"] == DBNull.Value ? "" : Convert.ToString(reader["AccountName"]),
                            AccountCode = reader["AccountCode"] == DBNull.Value ? "" : Convert.ToString(reader["AccountCode"]),
                            ParentAccountId = reader["ParentAccountId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ParentAccountId"]),
                            Credit = reader["Credit"] == DBNull.Value ? 0 : Convert.ToDouble(reader["Credit"]),
                            Debit = reader["Debit"] == DBNull.Value ? 0 : Convert.ToDouble(reader["Debit"]),
                            AccountBalance = reader["AccountBalance"] == DBNull.Value ? 0 : Convert.ToDouble(reader["AccountBalance"]),
                        });

                        Parents.Add(reader["ParentAccountId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ParentAccountId"]));

                        if(reader["ParentAccountId"] != DBNull.Value)
                        {
                            AccountingDTO _ac = ParentsValues.Where(q => q.AccountId == Convert.ToInt32(reader["ParentAccountId"])).FirstOrDefault();
                            if(_ac == null)
                            {
                                ParentsValues.Add(new AccountingDTO
                                {
                                    //AccountId = reader["AccountId"] == DBNull.Value ? 0 : Convert.ToInt64(reader["AccountId"]),
                                    AccountId = reader["ParentAccountId"] == DBNull.Value ? 0 : Convert.ToInt64(reader["ParentAccountId"]),
                                    AccountName = "",
                                    AccountCode = "",
                                    ParentAccountId = 0,
                                    Credit = reader["Credit"] == DBNull.Value ? 0 : Convert.ToDouble(reader["Credit"]),
                                    Debit = reader["Debit"] == DBNull.Value ? 0 : Convert.ToDouble(reader["Debit"]),
                                    //AccountBalance = reader["AccountBalance"] == DBNull.Value ? 0 : Convert.ToDouble(reader["AccountBalance"]),
                                    AccountBalance = (reader["Credit"] == DBNull.Value ? 0 : Convert.ToDouble(reader["Credit"])) - (reader["Debit"] == DBNull.Value ? 0 : Convert.ToDouble(reader["Debit"])),
                                });
                            }
                            else
                            {
                                if(ParentsValues.Remove(_ac))
                                {
                                    double credit, debit = 0;
                                    credit = reader["Credit"] == DBNull.Value ? 0 : Convert.ToDouble(reader["Credit"]);
                                    _ac.Credit += credit;
                                    debit = reader["Debit"] == DBNull.Value ? 0 : Convert.ToDouble(reader["Debit"]);
                                    _ac.Debit += debit;
                                    //accoutbalance = reader["AccountBalance"] == DBNull.Value ? 0 : Convert.ToDouble(reader["AccountBalance"]);
                                    _ac.AccountBalance = _ac.Credit - _ac.Debit;
                                    ParentsValues.Add(new AccountingDTO
                                    {
                                        AccountId = _ac.AccountId,
                                        AccountName = "",
                                        AccountCode = "",
                                        ParentAccountId = 0,
                                        Credit = _ac.Credit,
                                        Debit = _ac.Debit,
                                        AccountBalance = _ac.AccountBalance,
                                    });
                                }
                            }
                        }
                        
                    }
                }

                //ingresos = Math.Abs(ingresos);
                //gastos = Math.Abs(gastos);
                Items.Add(new AccountingDTO
                {
                    AccountId = 9999999999,
                    AccountName = "Utilidades o perdidas",
                    AccountCode = "99999999",
                    AccountBalance = ingresos - gastos,
                    ParentAccountId = null
                });

                //Cuentas principales
                //foreach (var padre in typeaccountsprofitandloss.Split(","))
                //{
                //    Accounting _ac = await _context.Accounting.Where(q => q.AccountCode == padre).FirstOrDefaultAsync();
                //    Items.Add(new AccountingDTO
                //    {
                //       AccountId = _ac.AccountId,
                //       AccountName = _ac.AccountName,
                //       AccountCode = _ac.AccountCode,
                //       AccountBalance = _ac.AccountBalance,
                //       ParentAccountId = _ac.ParentAccountId
                //    });
                //}

                query = null;
                query = new List<AccountingDTO>();
                //Padres de las cuentas con saldo
                List<AccountingDTO> _categoria = new List<AccountingDTO>();
                _categoria = ObtenerCategoriarJerarquia(ParentsValues);

                //foreach (var padre in Items)
                //{
                //    AccountingDTO _ac = _categoria.Where(q => q.AccountId == padre.AccountId).FirstOrDefault();

                //    if (_ac != null)
                //    {
                //        _categoria.Remove(_ac);
                //    }
                //}

                Items.AddRange(_categoria);


                Items = (from c in Items
                         .OrderBy(q => q.AccountId)
                         select new AccountingDTO
                         {
                             AccountId = c.AccountId,
                             AccountName = c.AccountCode + "--" + c.AccountName,
                             AccountCode = c.AccountCode,
                             ParentAccountId = c.ParentAccountId == 0 ? null : c.ParentAccountId,
                             Credit = c.Credit,
                             Debit = c.Debit,
                             AccountBalance = c.AccountBalance

                         }).ToList();


            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                throw ex;
            }

            return await Task.Run(() => Ok(Items));
        }



        List<AccountingDTO> query = new List<AccountingDTO>();


        private List<AccountingDTO> ObtenerCategoriarJerarquia(List<AccountingDTO> Parents)
        {
            List<AccountingDTO> Items = new List<AccountingDTO>();
            List<AccountingDTO> _padre = new List<AccountingDTO>();
            foreach (var padre in Parents)
            {
                Accounting _ac =  _context.Accounting.Where(q => q.AccountId == padre.AccountId).FirstOrDefault();
              
                if (_ac.ParentAccountId != null)
                {
                    if (_padre.Where(q => q.AccountId == _ac.ParentAccountId).Count() == 0)
                    {
                        _padre.Add(new AccountingDTO
                        {
                            AccountId = _ac.ParentAccountId.Value,
                            AccountName = "",
                            AccountCode = "",
                            ParentAccountId = 0,
                            Credit = padre.Credit,
                            Debit = padre.Debit,
                            AccountBalance = padre.Debit - padre.Credit,
                            //AccountBalance = padre.AccountBalance,
                        });
                    }
                    else
                    {
                        AccountingDTO _cuenta = _padre.Where(q => q.AccountId == _ac.ParentAccountId).FirstOrDefault();
                        _padre.Remove(_cuenta);
                        _padre.Add(new AccountingDTO
                        {
                            AccountId = _cuenta.AccountId,
                            AccountName = "",
                            AccountCode = "",
                            ParentAccountId = 0,
                            Credit = padre.Credit + _cuenta.Credit,
                            Debit = padre.Debit + _cuenta.Debit,
                            AccountBalance = (padre.Debit + _cuenta.Debit) - (padre.Credit + _cuenta.Credit),
                            //AccountBalance = padre.AccountBalance,
                        });
                    }
                    //_padre.Add(_ac.ParentAccountId.Value);
                }

                Items.Add(new AccountingDTO
                {
                    AccountId = _ac.AccountId,
                    AccountName = _ac.AccountName,
                    AccountCode = _ac.AccountCode,
                    Credit = padre.Credit,
                    Debit = padre.Debit,
                    //AccountBalance = padre.AccountBalance,
                    AccountBalance = padre.Debit - padre.Credit,
                    ParentAccountId = _ac.ParentAccountId,
                });
            }

            List<AccountingDTO> categoriasList = (from c in Items
                                               select new AccountingDTO
                                               {
                                                   AccountId = c.AccountId,
                                                   AccountBalance = c.AccountBalance,
                                                   AccountCode = c.AccountCode,
                                                   AccountName = c.AccountName,
                                                   Credit = c.Credit,
                                                   Debit = c.Debit,
                                                   ParentAccountId = c.ParentAccountId,
                                               }

                                                ).ToList();


            var res = (from c in categoriasList
                           // where c.ParentAccountId == null || c.ParentAccountId == 0
                       select new AccountingDTO
                       {
                           AccountId = c.AccountId,
                           AccountName = c.AccountName,
                           AccountBalance = c.AccountBalance,
                           Credit = c.Credit,
                           Debit = c.Debit,
                           AccountCode = c.AccountCode,
                           ParentAccountId = c.ParentAccountId,
                        //   Children = ObtenerHijos(c.AccountId, categoriasList)
                       }).ToList();

           if(res.Count>0)
            {
                foreach (var item in res)
                {
                    var existe =   query.Where(q => q.AccountId == item.AccountId).ToList();
                    if(existe.Count==0)
                    {
                        query.Add(item);
                    }
                }
               
            }
         
                                         
                                      
            if (_padre.Count > 0)
            {
                ObtenerCategoriarJerarquia(_padre);
            }

            return query;
        }

    }
}