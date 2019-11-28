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
    [Route("api/PeriodicidadPago")]
    [ApiController]
    public class PeriodicidadPagoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public PeriodicidadPagoController(ILogger<PeriodicidadPagoController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }



        /// <summary>
        /// Obtiene el Listado de Bankes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPeriodicidadPago()
        {
            List<PeriodicidadPago> Items = new List<PeriodicidadPago>();
            try
            {
                Items = await _context.PeriodicidadPago.ToListAsync();
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
        /// Obtiene los Datos de la Bank por medio del Id enviado.
        /// </summary>
        /// <param name="PeriodicidadId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{PeriodicidadId}")]
        public async Task<IActionResult> GetPeriodicidadPagoById(Int64 PeriodicidadId)
        {
            PeriodicidadPago Items = new PeriodicidadPago();
            try
            {
                Items = await _context.PeriodicidadPago.Where(q => q.PeriodicidadId == PeriodicidadId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{Nombre}")]
        public async Task<IActionResult> GetPeriodicidadPagoByNombre(String Nombre)
        {
            PeriodicidadPago Items = new PeriodicidadPago();
            try
            {
                Items = await _context.PeriodicidadPago.Where(q => q.Nombre == Nombre).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Inserta una nueva Bank
        /// </summary>
        /// <param name="_PeriodicidadPago"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<PeriodicidadPago>> Insert([FromBody]PeriodicidadPago _PeriodicidadPago)
        {
            PeriodicidadPago _PeriodicidadPagoq = new PeriodicidadPago();
            try
            {
                _PeriodicidadPagoq = _PeriodicidadPago;
                _context.PeriodicidadPago.Add(_PeriodicidadPagoq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_PeriodicidadPagoq));
        }

        /// <summary>
        /// Actualiza la Bank
        /// </summary>
        /// <param name="_PeriodicidadPago"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<PeriodicidadPago>> Update([FromBody]PeriodicidadPago _PeriodicidadPago)
        {
            PeriodicidadPago _PeriodicidadPagoq = _PeriodicidadPago;
            try
            {
                _PeriodicidadPagoq = await (from c in _context.PeriodicidadPago
                                 .Where(q => q.PeriodicidadId == _PeriodicidadPago.PeriodicidadId)
                                            select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_PeriodicidadPagoq).CurrentValues.SetValues((_PeriodicidadPago));


                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_PeriodicidadPagoq));
        }

        /// <summary>
        /// Elimina una Bank       
        /// </summary>
        /// <param name="_PeriodicidadPago"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]PeriodicidadPago _PeriodicidadPago)
        {
            PeriodicidadPago _PeriodicidadPagoq = new PeriodicidadPago();
            try
            {
                _PeriodicidadPagoq = _context.PeriodicidadPago
                .Where(x => x.PeriodicidadId == (Int64)_PeriodicidadPago.PeriodicidadId)
                .FirstOrDefault();

                _context.PeriodicidadPago.Remove(_PeriodicidadPagoq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_PeriodicidadPagoq));

        }

    }
}