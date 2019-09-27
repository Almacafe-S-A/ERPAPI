﻿using System;
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
                                          Debit = Debit(c.AccountId),
                                          Credit = Credit(c.AccountId),

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

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAccounting()

        {
            List<AccountingDTO> Items = new List<AccountingDTO>();
            try
            {
                List<Accounting> _cuentas = await _context.Accounting.ToListAsync();
                Items =  (from c in _cuentas
                               select new AccountingDTO
                               {
                                   AccountId = c.AccountId,
                                   AccountName = c.AccountCode + "--" + c.AccountName,
                                   ParentAccountId = c.ParentAccountId,
                                   Credit = Credit(c.AccountId),
                                   Debit = Debit(c.AccountId),
                                   AccountBalance = c.AccountBalance
                               }
                               )
                               .ToList();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));

        }


        private double Debit(Int64 AccountId)
        {
            return _context.JournalEntryLine
                    .Where(q => q.AccountId == AccountId).Sum(q => q.Debit);
        }

        private double Credit(Int64 AccountId)
        {
            return _context.JournalEntryLine
                    .Where(q => q.AccountId == AccountId).Sum(q => q.Credit);
        }





    }


 

}