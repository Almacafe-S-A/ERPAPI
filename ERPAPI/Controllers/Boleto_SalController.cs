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
    [Route("api/Boleto_Sal")]
    [ApiController]
    public class Boleto_SalController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public Boleto_SalController(ILogger<Boleto_SalController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Boleto_Sales 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBoleto_Sal()
        {
            List<Boleto_Sal> Items = new List<Boleto_Sal>();
            try
            {
                Items = await _context.Boleto_Sal.ToListAsync();
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
        /// Obtiene los Datos de la Boleto_Sal por medio del Id enviado.
        /// </summary>
        /// <param name="clave_e"></param>
        /// <returns></returns>
        [HttpGet("[action]/{clave_e}")]
        public async Task<IActionResult> GetBoleto_SalById(Int64 clave_e)
        {
            Boleto_Sal Items = new Boleto_Sal();
            try
            {
                Items = await _context.Boleto_Sal.Where(q => q.clave_e == clave_e).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }


        /// <summary>
        /// Inserta una nueva Boleto_Sal
        /// </summary>
        /// <param name="_Boleto_Sal"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Boleto_Sal>> Insert([FromBody]Boleto_Sal _Boleto_Sal)
        {
            Boleto_Sal _Boleto_Salq = new Boleto_Sal();
            try
            {
                _Boleto_Salq = _Boleto_Sal;
                _context.Boleto_Sal.Add(_Boleto_Salq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_Boleto_Salq);
        }

        /// <summary>
        /// Actualiza la Boleto_Sal
        /// </summary>
        /// <param name="_Boleto_Sal"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Boleto_Sal>> Update([FromBody]Boleto_Sal _Boleto_Sal)
        {
            Boleto_Sal _Boleto_Salq = _Boleto_Sal;
            try
            {
                _Boleto_Salq = await (from c in _context.Boleto_Sal
                                 .Where(q => q.clave_e == _Boleto_Sal.clave_e)
                                      select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_Boleto_Salq).CurrentValues.SetValues((_Boleto_Sal));

                //_context.Boleto_Sal.Update(_Boleto_Salq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_Boleto_Salq);
        }

        /// <summary>
        /// Elimina una Boleto_Sal       
        /// </summary>
        /// <param name="_Boleto_Sal"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Boleto_Sal _Boleto_Sal)
        {
            Boleto_Sal _Boleto_Salq = new Boleto_Sal();
            try
            {
                _Boleto_Salq = _context.Boleto_Sal
                .Where(x => x.clave_e == (Int64)_Boleto_Sal.clave_e)
                .FirstOrDefault();

                _context.Boleto_Sal.Remove(_Boleto_Salq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_Boleto_Salq);

        }







    }
}