﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ERPAPI.Controllers
{
   [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    //[Produces("application/json")]
    [Route("api/Currency")]
    public class CurrencyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CurrencyController(ILogger<CurrencyController> logger,ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }


        /// <summary>
        /// Obtiene el listado de Monedas.
        /// </summary>
        /// <returns></returns>
        // GET: api/Currency
        [HttpGet("[action]")]
        public async Task<ActionResult<Currency>> GetCurrency()
        {
            List<Currency> Items = new List<Currency>();
            try
            {
               Items =  await _context.Currency.ToListAsync();
               // int Count = Items.Count();
            }
            catch (Exception ex)
            {
                  _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
           


            return Ok( Items);
        }

        /// <summary>
        /// Obtiene los datos de la moneda con el id enviado
        /// </summary>
        /// <param name="CurrencyId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CurrencyId}")]
        public async Task<ActionResult<Currency>> GetCurrencyById(int CurrencyId)
        {
            Currency Items = new Currency();
            try
            {
                Items = await _context.Currency.Where(q=>q.CurrencyId== CurrencyId).FirstOrDefaultAsync();
                // int Count = Items.Count();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }



            return Ok(Items);
        }

        /// <summary>
        /// Inserta la moneda 
        /// </summary>
        /// <param name="_Currency"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Currency>> Insert([FromBody]Currency _Currency)
        {
            Currency currency = _Currency;
            try
            {
                _context.Currency.Add(currency);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                  _logger.LogError($"Ocurrio un error: { ex.ToString() }");
               return BadRequest($"Ocurrio un error:{ex.Message}");
            }
          
            return Ok(currency);
        }

        /// <summary>
        /// Actualiza la moneda
        /// </summary>
        /// <param name="_Currency"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Currency>> Update([FromBody]Currency _Currency)
        {
            Currency currency = _Currency;
            try
            {
                _context.Currency.Update(currency);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                  _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
           
            return Ok(currency);
        }

        /// <summary>
        /// Elimina la moneda
        /// </summary>
        /// <param name="_Currency"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Currency>> Delete([FromBody]Currency _Currency)
        {
            Currency currency = new Currency();
            try
            {
                currency = _context.Currency
               .Where(x => x.CurrencyId == (int)_Currency.CurrencyId)
               .FirstOrDefault();
                _context.Currency.Remove(currency);
              await  _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                 _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                 return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            
            return Ok(currency);

        }


    }
}