﻿using System;
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
    [Route("api/Tax")]
    [ApiController]
    public class TaxController : Controller
    {

        private readonly ApplicationDbContext _context;
          private readonly ILogger _logger;

        public TaxController(ILogger<TaxController> logger,ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;

        }

        /// <summary>
        /// Obtiene todos los codigos de impuesto
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<ActionResult> GetTaxes()
        {

            try
            {
                List<Tax> Items = await _context.Tax.ToListAsync();
                return Ok(Items);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

        }


        /// <summary>
        /// Obtiene el codigo de impuesto por el id enviado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{TaxId}")]
        public async Task<ActionResult> GetTaxById(Int64 TaxId)
        {

            try
            {
                Tax Items = await _context.Tax.Where(q=>q.TaxId==TaxId).FirstOrDefaultAsync();
                return Ok(Items);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

        }

        /// <summary>
        /// Inserta un codigo de impuesto
        /// </summary>
        /// <param name="_Tax"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Tax>> Insert([FromBody]Tax _Tax)
        {

            try
            {
                Tax tax = _Tax;
                _context.Tax.Add(tax);
                await _context.SaveChangesAsync();
                return (tax); 

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

        }

        /// <summary>
        /// Actualiza un Codigo de impuesto.
        /// </summary>
        /// <param name="_Tax"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Tax>> Update([FromBody]Tax _Tax)
        {
            try
            {
                Tax customer = _Tax;
                _context.Tax.Update(_Tax);
                await _context.SaveChangesAsync();
                return (_Tax);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

        }

        /// <summary>
        /// Elimina un codigo de impuesto.
        /// </summary>
        /// <param name="_Tax"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Tax>> Delete([FromBody]Tax _Tax)
        {

            try
            {
                Tax tax = _context.Tax
               .Where(x => x.TaxId == (Int64)_Tax.TaxId)
               .FirstOrDefault();
                _context.Tax.Remove(tax);
                await _context.SaveChangesAsync();
                return (tax);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


        }







    }
}