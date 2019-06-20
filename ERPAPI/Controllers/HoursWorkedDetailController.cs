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
    [Route("api/HoursWorkedDetail")]
    [ApiController]
    public class HoursWorkedDetailController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public HoursWorkedDetailController(ILogger<HoursWorkedDetailController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de HoursWorkedDetailes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetHoursWorkedDetail()
        {
            List<HoursWorkedDetail> Items = new List<HoursWorkedDetail>();
            try
            {
                Items = await _context.HoursWorkedDetail.ToListAsync();
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
        /// Obtiene los Datos de la HoursWorkedDetail por medio del Id enviado.
        /// </summary>
        /// <param name="IdDetallehorastrabajadas"></param>
        /// <returns></returns>
        [HttpGet("[action]/{IdDetallehorastrabajadas}")]
        public async Task<IActionResult> GetHoursWorkedDetailById(Int64 IdDetallehorastrabajadas)
        {
            HoursWorkedDetail Items = new HoursWorkedDetail();
            try
            {
                Items = await _context.HoursWorkedDetail.Where(q => q.IdDetallehorastrabajadas == IdDetallehorastrabajadas).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return Ok(Items);
        }


        /// <summary>
        /// Inserta una nueva HoursWorkedDetail
        /// </summary>
        /// <param name="_HoursWorkedDetail"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<HoursWorkedDetail>> Insert([FromBody]HoursWorkedDetail _HoursWorkedDetail)
        {
            HoursWorkedDetail _HoursWorkedDetailq = new HoursWorkedDetail();
            try
            {
                _HoursWorkedDetailq = _HoursWorkedDetail;
                _context.HoursWorkedDetail.Add(_HoursWorkedDetailq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_HoursWorkedDetailq));
        }

        /// <summary>
        /// Actualiza la HoursWorkedDetail
        /// </summary>
        /// <param name="_HoursWorkedDetail"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<HoursWorkedDetail>> Update([FromBody]HoursWorkedDetail _HoursWorkedDetail)
        {
            HoursWorkedDetail _HoursWorkedDetailq = _HoursWorkedDetail;
            try
            {
                _HoursWorkedDetailq = await (from c in _context.HoursWorkedDetail
                                 .Where(q => q.IdDetallehorastrabajadas == _HoursWorkedDetail.IdDetallehorastrabajadas)
                                             select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_HoursWorkedDetailq).CurrentValues.SetValues((_HoursWorkedDetail));

                //_context.HoursWorkedDetail.Update(_HoursWorkedDetailq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_HoursWorkedDetailq));
        }

        /// <summary>
        /// Elimina una HoursWorkedDetail       
        /// </summary>
        /// <param name="_HoursWorkedDetail"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]HoursWorkedDetail _HoursWorkedDetail)
        {
            HoursWorkedDetail _HoursWorkedDetailq = new HoursWorkedDetail();
            try
            {
                _HoursWorkedDetailq = _context.HoursWorkedDetail
                .Where(x => x.IdDetallehorastrabajadas == (Int64)_HoursWorkedDetail.IdDetallehorastrabajadas)
                .FirstOrDefault();

                _context.HoursWorkedDetail.Remove(_HoursWorkedDetailq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_HoursWorkedDetailq));

        }







    }
}