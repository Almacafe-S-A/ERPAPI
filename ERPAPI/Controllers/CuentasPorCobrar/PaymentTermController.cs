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
    public class PaymentTermController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public PaymentTermController(ILogger<PaymentTermController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de PaymentTermss paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPaymentTermssPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<PaymentTerms> Items = new List<PaymentTerms>();
            try
            {
                var query = _context.PaymentTerms.AsQueryable();
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

            return await Task.Run(() => Ok(Items));
        }

        // GET: api/PaymentTerms
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPaymentTerms()
        {
            List<PaymentTerms> Items = new List<PaymentTerms>();
            try
            {
                Items = await _context.PaymentTerms.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        // api/PaymentTermsGetPaymentTermsById
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetPaymentTermsById(int Id)
        {
            PaymentTerms Items = new PaymentTerms();
            try
            {
                Items = await _context.PaymentTerms.Where(q => q.Id.Equals(Id)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<PaymentTerms>> Insert([FromBody]PaymentTerms payload)
        {
            PaymentTerms PaymentTerms = payload;

            try
            {
                _context.PaymentTerms.Add(PaymentTerms);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(PaymentTerms));
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<PaymentTerms>> Update([FromBody]PaymentTerms _PaymentTerms)
        {

            try
            {
                PaymentTerms PaymentTermsq = (from c in _context.PaymentTerms
                   .Where(q => q.Id == _PaymentTerms.Id)
                                select c
                     ).FirstOrDefault();

                _PaymentTerms.FechaCreacion = PaymentTermsq.FechaCreacion;
                _PaymentTerms.UsuarioCreacion = PaymentTermsq.UsuarioCreacion;

                _context.Entry(PaymentTermsq).CurrentValues.SetValues((_PaymentTerms));
                // _context.PaymentTerms.Update(_PaymentTerms);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_PaymentTerms));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]PaymentTerms payload)
        {
            PaymentTerms PaymentTerms = new PaymentTerms();
            try
            {
                PaymentTerms = _context.PaymentTerms
                .Where(x => x.Id == (int)payload.Id)
                .FirstOrDefault();
                _context.PaymentTerms.Remove(PaymentTerms);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(PaymentTerms));

        }


    }
}
