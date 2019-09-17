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
    [Route("api/EndososBono")]
    [ApiController]
    public class EndososBonoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public EndososBonoController(ILogger<EndososBonoController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de EndososBono paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEndososBonoPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<EndososBono> Items = new List<EndososBono>();
            try
            {
                var query = _context.EndososBono.AsQueryable();
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
        /// Obtiene el Listado de EndososBonoes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEndososBono()
        {
            List<EndososBono> Items = new List<EndososBono>();
            try
            {
                Items = await _context.EndososBono.ToListAsync();
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
        /// Obtiene los Datos de la EndososBono por medio del Id enviado.
        /// </summary>
        /// <param name="EndososBonoId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{EndososBonoId}")]
        public async Task<IActionResult> GetEndososBonoById(Int64 EndososBonoId)
        {
            EndososBono Items = new EndososBono();
            try
            {
                Items = await _context.EndososBono.Where(q => q.EndososBonoId == EndososBonoId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }


        /// <summary>
        /// Inserta una nueva EndososBono
        /// </summary>
        /// <param name="_EndososBono"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<EndososBono>> Insert([FromBody]EndososBono _EndososBono)
        {
            EndososBono _EndososBonoq = new EndososBono();
            try
            {
                _EndososBonoq = _EndososBono;
                _context.EndososBono.Add(_EndososBonoq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_EndososBonoq);
        }

        /// <summary>
        /// Actualiza la EndososBono
        /// </summary>
        /// <param name="_EndososBono"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<EndososBono>> Update([FromBody]EndososBono _EndososBono)
        {
            EndososBono _EndososBonoq = _EndososBono;
            try
            {
                _EndososBonoq = await (from c in _context.EndososBono
                                 .Where(q => q.EndososBonoId == _EndososBono.EndososBonoId)
                                       select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_EndososBonoq).CurrentValues.SetValues((_EndososBono));

                //_context.EndososBono.Update(_EndososBonoq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_EndososBonoq);
        }

        /// <summary>
        /// Elimina una EndososBono       
        /// </summary>
        /// <param name="_EndososBono"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]EndososBono _EndososBono)
        {
            EndososBono _EndososBonoq = new EndososBono();
            try
            {
                _EndososBonoq = _context.EndososBono
                .Where(x => x.EndososBonoId == (Int64)_EndososBono.EndososBonoId)
                .FirstOrDefault();

                _context.EndososBono.Remove(_EndososBonoq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_EndososBonoq);

        }







    }
}