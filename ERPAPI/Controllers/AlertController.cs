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
    [Route("api/Alert")]
    [ApiController]
    public class AlertController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public AlertController(ILogger<AlertController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Alertes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAlert()
        {
            List<Alert> Items = new List<Alert>();
            try
            {
                Items = await _context.Alert.ToListAsync();
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
        /// Obtiene los Datos de la Alert por medio del Id enviado.
        /// </summary>
        /// <param name="AlertId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{AlertId}")]
        public async Task<IActionResult> GetAlertById(Int64 AlertId)
        {
            Alert Items = new Alert();
            try
            {
                Items = await _context.Alert.Where(q => q.AlertId == AlertId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva Alert
        /// </summary>
        /// <param name="_Alert"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Alert>> Insert([FromBody]Alert _Alert)
        {
            Alert _Alertq = new Alert();
            try
            {
                _Alertq = _Alert;
                _context.Alert.Add(_Alertq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Alertq));
        }

        /// <summary>
        /// Actualiza la Alert
        /// </summary>
        /// <param name="_Alert"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Alert>> Update([FromBody]Alert _Alert)
        {
            Alert _Alertq = _Alert;
            try
            {
                _Alertq = await (from c in _context.Alert
                                 .Where(q => q.AlertId == _Alert.AlertId)
                                 select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_Alertq).CurrentValues.SetValues((_Alert));

                //_context.Alert.Update(_Alertq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Alertq));
        }

        /// <summary>
        /// Elimina una Alert       
        /// </summary>
        /// <param name="_Alert"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Alert _Alert)
        {
            Alert _Alertq = new Alert();
            try
            {
                _Alertq = _context.Alert
                .Where(x => x.AlertId == (Int64)_Alert.AlertId)
                .FirstOrDefault();

                _context.Alert.Remove(_Alertq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Alertq));

        }







    }
}