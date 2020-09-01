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
    [Route("api/ControlPalletsLine")]
    [ApiController]
    public class ControlPalletsLineController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ControlPalletsLineController(ILogger<ControlPalletsLineController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }


        /// <summary>
        /// Obtiene el Listado de ControlPalletsLine paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetControlPalletsLinePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<ControlPalletsLine> Items = new List<ControlPalletsLine>();
            try
            {
                var query = _context.ControlPalletsLine.AsQueryable();
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

        /// <summary>
        /// Obtiene el Listado de ControlPalletsLinees 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetControlPalletsLine()
        {
            List<ControlPalletsLine> Items = new List<ControlPalletsLine>();
            try
            {
                Items = await _context.ControlPalletsLine.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }


        [HttpGet("[action]/{ControlPalletsId}")]
        public async Task<IActionResult> GetControlPalletsLineByControlPalletId(Int64 ControlPalletsId)
        {
            List<ControlPalletsLine> Items = new List<ControlPalletsLine>();
            try
            {
                Items = await _context.ControlPalletsLine
                             .Where(q=>q.ControlPalletsId== ControlPalletsId).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return Ok(Items);
        }



        /// <summary>
        /// Obtiene los Datos de la ControlPalletsLine por medio del Id enviado.
        /// </summary>
        /// <param name="ControlPalletsLineId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{ControlPalletsLineId}")]
        public async Task<IActionResult> GetControlPalletsLineById(Int64 ControlPalletsLineId)
        {
            ControlPalletsLine Items = new ControlPalletsLine();
            try
            {
                Items = await _context.ControlPalletsLine.Where(q => q.ControlPalletsLineId == ControlPalletsLineId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }


        /// <summary>
        /// Inserta una nueva ControlPalletsLine
        /// </summary>
        /// <param name="_ControlPalletsLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<ControlPalletsLine>> Insert([FromBody]ControlPalletsLine _ControlPalletsLine)
        {
            ControlPalletsLine _ControlPalletsLineq = new ControlPalletsLine();
            try
            {
                _ControlPalletsLineq = _ControlPalletsLine;
                _context.ControlPalletsLine.Add(_ControlPalletsLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_ControlPalletsLineq);
        }

        /// <summary>
        /// Actualiza la ControlPalletsLine
        /// </summary>
        /// <param name="_ControlPalletsLine"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<ControlPalletsLine>> Update([FromBody]ControlPalletsLine _ControlPalletsLine)
        {
            ControlPalletsLine _ControlPalletsLineq = _ControlPalletsLine;
            try
            {
                _ControlPalletsLineq = await (from c in _context.ControlPalletsLine
                                 .Where(q => q.ControlPalletsLineId == _ControlPalletsLine.ControlPalletsLineId)
                                              select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_ControlPalletsLineq).CurrentValues.SetValues((_ControlPalletsLine));

                //_context.ControlPalletsLine.Update(_ControlPalletsLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_ControlPalletsLineq);
        }

        /// <summary>
        /// Elimina una ControlPalletsLine       
        /// </summary>
        /// <param name="_ControlPalletsLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]ControlPalletsLine _ControlPalletsLine)
        {
            ControlPalletsLine _ControlPalletsLineq = new ControlPalletsLine();
            try
            {
                _ControlPalletsLineq = _context.ControlPalletsLine
                .Where(x => x.ControlPalletsLineId == (Int64)_ControlPalletsLine.ControlPalletsLineId)
                .FirstOrDefault();

                _context.ControlPalletsLine.Remove(_ControlPalletsLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_ControlPalletsLineq);

        }







    }
}