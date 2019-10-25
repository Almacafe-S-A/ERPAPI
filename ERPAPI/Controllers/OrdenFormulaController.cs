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
    [Route("api/OrdenFormula")]
    [ApiController]
    public class OrdenFormulaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public OrdenFormulaController(ILogger<OrdenFormulaController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de OrdenFormula paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetOrdenFormulaPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<OrdenFormula> Items = new List<OrdenFormula>();
            try
            {
                var query = _context.OrdenFormula.AsQueryable();
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
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Obtiene el Listado de OrdenFormulaes 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetOrdenFormula()
        {
            List<OrdenFormula> Items = new List<OrdenFormula>();
            try
            {
                Items = await _context.OrdenFormula.ToListAsync();
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
        /// Obtiene los Datos de la OrdenFormula por medio del Id enviado.
        /// </summary>
        /// <param name="Idordenformula"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Idordenformula}")]
        public async Task<IActionResult> GetOrdenFormulaById(Int64 Idordenformula)
        {
            OrdenFormula Items = new OrdenFormula();
            try
            {
                Items = await _context.OrdenFormula.Where(q => q.Idordenformula == Idordenformula).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva OrdenFormula
        /// </summary>
        /// <param name="_OrdenFormula"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<OrdenFormula>> Insert([FromBody]OrdenFormula _OrdenFormula)
        {
            OrdenFormula _OrdenFormulaq = new OrdenFormula();
            try
            {
                _OrdenFormulaq = _OrdenFormula;
                _context.OrdenFormula.Add(_OrdenFormulaq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_OrdenFormulaq));
        }

        /// <summary>
        /// Actualiza la OrdenFormula
        /// </summary>
        /// <param name="_OrdenFormula"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<OrdenFormula>> Update([FromBody]OrdenFormula _OrdenFormula)
        {
            OrdenFormula _OrdenFormulaq = _OrdenFormula;
            try
            {
                _OrdenFormulaq = await (from c in _context.OrdenFormula
                                 .Where(q => q.Idordenformula == _OrdenFormula.Idordenformula)
                                        select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_OrdenFormulaq).CurrentValues.SetValues((_OrdenFormula));

                //_context.OrdenFormula.Update(_OrdenFormulaq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_OrdenFormulaq));
        }

        /// <summary>
        /// Elimina una OrdenFormula       
        /// </summary>
        /// <param name="_OrdenFormula"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]OrdenFormula _OrdenFormula)
        {
            OrdenFormula _OrdenFormulaq = new OrdenFormula();
            try
            {
                _OrdenFormulaq = _context.OrdenFormula
                .Where(x => x.Idordenformula == (Int64)_OrdenFormula.Idordenformula)
                .FirstOrDefault();

                _context.OrdenFormula.Remove(_OrdenFormulaq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_OrdenFormulaq));

        }







    }
}