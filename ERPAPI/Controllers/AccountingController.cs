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
    [Route("api/Accounting")]
    [ApiController]
    public class AccountingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        /*public DimensionsController(ApplicationDbContext context)
        {
            _context = context;
        }*/
        public AccountingController(ILogger<AccountingController> logger, ApplicationDbContext context)
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
            List<Accounting> Items = new List<Accounting>();
            try
            {
                Items = await _context.Accounting.ToListAsync();
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
            List<Accounting> Items = new List<Accounting>();
            try
            {
                Items = await _context.Accounting.Where(q => q.BlockedInJournal == false).ToListAsync();
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
        /// <param name="AccountId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{AccountId}")]
        public async Task<IActionResult> GetAccountById(Int64 AccountId)
        {
            Accounting Items = new Accounting();
            try
            {
                Items = await _context.Accounting.Where(q => q.AccountId == AccountId).FirstOrDefaultAsync();
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
            Accounting Items = new Accounting();
            try
            {
                Items = await _context.Accounting.Where(q => q.AccountCode == AccountCode).FirstOrDefaultAsync();
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
            Accounting Items = new Accounting();
            try
            {
                Items = await _context.Accounting.Where(
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
                Accounting _Accounting = new Accounting();
                _Accounting = _context.Accounting.Where(a => a.TypeAccountId == _TypeAcountId)
                    .OrderByDescending(b => b.HierarchyAccount).FirstOrDefault();
                var Items = _Accounting.HierarchyAccount;
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
            List<Accounting> Items = new List<Accounting>();
            try
            {
                Items = await _context.Accounting.Where(p => p.TypeAccountId == _TypeAcountId)
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
        /// Obtiene el Listado de Cuentas sin hijos 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetNoChildAccounts()
        {
            List<Accounting> Items = new List<Accounting>();
            try
            {
                Items = await _context.Accounting.Where(q=> !q.Totaliza).OrderBy(q=>q.AccountCode).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Retorna los nodos padres de un padre contable 
        /// </summary>
        /// <param name="ParentAcountId"></param>
        /// <returns></returns>

        [HttpGet("[action]/{ParentAcountId}")]
        public async Task<IActionResult> GetFathersAccounting(Int64 ParentAcountId)

        {
            List<Accounting> Items = new List<Accounting>();
            try
            {
                Items = await _context.Accounting.Where(p => p.ParentAccountId == ParentAcountId)
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
        public async Task<IActionResult> GetAccountingType([FromBody]AccountingFilter _AccountingDTO)

        {
            List<AccountingDTO> Items = new List<AccountingDTO>();
            List<Int32> _parents = new List<int>();
            try
            {
                List<Accounting> _cuentas = new List<Accounting>();

                if (_AccountingDTO.TypeAccountId == 0 && _AccountingDTO.estadocuenta==true)
                {
                    _cuentas = await _context.Accounting.Where(m => m.IdEstado ==1).ToListAsync();
                }
                if (_AccountingDTO.TypeAccountId == 0 && _AccountingDTO.estadocuenta == false)
                {
                    _cuentas = await _context.Accounting.Where(m => m.IdEstado == 2).ToListAsync();
                    _parents = _cuentas.Select(q => q.ParentAccountId==null?0 : q.ParentAccountId.Value).ToList();
                    _cuentas.AddRange( ObtenerCategoriarJerarquia(_parents));
                }
                else  if (_AccountingDTO.TypeAccountId == 0 && _AccountingDTO.estadocuenta == null)
                {
                    _cuentas = await _context.Accounting .ToListAsync();
                }
                else if (_AccountingDTO.TypeAccountId > 0 && _AccountingDTO.estadocuenta == true)
                {
                    _cuentas = await _context.Accounting
                        .Where(q => q.TypeAccountId == _AccountingDTO.TypeAccountId)
                        .Where(m => m.IdEstado == 1)                        
                        .ToListAsync();
                }
                else if (_AccountingDTO.TypeAccountId > 0 && _AccountingDTO.estadocuenta == false)
                {
                    _cuentas = await _context.Accounting
                        .Where(q => q.TypeAccountId == _AccountingDTO.TypeAccountId)
                        .Where(m => m.IdEstado == 2)
                        .ToListAsync();
                }
                else if (_AccountingDTO.TypeAccountId> 0 && _AccountingDTO.estadocuenta == null)
                {
                    _cuentas = await _context.Accounting
                        .Where(q => q.TypeAccountId == _AccountingDTO.TypeAccountId
                        )
                        .ToListAsync();
                }

                Items = (from c in _cuentas
                         select new AccountingDTO
                         {
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

        List<AccountingDTO> query = new List<AccountingDTO>();
        private List<AccountingDTO> ObtenerCategoriarJerarquia(List<Int32> Parents)
        {

            List<AccountingDTO> Items = new List<AccountingDTO>();
            List<Int32> _padre = new List<Int32>();
            foreach (var padre in Parents)
            {
                Accounting _ac = _context.Accounting.Where(q => q.AccountId == padre).FirstOrDefault();

                if (_ac.ParentAccountId != null)
                {
                    _padre.Add(_ac.ParentAccountId.Value);
                }

                Items.Add(new AccountingDTO
                {
                    AccountId = _ac.AccountId,
                    AccountName = _ac.AccountName,
                    AccountCode = _ac.AccountCode,
                    AccountBalance = _ac.AccountBalance,
                    ParentAccountId = _ac.ParentAccountId,
                });
            }

            List<Accounting> categoriasList = (from c in Items
                                               select new Accounting
                                               {
                                                   AccountId = c.AccountId,
                                                   AccountBalance = c.AccountBalance,
                                                   AccountCode = c.AccountCode,
                                                   AccountName = c.AccountName,
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
                           AccountCode = c.AccountCode,
                           ParentAccountId = c.ParentAccountId,
                           //   Children = ObtenerHijos(c.AccountId, categoriasList)
                       }).ToList();

            if (res.Count > 0)
            {
                foreach (var item in res)
                {
                    var existe = query.Where(q => q.AccountId == item.AccountId).ToList();
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

        

        /// <summary>
        /// Inserta una nueva Account
        /// </summary>
        /// <param name="_Account"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Accounting>> Insert([FromBody]Accounting _Account)
        {
            Accounting _Accountq = new Accounting();
            
           
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Accountq = _Account;
                        _context.Accounting.Add(_Accountq);
                        await _context.SaveChangesAsync();
                       
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Accountq.AccountId,
                            DocType = "Accounting",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Accountq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Accountq.UsuarioCreacion,
                            UsuarioModificacion = _Accountq.UsuarioModificacion,
                            UsuarioEjecucion = _Accountq.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        throw ex;
                        // return BadRequest($"Ocurrio un error:{ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Accountq));
            /*
            return await Task.Run(() => Ok(_ConfigurationVendorq));
       */
        }

        /// <summary>
        /// Actualiza la Account
        /// </summary>
        /// <param name="_Account"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Accounting>> Update([FromBody]Accounting _Account)
        {
            Accounting _Accountq = _Account;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Accountq = await (from c in _context.Accounting
                                         .Where(q => q.AccountId == _Accountq.AccountId)
                                                       select c
                                        ).FirstOrDefaultAsync();

                        _context.Entry(_Accountq).CurrentValues.SetValues((_Account));

                        //_context.Alert.Update(_Alertq);
                        await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Accountq.AccountId,
                            DocType = "Accounting",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_Accountq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_Account, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Account.UsuarioCreacion,
                            UsuarioModificacion = _Account.UsuarioModificacion,
                            UsuarioEjecucion = _Account.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        throw ex;
                        // return BadRequest($"Ocurrio un error:{ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            
            return await Task.Run(() => Ok(_Accountq));
        }

        /// <summary>
        /// Elimina una Account       
        /// </summary>
        /// <param name="_Account"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Accounting _Account)
        {
            Accounting _Accountq = new Accounting();
            try
            {
                _Accountq = _context.Accounting
                .Where(x => x.AccountId == (Int64)_Account.AccountId)
                .FirstOrDefault();

                _context.Accounting.Remove(_Accountq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Accountq));

        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<Accounting>>> GetCuentasDiariasPatron([FromQuery(Name = "Patron")] string patron)
        {
            try
            {
                var cuentas = await _context.Accounting
                    .Where(c => c.AccountCode.StartsWith(patron) && c.BlockedInJournal == false && c.Totaliza == false)
                    .ToListAsync();
                return await Task.Run(() => Ok(cuentas));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
        }
    }
}