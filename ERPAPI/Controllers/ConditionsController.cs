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
    [Route("api/Conditions")]
    [ApiController]
    public class ConditionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ConditionsController(ILogger<ConditionsController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Condiciones 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetConditions()
        {
            List<Conditions> Items = new List<Conditions>();
            try
            {
                Items = await _context.Conditions.ToListAsync();
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
        /// Obtiene los Datos de la condicion por medio del Id enviado.
        /// </summary>
        /// <param name="ConditionId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{ConditionId}")]
        public async Task<IActionResult> GetConditionsById(Int64 ConditionId)
        {
            Conditions Items = new Conditions();
            try
            {
                Items = await _context.Conditions.Where(q=>q.ConditionId== ConditionId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            
            return Ok(Items);
        }


        /// <summary>
        /// Inserta una nueva condicion
        /// </summary>
        /// <param name="_Conditions"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Conditions>> Insert([FromBody]Conditions _Conditions)
        {
            Conditions _Conditionsq = new Conditions();
            try
            {
                _Conditionsq = _Conditions;
                _context.Conditions.Add(_Conditionsq);
               await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_Conditionsq);
        }

        /// <summary>
        /// Actualiza la condicion
        /// </summary>
        /// <param name="_conditions"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Conditions>> Update([FromBody]Conditions _conditions)
        {
            Conditions _conditionsq = _conditions;
            try
            {
                _context.Conditions.Update(_conditionsq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_conditionsq);
        }

        /// <summary>
        /// Elimina una condicion       
        /// </summary>
        /// <param name="_conditions"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Conditions _conditions)
        {
            Conditions _conditionsq = new Conditions();
            try
            {
                _conditionsq = _context.Conditions
                .Where(x => x.ConditionId == (Int64)_conditions.ConditionId)
                .FirstOrDefault();

                _context.Conditions.Remove(_conditionsq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_conditionsq);

        }







    }
}