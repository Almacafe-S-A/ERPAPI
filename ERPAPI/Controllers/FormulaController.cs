using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormulaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public FormulaController(Microsoft.Extensions.Logging.ILogger<FormulaController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Formulas paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetFormulasPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Formula> Items = new List<Formula>();
            try
            {
                var query = _context.Formula.AsQueryable();
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


            return await Task.Run(() => Ok(Items));
        }



        // GET: api/Formula
        [HttpGet("[action]")]
        public async Task<IActionResult> GetFormula()
        {
            List<Formula> Items = new List<Formula>();
            try
            {
                Items = await _context.Formula.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        // api/FormulaGetFormulaById
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetFormulaById(int Id)
        {
            Formula Items = new Formula();
            try
            {
                Items = await _context.Formula.Where(q => q.IdFormula.Equals(Id)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Formula>> Insert([FromBody]Formula payload)
        {
            Formula Formula = payload;

            try
            {
                _context.Formula.Add(Formula);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Formula));
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<Formula>> Update([FromBody]Formula _Formula)
        {

            try
            {
                Formula Formulaq = (from c in _context.Formula
                   .Where(q => q.IdFormula == _Formula.IdFormula)
                                                      select c
                     ).FirstOrDefault();

                _Formula.FechaCreacion = Formulaq.FechaCreacion;
                _Formula.UsuarioCreacion = Formulaq.UsuarioCreacion;

                _context.Entry(Formulaq).CurrentValues.SetValues((_Formula));
                // _context.Formula.Update(_Formula);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Formula));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Formula _Formula)
        {
            Formula _Formulaq = new Formula();
            try
            {
                _Formulaq = _context.Formula
                .Where(x => x.IdFormula == (Int64)_Formula.IdFormula)
                .FirstOrDefault();
                _context.Formula.Remove(_Formulaq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Formulaq));

        }
    }
}
