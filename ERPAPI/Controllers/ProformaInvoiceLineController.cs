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
    [Route("api/ProformaInvoiceLine")]
    [ApiController]
    public class ProformaInvoiceLineController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ProformaInvoiceLineController(ILogger<ProformaInvoiceLineController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de ProformaInvoiceLinees 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProformaInvoiceLine()
        {
            List<ProformaInvoiceLine> Items = new List<ProformaInvoiceLine>();
            try
            {
                Items = await _context.ProformaInvoiceLine.ToListAsync();
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
        /// Obtiene los Datos de la ProformaInvoiceLine por medio del Id enviado.
        /// </summary>
        /// <param name="ProformaLineId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{ProformaLineId}")]
        public async Task<IActionResult> GetProformaInvoiceLineById(Int64 ProformaLineId)
        {
            ProformaInvoiceLine Items = new ProformaInvoiceLine();
            try
            {
                Items = await _context.ProformaInvoiceLine.Where(q => q.ProformaLineId == ProformaLineId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva ProformaInvoiceLine
        /// </summary>
        /// <param name="_ProformaInvoiceLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<ProformaInvoiceLine>> Insert([FromBody]ProformaInvoiceLine _ProformaInvoiceLine)
        {
            ProformaInvoiceLine _ProformaInvoiceLineq = new ProformaInvoiceLine();
            try
            {
                _ProformaInvoiceLineq = _ProformaInvoiceLine;
                _context.ProformaInvoiceLine.Add(_ProformaInvoiceLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_ProformaInvoiceLineq));
        }

        [HttpGet("[action]/{ProformaInvoiceId}")]
        public async Task<IActionResult> GetProformaInvoiceLineByProformaInvoiceId(Int64 ProformaInvoiceId)
        {
            List<ProformaInvoiceLine> Items = new List<ProformaInvoiceLine>();
            try
            {
                Items = await _context.ProformaInvoiceLine
                             .Where(q => q.ProformaInvoiceId == ProformaInvoiceId).ToListAsync();
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
        /// Actualiza la ProformaInvoiceLine
        /// </summary>
        /// <param name="_ProformaInvoiceLine"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<ProformaInvoiceLine>> Update([FromBody]ProformaInvoiceLine _ProformaInvoiceLine)
        {
            ProformaInvoiceLine _ProformaInvoiceLineq = _ProformaInvoiceLine;
            try
            {
                _ProformaInvoiceLineq = await (from c in _context.ProformaInvoiceLine
                                 .Where(q => q.ProformaLineId == _ProformaInvoiceLine.ProformaLineId)
                                               select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_ProformaInvoiceLineq).CurrentValues.SetValues((_ProformaInvoiceLine));

                //_context.ProformaInvoiceLine.Update(_ProformaInvoiceLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_ProformaInvoiceLineq));
        }

        /// <summary>
        /// Elimina una ProformaInvoiceLine       
        /// </summary>
        /// <param name="_ProformaInvoiceLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]ProformaInvoiceLine _ProformaInvoiceLine)
        {
            ProformaInvoiceLine _ProformaInvoiceLineq = new ProformaInvoiceLine();
            try
            {
                _ProformaInvoiceLineq = _context.ProformaInvoiceLine
                .Where(x => x.ProformaLineId == (Int64)_ProformaInvoiceLine.ProformaLineId)
                .FirstOrDefault();

                _context.ProformaInvoiceLine.Remove(_ProformaInvoiceLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_ProformaInvoiceLineq));

        }







    }
}