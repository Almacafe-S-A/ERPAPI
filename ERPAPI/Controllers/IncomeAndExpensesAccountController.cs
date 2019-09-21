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

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/IncomeAndExpensesAccount")]
    [ApiController]
    public class IncomeAndExpensesAccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public IncomeAndExpensesAccountController(ILogger<IncomeAndExpensesAccountController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de IncomeAndExpensesAccount paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetIncomeAndExpensesAccountPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<IncomeAndExpensesAccount> Items = new List<IncomeAndExpensesAccount>();
            try
            {
                var query = _context.IncomeAndExpensesAccount.AsQueryable();
                var totalRegistro = query.Count();

                Items = await query
                   .Skip(cantidadDeRegistros * (numeroDePagina - 1))
                   .Take(cantidadDeRegistros)
                    .ToListAsync();

                Response.Headers["X-Total-Registros"] = totalRegistro.ToString();
                Response.Headers["X-Cantidad-Paginas"] = ((Int64)Math.Ceiling((double)totalRegistro / cantidadDeRegistros)).ToString();
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
        /// Obtiene el Listado de IncomeAndExpensesAccount
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetIncomeAndExpensesAccount()
        {
            List<IncomeAndExpensesAccount> Items = new List<IncomeAndExpensesAccount>();
            try
            {
                Items = await _context.IncomeAndExpensesAccount.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene los Datos de la IncomeAndExpensesAccount por medio del Id enviado.
        /// </summary>
        /// <param name="IncomeAndExpensesAccountId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{IncomeAndExpensesAccountId}")]
        public async Task<IActionResult> GetIncomeAndExpensesAccountById(Int64 IncomeAndExpensesAccountId)
        {
            IncomeAndExpensesAccount Items = new IncomeAndExpensesAccount();
            try
            {
                Items = await _context.IncomeAndExpensesAccount.Where(q => q.IncomeAndExpensesAccountId == IncomeAndExpensesAccountId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva IncomeAndExpensesAccount
        /// </summary>
        /// <param name="_IncomeAndExpensesAccount"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<IncomeAndExpensesAccount>> Insert([FromBody]IncomeAndExpensesAccount _IncomeAndExpensesAccount)
        {
            IncomeAndExpensesAccount _IncomeAndExpensesAccountq = new IncomeAndExpensesAccount();
            try
            {
                _IncomeAndExpensesAccountq = _IncomeAndExpensesAccount;
                _context.IncomeAndExpensesAccount.Add(_IncomeAndExpensesAccountq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_IncomeAndExpensesAccountq));
        }

        /// <summary>
        /// Actualiza la IncomeAndExpensesAccount
        /// </summary>
        /// <param name="_IncomeAndExpensesAccount"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<IncomeAndExpensesAccount>> Update([FromBody]IncomeAndExpensesAccount _IncomeAndExpensesAccount)
        {
            IncomeAndExpensesAccount _IncomeAndExpensesAccountq = _IncomeAndExpensesAccount;
            try
            {
                _IncomeAndExpensesAccountq = await (from c in _context.IncomeAndExpensesAccount
                                 .Where(q => q.IncomeAndExpensesAccountId == _IncomeAndExpensesAccount.IncomeAndExpensesAccountId)
                                                    select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_IncomeAndExpensesAccountq).CurrentValues.SetValues((_IncomeAndExpensesAccount));

                //_context.IncomeAndExpensesAccount.Update(_IncomeAndExpensesAccountq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_IncomeAndExpensesAccountq));
        }

        /// <summary>
        /// Elimina una IncomeAndExpensesAccount       
        /// </summary>
        /// <param name="_IncomeAndExpensesAccount"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]IncomeAndExpensesAccount _IncomeAndExpensesAccount)
        {
            IncomeAndExpensesAccount _IncomeAndExpensesAccountq = new IncomeAndExpensesAccount();
            try
            {
                _IncomeAndExpensesAccountq = _context.IncomeAndExpensesAccount
                .Where(x => x.IncomeAndExpensesAccountId == (Int64)_IncomeAndExpensesAccount.IncomeAndExpensesAccountId)
                .FirstOrDefault();

                _context.IncomeAndExpensesAccount.Remove(_IncomeAndExpensesAccountq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_IncomeAndExpensesAccountq));

        }







    }
}