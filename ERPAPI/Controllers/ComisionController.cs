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
    [Route("api/Comision")]
    [ApiController]
    public class ComisionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ComisionController(ILogger<ComisionController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }



        /// <summary>
        /// Obtiene el Listado de Bankes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetComision()
        {
            List<Comision> Items = new List<Comision>();
            try
            {
                Items = await _context.Comision.ToListAsync();
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
        /// Obtiene los Datos de la Bank por medio del Id enviado.
        /// </summary>
        /// <param name="ComisionId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{ComisionId}")]
        public async Task<IActionResult> GetComisionById(Int64 ComisionId)
        {
            Comision Items = new Comision();
            try
            {
                Items = await _context.Comision.Where(q => q.ComisionId == ComisionId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }
        /// <summary>
        /// Obtiene los Datos de la Bank por medio del Id enviado.
        /// </summary>
        /// <param name="ComisionName"></param>
        /// <returns></returns>
        [HttpGet("[action]/{ComisionName}")]
        public async Task<IActionResult> GetComisionByName(String ComisionName)
        {
            Comision Items = new Comision();
            try
            {
                Items = await _context.Comision.Where(q => q.Description == ComisionName).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }



        /// <summary>
        /// Obtiene los Datos de la Bank por medio del Id enviado.
        /// </summary>
        /// <param name="ComisionName"></param>
        /// <returns></returns>
        [HttpGet("[action]/{ComisionName}")]
        public async Task<IActionResult> GetComisionByName(String ComisionName)
        {
            Comision Items = new Comision();
            try
            {
                Items = await _context.Comision.Where(q => q.Description == ComisionName).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items)); 
        }
    



    /// <summary>
    /// Inserta una nueva Bank
    /// </summary>
    /// <param name="_Comision"></param>
    /// <returns></returns>
    [HttpPost("[action]")]
        public async Task<ActionResult<Comision>> Insert([FromBody]Comision _Comision)
        {
            Comision _Comisionq = new Comision();
            try
            {
                _Comisionq = _Comision;
                _context.Comision.Add(_Comisionq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Comisionq));
        }

        /// <summary>
        /// Actualiza la Bank
        /// </summary>
        /// <param name="_Comision"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Comision>> Update([FromBody]Comision _Comision)
        {
            Comision _Comisionq = _Comision;
            try
            {
                _Comisionq = await (from c in _context.Comision
                                 .Where(q => q.ComisionId == _Comision.ComisionId)
                                    select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_Comisionq).CurrentValues.SetValues((_Comision));


                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Comisionq));
        }

        /// <summary>
        /// Elimina una Bank       
        /// </summary>
        /// <param name="_Comision"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Comision _Comision)
        {
            Comision _Comisionq = new Comision();
            try
            {
                _Comisionq = _context.Comision
                .Where(x => x.ComisionId == (Int64)_Comision.ComisionId)
                .FirstOrDefault();

                _context.Comision.Remove(_Comisionq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Comisionq));

        }

    }
}