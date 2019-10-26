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
    [Route("api/InsurancesCertificateLine")]
    [ApiController]
    public class InsurancesCertificateLineController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public InsurancesCertificateLineController(ILogger<InsurancesCertificateLineController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de InsurancesCertificateLine 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInsurancesCertificateLine()
        {
            List<InsurancesCertificateLine> Items = new List<InsurancesCertificateLine>();
            try
            {
                Items = await _context.InsurancesCertificateLine.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetInsurancesCertificateLineById(int Id)
        {
            InsurancesCertificateLine Items = new InsurancesCertificateLine();
            try
            {
                Items = await _context.InsurancesCertificateLine
                             .Where(q => q.InsurancesCertificateLineId == Id).FirstOrDefaultAsync();
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
        /// Inserta una nueva InsurancesCertificateLine
        /// </summary>
        /// <param name="_InsurancesCertificateLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<InsurancesCertificateLine>> Insert([FromBody]InsurancesCertificateLine _InsurancesCertificateLine)
        {
            InsurancesCertificateLine _InsurancesCertificateLineq = new InsurancesCertificateLine();
            try
            {
                _InsurancesCertificateLineq = _InsurancesCertificateLine;
                _context.InsurancesCertificateLine.Add(_InsurancesCertificateLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_InsurancesCertificateLineq));
        }

        /// <summary>
        /// Actualiza la InsurancesCertificateLine
        /// </summary>
        /// <param name="_InsurancesCertificateLine"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<InsurancesCertificateLine>> Update([FromBody]InsurancesCertificateLine _InsurancesCertificateLine)
        {
            InsurancesCertificateLine _InsurancesCertificateLineq = _InsurancesCertificateLine;
            try
            {
                _InsurancesCertificateLineq = await (from c in _context.InsurancesCertificateLine
                                 .Where(q => q.InsurancesCertificateLineId == _InsurancesCertificateLine.InsurancesCertificateLineId)
                                           select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_InsurancesCertificateLineq).CurrentValues.SetValues((_InsurancesCertificateLine));

                //_context.CertificadoLine.Update(_CertificadoLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_InsurancesCertificateLineq));
        }

        /// <summary>
        /// Elimina una InsurancesCertificateLine       
        /// </summary>
        /// <param name="_InsurancesCertificateLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]InsurancesCertificateLine _InsurancesCertificateLine)
        {
            InsurancesCertificateLine _InsurancesCertificateLineq = new InsurancesCertificateLine();
            try
            {
                _InsurancesCertificateLineq = _context.InsurancesCertificateLine
                .Where(x => x.InsurancesCertificateLineId == (int)_InsurancesCertificateLine.InsurancesCertificateLineId)
                .FirstOrDefault();

                _context.InsurancesCertificateLine.Remove(_InsurancesCertificateLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_InsurancesCertificateLineq));

        }







    }
}