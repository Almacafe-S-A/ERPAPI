﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/ExchangeRate")]
    [ApiController]
    public class ExchangeRateController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        /*public DimensionsController(ApplicationDbContext context)
        {
            _context = context;
        }*/
        public ExchangeRateController(ILogger<ExchangeRateController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// Obtiene los Datos de la Tasa de Cambio en una lista.
        /// </summary>

        // GET: api/ExchangeRate
        [HttpGet("[action]")]
        public async Task<IActionResult> GetExchangeRate()

        {
            List<ExchangeRate> Items = new List<ExchangeRate>();
            try
            {
                Items = await _context.ExchangeRate.OrderByDescending(c => c.ExchangeRateId).ToListAsync();
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
        /// Obtiene los Datos de la ExchangeRate por medio del Id enviado.
        /// </summary>
        /// <param name="ExchangeRateId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{ExchangeRateId}")]
        public async Task<IActionResult> GetExchangeRateById(Int64 ExchangeRateId)
        {
            ExchangeRate Items = new ExchangeRate();
            try
            {
                Items = await _context.ExchangeRate.Where(q => q.ExchangeRateId == ExchangeRateId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene un Exchange Rate para la fecha y moneda
        /// </summary>
        /// <param name="_ExchangeRate"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> GetExchangeRateByFecha([FromBody]ExchangeRate _ExchangeRate)
        {
            ExchangeRate Items = new ExchangeRate();
            try
            {
                // DateTime filtro = Convert.ToDateTime(fecha);
                Items = await _context.ExchangeRate.Where(q => q.DayofRate.ToString("yyyy-MM-dd") == _ExchangeRate.DayofRate.ToString("yyyy-MM-dd") && q.CurrencyId == _ExchangeRate.CurrencyId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene un Exchange Rate para la fecha y moneda
        /// </summary>
        /// <param name="_ExchangeRate"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> GetExchangeRateByDateNCurrency([FromBody] ExchangeRate _ExchangeRate)
        {
            ExchangeRate Items = new ExchangeRate();
            try
            {
                // DateTime filtro = Convert.ToDateTime(fecha);
                Items = await _context.ExchangeRate.Where(q => q.DayofRate.Date == _ExchangeRate.DayofRate.Date && q.CurrencyId ==_ExchangeRate.CurrencyId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Devuelve la tasa de cambio que coinciden con los parametros
        /// </summary>
        /// <param name="Exchange"></param>
        /// <returns></returns>

        [HttpPost("[action]")]
        public async Task<ActionResult<Int32>> GetTasadecambio([FromBody] ExchangeRate Exchange)
        {
            try
            {
                
               //var Items = await _context.ExchangeRate.Where(q => q.DayofRate.ToString("yyyy-MM-dd") == Exchange.DayofRate.ToString("yyyy-MM-dd")).CountAsync();

               // return await Task.Run(() => Ok(Items));
                var Items = await _context.ExchangeRate.Where(q => q.ExchangeRateId >2).CountAsync();
                return await Task.Run(() => Ok(Items));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

        }



        /// <summary>
        /// Inserta una nueva ExchangeRate 
        /// /// </summary>
        /// <param name="_ExchangeRate"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<ExchangeRate>> Insert([FromBody]ExchangeRate _ExchangeRate)
        {
            ExchangeRate _ExchangeRateq = new ExchangeRate();
            try
            {
                _ExchangeRateq = _ExchangeRate;
                _context.ExchangeRate.Add(_ExchangeRateq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();


                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_ExchangeRateq));
        }

        /// <summary>
        /// Actualiza la ExchangeRate
        /// </summary>
        /// <param name="_ExchangeRate"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<ExchangeRate>> Update([FromBody]ExchangeRate _ExchangeRate)
        {
            ExchangeRate _ExchangeRateq = _ExchangeRate;
            try
            {
                _ExchangeRateq = await (from c in _context.ExchangeRate
                                 .Where(q => q.ExchangeRateId == _ExchangeRate.ExchangeRateId)
                                        select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_ExchangeRateq).CurrentValues.SetValues((_ExchangeRate));

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();


                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_ExchangeRateq));
        }

        /// <summary>
        /// Elimina una ExchangeRate       
        /// </summary>
        /// <param name="_ExchangeRate"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]ExchangeRate _ExchangeRate)
        {
            ExchangeRate _ExchangeRateq = new ExchangeRate();
            try
            {
                _ExchangeRateq = _context.ExchangeRate
                .Where(x => x.ExchangeRateId == (Int64)_ExchangeRate.ExchangeRateId)
                .FirstOrDefault();

                _context.ExchangeRate.Remove(_ExchangeRateq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
               new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();


                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_ExchangeRateq));

        }
    }
}