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
    [Route("api/Escala")]
    [ApiController]
    public class EscalaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public EscalaController(ILogger<EscalaController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Escala paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEscalaPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Escala> Items = new List<Escala>();
            try
            {
                var query = _context.Escala.AsQueryable();
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
        /// Obtiene el Listado de Escalaes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEscala()
        {
            List<Escala> Items = new List<Escala>();
            try
            {
                Items = await _context.Escala.ToListAsync();
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
        /// Obtiene los Datos de la Escala por medio del Id enviado.
        /// </summary>
        /// <param name="IdEscala"></param>
        /// <returns></returns>
        [HttpGet("[action]/{IdEscala}")]
        public async Task<IActionResult> GetEscalaById(Int64 IdEscala)
        {
            Escala Items = new Escala();
            try
            {
                Items = await _context.Escala.Where(q => q.IdEscala == IdEscala).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }


        /// <summary>
        /// Inserta una nueva Escala
        /// </summary>
        /// <param name="_Escala"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Escala>> Insert([FromBody]Escala _Escala)
        {
            Escala _Escalaq = new Escala();
            try
            {
                _Escalaq = _Escala;
                _context.Escala.Add(_Escalaq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_Escalaq);
        }

        /// <summary>
        /// Actualiza la Escala
        /// </summary>
        /// <param name="_Escala"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Escala>> Update([FromBody]Escala _Escala)
        {
            Escala _Escalaq = _Escala;
            try
            {
                _Escalaq = await (from c in _context.Escala
                                 .Where(q => q.IdEscala == _Escala.IdEscala)
                                  select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_Escalaq).CurrentValues.SetValues((_Escala));

                //_context.Escala.Update(_Escalaq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_Escalaq);
        }

        /// <summary>
        /// Elimina una Escala       
        /// </summary>
        /// <param name="_Escala"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Escala _Escala)
        {
            Escala _Escalaq = new Escala();
            try
            {
                _Escalaq = _context.Escala
                .Where(x => x.IdEscala == (Int64)_Escala.IdEscala)
                .FirstOrDefault();

                _context.Escala.Remove(_Escalaq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_Escalaq);

        }







    }
}