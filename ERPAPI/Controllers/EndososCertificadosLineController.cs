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
    [Route("api/EndososCertificadosLine")]
    [ApiController]
    public class EndososCertificadosLineController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public EndososCertificadosLineController(ILogger<EndososCertificadosLineController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de EndososCertificadosLinees 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEndososCertificadosLine()
        {
            List<EndososCertificadosLine> Items = new List<EndososCertificadosLine>();
            try
            {
                Items = await _context.EndososCertificadosLine.ToListAsync();
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
        /// Obtiene los Datos de la EndososCertificadosLine por medio del Id enviado.
        /// </summary>
        /// <param name="EndososCertificadosLineId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{EndososCertificadosLineId}")]
        public async Task<IActionResult> GetEndososCertificadosLineById(Int64 EndososCertificadosLineId)
        {
            EndososCertificadosLine Items = new EndososCertificadosLine();
            try
            {
                Items = await _context.EndososCertificadosLine.Where(q => q.EndososCertificadosLineId == EndososCertificadosLineId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }

        [HttpGet("[action]/{EndososCertificadosId}")]
        public async Task<IActionResult> GetEndososCertificadosLineByEndososCertificadosId(Int64 EndososCertificadosId)
        {
            List<EndososCertificadosLine> Items = new List<EndososCertificadosLine>();
            try
            {
                Items = await _context.EndososCertificadosLine
                             .Where(q => q.EndososCertificadosId == EndososCertificadosId).ToListAsync();
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
        /// Inserta una nueva EndososCertificadosLine
        /// </summary>
        /// <param name="_EndososCertificadosLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<EndososCertificadosLine>> Insert([FromBody]EndososCertificadosLine _EndososCertificadosLine)
        {
            EndososCertificadosLine _EndososCertificadosLineq = new EndososCertificadosLine();
            try
            {
                _EndososCertificadosLineq = _EndososCertificadosLine;
                _context.EndososCertificadosLine.Add(_EndososCertificadosLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_EndososCertificadosLineq);
        }

        /// <summary>
        /// Actualiza la EndososCertificadosLine
        /// </summary>
        /// <param name="_EndososCertificadosLine"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<EndososCertificadosLine>> Update([FromBody]EndososCertificadosLine _EndososCertificadosLine)
        {
            EndososCertificadosLine _EndososCertificadosLineq = _EndososCertificadosLine;
            try
            {
                _EndososCertificadosLineq = await (from c in _context.EndososCertificadosLine
                                 .Where(q => q.EndososCertificadosLineId == _EndososCertificadosLine.EndososCertificadosLineId)
                                                   select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_EndososCertificadosLineq).CurrentValues.SetValues((_EndososCertificadosLine));

                //_context.EndososCertificadosLine.Update(_EndososCertificadosLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_EndososCertificadosLineq);
        }

        /// <summary>
        /// Elimina una EndososCertificadosLine       
        /// </summary>
        /// <param name="_EndososCertificadosLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]EndososCertificadosLine _EndososCertificadosLine)
        {
            EndososCertificadosLine _EndososCertificadosLineq = new EndososCertificadosLine();
            try
            {
                _EndososCertificadosLineq = _context.EndososCertificadosLine
                .Where(x => x.EndososCertificadosLineId == (Int64)_EndososCertificadosLine.EndososCertificadosLineId)
                .FirstOrDefault();

                _context.EndososCertificadosLine.Remove(_EndososCertificadosLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_EndososCertificadosLineq);

        }







    }
}