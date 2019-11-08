using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using EFCore.BulkExtensions;
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Conciliacion")]
    [ApiController]
    public class ConciliacionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        /*public DimensionsController(ApplicationDbContext context)
        {
            _context = context;
        }*/
        public ConciliacionController(ILogger<Conciliacion> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Conciliacion"></param>
        /// <returns></returns>

        /// <summary>
        /// Inserta una nueva Conciliacion
        /// </summary>
        /// <param name="_Conciliacion"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Conciliacion>> Insert([FromBody]Conciliacion _Conciliacion)
        {
            Conciliacion _Conciliacionq = new Conciliacion();
            try
            {
                _Conciliacionq = _Conciliacion;
                _context.Conciliacion.Add(_Conciliacionq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Conciliacionq));
        }

        /// <summary>
        /// Actualiza la Conciliacion
        /// </summary>
        /// <param name="_Conciliacion"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Conciliacion>> Update([FromBody]Conciliacion _Conciliacion)
        {
            Conciliacion _Conciliacionq = _Conciliacion;
            try
            {
                _Conciliacionq = await (from c in _context.Conciliacion
                                 .Where(q => q.ConciliacionId == _Conciliacion.ConciliacionId)
                                            select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_Conciliacionq).CurrentValues.SetValues((_Conciliacion));

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Conciliacionq));
        }

        /// <summary>
        /// Elimina una Conciliacion       
        /// </summary>
        /// <param name="_Conciliacion"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Conciliacion _Conciliacion)
        {
            Conciliacion _Conciliacionq = new Conciliacion();
            try
            {
                _Conciliacionq = _context.Conciliacion
                .Where(x => x.ConciliacionId == (Int64)_Conciliacion.ConciliacionId)
                .FirstOrDefault();

                _context.Conciliacion.Remove(_Conciliacionq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Conciliacionq));

        }
    }
}