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
    [Route("api/EndososTalon")]
    [ApiController]
    public class EndososTalonController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public EndososTalonController(ILogger<EndososTalonController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de EndososTalon paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEndososTalonPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<EndososTalon> Items = new List<EndososTalon>();
            try
            {
                var query = _context.EndososTalon.AsQueryable();
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

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Obtiene el Listado de EndososTalones 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEndososTalon()
        {
            List<EndososTalon> Items = new List<EndososTalon>();
            try
            {
                Items = await _context.EndososTalon.ToListAsync();
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
        /// Obtiene los Datos de la EndososTalon por medio del Id enviado.
        /// </summary>
        /// <param name="EndososTalonId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{EndososTalonId}")]
        public async Task<IActionResult> GetEndososTalonById(Int64 EndososTalonId)
        {
            EndososTalon Items = new EndososTalon();
            try
            {
                Items = await _context.EndososTalon.Where(q => q.EndososTalonId == EndososTalonId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }


        /// <summary>
        /// Inserta una nueva EndososTalon
        /// </summary>
        /// <param name="_EndososTalon"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<EndososTalon>> Insert([FromBody]EndososTalon _EndososTalon)
        {
            EndososTalon _EndososTalonq = new EndososTalon();
            try
            {
                _EndososTalonq = _EndososTalon;
                _context.EndososTalon.Add(_EndososTalonq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_EndososTalonq);
        }

        /// <summary>
        /// Actualiza la EndososTalon
        /// </summary>
        /// <param name="_EndososTalon"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<EndososTalon>> Update([FromBody]EndososTalon _EndososTalon)
        {
            EndososTalon _EndososTalonq = _EndososTalon;
            try
            {
                _EndososTalonq = await (from c in _context.EndososTalon
                                 .Where(q => q.EndososTalonId == _EndososTalon.EndososTalonId)
                                        select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_EndososTalonq).CurrentValues.SetValues((_EndososTalon));

                //_context.EndososTalon.Update(_EndososTalonq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_EndososTalonq);
        }

        /// <summary>
        /// Elimina una EndososTalon       
        /// </summary>
        /// <param name="_EndososTalon"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]EndososTalon _EndososTalon)
        {
            EndososTalon _EndososTalonq = new EndososTalon();
            try
            {
                _EndososTalonq = _context.EndososTalon
                .Where(x => x.EndososTalonId == (Int64)_EndososTalon.EndososTalonId)
                .FirstOrDefault();

                _context.EndososTalon.Remove(_EndososTalonq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_EndososTalonq);

        }







    }
}