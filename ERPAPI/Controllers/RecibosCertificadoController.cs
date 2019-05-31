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
    [Route("api/RecibosCertificado")]
    [ApiController]
    public class RecibosCertificadoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public RecibosCertificadoController(ILogger<RecibosCertificadoController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de RecibosCertificadoes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetRecibosCertificado()
        {
            List<RecibosCertificado> Items = new List<RecibosCertificado>();
            try
            {
                Items = await _context.RecibosCertificado.ToListAsync();
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
        /// Obtiene los Datos de la RecibosCertificado por medio del Id enviado.
        /// </summary>
        /// <param name="IdReciboCertificado"></param>
        /// <returns></returns>
        [HttpGet("[action]/{IdReciboCertificado}")]
        public async Task<IActionResult> GetRecibosCertificadoById(Int64 IdReciboCertificado)
        {
            RecibosCertificado Items = new RecibosCertificado();
            try
            {
                Items = await _context.RecibosCertificado.Where(q => q.IdReciboCertificado == IdReciboCertificado).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }


        /// <summary>
        /// Inserta una nueva RecibosCertificado
        /// </summary>
        /// <param name="_RecibosCertificado"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<RecibosCertificado>> Insert([FromBody]RecibosCertificado _RecibosCertificado)
        {
            RecibosCertificado _RecibosCertificadoq = new RecibosCertificado();
            try
            {
                _RecibosCertificadoq = _RecibosCertificado;
                _context.RecibosCertificado.Add(_RecibosCertificadoq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_RecibosCertificadoq);
        }

        /// <summary>
        /// Actualiza la RecibosCertificado
        /// </summary>
        /// <param name="_RecibosCertificado"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<RecibosCertificado>> Update([FromBody]RecibosCertificado _RecibosCertificado)
        {
            RecibosCertificado _RecibosCertificadoq = _RecibosCertificado;
            try
            {
                _RecibosCertificadoq = await (from c in _context.RecibosCertificado
                                 .Where(q => q.IdReciboCertificado == _RecibosCertificado.IdReciboCertificado)
                                              select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_RecibosCertificadoq).CurrentValues.SetValues((_RecibosCertificado));

                //_context.RecibosCertificado.Update(_RecibosCertificadoq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_RecibosCertificadoq);
        }

        /// <summary>
        /// Elimina una RecibosCertificado       
        /// </summary>
        /// <param name="_RecibosCertificado"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]RecibosCertificado _RecibosCertificado)
        {
            RecibosCertificado _RecibosCertificadoq = new RecibosCertificado();
            try
            {
                _RecibosCertificadoq = _context.RecibosCertificado
                .Where(x => x.IdReciboCertificado == (Int64)_RecibosCertificado.IdReciboCertificado)
                .FirstOrDefault();

                _context.RecibosCertificado.Remove(_RecibosCertificadoq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_RecibosCertificadoq);

        }







    }
}