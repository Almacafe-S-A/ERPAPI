using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[Produces("application/json")]
    [Route("api/MantenimientoImpacto")]
    public class MantenimientoImpactoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public MantenimientoImpactoController(ILogger<MantenimientoImpactoController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }



        /// <summary>
        /// Obtiene el listado de Mantenimiento de Impacto.
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<ActionResult<MantenimientoImpacto>> GetMantenimientoImpacto()
        {
            List<MantenimientoImpacto> Items = new List<MantenimientoImpacto>();
            try
            {
                Items = await _context.MantenimientoImpacto.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }



            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Obtiene los datos de MantenimientoImpacto con el id enviado
        /// </summary>
        /// <param name="MantenimientoImpactoId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{MantenimientoImpactoId}")]
        public async Task<ActionResult<Currency>> GetMantenimientoImpactoById(int MantenimientoImpactoId)
        {
            MantenimientoImpacto Items = new MantenimientoImpacto();
            try
            {
                Items = await _context.MantenimientoImpacto.Where(q => q.MantenimientoImpactoId == MantenimientoImpactoId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }



            return Ok(Items);
        }



        /// <summary>
        /// Inserta Mantenimiento de Impacto
        /// </summary>
        /// <param name="_MantenimientoImpacto"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<MantenimientoImpacto>> Insert([FromBody]MantenimientoImpacto _MantenimientoImpacto)
        {
            MantenimientoImpacto mantenimientoImpacto = _MantenimientoImpacto;
            try
            {
                _context.MantenimientoImpacto.Add(mantenimientoImpacto);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(mantenimientoImpacto));
        }

        /// <summary>
        /// Actualiza Mantenimiento de Impacto
        /// </summary>
        /// <param name="_MantenimientoImpacto"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody]MantenimientoImpacto _MantenimientoImpacto)
        {

            try
            {
                MantenimientoImpacto mantenimiento = (from c in _context.MantenimientoImpacto
                                       .Where(q => q.MantenimientoImpactoId == _MantenimientoImpacto.MantenimientoImpactoId)
                                      select c
                                      ).FirstOrDefault();

                _MantenimientoImpacto.FechaCreacion = mantenimiento.FechaCreacion;
                _MantenimientoImpacto.UsuarioCreacion = mantenimiento.UsuarioCreacion;

                _context.Entry(mantenimiento).CurrentValues.SetValues((_MantenimientoImpacto));
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_MantenimientoImpacto));
        }

        /// <summary>
        /// Elimina un Mantenimiento de Impacto       
        /// </summary>
        /// <param name="_MantenimientoImpacto"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<MantenimientoImpacto>> Delete([FromBody]MantenimientoImpacto _MantenimientoImpacto)
        {
            MantenimientoImpacto mantenimiento = new MantenimientoImpacto();
            try
            {
                mantenimiento = _context.MantenimientoImpacto
                .Where(x => x.MantenimientoImpactoId == (Int64)_MantenimientoImpacto.MantenimientoImpactoId)
                .FirstOrDefault();

                _context.MantenimientoImpacto.Remove(mantenimiento);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(mantenimiento));

        }




    }
}