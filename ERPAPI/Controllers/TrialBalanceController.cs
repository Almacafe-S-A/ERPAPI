using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ERP.Contexts;
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

            List<Accounting> _categoria = new List<Accounting>();

            _categoria= ObtenerCategoriarJerarquia(null);


            return await Task.Run(() => Json(_categoria));

        }

        private  List<Accounting> ObtenerCategoriarJerarquia(Int32? idCurso)
        {
            List<Accounting> categoriasList = ObtenerCategorias(idCurso);

            List<Accounting> query = (from c in categoriasList
                                      where c.ParentAccountId == null || c.ParentAccountId == 0
                                      select new Accounting
                                      {
                                          AccountId = c.AccountId,
                                          AccountName = c.AccountName,
                                          AccountBalance = c.AccountBalance,
                                          AccountCode = c.AccountCode,
                                          ParentAccountId = c.ParentAccountId,
                                          ChildAccounts = ObtenerHijos(c.AccountId, categoriasList)
                                      }).ToList();

            return query;
        }

        private  List<Accounting> ObtenerHijos(Int64 idCategoria, List<Accounting> categoriasList)
        {
             categoriasList =  _context.Accounting.ToList();
            List<Accounting> query = (from c in categoriasList
                                      let tieneHijos = categoriasList.Where(o => o.ParentAccountId == c.AccountId).Any()
                                      where c.ParentAccountId == idCategoria
                                      select new Accounting
                                      {
                                          AccountId = c.AccountId,
                                          AccountName = c.AccountName,
                                          AccountBalance = c.AccountBalance,
                                          AccountCode = c.AccountCode,
                                          ParentAccountId = c.ParentAccountId,
                                          ChildAccounts = tieneHijos ? ObtenerHijos(c.AccountId, categoriasList) : null
                                      }).ToList();

            return query;

        }


        private List<Accounting> ObtenerCategorias(Int32? idCurso)
        {
            List<Accounting> material = new List<Accounting>();
            //using (var db = new _conte())
            //{
                try
                {
                    material = (from c in _context.Accounting
                                 .Where(q => q.ParentAccountId == null)
                                select new Accounting
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

      




    }
}