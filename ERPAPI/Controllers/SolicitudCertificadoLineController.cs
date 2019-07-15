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
    [Route("api/SolicitudCertificadoLine")]
    [ApiController]
    public class SolicitudCertificadoLineController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public SolicitudCertificadoLineController(ILogger<SolicitudCertificadoLineController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de SolicitudCertificadoLinees 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSolicitudCertificadoLine()
        {
            List<SolicitudCertificadoLine> Items = new List<SolicitudCertificadoLine>();
            try
            {
                Items = await _context.SolicitudCertificadoLine.ToListAsync();
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
        /// Obtiene los Datos de la SolicitudCertificadoLine por medio del Id enviado.
        /// </summary>
        /// <param name="CertificadoLineId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CertificadoLineId}")]
        public async Task<IActionResult> GetSolicitudCertificadoLineById(Int64 CertificadoLineId)
        {
            SolicitudCertificadoLine Items = new SolicitudCertificadoLine();
            try
            {
                Items = await _context.SolicitudCertificadoLine.Where(q => q.CertificadoLineId == CertificadoLineId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva SolicitudCertificadoLine
        /// </summary>
        /// <param name="_SolicitudCertificadoLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<SolicitudCertificadoLine>> Insert([FromBody]SolicitudCertificadoLine _SolicitudCertificadoLine)
        {
            SolicitudCertificadoLine _SolicitudCertificadoLineq = new SolicitudCertificadoLine();
            try
            {
                _SolicitudCertificadoLineq = _SolicitudCertificadoLine;
                _context.SolicitudCertificadoLine.Add(_SolicitudCertificadoLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_SolicitudCertificadoLineq));
        }

        /// <summary>
        /// Actualiza la SolicitudCertificadoLine
        /// </summary>
        /// <param name="_SolicitudCertificadoLine"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<SolicitudCertificadoLine>> Update([FromBody]SolicitudCertificadoLine _SolicitudCertificadoLine)
        {
            SolicitudCertificadoLine _SolicitudCertificadoLineq = _SolicitudCertificadoLine;
            try
            {
                _SolicitudCertificadoLineq = await (from c in _context.SolicitudCertificadoLine
                                 .Where(q => q.CertificadoLineId == _SolicitudCertificadoLine.CertificadoLineId)
                                                    select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_SolicitudCertificadoLineq).CurrentValues.SetValues((_SolicitudCertificadoLine));

                //_context.SolicitudCertificadoLine.Update(_SolicitudCertificadoLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_SolicitudCertificadoLineq));
        }

        /// <summary>
        /// Elimina una SolicitudCertificadoLine       
        /// </summary>
        /// <param name="_SolicitudCertificadoLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]SolicitudCertificadoLine _SolicitudCertificadoLine)
        {
            SolicitudCertificadoLine _SolicitudCertificadoLineq = new SolicitudCertificadoLine();
            try
            {
                _SolicitudCertificadoLineq = _context.SolicitudCertificadoLine
                .Where(x => x.CertificadoLineId == (Int64)_SolicitudCertificadoLine.CertificadoLineId)
                .FirstOrDefault();

                _context.SolicitudCertificadoLine.Remove(_SolicitudCertificadoLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_SolicitudCertificadoLineq));

        }







    }
}