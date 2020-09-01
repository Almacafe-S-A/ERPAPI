using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorInvoiceLineController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public VendorInvoiceLineController(ILogger<VendorInvoiceLineController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de VendorInvoiceLine paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetVendorInvoiceLinePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<VendorInvoiceLine> Items = new List<VendorInvoiceLine>();
            try
            {
                var query = _context.VendorInvoiceLine.AsQueryable();
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
        public async Task<IActionResult> GetVendorInvoiceLineByInvoiceId(Int64 InvoiceId)
        {
            List<VendorInvoiceLine> Items = new List<VendorInvoiceLine>();
            try
            {
                Items = await _context.VendorInvoiceLine
                             .Where(q => q.VendorInvoiceId == InvoiceId).ToListAsync();
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
        /// Obtiene el Listado de VendorInvoiceLinees 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetVendorInvoiceLine()
        {
            List<VendorInvoiceLine> Items = new List<VendorInvoiceLine>();
            try
            {
                Items = await _context.VendorInvoiceLine.ToListAsync();
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
        /// Obtiene los Datos de la VendorInvoiceLine por medio del Id enviado.
        /// </summary>
        /// <param name="VendorInvoiceLineId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{VendorInvoiceLineId}")]
        public async Task<IActionResult> GetVendorInvoiceLineById(Int64 VendorInvoiceLineId)
        {
            VendorInvoiceLine Items = new VendorInvoiceLine();
            try
            {
                Items = await _context.VendorInvoiceLine.Where(q => q.VendorInvoiceLineId == VendorInvoiceLineId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva VendorInvoiceLine
        /// </summary>
        /// <param name="_VendorInvoiceLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<VendorInvoiceLine>> Insert([FromBody]VendorInvoiceLine _VendorInvoiceLine)
        {
            VendorInvoiceLine _VendorInvoiceLineq = new VendorInvoiceLine();
            try
            {
                _VendorInvoiceLineq = _VendorInvoiceLine;
                _context.VendorInvoiceLine.Add(_VendorInvoiceLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_VendorInvoiceLineq));
        }

        /// <summary>
        /// Actualiza la VendorInvoiceLine
        /// </summary>
        /// <param name="_VendorInvoiceLine"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<VendorInvoiceLine>> Update([FromBody]VendorInvoiceLine _VendorInvoiceLine)
        {
            VendorInvoiceLine _VendorInvoiceLineq = _VendorInvoiceLine;
            try
            {
                _VendorInvoiceLineq = await (from c in _context.VendorInvoiceLine
                                 .Where(q => q.VendorInvoiceLineId == _VendorInvoiceLine.VendorInvoiceLineId)
                                       select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_VendorInvoiceLineq).CurrentValues.SetValues((_VendorInvoiceLine));

                //_context.VendorInvoiceLine.Update(_VendorInvoiceLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_VendorInvoiceLineq));
        }

        /// <summary>
        /// Elimina una VendorInvoiceLine       
        /// </summary>
        /// <param name="_VendorInvoiceLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]VendorInvoiceLine _VendorInvoiceLine)
        {
            VendorInvoiceLine _VendorInvoiceLineq = new VendorInvoiceLine();
            try
            {
                _VendorInvoiceLineq = _context.VendorInvoiceLine
                .Where(x => x.VendorInvoiceLineId == (Int64)_VendorInvoiceLine.VendorInvoiceLineId)
                .FirstOrDefault();

                _context.VendorInvoiceLine.Remove(_VendorInvoiceLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_VendorInvoiceLineq));

        }







    }
}
