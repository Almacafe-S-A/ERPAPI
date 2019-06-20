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
    [Route("api/HoursWorked")]
    [ApiController]
    public class HoursWorkedController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public HoursWorkedController(ILogger<HoursWorkedController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de HoursWorkedes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetHoursWorked()
        {
            List<HoursWorked> Items = new List<HoursWorked>();
            try
            {
                Items = await _context.HoursWorked.ToListAsync();
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
        /// Obtiene los Datos de la HoursWorked por medio del Id enviado.
        /// </summary>
        /// <param name="IdHorastrabajadas"></param>
        /// <returns></returns>
        [HttpGet("[action]/{IdHorastrabajadas}")]
        public async Task<ActionResult<HoursWorked>> GetHoursWorkedById(Int64 IdHorastrabajadas)
        {
            HoursWorked Items = new HoursWorked();
            try
            {
                Items = await _context.HoursWorked.Where(q => q.IdHorastrabajadas == IdHorastrabajadas).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva HoursWorked
        /// </summary>
        /// <param name="_HoursWorked"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<HoursWorked>> Insert([FromBody]HoursWorked _HoursWorked)
        {
            HoursWorked _HoursWorkedq = new HoursWorked();
            try
            {
                _HoursWorkedq = _HoursWorked;
                _context.HoursWorked.Add(_HoursWorkedq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_HoursWorkedq));
        }

        /// <summary>
        /// Actualiza la HoursWorked
        /// </summary>
        /// <param name="_HoursWorked"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<HoursWorked>> Update([FromBody]HoursWorked _HoursWorked)
        {
            HoursWorked _HoursWorkedq = _HoursWorked;
            try
            {
                _HoursWorkedq = await (from c in _context.HoursWorked
                                 .Where(q => q.IdHorastrabajadas == _HoursWorked.IdHorastrabajadas)
                                       select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_HoursWorkedq).CurrentValues.SetValues((_HoursWorked));

                //_context.HoursWorked.Update(_HoursWorkedq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_HoursWorkedq));
        }

        /// <summary>
        /// Elimina una HoursWorked       
        /// </summary>
        /// <param name="_HoursWorked"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]HoursWorked _HoursWorked)
        {
            HoursWorked _HoursWorkedq = new HoursWorked();
            try
            {
                _HoursWorkedq = _context.HoursWorked
                .Where(x => x.IdHorastrabajadas == (Int64)_HoursWorked.IdHorastrabajadas)
                .FirstOrDefault();

                _context.HoursWorked.Remove(_HoursWorkedq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_HoursWorkedq));

        }







    }
}