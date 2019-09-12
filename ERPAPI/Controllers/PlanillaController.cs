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
    public class PlanillaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public PlanillaController(ILogger<PlanillaController> logger, ApplicationDbContext context)
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
            List<Planilla> Items = new List<Planilla>();
            try
            {
                Items = await _context.Planilla.ToListAsync();
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
            Planilla Items = new Planilla();
            try
            {
                Items = await _context.Planilla.Where(q => q.IdPlanilla == Id).FirstOrDefaultAsync();
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
        public async Task<ActionResult<Planilla>> Insert([FromBody]Planilla _Planilla)
        {
            Planilla _Planillaq = new Planilla();
            try
            {
                _Planillaq = _Planilla;
                _context.Planilla.Add(_Planillaq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Planillaq));
        }

        /// <summary>
        /// Actualiza la Planilla
        /// </summary>
        /// <param name="_Planilla"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Planilla>> Update([FromBody]Planilla _Planilla)
        {
            Planilla _Planillaq = _Planilla;
            try
            {
                _Planillaq = await (from c in _context.Planilla
                                 .Where(q => q.IdPlanilla == _Planilla.IdPlanilla)
                                    select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_Planillaq).CurrentValues.SetValues((_Planilla));


                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Planillaq));
        }




    }
}