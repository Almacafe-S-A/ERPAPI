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
    [Route("api/JournalEntryLine")]
    [ApiController]
    public class JournalEntryLineController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        /*public DimensionsController(ApplicationDbContext context)
        {
            _context = context;
        }*/
        public JournalEntryLineController(ILogger<JournalEntryLineController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// Obtiene los Datos de la Diarios en una lista.
        /// </summary>

        // GET: api/JournalEntry
        [HttpGet("[action]")]
        public async Task<IActionResult> GetJournalEntryLine()

        {
            List<JournalEntryLine> Items = new List<JournalEntryLine>();
            try
            {
                Items = await _context.JournalEntryLine.ToListAsync();
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
        /// Obtiene los Datos de la JournalEntryLine por medio del Id enviado.
        /// </summary>
        /// <param name="JournalEntryLineId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{JournalEntryLineId}")]
        public async Task<IActionResult> GetJournalEntryLineById(Int64 JournalEntryLineId)
        {
            JournalEntryLine Items = new JournalEntryLine();
            try
            {
                Items = await _context.JournalEntryLine.Where(q => q.JournalEntryLineId == JournalEntryLineId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <returns></returns>
        [HttpGet("[action]/{JournalEntryId}")]
        public async Task<IActionResult> GetJournalEntryLineByJournalId(Int64 JournalEntryId)
        {

            //JournalEntryLine Items = new JournalEntryLine();
            List<JournalEntryLine> Items = new List<JournalEntryLine>();
            try
            {
                //Items = await _context.JournalEntryLine.Where(q => q.JournalEntryId == JournalEntryId).FirstOrDefaultAsync();
                Items = await _context.JournalEntryLine.Where(q => q.JournalEntryId == JournalEntryId).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene los Datos de la JournalEntryLine por medio del Id enviado.
        /// </summary>
        /// 
        /// <param name="Date"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Date}")]
        public async Task<IActionResult> GetJournalEntryLineByDate(DateTime Date)
        {
            string fecha = Date.ToString("yyyy-MM-dd");
            JournalEntryLine Items = new JournalEntryLine();
            try
            {
                Items = await _context.JournalEntryLine.Where(q => q.CreatedDate == Convert.ToDateTime(fecha)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva JournalEntryLine
        /// </summary>
        /// <param name="_JournalEntryLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<JournalEntryLine>> Insert([FromBody]JournalEntryLine _JournalEntryLine)
        {
            JournalEntryLine _JournalEntryLineq = new JournalEntryLine();
            try
            {
                _JournalEntryLineq = _JournalEntryLine;
                _context.JournalEntryLine.Add(_JournalEntryLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_JournalEntryLineq));
        }

        /// <summary>
        /// Actualiza la JournalEntryLine
        /// </summary>
        /// <param name="_JournalEntryLine"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<JournalEntryLine>> Update([FromBody]JournalEntryLine _JournalEntryLine)
        {
            JournalEntryLine _JournalEntryLineq = _JournalEntryLine;
            try
            {
                _JournalEntryLineq = await (from c in _context.JournalEntryLine
                                 .Where(q => q.JournalEntryLineId == _JournalEntryLine.JournalEntryLineId)
                                        select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_JournalEntryLineq).CurrentValues.SetValues((_JournalEntryLine));

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_JournalEntryLineq));
        }

        /// <summary>
        /// Elimina una JournalEntryLine       
        /// </summary>
        /// <param name="_JournalEntryLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]JournalEntryLine _JournalEntryLine)
        {
            JournalEntryLine _JournalEntryLineq = new JournalEntryLine();
            try
            {
                _JournalEntryLineq = _context.JournalEntryLine
                .Where(x => x.JournalEntryLineId == (Int64)_JournalEntryLine.JournalEntryLineId)
                .FirstOrDefault();

                _context.JournalEntryLine.Remove(_JournalEntryLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_JournalEntryLineq));

        }
    }
}