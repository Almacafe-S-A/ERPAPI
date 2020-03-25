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
    [Route("api/TipodeAccionderiesgo")]
    public class TipodeAccionderiesgoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public TipodeAccionderiesgoController(ILogger<TipodeAccionderiesgoController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }



        /// <summary>
        /// Obtiene el listado de TipodeAccionderiesgo
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<ActionResult<TipodeAccionderiesgo>> GetTipodeAccionderiesgo()
        {
            List<TipodeAccionderiesgo> Items = new List<TipodeAccionderiesgo>();
            try
            {
                Items = await _context.TipodeAccionderiesgo.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }



            return await Task.Run(() => Ok(Items));
        }



        /// <summary>
        /// Inserta TipodeAccionderiesgo
        /// </summary>
        /// <param name="_TipodeAccionderiesgo"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<TipodeAccionderiesgo>> Insert([FromBody]TipodeAccionderiesgo _TipodeAccionderiesgo)
        {
            TipodeAccionderiesgo tipodeAccionderiesgo = _TipodeAccionderiesgo;
            try
            {
                _context.TipodeAccionderiesgo.Add(_TipodeAccionderiesgo);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(tipodeAccionderiesgo));
        }

        /// <summary>
        /// Actualiza TipodeAccionderiesgo
        /// </summary>
        /// <param name="_TipodeAccionderiesgo"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<TipodeAccionderiesgo>> Update([FromBody]TipodeAccionderiesgo _TipodeAccionderiesgo)
        {

            try
            {
                TipodeAccionderiesgo tipodeAccionderiesgo = (from c in _context.TipodeAccionderiesgo
                                       .Where(q => q.TipodeAccionderiesgoId == _TipodeAccionderiesgo.TipodeAccionderiesgoId)
                                                      select c
                                      ).FirstOrDefault();

                _TipodeAccionderiesgo.FechaCreacion = tipodeAccionderiesgo.FechaCreacion;
                _TipodeAccionderiesgo.UsuarioCreacion = tipodeAccionderiesgo.UsuarioCreacion;

                _context.Entry(tipodeAccionderiesgo).CurrentValues.SetValues((tipodeAccionderiesgo));
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_TipodeAccionderiesgo));
        }

        /// <summary>
        /// Elimina un Tipodeaccionderiesgo      
        /// </summary>
        /// <param name="_TipodeAccionderiesgo"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<TipodeAccionderiesgo>> Delete([FromBody]TipodeAccionderiesgo _TipodeAccionderiesgo)
        {
            TipodeAccionderiesgo tipodeAccionderiesgo = new TipodeAccionderiesgo();
            try
            {
                tipodeAccionderiesgo = _context.TipodeAccionderiesgo
                .Where(x => x.TipodeAccionderiesgoId == (Int64)_TipodeAccionderiesgo.TipodeAccionderiesgoId)
                .FirstOrDefault();

                _context.TipodeAccionderiesgo.Remove(tipodeAccionderiesgo);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(tipodeAccionderiesgo));

        }




    }
}