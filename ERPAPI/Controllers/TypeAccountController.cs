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

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/TypeAccount")]
    [ApiController]
    public class TypeAccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        /*public DimensionsController(ApplicationDbContext context)
        {
            _context = context;
        }*/
        public TypeAccountController(ILogger<AccountController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// Obtiene los Datos de la TypeAccount en una lista.
        /// </summary>

        // GET: api/TypeAccount
        [HttpGet("[action]")]
        public async Task<IActionResult> GetTypeAccount()

        {
            List<TypeAccount> Items = new List<TypeAccount>();
            try
            {
                Items = await _context.TypeAccount.ToListAsync();
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
        /// <param name="TypeAccountId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{TypeAccountId}")]
        public async Task<IActionResult> GetTypeAccountById(Int64 TypeAccountId)
        {
            TypeAccount Items = new TypeAccount();
            try
            {
                Items = await _context.TypeAccount.Where(q => q.TypeAccountId == TypeAccountId).FirstOrDefaultAsync();
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
        /// <param name="_TypeAccount"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<TypeAccount>> Insert([FromBody]TypeAccount _TypeAccount)
        {
            TypeAccount _TypeAccountq = new TypeAccount();
            try
            {
                _TypeAccountq = _TypeAccount;
                _context.TypeAccount.Add(_TypeAccountq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_TypeAccountq));
        }

        /// <summary>
        /// Actualiza la Account
        /// </summary>
        /// <param name="_TypeAccount"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<TypeAccount>> Update([FromBody]TypeAccount _TypeAccount)
        {
            TypeAccount _TypeAccountq = _TypeAccount;
            try
            {
                _TypeAccountq = await (from c in _context.TypeAccount
                                 .Where(q => q.TypeAccountId == _TypeAccount.TypeAccountId)
                                       select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_TypeAccountq).CurrentValues.SetValues((_TypeAccount));

                //_context.Bank.Update(_Bankq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_TypeAccountq));
        }

        /// <summary>
        /// Elimina una Account       
        /// </summary>
        /// <param name="_TypeAccount"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]TypeAccount _TypeAccount)
        {
            TypeAccount _TypeAccountq = new TypeAccount();
            try
            {
                _TypeAccountq = _context.TypeAccount
                .Where(x => x.TypeAccountId == (Int64)_TypeAccount.TypeAccountId)
                .FirstOrDefault();

                _context.TypeAccount.Remove(_TypeAccountq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_TypeAccountq));

        }
    }
}