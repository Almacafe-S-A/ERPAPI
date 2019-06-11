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
    [Route("api/Bank")]
    [ApiController]
    public class BankController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public BankController(ILogger<BankController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Bankes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBank()
        {
            List<Bank> Items = new List<Bank>();
            try
            {
                Items = await _context.Bank.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return Ok(Items);
        }

        /// <summary>
        /// Obtiene los Datos de la Bank por medio del Id enviado.
        /// </summary>
        /// <param name="BankId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{BankId}")]
        public async Task<IActionResult> GetBankById(Int64 BankId)
        {
            Bank Items = new Bank();
            try
            {
                Items = await _context.Bank.Where(q => q.BankId == BankId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }


        /// <summary>
        /// Inserta una nueva Bank
        /// </summary>
        /// <param name="_Bank"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Bank>> Insert([FromBody]Bank _Bank)
        {
            Bank _Bankq = new Bank();
            try
            {
                _Bankq = _Bank;
                _context.Bank.Add(_Bankq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_Bankq);
        }

        /// <summary>
        /// Actualiza la Bank
        /// </summary>
        /// <param name="_Bank"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Bank>> Update([FromBody]Bank _Bank)
        {
            Bank _Bankq = _Bank;
            try
            {
                _Bankq = await (from c in _context.Bank
                                 .Where(q => q.BankId == _Bank.BankId)
                                select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_Bankq).CurrentValues.SetValues((_Bank));

                //_context.Bank.Update(_Bankq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_Bankq);
        }

        /// <summary>
        /// Elimina una Bank       
        /// </summary>
        /// <param name="_Bank"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Bank _Bank)
        {
            Bank _Bankq = new Bank();
            try
            {
                _Bankq = _context.Bank
                .Where(x => x.BankId == (Int64)_Bank.BankId)
                .FirstOrDefault();

                _context.Bank.Remove(_Bankq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_Bankq);

        }







    }
}