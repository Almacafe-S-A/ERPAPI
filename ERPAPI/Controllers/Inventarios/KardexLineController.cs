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
    [Route("api/KardexLine")]
    [ApiController]
    public class KardexLineController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public KardexLineController(ILogger<KardexLineController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de KardexLinees 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetKardexLine()
        {
            List<KardexLine> Items = new List<KardexLine>();
            try
            {
                Items = await _context.KardexLine.ToListAsync();
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
        /// Obtiene los Datos de la KardexLine por medio del Id enviado.
        /// </summary>
        /// <param name="KardexLineId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{KardexLineId}")]
        public async Task<IActionResult> GetKardexLineById(Int64 KardexLineId)
        {
            KardexLine Items = new KardexLine();
            try
            {
                Items = await _context.KardexLine.Where(q => q.KardexLineId == KardexLineId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }


        /// <summary>
        /// Inserta una nueva KardexLine
        /// </summary>
        /// <param name="_KardexLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<KardexLine>> Insert([FromBody]KardexLine _KardexLine)
        {
            KardexLine _KardexLineq = new KardexLine();
            try
            {
                _KardexLineq = _KardexLine;
                _context.KardexLine.Add(_KardexLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_KardexLineq));
        }

        /// <summary>
        /// Actualiza la KardexLine
        /// </summary>
        /// <param name="_KardexLine"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<KardexLine>> Update([FromBody]KardexLine _KardexLine)
        {
            KardexLine _KardexLineq = _KardexLine;
            try
            {
                _KardexLineq = await (from c in _context.KardexLine
                                 .Where(q => q.KardexLineId == _KardexLine.KardexLineId)
                                      select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_KardexLineq).CurrentValues.SetValues((_KardexLine));

                //_context.KardexLine.Update(_KardexLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_KardexLineq));
        }

        /// <summary>
        /// Elimina una KardexLine       
        /// </summary>
        /// <param name="_KardexLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]KardexLine _KardexLine)
        {
            KardexLine _KardexLineq = new KardexLine();
            try
            {
                _KardexLineq = _context.KardexLine
                .Where(x => x.KardexLineId == (Int64)_KardexLine.KardexLineId)
                .FirstOrDefault();

                _context.KardexLine.Remove(_KardexLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_KardexLineq));

        }







    }
}