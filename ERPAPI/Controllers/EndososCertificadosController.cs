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
    [Route("api/EndososCertificados")]
    [ApiController]
    public class EndososCertificadosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public EndososCertificadosController(ILogger<EndososCertificadosController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de EndososCertificadoses 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEndososCertificados()
        {
            List<EndososCertificados> Items = new List<EndososCertificados>();
            try
            {
                Items = await _context.EndososCertificados.ToListAsync();
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
        /// Obtiene los Datos de la EndososCertificados por medio del Id enviado.
        /// </summary>
        /// <param name="EndososCertificadosId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{EndososCertificadosId}")]
        public async Task<IActionResult> GetEndososCertificadosById(Int64 EndososCertificadosId)
        {
            EndososCertificados Items = new EndososCertificados();
            try
            {
                Items = await _context.EndososCertificados.Where(q => q.EndososCertificadosId == EndososCertificadosId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }


        /// <summary>
        /// Inserta una nueva EndososCertificados
        /// </summary>
        /// <param name="_EndososCertificados"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<EndososCertificados>> Insert([FromBody]EndososCertificados _EndososCertificados)
        {
            EndososCertificados _EndososCertificadosq = new EndososCertificados();
            try
            {
                _EndososCertificadosq = _EndososCertificados;
                _context.EndososCertificados.Add(_EndososCertificadosq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_EndososCertificadosq);
        }

        /// <summary>
        /// Actualiza la EndososCertificados
        /// </summary>
        /// <param name="_EndososCertificados"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<EndososCertificados>> Update([FromBody]EndososCertificados _EndososCertificados)
        {
            EndososCertificados _EndososCertificadosq = _EndososCertificados;
            try
            {
                _EndososCertificadosq = await (from c in _context.EndososCertificados
                                 .Where(q => q.EndososCertificadosId == _EndososCertificados.EndososCertificadosId)
                                               select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_EndososCertificadosq).CurrentValues.SetValues((_EndososCertificados));

                //_context.EndososCertificados.Update(_EndososCertificadosq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_EndososCertificadosq);
        }

        /// <summary>
        /// Elimina una EndososCertificados       
        /// </summary>
        /// <param name="_EndososCertificados"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]EndososCertificados _EndososCertificados)
        {
            EndososCertificados _EndososCertificadosq = new EndososCertificados();
            try
            {
                _EndososCertificadosq = _context.EndososCertificados
                .Where(x => x.EndososCertificadosId == (Int64)_EndososCertificados.EndososCertificadosId)
                .FirstOrDefault();

                _context.EndososCertificados.Remove(_EndososCertificadosq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_EndososCertificadosq);

        }







    }
}