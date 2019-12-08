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
    [Route("api/PurchaseOrderLine")]
    [ApiController]
    public class PurchaseOrderLineController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public PurchaseOrderLineController(ILogger<PurchaseOrderLineController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de PurchaseOrderLinees 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPurchaseOrderLine()
        {
            List<PurchaseOrderLine> Items = new List<PurchaseOrderLine>();
            try
            {
                Items = await _context.PurchaseOrderLine.ToListAsync();
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
        /// Obtiene los Datos de la PurchaseOrderLine por medio del Id enviado.
        /// </summary>
        /// <param name="PurchaseOrderLineId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{PurchaseOrderLineId}")]
        public async Task<IActionResult> GetPurchaseOrderLineById(Int64 PurchaseOrderLineId)
        {
            PurchaseOrderLine Items = new PurchaseOrderLine();
            try
            {
                Items = await _context.PurchaseOrderLine.Where(q => q.Id == PurchaseOrderLineId).FirstOrDefaultAsync();
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
        /// <param name="PurchaseOrderId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{PurchaseOrderId}")]
        public async Task<IActionResult> GetPurchaseOrderLineByPurchaseOrderId(Int64 PurchaseOrderId)
        {
            List<PurchaseOrderLine> Items = new List<PurchaseOrderLine>();
            try
            {
                Items = await _context.PurchaseOrderLine
                             .Where(q => q.PurchaseOrderId == PurchaseOrderId).ToListAsync();
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
        /// Inserta una nueva PurchaseOrderLine
        /// </summary>
        /// <param name="_PurchaseOrderLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<PurchaseOrderLine>> Insert([FromBody]PurchaseOrderLine _PurchaseOrderLine)
        {
            PurchaseOrderLine _PurchaseOrderLineq = new PurchaseOrderLine();
            try
            {
                _PurchaseOrderLineq = _PurchaseOrderLine;
                _context.PurchaseOrderLine.Add(_PurchaseOrderLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_PurchaseOrderLineq));
        }

        /// <summary>
        /// Actualiza la PurchaseOrderLine
        /// </summary>
        /// <param name="_PurchaseOrderLine"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<PurchaseOrderLine>> Update([FromBody]PurchaseOrderLine _PurchaseOrderLine)
        {
            PurchaseOrderLine _PurchaseOrderLineq = _PurchaseOrderLine;
            try
            {
                _PurchaseOrderLineq = await (from c in _context.PurchaseOrderLine
                                 .Where(q => q.Id == _PurchaseOrderLine.Id)
                                             select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_PurchaseOrderLineq).CurrentValues.SetValues((_PurchaseOrderLine));

                //_context.PurchaseOrderLine.Update(_PurchaseOrderLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_PurchaseOrderLineq));
        }

        /// <summary>
        /// Elimina una PurchaseOrderLine       
        /// </summary>
        /// <param name="_PurchaseOrderLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]PurchaseOrderLine _PurchaseOrderLine)
        {
            PurchaseOrderLine _PurchaseOrderLineq = new PurchaseOrderLine();
            try
            {
                _PurchaseOrderLineq = _context.PurchaseOrderLine
                .Where(x => x.Id == (Int64)_PurchaseOrderLine.Id)
                .FirstOrDefault();

                _context.PurchaseOrderLine.Remove(_PurchaseOrderLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_PurchaseOrderLineq));

        }







    }
}