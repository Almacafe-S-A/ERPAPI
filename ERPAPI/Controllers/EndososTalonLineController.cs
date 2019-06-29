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
    [Route("api/EndososTalonLine")]
    [ApiController]
    public class EndososTalonLineController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public EndososTalonLineController(ILogger<EndososTalonLineController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de EndososTalonLinees 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEndososTalonLine()
        {
            List<EndososTalonLine> Items = new List<EndososTalonLine>();
            try
            {
                Items = await _context.EndososTalonLine.ToListAsync();
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
        /// Obtiene los Datos de la EndososTalonLine por medio del Id enviado.
        /// </summary>
        /// <param name="EndososTalonLineId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{EndososTalonLineId}")]
        public async Task<IActionResult> GetEndososTalonLineById(Int64 EndososTalonLineId)
        {
            EndososTalonLine Items = new EndososTalonLine();
            try
            {
                Items = await _context.EndososTalonLine.Where(q => q.EndososTalonLineId == EndososTalonLineId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }


        /// <summary>
        /// Inserta una nueva EndososTalonLine
        /// </summary>
        /// <param name="_EndososTalonLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<EndososTalonLine>> Insert([FromBody]EndososTalonLine _EndososTalonLine)
        {
            EndososTalonLine _EndososTalonLineq = new EndososTalonLine();
            try
            {
                _EndososTalonLineq = _EndososTalonLine;
                _context.EndososTalonLine.Add(_EndososTalonLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_EndososTalonLineq);
        }

        /// <summary>
        /// Actualiza la EndososTalonLine
        /// </summary>
        /// <param name="_EndososTalonLine"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<EndososTalonLine>> Update([FromBody]EndososTalonLine _EndososTalonLine)
        {
            EndososTalonLine _EndososTalonLineq = _EndososTalonLine;
            try
            {
                _EndososTalonLineq = await (from c in _context.EndososTalonLine
                                 .Where(q => q.EndososTalonLineId == _EndososTalonLine.EndososTalonLineId)
                                            select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_EndososTalonLineq).CurrentValues.SetValues((_EndososTalonLine));

                //_context.EndososTalonLine.Update(_EndososTalonLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_EndososTalonLineq);
        }

        /// <summary>
        /// Elimina una EndososTalonLine       
        /// </summary>
        /// <param name="_EndososTalonLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]EndososTalonLine _EndososTalonLine)
        {
            EndososTalonLine _EndososTalonLineq = new EndososTalonLine();
            try
            {
                _EndososTalonLineq = _context.EndososTalonLine
                .Where(x => x.EndososTalonLineId == (Int64)_EndososTalonLine.EndososTalonLineId)
                .FirstOrDefault();

                _context.EndososTalonLine.Remove(_EndososTalonLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_EndososTalonLineq);

        }







    }
}