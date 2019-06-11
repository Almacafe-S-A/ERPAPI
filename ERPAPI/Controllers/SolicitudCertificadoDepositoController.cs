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
    [Route("api/SolicitudCertificadoDeposito")]
    [ApiController]
    public class SolicitudCertificadoDepositoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public SolicitudCertificadoDepositoController(ILogger<SolicitudCertificadoDepositoController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de SolicitudCertificadoDepositoes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSolicitudCertificadoDeposito()
        {
            List<SolicitudCertificadoDeposito> Items = new List<SolicitudCertificadoDeposito>();
            try
            {
                Items = await _context.SolicitudCertificadoDeposito.ToListAsync();
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
        /// Obtiene los Datos de la SolicitudCertificadoDeposito por medio del Id enviado.
        /// </summary>
        /// <param name="SolicitudCertificadoDepositoId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{SolicitudCertificadoDepositoId}")]
        public async Task<IActionResult> GetSolicitudCertificadoDepositoById(Int64 SolicitudCertificadoDepositoId)
        {
            SolicitudCertificadoDeposito Items = new SolicitudCertificadoDeposito();
            try
            {
                Items = await _context.SolicitudCertificadoDeposito.Where(q => q.IdCD == SolicitudCertificadoDepositoId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }


        /// <summary>
        /// Inserta una nueva SolicitudCertificadoDeposito
        /// </summary>
        /// <param name="_SolicitudCertificadoDeposito"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<SolicitudCertificadoDeposito>> Insert([FromBody]SolicitudCertificadoDeposito _SolicitudCertificadoDeposito)
        {
            SolicitudCertificadoDeposito _SolicitudCertificadoDepositoq = new SolicitudCertificadoDeposito();
            try
            {
                _SolicitudCertificadoDepositoq = _SolicitudCertificadoDeposito;
                _context.SolicitudCertificadoDeposito.Add(_SolicitudCertificadoDepositoq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_SolicitudCertificadoDepositoq);
        }

        /// <summary>
        /// Actualiza la SolicitudCertificadoDeposito
        /// </summary>
        /// <param name="_SolicitudCertificadoDeposito"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<SolicitudCertificadoDeposito>> Update([FromBody]SolicitudCertificadoDeposito _SolicitudCertificadoDeposito)
        {
            SolicitudCertificadoDeposito _SolicitudCertificadoDepositoq = _SolicitudCertificadoDeposito;
            try
            {
                _SolicitudCertificadoDepositoq = await (from c in _context.SolicitudCertificadoDeposito
                                 .Where(q => q.IdCD == _SolicitudCertificadoDeposito.IdCD)
                                                        select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_SolicitudCertificadoDepositoq).CurrentValues.SetValues((_SolicitudCertificadoDeposito));

                //_context.SolicitudCertificadoDeposito.Update(_SolicitudCertificadoDepositoq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_SolicitudCertificadoDepositoq);
        }

        /// <summary>
        /// Elimina una SolicitudCertificadoDeposito       
        /// </summary>
        /// <param name="_SolicitudCertificadoDeposito"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]SolicitudCertificadoDeposito _SolicitudCertificadoDeposito)
        {
            SolicitudCertificadoDeposito _SolicitudCertificadoDepositoq = new SolicitudCertificadoDeposito();
            try
            {
                _SolicitudCertificadoDepositoq = _context.SolicitudCertificadoDeposito
                .Where(x => x.IdCD == (Int64)_SolicitudCertificadoDeposito.IdCD)
                .FirstOrDefault();

                _context.SolicitudCertificadoDeposito.Remove(_SolicitudCertificadoDepositoq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_SolicitudCertificadoDepositoq);

        }







    }
}