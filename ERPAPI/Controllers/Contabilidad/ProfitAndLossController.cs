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