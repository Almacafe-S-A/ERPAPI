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
    [Route("api/CustomerConditions")]
    [ApiController]
    public class CustomerConditionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CustomerConditionsController(ILogger<CustomerConditionsController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de CustomerConditionses 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCustomerConditions()
        {
            List<CustomerConditions> Items = new List<CustomerConditions>();
            try
            {
                Items = await _context.CustomerConditions.ToListAsync();
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
        /// Obtiene los Datos de la CustomerConditions por medio del Id enviado.
        /// </summary>
        /// <param name="CustomerConditionsId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CustomerConditionsId}")]
        public async Task<IActionResult> GetCustomerConditionsById(Int64 CustomerConditionsId)
        {
            CustomerConditions Items = new CustomerConditions();
            try
            {
                Items = await _context.CustomerConditions.Where(q => q.CustomerConditionId == CustomerConditionsId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> GetCustomerConditionsByClass([FromBody]CustomerConditions _Ccq)
        {
            List<CustomerConditions> Items = new List<CustomerConditions>();
            try
            {
                Items = await _context.CustomerConditions
                    .Where(q=>q.IdTipoDocumento==_Ccq.IdTipoDocumento)
                    .Where(q => q.DocumentId == _Ccq.DocumentId).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }


        /// <summary>
        /// Inserta una nueva CustomerConditions
        /// </summary>
        /// <param name="_CustomerConditions"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<CustomerConditions>> Insert([FromBody]CustomerConditions _CustomerConditions)
        {
            CustomerConditions _CustomerConditionsq = new CustomerConditions();
            try
            {
                _CustomerConditionsq = _CustomerConditions;
                _context.CustomerConditions.Add(_CustomerConditionsq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_CustomerConditionsq);
        }

        /// <summary>
        /// Actualiza la CustomerConditions
        /// </summary>
        /// <param name="_CustomerConditions"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<CustomerConditions>> Update([FromBody]CustomerConditions _CustomerConditions)
        {
            CustomerConditions _CustomerConditionsq = _CustomerConditions;
            try
            {
                _CustomerConditionsq = (from c in _context.CustomerConditions
                                 .Where(q => q.CustomerConditionId == _CustomerConditions.CustomerConditionId)
                                        select c
                                ).FirstOrDefault();

                _context.Entry(_CustomerConditionsq).CurrentValues.SetValues((_CustomerConditions));

                //_context.CustomerConditions.Update(_CustomerConditionsq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_CustomerConditionsq);
        }

        /// <summary>
        /// Elimina una CustomerConditions       
        /// </summary>
        /// <param name="_CustomerConditions"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]CustomerConditions _CustomerConditions)
        {
            CustomerConditions _CustomerConditionsq = new CustomerConditions();
            try
            {
                _CustomerConditionsq = _context.CustomerConditions
                .Where(x => x.CustomerConditionId == (Int64)_CustomerConditions.CustomerConditionId)
                .FirstOrDefault();

                _context.CustomerConditions.Remove(_CustomerConditionsq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_CustomerConditionsq);

        }







    }
}