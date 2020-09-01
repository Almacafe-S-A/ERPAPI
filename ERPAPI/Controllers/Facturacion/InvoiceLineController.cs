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
    [Route("api/InvoiceLine")]
    [ApiController]
    public class InvoiceLineController : Controller 
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public InvoiceLineController(ILogger<InvoiceLineController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de InvoiceLine paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInvoiceLinePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<InvoiceLine> Items = new List<InvoiceLine>();
            try
            {
                var query = _context.InvoiceLine.AsQueryable();
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


        [HttpGet("[action]/{InvoiceId}")]
        public async Task<IActionResult> GetInvoiceLineByInvoiceId(Int64 InvoiceId)
        {
            List<InvoiceLine> Items = new List<InvoiceLine>();
            try
            {
                Items = await _context.InvoiceLine
                             .Where(q => q.InvoiceId == InvoiceId).ToListAsync();
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
        /// Obtiene el Listado de InvoiceLinees 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInvoiceLine()
        {
            List<InvoiceLine> Items = new List<InvoiceLine>();
            try
            {
                Items = await _context.InvoiceLine.ToListAsync();
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
        /// Obtiene los Datos de la InvoiceLine por medio del Id enviado.
        /// </summary>
        /// <param name="InvoiceLineId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{InvoiceLineId}")]
        public async Task<IActionResult> GetInvoiceLineById(Int64 InvoiceLineId)
        {
            InvoiceLine Items = new InvoiceLine();
            try
            {
                Items = await _context.InvoiceLine.Where(q => q.InvoiceLineId == InvoiceLineId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva InvoiceLine
        /// </summary>
        /// <param name="_InvoiceLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<InvoiceLine>> Insert([FromBody]InvoiceLine _InvoiceLine)
        {
            InvoiceLine _InvoiceLineq = new InvoiceLine();
            try
            {
                _InvoiceLineq = _InvoiceLine;
                _context.InvoiceLine.Add(_InvoiceLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_InvoiceLineq));
        }

        /// <summary>
        /// Actualiza la InvoiceLine
        /// </summary>
        /// <param name="_InvoiceLine"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<InvoiceLine>> Update([FromBody]InvoiceLine _InvoiceLine)
        {
            InvoiceLine _InvoiceLineq = _InvoiceLine;
            try
            {
                _InvoiceLineq = await (from c in _context.InvoiceLine
                                 .Where(q => q.InvoiceLineId == _InvoiceLine.InvoiceLineId)
                                       select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_InvoiceLineq).CurrentValues.SetValues((_InvoiceLine));

                //_context.InvoiceLine.Update(_InvoiceLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_InvoiceLineq));
        }

        /// <summary>
        /// Elimina una InvoiceLine       
        /// </summary>
        /// <param name="_InvoiceLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]InvoiceLine _InvoiceLine)
        {
            InvoiceLine _InvoiceLineq = new InvoiceLine();
            try
            {
                _InvoiceLineq = _context.InvoiceLine
                .Where(x => x.InvoiceLineId == (Int64)_InvoiceLine.InvoiceLineId)
                .FirstOrDefault();

                _context.InvoiceLine.Remove(_InvoiceLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_InvoiceLineq));

        }







    }
}