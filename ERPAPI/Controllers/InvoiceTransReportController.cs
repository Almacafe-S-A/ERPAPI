using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceTransReportController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public InvoiceTransReportController(ILogger<InvoiceTransReportController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de toda la data cuya factura fue mayor al monto en el elemento 76 por cantidad de paginas
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInvoiceTransReportPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<InvoiceTransReport> Items = new List<InvoiceTransReport>();
            try
            {
                var query = _context.InvoiceTransReport.AsQueryable();
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
        /// Obtiene el Listado de toda la data cuya factura fue mayor al monto en el elemento 76
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInvoiceTransReport()
        {
            List<InvoiceTransReport> Items = new List<InvoiceTransReport>();
            try
            {
                Items = await _context.InvoiceTransReport.ToListAsync();
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
        /// Obtiene los Datos de Transacciones por Factura por medio del Id enviado.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{IdInvoiceTransReport}")]
        public async Task<IActionResult> GetInvoiceTransReportById(Int64 Id)
        {
            InvoiceTransReport Items = new InvoiceTransReport();
            try
            {
                Items = await _context.InvoiceTransReport.Where(q => q.IdInvoiceTransReport == Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene los Datos de Transacciones por Factura dentro de un rango de fechas.
        /// </summary>
        /// <param name="fechainicio"></param>
        /// <param name="fechafinal"></param>
        /// <returns></returns>
        [HttpGet("[action]/{IdInvoiceTransReport}")]
        public async Task<IActionResult> GetInvoiceTransReportByDates(DateTime fechainicio, DateTime fechafinal)
        {
            InvoiceTransReport Items = new InvoiceTransReport();
            try
            {
                Items = await _context.InvoiceTransReport.Where(q => q.InvoiceDate >= fechainicio && q.InvoiceDate <= fechafinal).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Inserta un nuevo registro de transacciones con montos mayores al establecido en el elemento 76
        /// </summary>
        /// <param name="_IdInvoiceTransReport"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<InvoiceTransReport>> Insert([FromBody]InvoiceTransReport _IdInvoiceTransReport)
        {
            InvoiceTransReport IdInvoiceTransReportq = new InvoiceTransReport();
            try
            {
                IdInvoiceTransReportq = _IdInvoiceTransReport;
                _context.InvoiceTransReport.Add(IdInvoiceTransReportq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(IdInvoiceTransReportq));
        }

        /// <summary>
        /// Actualiza la Severidad Riesgo
        /// </summary>
        /// <param name="_IdInvoiceTransReport"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<InvoiceTransReport>> Update([FromBody]InvoiceTransReport _IdInvoiceTransReport)
        {
            InvoiceTransReport _IdInvoiceTransReportq = _IdInvoiceTransReport;
            try
            {
                _IdInvoiceTransReportq = await (from c in _context.InvoiceTransReport
                                 .Where(q => q.IdInvoiceTransReport == _IdInvoiceTransReport.IdInvoiceTransReport)
                                                select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_IdInvoiceTransReportq).CurrentValues.SetValues((_IdInvoiceTransReport));

                //_context.Bank.Update(_Bankq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_IdInvoiceTransReportq));
        }

        /// <summary>
        /// Elimina una Severidad Riesgo       
        /// </summary>
        /// <param name="_IdInvoiceTransReport"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]InvoiceTransReport _IdInvoiceTransReport)
        {
            InvoiceTransReport _IdInvoiceTransReportq = new InvoiceTransReport();
            try
            {
                _IdInvoiceTransReportq = _context.InvoiceTransReport
                .Where(x => x.IdInvoiceTransReport == (Int64)_IdInvoiceTransReport.IdInvoiceTransReport)
                .FirstOrDefault();

                _context.InvoiceTransReport.Remove(_IdInvoiceTransReportq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_IdInvoiceTransReportq));

        }
    }
}

