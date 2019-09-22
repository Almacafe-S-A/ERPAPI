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
    [Route("api/Invoice")]
    [ApiController]
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public InvoiceController(ILogger<InvoiceController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Invoice paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInvoicePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Invoice> Items = new List<Invoice>();
            try
            {
                var query = _context.Invoice.AsQueryable();
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
        /// Obtiene el Listado de Invoicees 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInvoice()
        {
            List<Invoice> Items = new List<Invoice>();
            try
            {
                Items = await _context.Invoice.ToListAsync();
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
        /// Obtiene los Datos de la Invoice por medio del Id enviado.
        /// </summary>
        /// <param name="InvoiceId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{InvoiceId}")]
        public async Task<IActionResult> GetInvoiceById(Int64 InvoiceId)
        {
            Invoice Items = new Invoice();
            try
            {
                Items = await _context.Invoice.Where(q => q.InvoiceId == InvoiceId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva Invoice
        /// </summary>
        /// <param name="_Invoice"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Invoice>> Insert([FromBody]Invoice _Invoice)
        {
            Invoice _Invoiceq = new Invoice();
            try
            {
                _Invoiceq = _Invoice;
                _context.Invoice.Add(_Invoiceq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Invoiceq));
        }

        /// <summary>
        /// Actualiza la Invoice
        /// </summary>
        /// <param name="_Invoice"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Invoice>> Update([FromBody]Invoice _Invoice)
        {
            Invoice _Invoiceq = _Invoice;
            try
            {
                _Invoiceq = await (from c in _context.Invoice
                                 .Where(q => q.InvoiceId == _Invoice.InvoiceId)
                                   select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_Invoiceq).CurrentValues.SetValues((_Invoice));

                //_context.Invoice.Update(_Invoiceq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Invoiceq));
        }

        /// <summary>
        /// Elimina una Invoice       
        /// </summary>
        /// <param name="_Invoice"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Invoice _Invoice)
        {
            Invoice _Invoiceq = new Invoice();
            try
            {
                _Invoiceq = _context.Invoice
                .Where(x => x.InvoiceId == (Int64)_Invoice.InvoiceId)
                .FirstOrDefault();

                _context.Invoice.Remove(_Invoiceq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Invoiceq));

        }







    }
}