using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace ERPAPI.Helpers
{
    public static class ContabilidadHandler
    {
        
        /// <summary>
        ///Funcion que Actualiza el Saldo de las Cuentas conttables del Catalogo Contable recibe el contexto y el asiento que afecta el saldo 
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="_JournalEntry"></param>
         public static void ActualizarSaldoCuentas(ApplicationDbContext _context, JournalEntry _JournalEntry) { 
            foreach (JournalEntryLine jel in _JournalEntry.JournalEntryLines)
            {
                bool continuar = true;
                Accounting _account = new Accounting();
                _account = (from c in _context.Accounting
                .Where(q => q.AccountId == jel.AccountId)
                                 select c
                ).FirstOrDefault();
                do
                {
                    if (_account.DeudoraAcreedora == "D")
                    {
                        _account.AccountBalance -= jel.Credit;
                        _account.AccountBalance += jel.Debit;
                    }
                    else if (_account.DeudoraAcreedora == "A")
                    {
                        _account.AccountBalance += jel.Credit;
                        _account.AccountBalance -= jel.Debit;
                    }
                     _context.SaveChanges();
                    if (!_account.ParentAccountId.HasValue)
                    {
                        continuar = false;
                    }
                    else
                    {
                        _account = (from c in _context.Accounting
                       .Where(q => q.AccountId == _account.ParentAccountId)
                                         select c
                        ).FirstOrDefault();
                        if (_account == null)
                        {
                            continuar = false;
                        }
                    }
                }
                while (continuar);
                
            }
            _context.SaveChanges();

          




        }

       
        /// <summary>
        /// Funcion para agregar Asiento Contable, enviando la informacion del asiento como parametro, retorna nulo si algo falla
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="_logger"></param>
        /// <param name="_TransactionId"></param>
        /// <param name="_je"></param>
        /// <param name="_Monto"></param>
        /// <param name="_branchid"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public static async Task<ActionResult<JournalEntry>> AddJournalEntry(ApplicationDbContext _context, ILogger _logger, int _TransactionId, JournalEntry _je, decimal _Monto, int? _branchid)
        {
            JournalEntryConfiguration _journalentryconfiguration;
            if (_branchid.HasValue)
            {
                _journalentryconfiguration = await (_context.JournalEntryConfiguration
                                                                  .Where(q => q.TransactionId == _TransactionId)
                                                                  .Where(q => q.BranchId == _branchid)
                                                                  .Where(q => q.EstadoName == "Activo")
                                                                  .Include(q => q.JournalEntryConfigurationLine)
                                                                  ).FirstOrDefaultAsync();
            }
            else
            {
                _journalentryconfiguration = await (_context.JournalEntryConfiguration
                                                                  .Where(q => q.TransactionId == _TransactionId)
                                                                  .Where(q => q.EstadoName == "Activo")
                                                                  .Include(q => q.JournalEntryConfigurationLine)
                                                                  ).FirstOrDefaultAsync();
            }

            decimal sumacreditos = 0, sumadebitos = 0;
            if (_journalentryconfiguration != null)
            {

                foreach (var item in _journalentryconfiguration.JournalEntryConfigurationLine)
                {

                    _je.JournalEntryLines.Add(new JournalEntryLine
                    {
                        AccountId = Convert.ToInt32(item.AccountId),
                        AccountName = item.AccountName,
                        Description = item.AccountName,
                        Credit = item.DebitCredit == "Credito" ? _Monto : 0,
                        Debit = item.DebitCredit == "Debito" ? _Monto : 0,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        CreatedUser = _je.CreatedUser,
                        ModifiedUser = _je.ModifiedUser,
                        Memo = "",
                    });

                    sumacreditos += item.DebitCredit == "Credito" ? _Monto : 0;
                    sumadebitos += item.DebitCredit == "Debito" ? _Monto : 0;

                    // _context.JournalEntryLine.Add(_je);

                }


                if (sumacreditos != sumadebitos)
                {
                    _logger.LogError($"Ocurrio un error: No coinciden debitos :{sumadebitos} y creditos{sumacreditos}");
                    return null;
                }

                _je.TotalCredit = sumacreditos;
                _je.TotalDebit = sumadebitos;
                _context.JournalEntry.Add(_je);

                await _context.SaveChangesAsync();
            }

            return null;
        }


        /// <summary>
        /// Funcion para agregar asiento contable, recibe los valores para crearlo, devuelve nulo en caso de falla
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="_logger"></param>
        /// <param name="_TransactionId"></param>
        /// <param name="_DocumentId"></param>
        /// <param name="_TypeOfAdjustmentId"></param>
        /// <param name="_VoucherType"></param>
        /// <param name="_date"></param>
        /// <param name="_postdate"></param>
        /// <param name="_Monto"></param>
        /// <param name="_usuario"></param>
        /// <param name="_memo"></param>
        /// <param name="_branchid"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public  static async Task<ActionResult<JournalEntry>> AddJournalEntry(ApplicationDbContext _context, ILogger _logger, int _TransactionId, long _DocumentId, int _TypeOfAdjustmentId, int _VoucherType, DateTime _date, DateTime _postdate, decimal _Monto, string _usuario, string _memo, int? _branchid)
        {
            JournalEntryConfiguration _journalentryconfiguration;
            if (_branchid.HasValue)
            {
                _journalentryconfiguration = await (_context.JournalEntryConfiguration
                                                                  .Where(q => q.TransactionId == _TransactionId)
                                                                  .Where(q => q.BranchId == _branchid)
                                                                  .Where(q => q.EstadoName == "Activo")
                                                                  .Include(q => q.JournalEntryConfigurationLine)
                                                                  ).FirstOrDefaultAsync();
            }
            else
            {
                _journalentryconfiguration = await (_context.JournalEntryConfiguration
                                                                  .Where(q => q.TransactionId == _TransactionId)
                                                                  .Where(q => q.EstadoName == "Activo")
                                                                  .Include(q => q.JournalEntryConfigurationLine)
                                                                  ).FirstOrDefaultAsync();
            }

            decimal sumacreditos = 0, sumadebitos = 0;
            if (_journalentryconfiguration != null)
            {

                //Crear el asiento contable configurado
                //.............................///////
                JournalEntry _je = new JournalEntry
                {
                    Date = _date,
                    Memo = _memo,
                    DatePosted = _postdate,
                    ModifiedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    ModifiedUser = _usuario,
                    CreatedUser = _usuario,
                    DocumentId = _DocumentId,
                    TypeOfAdjustmentId = _TypeOfAdjustmentId,
                    VoucherType = _VoucherType,

                };

                foreach (var item in _journalentryconfiguration.JournalEntryConfigurationLine)
                {

                    _je.JournalEntryLines.Add(new JournalEntryLine
                    {
                        AccountId = Convert.ToInt32(item.AccountId),
                        AccountName = item.AccountName,
                        Description = item.AccountName,
                        Credit = item.DebitCredit == "Credito" ? _Monto : 0,
                        Debit = item.DebitCredit == "Debito" ? _Monto : 0,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        CreatedUser = _je.CreatedUser,
                        ModifiedUser = _je.ModifiedUser,
                        Memo = "",
                    });

                    sumacreditos += item.DebitCredit == "Credito" ? _Monto : 0;
                    sumadebitos += item.DebitCredit == "Debito" ? _Monto : 0;

                    // _context.JournalEntryLine.Add(_je);

                }


                if (sumacreditos != sumadebitos)
                {
                    _logger.LogError($"Ocurrio un error: No coinciden debitos :{sumadebitos} y creditos{sumacreditos}");
                    return null;
                }

                _je.TotalCredit = sumacreditos;
                _je.TotalDebit = sumadebitos;
                _context.JournalEntry.Add(_je);

                await _context.SaveChangesAsync();
            }

            return null;
        }
    }

    
    
}
