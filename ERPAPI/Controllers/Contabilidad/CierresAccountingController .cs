using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/CierresAccounting")]
    [ApiController]
    public class CierresAccountingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        /*public DimensionsController(ApplicationDbContext context)
        {
            _context = context;
        }*/
        public CierresAccountingController(ILogger<AccountingController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// Obtiene los Datos de la Account en una lista.
        /// </summary>

        // GET: api/Account
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAccount()

        {
            List<CierresAccounting> Items = new List<CierresAccounting>();
            try
            {
                Items = await _context.CierresAccounting.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
            //return await _context.Dimensions.ToListAsync();
        }
        /// <summary>
        /// Obtiene los Datos de la Account en una lista.
        /// </summary>

        // GET: api/Account
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAccountDiary()

        {
            List<CierresAccounting> Items = new List<CierresAccounting>();
            try
            {
                Items = await _context.CierresAccounting.Where(q => q.BlockedInJournal == false).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
            //return await _context.Dimensions.ToListAsync();
        }


        /// <summary>
        /// Obtiene los Datos de la Account por medio del Id enviado.
        /// </summary>
        /// <param name="CierreAccountingId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CierreAccountingId}")]
        public async Task<IActionResult> GetAccountById(Int64 CierreAccountingId)
        {
            CierresAccounting Items = new CierresAccounting();
            try
            {
                Items = await _context.CierresAccounting.Where(q => q.CierreAccountingId == CierreAccountingId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }
        /// <summary>
        /// Obtiene los Datos de la Account por medio del Codigo de Cuenta enviado.
        /// </summary>
        /// <param name="AccountCode"></param>
        /// <returns></returns>

        [HttpGet("[action]/{AccountCode}")]
        public async Task<IActionResult> GetAccountingByAccountCode(String AccountCode)
        {
            CierresAccounting Items = new CierresAccounting();
            try
            {
                Items = await _context.CierresAccounting.Where(q => q.AccountCode == AccountCode).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }
        /// <summary>
        /// Obtiene los Datos de la Account por medio del Id de Tipo de Cuenta enviado.
        /// </summary>
        /// <param name="TypeAccounting"></param>
        /// <returns></returns>
        [HttpGet("[action]/{TypeAccounting}")]
        public async Task<IActionResult> GetFatherAccountById(Int64 TypeAccounting)
        {
            CierresAccounting Items = new CierresAccounting();
            try
            {
                Items = await _context.CierresAccounting.Where(
                                q => q.TypeAccountId == TypeAccounting &&
                                     q.ParentAccountId == null

                ).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }
        /// <summary>
        /// Inserta una nueva Account
        /// </summary>
        /// <param name="_TypeAcountId"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<ActionResult<Int32>> GetHightLevelHierarchy(Int64 _TypeAcountId)
        {

            try
            {
                CierresAccounting _CierresAccounting = new CierresAccounting();
                _CierresAccounting = _context.CierresAccounting.Where(a => a.TypeAccountId == _TypeAcountId)
                    .OrderByDescending(b => b.HierarchyAccount).FirstOrDefault();
                var Items = _CierresAccounting.HierarchyAccount;
                return await Task.Run(() => Ok(Items));
                //  return Ok(Items);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

        }
        /// <summary>
        /// Inserta una nueva Account
        /// </summary>
        /// <param name="_TypeAcountId"></param>
        /// <returns></returns>

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAccountingByTypeAccount(Int64 _TypeAcountId)

        {
            List<CierresAccounting> Items = new List<CierresAccounting>();
            try
            {
                Items = await _context.CierresAccounting.Where(p => p.TypeAccountId == _TypeAcountId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
            //return await _context.Dimensions.ToListAsync();
        }


     

        /// <summary>
        /// Retorna los nodos padres de un padre contable 
        /// </summary>
        /// <param name="ParentAcountId"></param>
        /// <returns></returns>

        [HttpGet("[action]/{ParentAcountId}")]
        public async Task<IActionResult> GetFathersAccounting(Int64 ParentAcountId)

        {
            List<CierresAccounting> Items = new List<CierresAccounting>();
            try
            {
                Items = await _context.CierresAccounting.Where(p => p.ParentAccountId == ParentAcountId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
            //return await _context.Dimensions.ToListAsync();
        }

        /// <summary>
        /// Obtiene los Datos de la tabla Accounting por clasificacion de cuenta.
        /// </summary>
        List<Int32> Parents = new List<Int32>();
        [HttpPost("[action]")]
        public async Task<IActionResult> GetAccountingType([FromBody]CierresAccountingFilter _CierresAccountingDTO)

        {
            List<CierresAccountingDTO> Items = new List<CierresAccountingDTO>();
            List<Int32> _parents = new List<int>();
            try
            {
                List<CierresAccounting> _cuentas = new List<CierresAccounting>();

                if (_CierresAccountingDTO.TypeAccountId == 0 && _CierresAccountingDTO.estadocuenta==true && _CierresAccountingDTO.BitacoraCierreContableId == 0)
                {
                    _cuentas = await _context.CierresAccounting.Where(m => m.IdEstado ==1).ToListAsync();
                }
                if (_CierresAccountingDTO.TypeAccountId == 0 && _CierresAccountingDTO.estadocuenta == false && _CierresAccountingDTO.BitacoraCierreContableId == 0)
                {
                    _cuentas = await _context.CierresAccounting.Where(m => m.IdEstado == 2).ToListAsync();
                    _parents = _cuentas.Select(q => q.ParentAccountId==null?0 : q.ParentAccountId.Value).ToList();
                    _cuentas.AddRange( ObtenerCategoriarJerarquia(_parents));
                }
                else  if (_CierresAccountingDTO.TypeAccountId == 0 && _CierresAccountingDTO.estadocuenta == null && _CierresAccountingDTO.BitacoraCierreContableId == 0)
                {
                    _cuentas = await _context.CierresAccounting.ToListAsync();
                }
                else if (_CierresAccountingDTO.TypeAccountId > 0 && _CierresAccountingDTO.estadocuenta == true && _CierresAccountingDTO.BitacoraCierreContableId == 0)
                {
                    _cuentas = await _context.CierresAccounting
                        .Where(q => q.TypeAccountId == _CierresAccountingDTO.TypeAccountId)
                        .Where(m => m.IdEstado == 1)                        
                        .ToListAsync();
                }
                else if (_CierresAccountingDTO.TypeAccountId > 0 && _CierresAccountingDTO.estadocuenta == false && _CierresAccountingDTO.BitacoraCierreContableId == 0)
                {
                    _cuentas = await _context.CierresAccounting
                        .Where(q => q.TypeAccountId == _CierresAccountingDTO.TypeAccountId)
                        .Where(m => m.IdEstado == 2)
                        .ToListAsync();
                }
                else if (_CierresAccountingDTO.TypeAccountId> 0 && _CierresAccountingDTO.estadocuenta == null && _CierresAccountingDTO.BitacoraCierreContableId == 0)
                {
                    _cuentas = await _context.CierresAccounting
                        .Where(q => q.TypeAccountId == _CierresAccountingDTO.TypeAccountId
                        )
                        .ToListAsync();
                }

                else if (_CierresAccountingDTO.TypeAccountId > 0 && _CierresAccountingDTO.estadocuenta == true && _CierresAccountingDTO.BitacoraCierreContableId > 0)
                {
                    _cuentas = await _context.CierresAccounting
                        .Where(q => q.TypeAccountId == _CierresAccountingDTO.TypeAccountId)
                        .Where(m => m.IdEstado == 1)
                        .Where(m => m.BitacoraCierreContableId == _CierresAccountingDTO.BitacoraCierreContableId)
                        .ToListAsync();
                }
                else if (_CierresAccountingDTO.TypeAccountId > 0 && _CierresAccountingDTO.estadocuenta == false && _CierresAccountingDTO.BitacoraCierreContableId > 0)
                {
                    _cuentas = await _context.CierresAccounting
                        .Where(q => q.TypeAccountId == _CierresAccountingDTO.TypeAccountId)
                        .Where(m => m.IdEstado == 2)
                        .Where(m => m.BitacoraCierreContableId == _CierresAccountingDTO.BitacoraCierreContableId)
                        .ToListAsync();
                }
                else if (_CierresAccountingDTO.TypeAccountId > 0 && _CierresAccountingDTO.estadocuenta == null && _CierresAccountingDTO.BitacoraCierreContableId > 0)
                {
                    _cuentas = await _context.CierresAccounting
                        .Where(q => q.TypeAccountId == _CierresAccountingDTO.TypeAccountId)
                        .Where(m => m.BitacoraCierreContableId == _CierresAccountingDTO.BitacoraCierreContableId
                        )
                        .ToListAsync();
                }




                Items = (from c in _cuentas
                         select new CierresAccountingDTO
                         {
                             //CierreAccountingId =c.CierreAccountingId,
                             CompanyInfoId = c.CompanyInfoId,
                             AccountId = c.AccountId,
                             AccountName = c.AccountCode + "--" + c.AccountName,
                             ParentAccountId = c.ParentAccountId,
                             // Credit = Credit(c.AccountId),
                             // Debit = Debit(c.AccountId),
                             IdEstado = c.IdEstado,
                             Estado = c.Estado,
                             AccountBalance = c.AccountBalance,
                             IsCash = c.IsCash,
                             Description = c.Description,
                             TypeAccountId = c.TypeAccountId,
                             BlockedInJournal = c.BlockedInJournal,
                             AccountCode = c.AccountCode,
                             HierarchyAccount = c.HierarchyAccount,
                             UsuarioCreacion = c.UsuarioCreacion,
                             UsuarioModificacion = c.UsuarioModificacion,
                             FechaCreacion = c.FechaCreacion,
                             FechaModificacion = c.FechaModificacion
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

        List<CierresAccountingDTO> query = new List<CierresAccountingDTO>();
        private List<CierresAccountingDTO> ObtenerCategoriarJerarquia(List<Int32> Parents)
        {

            List<CierresAccountingDTO> Items = new List<CierresAccountingDTO>();
            List<Int32> _padre = new List<Int32>();
            foreach (var padre in Parents)
            {
                CierresAccounting _ac = _context.CierresAccounting.Where(q => q.CierreAccountingId == padre).FirstOrDefault();

                if (_ac.ParentAccountId != null)
                {
                    _padre.Add(_ac.ParentAccountId.Value);
                }

                Items.Add(new CierresAccountingDTO
                {
                    //CierreAccountingId = _ac.CierreAccountingId,
                    AccountId = _ac.AccountId,
                    AccountName = _ac.AccountName,
                    AccountCode = _ac.AccountCode,
                    AccountBalance = _ac.AccountBalance,
                    ParentAccountId = _ac.ParentAccountId,
                });
            }

            List<CierresAccounting> categoriasList = (from c in Items
                                               select new CierresAccounting
                                               {
                                                   //CierreAccountingId = c.CierreAccountingId,
                                                   AccountId = c.AccountId,
                                                   AccountBalance = c.AccountBalance,
                                                   AccountCode = c.AccountCode,
                                                   AccountName = c.AccountName,
                                                   ParentAccountId = c.ParentAccountId,
                                               }

                                                ).ToList();


            var res = (from c in categoriasList
                           // where c.ParentAccountId == null || c.ParentAccountId == 0
                       select new CierresAccountingDTO
                       {
                           //CierreAccountingId = c.CierreAccountingId,
                           AccountId = c.AccountId,
                           AccountName = c.AccountName,
                           AccountBalance = c.AccountBalance,
                           AccountCode = c.AccountCode,
                           ParentAccountId = c.ParentAccountId,
                           //   Children = ObtenerHijos(c.AccountId, categoriasList)
                       }).ToList();

            if (res.Count > 0)
            {
                foreach (var item in res)
                {
                    var existe = query.Where(q => q.CierreAccountingId == item.CierreAccountingId).ToList();
                    if (existe.Count == 0)
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

        private decimal Debit(Int64 AccountId)
        {
            return _context.JournalEntryLine
                    .Where(q => q.AccountId == AccountId).Sum(q => q.Debit);
        }

        private decimal Credit(Int64 AccountId)
        {
            return _context.JournalEntryLine
                    .Where(q => q.AccountId == AccountId).Sum(q => q.Credit);
        }

        //[HttpGet("[action]")]
        //public async Task<ActionResult<List<CierresAccounting>>> GetCuentasDiariasPatron([FromQuery(Name = "Patron")] string patron)
        //{
        //    try
        //    {
        //        var cuentas = await _context.CierresAccounting
        //            .Where(c => c.AccountCode.StartsWith(patron) && c.BlockedInJournal == false && c.Totaliza == false)
        //            .ToListAsync();
        //        return await Task.Run(() => Ok(cuentas));
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
        //        return BadRequest($"Ocurrio un error:{ex.Message}");
        //    }
        //}
    }
}