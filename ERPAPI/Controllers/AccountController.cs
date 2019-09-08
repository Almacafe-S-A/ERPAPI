using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Helpers;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Account")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        /*public DimensionsController(ApplicationDbContext context)
        {
            _context = context;
        }*/
        public AccountController(ILogger<AccountController> logger, ApplicationDbContext context)
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
            //return await _context.Dimensions.ToListAsync();
        }
        /// <summary>
        /// Obtiene los Datos de la Account en una lista.
        /// </summary>

        // GET: api/Account
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAccountDiary()

        {
            List<Account> Items = new List<Account>();
            try
            {
                Items = await _context.Account.Where(q => q.BlockedInJournal == false).ToListAsync();
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
            Account Items = new Account();
            try
            {
                Items = await _context.Account.Where(q => q.AccountId == AccountId).FirstOrDefaultAsync();
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
        /// <param name="_Account"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Account>> Insert([FromBody]Account _Account)
        {
            Account _Accountq = new Account();
            try
            {
                _Accountq = _Account;
                _context.Account.Add(_Accountq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Accountq));
        }

        /// <summary>
        /// Actualiza la Account
        /// </summary>
        /// <param name="_Account"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Account>> Update([FromBody]Account _Account)
        {
            Account _Accountq = _Account;
            try
            {
                _Accountq = await (from c in _context.Account
                                 .Where(q => q.AccountId == _Account.AccountId)
                                   select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_Accountq).CurrentValues.SetValues((_Account));

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Accountq));
        }

        /// <summary>
        /// Elimina una Account       
        /// </summary>
        /// <param name="_Account"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Account _Account)
        {
            Account _Accountq = new Account();
            try
            {
                _Accountq = _context.Account
                .Where(x => x.AccountId == (Int64)_Account.AccountId)
                .FirstOrDefault();

                _context.Account.Remove(_Accountq);
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