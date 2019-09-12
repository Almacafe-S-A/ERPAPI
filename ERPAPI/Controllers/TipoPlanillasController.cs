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
    [Route("api/Planilla")]
    [ApiController]
    public class TipoPlanillasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public TipoPlanillasController(ILogger<TipoPlanillasController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Planillas
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPlanilla()
        {
            List<TipoPlanillas> Items = new List<TipoPlanillas>();
            try
            {
                Items = await _context.TipoPlanillas.ToListAsync();
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
        /// Obtiene los Datos de la Planilla por medio del Id enviado.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetPlanillaById(Int64 Id)
        {
            TipoPlanillas Items = new TipoPlanillas();
            try
            {
                Items = await _context.TipoPlanillas.Where(q => q.IdTipoPlanilla == Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva Planilla
        /// </summary>
        /// <param name="_Planilla"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<TipoPlanillas>> Insert([FromBody]TipoPlanillas _TipoPlanillas)
        {
            TipoPlanillas _TipoPlanillasq = new TipoPlanillas();
            try
            {
                _TipoPlanillasq = _TipoPlanillas;
                _context.TipoPlanillas.Add(_TipoPlanillasq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_TipoPlanillasq));
        }

        /// <summary>
        /// Actualiza la Planilla
        /// </summary>
        /// <param name="_TipoPlanillas"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<TipoPlanillas>> Update([FromBody]TipoPlanillas _TipoPlanillas)
        {
            TipoPlanillas _TipoPlanillasq = _TipoPlanillas;
            try
            {
                _TipoPlanillasq = await (from c in _context.TipoPlanillas
                                 .Where(q => q.IdTipoPlanilla == _TipoPlanillas.IdTipoPlanilla)
                                         select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_TipoPlanillasq).CurrentValues.SetValues((_TipoPlanillas));


                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_TipoPlanillasq));
        }




    }
}