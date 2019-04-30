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

namespace ERPAPI.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/NumeracionSAR")]
    [ApiController]
    public class NumeracionSARController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public NumeracionSARController(ILogger<NumeracionSARController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene las numeraciones de SAR       
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetNumeracion()
        {
            List<NumeracionSAR> Items = new List<NumeracionSAR>();
            try
            {
                Items = await _context.NumeracionSAR.ToListAsync();
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
        /// Obtiene la Numeracion por medio del Id enviado
        /// </summary>
        /// <param name="IdNumeracion"></param>
        /// <returns></returns>
        [HttpGet("[action]/{IdNumeracion}")]
        public async Task<ActionResult<NumeracionSAR>> GetNumeracionById(Int64 IdNumeracion )
        {
            NumeracionSAR Items = new NumeracionSAR();
            try
            {
                Items = await _context.NumeracionSAR.Where(q=>q.IdNumeracion== IdNumeracion).FirstOrDefaultAsync();
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
        /// Inserta numeracion SAR
        /// </summary>
        /// <param name="_NumeracionSAR"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<NumeracionSAR>> Insert([FromBody]NumeracionSAR _NumeracionSAR)
        {
            NumeracionSAR _NumeracionSARq = new NumeracionSAR();
            try
            {
                _NumeracionSARq = _NumeracionSAR;
                _context.NumeracionSAR.Add(_NumeracionSARq);
               await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_NumeracionSAR);
        }

        /// <summary>
        /// Actualiza la numeracion SAR
        /// </summary>
        /// <param name="_NumeracionSARq"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<NumeracionSAR>> Update([FromBody]NumeracionSAR _NumeracionSARq)
        {
            NumeracionSAR __NumeracionSARq = _NumeracionSARq;
            try
            {
                _context.NumeracionSAR.Update(_NumeracionSARq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(__NumeracionSARq);
        }

        /// <summary>
        /// Elimina la Numeración  
        /// </summary>
        /// <param name="__NumeracionSAR"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<NumeracionSAR>> Delete([FromBody]NumeracionSAR __NumeracionSAR)
        {
            NumeracionSAR __NumeracionSARq = new NumeracionSAR();
            try
            {
                __NumeracionSARq = _context.NumeracionSAR
                .Where(x => x.IdNumeracion== (int)__NumeracionSARq.IdNumeracion)
                .FirstOrDefault();
                _context.NumeracionSAR.Remove(__NumeracionSARq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(__NumeracionSARq);

        }







    }
}