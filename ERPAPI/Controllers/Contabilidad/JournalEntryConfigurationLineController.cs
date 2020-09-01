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
    [Route("api/JournalEntryConfigurationLine")]
    [ApiController]
    public class JournalEntryConfigurationLineController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public JournalEntryConfigurationLineController(ILogger<JournalEntryConfigurationLineController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de JournalEntryConfigurationLine paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetJournalEntryConfigurationLinePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<JournalEntryConfigurationLine> Items = new List<JournalEntryConfigurationLine>();
            try
            {
                var query = _context.JournalEntryConfigurationLine.AsQueryable();
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
        /// Obtiene el Listado de JournalEntryConfigurationLinees 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetJournalEntryConfigurationLine()
        {
            List<JournalEntryConfigurationLine> Items = new List<JournalEntryConfigurationLine>();
            try
            {
                Items = await _context.JournalEntryConfigurationLine.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }



        [HttpGet("[action]/{JournalEntryConfigurationId}")]
        public async Task<IActionResult> GetJournalEntryConfigurationLineByConfigurationId(Int64 JournalEntryConfigurationId)
        {
            List<JournalEntryConfigurationLine> Items = new List<JournalEntryConfigurationLine>();
            try
            {
                Items = await _context.JournalEntryConfigurationLine.Where(q => q.JournalEntryConfigurationId == JournalEntryConfigurationId).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene los Datos de la JournalEntryConfigurationLine por medio del Id enviado.
        /// </summary>
        /// <param name="JournalEntryConfigurationLineId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{JournalEntryConfigurationLineId}")]
        public async Task<IActionResult> GetJournalEntryConfigurationLineById(Int64 JournalEntryConfigurationLineId)
        {
            JournalEntryConfigurationLine Items = new JournalEntryConfigurationLine();
            try
            {
                Items = await _context.JournalEntryConfigurationLine.Where(q => q.JournalEntryConfigurationLineId == JournalEntryConfigurationLineId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva JournalEntryConfigurationLine
        /// </summary>
        /// <param name="_JournalEntryConfigurationLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<JournalEntryConfigurationLine>> Insert([FromBody]JournalEntryConfigurationLine _JournalEntryConfigurationLine)
        {
            JournalEntryConfigurationLine _JournalEntryConfigurationLineq = new JournalEntryConfigurationLine();
            try
            {
                _JournalEntryConfigurationLineq = _JournalEntryConfigurationLine;
                _JournalEntryConfigurationLineq.FechaCreacion = DateTime.Now;
                _JournalEntryConfigurationLineq.FechaModificacion = DateTime.Now;
                _context.JournalEntryConfigurationLine.Add(_JournalEntryConfigurationLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_JournalEntryConfigurationLineq));
        }

        /// <summary>
        /// Actualiza la JournalEntryConfigurationLine
        /// </summary>
        /// <param name="_JournalEntryConfigurationLine"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<JournalEntryConfigurationLine>> Update([FromBody]JournalEntryConfigurationLine _JournalEntryConfigurationLine)
        {
            JournalEntryConfigurationLine _JournalEntryConfigurationLineq = _JournalEntryConfigurationLine;
            try
            {
                _JournalEntryConfigurationLineq = await (from c in _context.JournalEntryConfigurationLine
                                 .Where(q => q.JournalEntryConfigurationLineId == _JournalEntryConfigurationLine.JournalEntryConfigurationLineId)
                                                         select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_JournalEntryConfigurationLineq).CurrentValues.SetValues((_JournalEntryConfigurationLine));

                //_context.JournalEntryConfigurationLine.Update(_JournalEntryConfigurationLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_JournalEntryConfigurationLineq));
        }

        /// <summary>
        /// Elimina una JournalEntryConfigurationLine       
        /// </summary>
        /// <param name="_JournalEntryConfigurationLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]JournalEntryConfigurationLine _JournalEntryConfigurationLine)
        {
            JournalEntryConfigurationLine _JournalEntryConfigurationLineq = new JournalEntryConfigurationLine();
            try
            {
                _JournalEntryConfigurationLineq = _context.JournalEntryConfigurationLine
                .Where(x => x.JournalEntryConfigurationLineId == (Int64)_JournalEntryConfigurationLine.JournalEntryConfigurationLineId)
                .FirstOrDefault();

                _context.JournalEntryConfigurationLine.Remove(_JournalEntryConfigurationLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_JournalEntryConfigurationLineq));

        }







    }
}