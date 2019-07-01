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
    [Route("api/Boleto_Ent")]
    [ApiController]
    public class Boleto_EntController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public Boleto_EntController(ILogger<Boleto_EntController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Boleto_Entes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBoleto_Ent()
        {
            List<Boleto_Ent> Items = new List<Boleto_Ent>();
            try
            {
                Items = await _context.Boleto_Ent.ToListAsync();
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
        /// Obtiene los Datos de la Boleto_Ent por medio del Id enviado.
        /// </summary>
        /// <param name="Boleto_EntId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Boleto_EntId}")]
        public async Task<IActionResult> GetBoleto_EntById(Int64 Boleto_EntId)
        {
            Boleto_Ent Items = new Boleto_Ent();
            try
            {
                Items = await _context.Boleto_Ent.Where(q => q.clave_e == Boleto_EntId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }


        /// <summary>
        /// Inserta una nueva Boleto_Ent
        /// </summary>
        /// <param name="_Boleto_Ent"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Boleto_Ent>> Insert([FromBody]Boleto_Ent _Boleto_Ent)
        {
            Boleto_Ent _Boleto_Entq = new Boleto_Ent();
            try
            {
                _Boleto_Entq = _Boleto_Ent;
                _context.Boleto_Ent.Add(_Boleto_Entq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_Boleto_Entq);
        }

        /// <summary>
        /// Actualiza la Boleto_Ent
        /// </summary>
        /// <param name="_Boleto_Ent"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Boleto_Ent>> Update([FromBody]Boleto_Ent _Boleto_Ent)
        {
            Boleto_Ent _Boleto_Entq = _Boleto_Ent;
            try
            {
                _Boleto_Entq = await (from c in _context.Boleto_Ent
                                 .Where(q => q.clave_e == _Boleto_Ent.clave_e)
                                      select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_Boleto_Entq).CurrentValues.SetValues((_Boleto_Ent));

                //_context.Boleto_Ent.Update(_Boleto_Entq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_Boleto_Entq);
        }

        /// <summary>
        /// Elimina una Boleto_Ent       
        /// </summary>
        /// <param name="_Boleto_Ent"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Boleto_Ent _Boleto_Ent)
        {
            Boleto_Ent _Boleto_Entq = new Boleto_Ent();
            try
            {
                _Boleto_Entq = _context.Boleto_Ent
                .Where(x => x.clave_e == (Int64)_Boleto_Ent.clave_e)
                .FirstOrDefault();

                _context.Boleto_Ent.Remove(_Boleto_Entq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_Boleto_Entq);

        }







    }
}