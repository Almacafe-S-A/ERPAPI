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
    [Route("api/CreditNoteLine")]
    [ApiController]
    public class CreditNoteLineController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CreditNoteLineController(ILogger<CreditNoteLineController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de CreditNoteLine paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCreditNoteLinePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<CreditNoteLine> Items = new List<CreditNoteLine>();
            try
            {
                var query = _context.CreditNoteLine.AsQueryable();
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
        /// Obtiene el Listado de CreditNoteLinees 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCreditNoteLine()
        {
            List<CreditNoteLine> Items = new List<CreditNoteLine>();
            try
            {
                Items = await _context.CreditNoteLine.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene los Datos de la CreditNoteLine por medio del Id enviado.
        /// </summary>
        /// <param name="CreditNoteLineId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CreditNoteLineId}")]
        public async Task<IActionResult> GetCreditNoteLineById(Int64 CreditNoteLineId)
        {
            CreditNoteLine Items = new CreditNoteLine();
            try
            {
                Items = await _context.CreditNoteLine.Where(q => q.CreditNoteLineId == CreditNoteLineId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{CreditNoteId}")]
        public async Task<IActionResult> GetCreditNoteLineByCreditNoteId(Int64 CreditNoteId)
        {
            List<CreditNoteLine> Items = new List<CreditNoteLine>();
            try
            {
                Items = await _context.CreditNoteLine
                             .Where(q => q.CreditNoteId == CreditNoteId).ToListAsync();
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
        /// Inserta una nueva CreditNoteLine
        /// </summary>
        /// <param name="_CreditNoteLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<CreditNoteLine>> Insert([FromBody]CreditNoteLine _CreditNoteLine)
        {
            CreditNoteLine _CreditNoteLineq = new CreditNoteLine();
            try
            {
                _CreditNoteLineq = _CreditNoteLine;
                _context.CreditNoteLine.Add(_CreditNoteLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CreditNoteLineq));
        }

        /// <summary>
        /// Actualiza la CreditNoteLine
        /// </summary>
        /// <param name="_CreditNoteLine"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<CreditNoteLine>> Update([FromBody]CreditNoteLine _CreditNoteLine)
        {
            CreditNoteLine _CreditNoteLineq = _CreditNoteLine;
            try
            {
                _CreditNoteLineq = await (from c in _context.CreditNoteLine
                                 .Where(q => q.CreditNoteLineId == _CreditNoteLine.CreditNoteLineId)
                                          select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_CreditNoteLineq).CurrentValues.SetValues((_CreditNoteLine));

                //_context.CreditNoteLine.Update(_CreditNoteLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CreditNoteLineq));
        }

        /// <summary>
        /// Elimina una CreditNoteLine       
        /// </summary>
        /// <param name="_CreditNoteLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]CreditNoteLine _CreditNoteLine)
        {
            CreditNoteLine _CreditNoteLineq = new CreditNoteLine();
            try
            {
                _CreditNoteLineq = _context.CreditNoteLine
                .Where(x => x.CreditNoteLineId == (Int64)_CreditNoteLine.CreditNoteLineId)
                .FirstOrDefault();

                _context.CreditNoteLine.Remove(_CreditNoteLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CreditNoteLineq));

        }







    }
}