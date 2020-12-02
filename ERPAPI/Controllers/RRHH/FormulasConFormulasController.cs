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
    public class FormulasConFormulasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public FormulasConFormulasController(Microsoft.Extensions.Logging.ILogger<FormulasConFormulasController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de FormulasConFormulass paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetFormulasConFormulassPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<FormulasConFormulas> Items = new List<FormulasConFormulas>();
            try
            {
                var query = _context.FormulasConFormulas.AsQueryable();
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



        // GET: api/FormulasConFormulas
        [HttpGet("[action]")]
        public async Task<IActionResult> GetFormulasConFormulas()
        {
            List<FormulasConFormulas> Items = new List<FormulasConFormulas>();
            try
            {
                Items = await _context.FormulasConFormulas.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        // api/FormulasConFormulasGetFormulasConFormulasById
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetFormulasConFormulasById(int Id)
        {
            FormulasConFormulas Items = new FormulasConFormulas();
            try
            {
                Items = await _context.FormulasConFormulas.Where(q => q.IdFormulaconformula.Equals(Id)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<FormulasConFormulas>> Insert([FromBody]FormulasConFormulas payload)
        {
            FormulasConFormulas FormulasConFormulas = payload;

            try
            {
                _context.FormulasConFormulas.Add(FormulasConFormulas);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(FormulasConFormulas));
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<FormulasConFormulas>> Update([FromBody]FormulasConFormulas _FormulasConFormulas)
        {

            try
            {
                FormulasConFormulas FormulasConFormulasq = (from c in _context.FormulasConFormulas
                   .Where(q => q.IdFormulaconformula == _FormulasConFormulas.IdFormulaconformula)
                                                      select c
                     ).FirstOrDefault();

                _FormulasConFormulas.FechaCreacion = FormulasConFormulasq.FechaCreacion;
                _FormulasConFormulas.UsuarioCreacion = FormulasConFormulasq.UsuarioCreacion;

                _context.Entry(FormulasConFormulasq).CurrentValues.SetValues((_FormulasConFormulas));
                // _context.FormulasConFormulas.Update(_FormulasConFormulas);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_FormulasConFormulas));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]FormulasConFormulas payload)
        {
            FormulasConFormulas FormulasConFormulas = new FormulasConFormulas();
            try
            {
                FormulasConFormulas = _context.FormulasConFormulas
                .Where(x => x.IdFormulaconformula == (int)payload.IdFormulaconformula)
                .FirstOrDefault();
                _context.FormulasConFormulas.Remove(FormulasConFormulas);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(FormulasConFormulas));

        }
    }
}
