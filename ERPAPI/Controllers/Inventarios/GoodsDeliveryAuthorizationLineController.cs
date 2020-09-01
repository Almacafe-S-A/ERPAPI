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
    [Route("api/GoodsDeliveryAuthorizationLine")]
    [ApiController]
    public class GoodsDeliveryAuthorizationLineController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public GoodsDeliveryAuthorizationLineController(ILogger<GoodsDeliveryAuthorizationLineController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de GoodsDeliveryAuthorizationLinees 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetGoodsDeliveryAuthorizationLine()
        {
            List<GoodsDeliveryAuthorizationLine> Items = new List<GoodsDeliveryAuthorizationLine>();
            try
            {
                Items = await _context.GoodsDeliveryAuthorizationLine.ToListAsync();
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
        /// Obtiene los Datos de la GoodsDeliveryAuthorizationLine por medio del Id enviado.
        /// </summary>
        /// <param name="GoodsDeliveryAuthorizationLineId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{GoodsDeliveryAuthorizationLineId}")]
        public async Task<IActionResult> GetGoodsDeliveryAuthorizationLineById(Int64 GoodsDeliveryAuthorizationLineId)
        {
            GoodsDeliveryAuthorizationLine Items = new GoodsDeliveryAuthorizationLine();
            try
            {
                Items = await _context.GoodsDeliveryAuthorizationLine.Where(q => q.GoodsDeliveryAuthorizationLineId == GoodsDeliveryAuthorizationLineId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        [HttpGet("[action]/{GoodsDeliveryAuthorizationId}")]
        public async Task<IActionResult> GetGoodsReceivedLineByGoodsReceivedId(Int64 GoodsDeliveryAuthorizationId)
        {
            List<GoodsDeliveryAuthorizationLine> Items = new List<GoodsDeliveryAuthorizationLine>();
            try
            {
                Items = await _context.GoodsDeliveryAuthorizationLine
                             .Where(q => q.GoodsDeliveryAuthorizationId == GoodsDeliveryAuthorizationId).ToListAsync();
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
        /// Inserta una nueva GoodsDeliveryAuthorizationLine
        /// </summary>
        /// <param name="_GoodsDeliveryAuthorizationLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<GoodsDeliveryAuthorizationLine>> Insert([FromBody]GoodsDeliveryAuthorizationLine _GoodsDeliveryAuthorizationLine)
        {
            GoodsDeliveryAuthorizationLine _GoodsDeliveryAuthorizationLineq = new GoodsDeliveryAuthorizationLine();
            try
            {
                _GoodsDeliveryAuthorizationLineq = _GoodsDeliveryAuthorizationLine;
                _context.GoodsDeliveryAuthorizationLine.Add(_GoodsDeliveryAuthorizationLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_GoodsDeliveryAuthorizationLineq));
        }

        /// <summary>
        /// Actualiza la GoodsDeliveryAuthorizationLine
        /// </summary>
        /// <param name="_GoodsDeliveryAuthorizationLine"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<GoodsDeliveryAuthorizationLine>> Update([FromBody]GoodsDeliveryAuthorizationLine _GoodsDeliveryAuthorizationLine)
        {
            GoodsDeliveryAuthorizationLine _GoodsDeliveryAuthorizationLineq = _GoodsDeliveryAuthorizationLine;
            try
            {
                _GoodsDeliveryAuthorizationLineq = await (from c in _context.GoodsDeliveryAuthorizationLine
                                 .Where(q => q.GoodsDeliveryAuthorizationLineId == _GoodsDeliveryAuthorizationLine.GoodsDeliveryAuthorizationLineId)
                                                          select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_GoodsDeliveryAuthorizationLineq).CurrentValues.SetValues((_GoodsDeliveryAuthorizationLine));

                //_context.GoodsDeliveryAuthorizationLine.Update(_GoodsDeliveryAuthorizationLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_GoodsDeliveryAuthorizationLineq));
        }

        /// <summary>
        /// Elimina una GoodsDeliveryAuthorizationLine       
        /// </summary>
        /// <param name="_GoodsDeliveryAuthorizationLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]GoodsDeliveryAuthorizationLine _GoodsDeliveryAuthorizationLine)
        {
            GoodsDeliveryAuthorizationLine _GoodsDeliveryAuthorizationLineq = new GoodsDeliveryAuthorizationLine();
            try
            {
                _GoodsDeliveryAuthorizationLineq = _context.GoodsDeliveryAuthorizationLine
                .Where(x => x.GoodsDeliveryAuthorizationLineId == (Int64)_GoodsDeliveryAuthorizationLine.GoodsDeliveryAuthorizationLineId)
                .FirstOrDefault();

                _context.GoodsDeliveryAuthorizationLine.Remove(_GoodsDeliveryAuthorizationLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_GoodsDeliveryAuthorizationLineq));

        }







    }
}