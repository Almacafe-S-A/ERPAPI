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
    [Route("api/EndososBonoLine")]
    [ApiController]
    public class EndososBonoLineController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public EndososBonoLineController(ILogger<EndososBonoLineController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de EndososBonoLinees 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEndososBonoLine()
        {
            List<EndososBonoLine> Items = new List<EndososBonoLine>();
            try
            {
                Items = await _context.EndososBonoLine.ToListAsync();
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
        /// Obtiene los Datos de la EndososBonoLine por medio del Id enviado.
        /// </summary>
        /// <param name="EndososBonoLineId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{EndososBonoLineId}")]
        public async Task<IActionResult> GetEndososBonoLineById(Int64 EndososBonoLineId)
        {
            EndososBonoLine Items = new EndososBonoLine();
            try
            {
                Items = await _context.EndososBonoLine.Where(q => q.EndososBonoLineId == EndososBonoLineId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }


        /// <summary>
        /// Inserta una nueva EndososBonoLine
        /// </summary>
        /// <param name="_EndososBonoLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<EndososBonoLine>> Insert([FromBody]EndososBonoLine _EndososBonoLine)
        {
            EndososBonoLine _EndososBonoLineq = new EndososBonoLine();
            try
            {
                _EndososBonoLineq = _EndososBonoLine;
                _context.EndososBonoLine.Add(_EndososBonoLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_EndososBonoLineq);
        }

        /// <summary>
        /// Actualiza la EndososBonoLine
        /// </summary>
        /// <param name="_EndososBonoLine"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<EndososBonoLine>> Update([FromBody]EndososBonoLine _EndososBonoLine)
        {
            EndososBonoLine _EndososBonoLineq = _EndososBonoLine;
            try
            {
                _EndososBonoLineq = await (from c in _context.EndososBonoLine
                                 .Where(q => q.EndososBonoLineId == _EndososBonoLine.EndososBonoLineId)
                                           select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_EndososBonoLineq).CurrentValues.SetValues((_EndososBonoLine));

                //_context.EndososBonoLine.Update(_EndososBonoLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_EndososBonoLineq);
        }

        /// <summary>
        /// Elimina una EndososBonoLine       
        /// </summary>
        /// <param name="_EndososBonoLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]EndososBonoLine _EndososBonoLine)
        {
            EndososBonoLine _EndososBonoLineq = new EndososBonoLine();
            try
            {
                _EndososBonoLineq = _context.EndososBonoLine
                .Where(x => x.EndososBonoLineId == (Int64)_EndososBonoLine.EndososBonoLineId)
                .FirstOrDefault();

                _context.EndososBonoLine.Remove(_EndososBonoLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_EndososBonoLineq);

        }







    }
}