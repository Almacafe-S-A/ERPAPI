using System;
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
    [Route("api/PEPS")]
    [ApiController]
    public class PEPSController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public PEPSController(ILogger<PEPSController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de PEPSes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPEPS()
        {
            List<PEPS> Items = new List<PEPS>();
            try
            {
                Items = await _context.PEPS.ToListAsync();
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
        /// Obtiene los Datos de la PEPS por medio del Id enviado.
        /// </summary>
        /// <param name="PEPSId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{PEPSId}")]
        public async Task<IActionResult> GetPEPSById(Int64 PEPSId)
        {
            PEPS Items = new PEPS();
            try
            {
                Items = await _context.PEPS.Where(q => q.PEPSId == PEPSId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> GetByParams([FromBody]PEPS _peps)
        {
            List<PEPS> Items = new List<PEPS>();
            try
            {
                Items = await _context.PEPS.Where(q=>q.Funcionario.Contains(_peps.Funcionario)).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

           
            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva PEPS
        /// </summary>
        /// <param name="_PEPS"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<PEPS>> Insert([FromBody]PEPS _PEPS)
        {
            PEPS _PEPSq = new PEPS();
            try
            {
                _PEPSq = _PEPS;
                _context.PEPS.Add(_PEPSq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

             return await Task.Run(() => Ok(_PEPSq));
        }

        /// <summary>
        /// Actualiza la PEPS
        /// </summary>
        /// <param name="_PEPS"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<PEPS>> Update([FromBody]PEPS _PEPS)
        {
            PEPS _PEPSq = _PEPS;
            try
            {
                _PEPSq = await (from c in _context.PEPS
                                 .Where(q => q.PEPSId == _PEPS.PEPSId)
                                select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_PEPSq).CurrentValues.SetValues((_PEPS));

                //_context.PEPS.Update(_PEPSq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_PEPSq));
        }

        /// <summary>
        /// Elimina una PEPS       
        /// </summary>
        /// <param name="_PEPS"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]PEPS _PEPS)
        {
            PEPS _PEPSq = new PEPS();
            try
            {
                _PEPSq = _context.PEPS
                .Where(x => x.PEPSId == (Int64)_PEPS.PEPSId)
                .FirstOrDefault();

                _context.PEPS.Remove(_PEPSq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_PEPSq));

        }







    }
}