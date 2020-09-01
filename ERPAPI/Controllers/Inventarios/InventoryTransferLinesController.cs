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
    [Route("api/InventoryTransferLine")]
    [ApiController]
    public class InventoryTransferLineController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public InventoryTransferLineController(ILogger<InventoryTransferLineController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de InventoryTransferLinees 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInventoryTransferLine()
        {
            List<InventoryTransferLine> Items = new List<InventoryTransferLine>();
            try
            {
                Items = await _context.InventoryTransferLine.ToListAsync();
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
        /// Obtiene los Datos de la InventoryTransferLine por medio del Id enviado.
        /// </summary>
        /// <param name="InventoryTransferLineId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{InventoryTransferLineId}")]
        public async Task<IActionResult> GetInventoryTransferLineById(Int64 InventoryTransferLineId)
        {
            InventoryTransferLine Items = new InventoryTransferLine();
            try
            {
                Items = await _context.InventoryTransferLine.Where(q => q.Id == InventoryTransferLineId).FirstOrDefaultAsync();
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
        public async Task<IActionResult> GetInventoryTransferLineByInventoryTransferId(Int64 InventoryTransferId)
        {
            List<InventoryTransferLine> Items = new List<InventoryTransferLine>();
            try
            {
                Items = await _context.InventoryTransferLine
                             .Where(q => q.InventoryTransferId == InventoryTransferId).ToListAsync();
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
        /// Inserta una nueva InventoryTransferLine
        /// </summary>
        /// <param name="_InventoryTransferLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<InventoryTransferLine>> Insert([FromBody]InventoryTransferLine _InventoryTransferLine)
        {
            InventoryTransferLine _InventoryTransferLineq = new InventoryTransferLine();
            try
            {
                _InventoryTransferLineq = _InventoryTransferLine;
                _context.InventoryTransferLine.Add(_InventoryTransferLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_InventoryTransferLineq));
        }

        /// <summary>
        /// Actualiza la InventoryTransferLine
        /// </summary>
        /// <param name="_InventoryTransferLine"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<InventoryTransferLine>> Update([FromBody]InventoryTransferLine _InventoryTransferLine)
        {
            InventoryTransferLine _InventoryTransferLineq = _InventoryTransferLine;
            try
            {
                _InventoryTransferLineq = await (from c in _context.InventoryTransferLine
                                 .Where(q => q.Id == _InventoryTransferLine.Id)
                                             select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_InventoryTransferLineq).CurrentValues.SetValues((_InventoryTransferLine));

                //_context.InventoryTransferLine.Update(_InventoryTransferLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_InventoryTransferLineq));
        }

        /// <summary>
        /// Elimina una InventoryTransferLine       
        /// </summary>
        /// <param name="_InventoryTransferLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]InventoryTransferLine _InventoryTransferLine)
        {
            InventoryTransferLine _InventoryTransferLineq = new InventoryTransferLine();
            try
            {
                _InventoryTransferLineq = _context.InventoryTransferLine
                .Where(x => x.Id == (Int64)_InventoryTransferLine.Id)
                .FirstOrDefault();

                _context.InventoryTransferLine.Remove(_InventoryTransferLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_InventoryTransferLineq));

        }







    }
}