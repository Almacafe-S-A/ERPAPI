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
using ERPAPI.Contexts;

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
        /// Obtiene los Datos de la tipo de acciondes de riesgo por medio del Id enviado.
        /// </summary>
        /// <param name="TipodeAccionderiesgoId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{TipodeAccionderiesgoId}")]
        public async Task<IActionResult> GetTipodeAccionderiesgoId(int TipodeAccionderiesgoId)
        {
            TipodeAccionderiesgo Items = new TipodeAccionderiesgo();
            try
            {
                Items = await _context.TipodeAccionderiesgo.Where(q => q.TipodeAccionderiesgoId == TipodeAccionderiesgoId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }




        [HttpGet("[action]/{Tipodeaccion}")]
        public async Task<IActionResult> GetTipodeAccionderiesgobyTipoAccion(string Tipodeaccion)
        {
            TipodeAccionderiesgo Items = new TipodeAccionderiesgo();
            try
            {
                Items = await _context.TipodeAccionderiesgo.Where(q => q.Tipodeaccion == Tipodeaccion).FirstOrDefaultAsync();
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

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

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
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody]TipodeAccionderiesgo _TipodeAccionderiesgo)
        {

            try
            {
                TipodeAccionderiesgo tipodeAccionderiesgo = (from c in _context.TipodeAccionderiesgo
                                       .Where(q => q.TipodeAccionderiesgoId == _TipodeAccionderiesgo.TipodeAccionderiesgoId)
                                                      select c
                                      ).FirstOrDefault();

                _TipodeAccionderiesgo.FechaCreacion = tipodeAccionderiesgo.FechaCreacion;
                _TipodeAccionderiesgo.UsuarioCreacion = tipodeAccionderiesgo.UsuarioCreacion;

                _context.Entry(tipodeAccionderiesgo).CurrentValues.SetValues((_TipodeAccionderiesgo));

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

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
        public async Task<IActionResult> Delete([FromBody]TipodeAccionderiesgo _TipodeAccionderiesgo)
        {
            TipodeAccionderiesgo tipodeAccionderiesgo = new TipodeAccionderiesgo();
            try
            {
                tipodeAccionderiesgo = _context.TipodeAccionderiesgo
                .Where(x => x.TipodeAccionderiesgoId == (Int64)_TipodeAccionderiesgo.TipodeAccionderiesgoId)
                .FirstOrDefault();

                _context.TipodeAccionderiesgo.Remove(tipodeAccionderiesgo);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

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