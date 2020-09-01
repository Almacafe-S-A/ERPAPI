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
    [Route("api/CertificadoLine")]
    [ApiController]
    public class CertificadoLineController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CertificadoLineController(ILogger<CertificadoLineController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de CertificadoLinees 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCertificadoLine()
        {
            List<CertificadoLine> Items = new List<CertificadoLine>();
            try
            {
                Items = await _context.CertificadoLine.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{IdCD}")]
        public async Task<IActionResult> GetCertificadoLineByIdCD(Int64 IdCD)
        {
            List<CertificadoLine> Items = new List<CertificadoLine>();
            try
            {
                Items = await _context.CertificadoLine
                             .Where(q => q.IdCD == IdCD).ToListAsync();
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
        /// Obtiene los Datos de la CertificadoLine por medio del Id enviado.
        /// </summary>
        /// <param name="CertificadoLineId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CertificadoLineId}")]
        public async Task<IActionResult> GetCertificadoLineById(Int64 CertificadoLineId)
        {
            CertificadoLine Items = new CertificadoLine();
            try
            {
                Items = await _context.CertificadoLine.Where(q => q.CertificadoLineId == CertificadoLineId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva CertificadoLine
        /// </summary>
        /// <param name="_CertificadoLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<CertificadoLine>> Insert([FromBody]CertificadoLine _CertificadoLine)
        {
            CertificadoLine _CertificadoLineq = new CertificadoLine();
            try
            {
                _CertificadoLineq = _CertificadoLine;
                _context.CertificadoLine.Add(_CertificadoLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_CertificadoLineq));
        }

        /// <summary>
        /// Actualiza la CertificadoLine
        /// </summary>
        /// <param name="_CertificadoLine"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<CertificadoLine>> Update([FromBody]CertificadoLine _CertificadoLine)
        {
            CertificadoLine _CertificadoLineq = _CertificadoLine;
            try
            {
                _CertificadoLineq = await (from c in _context.CertificadoLine
                                 .Where(q => q.CertificadoLineId == _CertificadoLine.CertificadoLineId)
                                           select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_CertificadoLineq).CurrentValues.SetValues((_CertificadoLine));

                //_context.CertificadoLine.Update(_CertificadoLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_CertificadoLineq));
        }

        /// <summary>
        /// Elimina una CertificadoLine       
        /// </summary>
        /// <param name="_CertificadoLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]CertificadoLine _CertificadoLine)
        {
            CertificadoLine _CertificadoLineq = new CertificadoLine();
            try
            {
                _CertificadoLineq = _context.CertificadoLine
                .Where(x => x.CertificadoLineId == (Int64)_CertificadoLine.CertificadoLineId)
                .FirstOrDefault();

                _context.CertificadoLine.Remove(_CertificadoLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_CertificadoLineq));

        }







    }
}