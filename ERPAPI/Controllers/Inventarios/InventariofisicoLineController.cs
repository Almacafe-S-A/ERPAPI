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
    [Route("api/InventarioFisicoLine")]
    [ApiController]
    public class InventarioFisicoLineController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public InventarioFisicoLineController(ILogger<InventarioFisicoLineController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de InventarioFisicoLinees 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInventarioFisicoLine()
        {
            List<InventarioFisicoLine> Items = new List<InventarioFisicoLine>();
            try
            {
                Items = await _context.InventarioFisicoLines.ToListAsync();
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
        /// Obtiene los Datos de la InventarioFisicoLine por medio del Id enviado.
        /// </summary>
        /// <param name="InventarioFisicoLineId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{InventarioFisicoLineId}")]
        public async Task<IActionResult> GetInventarioFisicoLineById(Int64 InventarioFisicoLineId)
        {
            InventarioFisicoLine Items = new InventarioFisicoLine();
            try
            {
                Items = await _context.InventarioFisicoLines.Where(q => q.Id == InventarioFisicoLineId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Obtiene el detalle de las mercaderias.
        /// </summary>
        /// <param name="InventoryTransferId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{InventoryTransferId}")]
        public async Task<IActionResult> GetInventarioFisicoLineByInventoryTransferId(Int64 InventoryTransferId)
        {
            List<InventarioFisicoLine> Items = new List<InventarioFisicoLine>();
            try
            {
                Items = await _context.InventarioFisicoLines
                             .Where(q => q.InventarioFisicoId == InventoryTransferId).ToListAsync();
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
        /// Inserta una nueva InventarioFisicoLine
        /// </summary>
        /// <param name="_InventarioFisicoLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<InventarioFisicoLine>> Insert([FromBody]InventarioFisicoLine _InventarioFisicoLine)
        {
            InventarioFisicoLine _InventarioFisicoLineq = new InventarioFisicoLine();
            try
            {
                _InventarioFisicoLineq = _InventarioFisicoLine;
                _context.InventarioFisicoLines.Add(_InventarioFisicoLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_InventarioFisicoLineq));
        }

        /// <summary>
        /// Actualiza la InventarioFisicoLine
        /// </summary>
        /// <param name="_InventarioFisicoLine"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<InventarioFisicoLine>> Update([FromBody]InventarioFisicoLine _InventarioFisicoLine)
        {
            InventarioFisicoLine _InventarioFisicoLineq = _InventarioFisicoLine;
            try
            {
                _InventarioFisicoLineq = await (from c in _context.InventarioFisicoLines
                                 .Where(q => q.Id == _InventarioFisicoLine.Id)
                                             select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_InventarioFisicoLineq).CurrentValues.SetValues((_InventarioFisicoLine));

                //_context.InventarioFisicoLines.Update(_InventarioFisicoLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_InventarioFisicoLineq));
        }

        /// <summary>
        /// Elimina una InventarioFisicoLine       
        /// </summary>
        /// <param name="_InventarioFisicoLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]InventarioFisicoLine _InventarioFisicoLine)
        {
            InventarioFisicoLine _InventarioFisicoLineq = new InventarioFisicoLine();
            try
            {
                _InventarioFisicoLineq = _context.InventarioFisicoLines
                .Where(x => x.Id == (Int64)_InventarioFisicoLine.Id)
                .FirstOrDefault();

                _context.InventarioFisicoLines.Remove(_InventarioFisicoLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_InventarioFisicoLineq));

        }







    }
}