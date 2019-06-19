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
    [Route("api/GoodsDelivered")]
    [ApiController]
    public class GoodsDeliveredController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public GoodsDeliveredController(ILogger<GoodsDeliveredController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de GoodsDeliveredes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetGoodsDelivered()
        {
            List<GoodsDelivered> Items = new List<GoodsDelivered>();
            try
            {
                Items = await _context.GoodsDelivered.ToListAsync();
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
        /// Obtiene los Datos de la GoodsDelivered por medio del Id enviado.
        /// </summary>
        /// <param name="GoodsDeliveredId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{GoodsDeliveredId}")]
        public async Task<IActionResult> GetGoodsDeliveredById(Int64 GoodsDeliveredId)
        {
            GoodsDelivered Items = new GoodsDelivered();
            try
            {
                Items = await _context.GoodsDelivered.Where(q => q.GoodsDeliveredId == GoodsDeliveredId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }


        /// <summary>
        /// Inserta una nueva GoodsDelivered
        /// </summary>
        /// <param name="_GoodsDelivered"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<GoodsDelivered>> Insert([FromBody]GoodsDelivered _GoodsDelivered)
        {
            GoodsDelivered _GoodsDeliveredq = new GoodsDelivered();
            try
            {
                _GoodsDeliveredq = _GoodsDelivered;
                _context.GoodsDelivered.Add(_GoodsDeliveredq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_GoodsDeliveredq);
        }

        /// <summary>
        /// Actualiza la GoodsDelivered
        /// </summary>
        /// <param name="_GoodsDelivered"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<GoodsDelivered>> Update([FromBody]GoodsDelivered _GoodsDelivered)
        {
            GoodsDelivered _GoodsDeliveredq = _GoodsDelivered;
            try
            {
                _GoodsDeliveredq = await (from c in _context.GoodsDelivered
                                 .Where(q => q.GoodsDeliveredId == _GoodsDelivered.GoodsDeliveredId)
                                          select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_GoodsDeliveredq).CurrentValues.SetValues((_GoodsDelivered));

                //_context.GoodsDelivered.Update(_GoodsDeliveredq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_GoodsDeliveredq);
        }

        /// <summary>
        /// Elimina una GoodsDelivered       
        /// </summary>
        /// <param name="_GoodsDelivered"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]GoodsDelivered _GoodsDelivered)
        {
            GoodsDelivered _GoodsDeliveredq = new GoodsDelivered();
            try
            {
                _GoodsDeliveredq = _context.GoodsDelivered
                .Where(x => x.GoodsDeliveredId == (Int64)_GoodsDelivered.GoodsDeliveredId)
                .FirstOrDefault();

                _context.GoodsDelivered.Remove(_GoodsDeliveredq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_GoodsDeliveredq);

        }







    }
}