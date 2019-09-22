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
        /// Obtiene los Datos de la Account por medio del Id enviado.
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
                _Accounting= _context.Accounting.Where(a => a.TypeAccountId == _TypeAcountId)
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
        /// Retorna los nodos padres de un padre contable 
        /// </summary>
        /// <param name="_ParentAcountId"></param>
        /// <returns></returns>

        [HttpGet("[action]")]
        public async Task<IActionResult> GetFathersAccounting(Int64 _ParentAcountId)

        {
            List<Accounting> Items = new List<Accounting>();
            try
            {
                Items = await _context.Accounting.Where(p => p.ParentAccountId == _ParentAcountId)
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
        /*[HttpGet("[action]")]
        public async Task<ActionResult<AccountClasses>> GetEnumClass()//Int64 idgrupoestado
        {
            List<AccountClasses> Items = new List<AccountClasses>();

            try
            {
                Items = await _context.enum//.AccountClasses.ToListAsync();
                //List<AccountClasses> Items; //await _context.Account.a//.Where(q => q.IdGrupoEstado == idgrupoestado).ToListAsync();
                //return await Task.Run(() => Ok(AccountClasses));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }*/
        /*
        List<Account> Items = new List<Account>();
        try
        {
            Items = await _context.Account.ToListAsync();
        }
        catch (Exception ex)
        {

            _logger.LogError($"Ocurrio un error: { ex.ToString() }");
            return BadRequest($"Ocurrio un error:{ex.Message}");
        }

        //  int Count = Items.Count();
        return await Task.Run(() => Ok(Items));

         */
        //}
    }
}