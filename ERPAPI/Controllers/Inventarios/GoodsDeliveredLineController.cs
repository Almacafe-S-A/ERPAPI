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
    [Route("api/GoodsDeliveredLine")]
    [ApiController]
    public class GoodsDeliveredLineController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public GoodsDeliveredLineController(ILogger<GoodsDeliveredLineController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de GoodsDeliveredLinees 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetGoodsDeliveredLine()
        {
            List<GoodsDeliveredLine> Items = new List<GoodsDeliveredLine>();
            try
            {
                Items = await _context.GoodsDeliveredLine.ToListAsync();
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
        /// Obtiene los Datos de la GoodsDeliveredLine por medio del Id enviado.
        /// </summary>
        /// <param name="GoodsDeliveredLineId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{GoodsDeliveredLineId}")]
        public async Task<IActionResult> GetGoodsDeliveredLineById(Int64 GoodsDeliveredLineId)
        {
            GoodsDeliveredLine Items = new GoodsDeliveredLine();
            try
            {
                Items = await _context.GoodsDeliveredLine.Where(q => q.GoodsDeliveredLinedId == GoodsDeliveredLineId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{GoodsDeliveredId}")]
        public async Task<IActionResult> GetGoodsReceivedLineByGoodsDeliveredId(Int64 GoodsDeliveredId)
        {
            List<GoodsDeliveredLine> Items = new List<GoodsDeliveredLine>();
            try
            {
                Items = await _context.GoodsDeliveredLine
                             .Where(q => q.GoodsDeliveredId == GoodsDeliveredId).ToListAsync();
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
        /// Inserta una nueva GoodsDeliveredLine
        /// </summary>
        /// <param name="_GoodsDeliveredLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<GoodsDeliveredLine>> Insert([FromBody]GoodsDeliveredLine _GoodsDeliveredLine)
        {
            GoodsDeliveredLine _GoodsDeliveredLineq = new GoodsDeliveredLine();
            try
            {
                _GoodsDeliveredLineq = _GoodsDeliveredLine;
                _context.GoodsDeliveredLine.Add(_GoodsDeliveredLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_GoodsDeliveredLineq));
        }

        /// <summary>
        /// Actualiza la GoodsDeliveredLine
        /// </summary>
        /// <param name="_GoodsDeliveredLine"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<GoodsDeliveredLine>> Update([FromBody]GoodsDeliveredLine _GoodsDeliveredLine)
        {
            GoodsDeliveredLine _GoodsDeliveredLineq = _GoodsDeliveredLine;
            try
            {
                _GoodsDeliveredLineq = await (from c in _context.GoodsDeliveredLine
                                 .Where(q => q.GoodsDeliveredLinedId == _GoodsDeliveredLine.GoodsDeliveredLinedId)
                                              select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_GoodsDeliveredLineq).CurrentValues.SetValues((_GoodsDeliveredLine));

                //_context.GoodsDeliveredLine.Update(_GoodsDeliveredLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_GoodsDeliveredLineq));
        }

        /// <summary>
        /// Elimina una GoodsDeliveredLine       
        /// </summary>
        /// <param name="_GoodsDeliveredLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]GoodsDeliveredLine _GoodsDeliveredLine)
        {
            GoodsDeliveredLine _GoodsDeliveredLineq = new GoodsDeliveredLine();
            try
            {
                _GoodsDeliveredLineq = _context.GoodsDeliveredLine
                .Where(x => x.GoodsDeliveredLinedId == (Int64)_GoodsDeliveredLine.GoodsDeliveredLinedId)
                .FirstOrDefault();

                _context.GoodsDeliveredLine.Remove(_GoodsDeliveredLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_GoodsDeliveredLineq));

        }







    }
}