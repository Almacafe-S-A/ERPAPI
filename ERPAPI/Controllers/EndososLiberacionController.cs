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
    [Route("api/EndososLiberacion")]
    [ApiController]
    public class EndososLiberacionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public EndososLiberacionController(ILogger<EndososLiberacionController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de EndososLiberacion paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEndososLiberacionPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<EndososLiberacion> Items = new List<EndososLiberacion>();
            try
            {
                var query = _context.EndososLiberacion.AsQueryable();
                var totalRegistro = query.Count();

                Items = await query
                   .Skip(cantidadDeRegistros * (numeroDePagina - 1))
                   .Take(cantidadDeRegistros)
                    .ToListAsync();

                Response.Headers["X-Total-Registros"] = totalRegistro.ToString();
                Response.Headers["X-Cantidad-Paginas"] = ((Int64)Math.Ceiling((double)totalRegistro / cantidadDeRegistros)).ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

           
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene el Listado de EndososLiberaciones 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEndososLiberacion()
        {
            List<EndososLiberacion> Items = new List<EndososLiberacion>();
            try
            {
                Items = await _context.EndososLiberacion.ToListAsync();
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
        /// Obtiene los Datos de la EndososLiberacion por medio del Id enviado.
        /// </summary>
        /// <param name="EndososLiberacionId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{EndososLiberacionId}")]
        public async Task<IActionResult> GetEndososLiberacionById(Int64 EndososLiberacionId)
        {
            EndososLiberacion Items = new EndososLiberacion();
            try
            {
                Items = await _context.EndososLiberacion.Where(q => q.EndososLiberacionId == EndososLiberacionId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }


        /// <summary>
        /// Inserta una nueva EndososLiberacion
        /// </summary>
        /// <param name="_EndososLiberacion"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<EndososLiberacion>> Insert([FromBody]EndososLiberacion _EndososLiberacion)
        {
            EndososLiberacion _EndososLiberacionq = new EndososLiberacion();
            try
            {
                _EndososLiberacionq = _EndososLiberacion;
                _context.EndososLiberacion.Add(_EndososLiberacionq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_EndososLiberacionq);
        }

        /// <summary>
        /// Actualiza la EndososLiberacion
        /// </summary>
        /// <param name="_EndososLiberacion"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<EndososLiberacion>> Update([FromBody]EndososLiberacion _EndososLiberacion)
        {
            EndososLiberacion _EndososLiberacionq = _EndososLiberacion;
            try
            {
                _EndososLiberacionq = await (from c in _context.EndososLiberacion
                                 .Where(q => q.EndososLiberacionId == _EndososLiberacion.EndososLiberacionId)
                                             select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_EndososLiberacionq).CurrentValues.SetValues((_EndososLiberacion));

                //_context.EndososLiberacion.Update(_EndososLiberacionq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_EndososLiberacionq);
        }

        /// <summary>
        /// Elimina una EndososLiberacion       
        /// </summary>
        /// <param name="_EndososLiberacion"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]EndososLiberacion _EndososLiberacion)
        {
            EndososLiberacion _EndososLiberacionq = new EndososLiberacion();
            try
            {
                _EndososLiberacionq = _context.EndososLiberacion
                .Where(x => x.EndososLiberacionId == (Int64)_EndososLiberacion.EndososLiberacionId)
                .FirstOrDefault();

                _context.EndososLiberacion.Remove(_EndososLiberacionq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_EndososLiberacionq);

        }







    }
}