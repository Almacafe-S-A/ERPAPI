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
    [Route("api/PuntoEmision")]
    [ApiController]
    public class PuntoEmisionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public PuntoEmisionController(ILogger<PuntoEmisionController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }


        /// <summary>
        /// Obtiene el Listado de PuntoEmision paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPuntoEmisionPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<PuntoEmision> Items = new List<PuntoEmision>();
            try
            {
                var query = _context.PuntoEmision.AsQueryable();
                var totalRegistro = query.Count();

                Items = await query
                   .Skip(cantidadDeRegistros * (numeroDePagina - 1))
                   .Take(cantidadDeRegistros)
                    .ToListAsync();

                Response.Headers["X-Total-Registros"] = totalRegistro.ToString();
                Response.Headers["X-Cantidad-Paginas"] = ((Int64)Math.Ceiling((double)totalRegistro / cantidadDeRegistros)).ToString();
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
        /// Obtiene  un punto de emision
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPuntoEmision()
        {
            List<PuntoEmision> Items = new List<PuntoEmision>();
            try
            {
                Items = await _context.PuntoEmision.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        
       [HttpGet("[action]/{BranchId}")]
        public async Task<IActionResult> GetPuntoEmisionByBranchId(Int64 BranchId)
        {
            List<PuntoEmision> Items = new List<PuntoEmision>();
            try
            {
                Items = await _context.PuntoEmision.Where(q=>q.BranchId== BranchId).ToListAsync();
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
        /// Obtiene el punto de emision por medio del Id Enviado.
        /// </summary>
        /// <param name="IdPuntoEmision"></param>
        /// <returns></returns>
        [HttpGet("[action]/{IdPuntoEmision}")]
        public async Task<IActionResult> GetPuntoEmisionById(Int64 IdPuntoEmision)
        {
            PuntoEmision Items = new PuntoEmision();
            try
            {
                Items = await _context.PuntoEmision.Where(q=>q.IdPuntoEmision == IdPuntoEmision).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Inserta un punto de emision
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async  Task<IActionResult> Insert([FromBody]PuntoEmision payload)
        {
            PuntoEmision _PuntoEmision = new PuntoEmision();
            try
            {
                _PuntoEmision = payload;
                _context.PuntoEmision.Add(_PuntoEmision);
               await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_PuntoEmision));
        }

        /// <summary>
        /// Actualiza un punto de emision
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Update([FromBody]PuntoEmision payload)
        {
            PuntoEmision _PuntoEmision = new PuntoEmision() ;
            try
            {
                _PuntoEmision = await (from c in _context.PuntoEmision
                                       .Where(q=>q.IdPuntoEmision==payload.IdPuntoEmision)
                                       select c).FirstOrDefaultAsync();

                _context.Entry(_PuntoEmision).CurrentValues.SetValues(payload);

               // _context.PuntoEmision.Update(_PuntoEmision);
               await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_PuntoEmision));
        }

        /// <summary>
        /// Elimina el punto de emision
        /// </summary>
        /// <param name="_puntoemision"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]PuntoEmision _puntoemision)
        {
            PuntoEmision _puntoemisionq = new PuntoEmision();
            try
            {
                _puntoemisionq = _context.PuntoEmision
               .Where(x => x.IdPuntoEmision == (Int64)_puntoemision.IdPuntoEmision)
               .FirstOrDefault();

                _context.PuntoEmision.Remove(_puntoemisionq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_puntoemisionq));

        }







    }
}