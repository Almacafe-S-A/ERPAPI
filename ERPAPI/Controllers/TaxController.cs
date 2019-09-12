﻿using System;
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
    [Route("api/Tax")]
    [ApiController]
    public class TaxController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public TaxController(ILogger<TaxController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Taxes
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetTax()
        {
            List<Tax> Items = new List<Tax>();
            try
            {
                Items = await _context.Tax.ToListAsync();
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
        /// Obtiene los Datos de la Tax por medio del Id enviado.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetTaxById(Int64 Id)
        {
            Tax Items = new Tax();
            try
            {
                Items = await _context.Tax.Where(q => q.TaxId == Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva Tax
        /// </summary>
        /// <param name="_Tax"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Tax>> Insert([FromBody]Tax _Tax)
        {
            Tax _Taxq = new Tax();
            try
            {
                _Taxq = _Tax;
                _context.Tax.Add(_Taxq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Taxq));
        }

        /// <summary>
        /// Actualiza la Tax
        /// </summary>
        /// <param name="_Tax"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Tax>> Update([FromBody]Tax _Tax)
        {
            Tax _Taxq = _Tax;
            try
            {
                _Taxq = await (from c in _context.Tax
                                 .Where(q => q.TaxId == _Tax.TaxId)
                               select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_Taxq).CurrentValues.SetValues((_Tax));


                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Taxq));
        }




    }
}