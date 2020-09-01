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
    public class FormulasConceptoController : ControllerBase
    {
       
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public FormulasConceptoController(ILogger<FormulasConceptoController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de FormulasConceptos paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetFormulasConceptosPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<FormulasConcepto> Items = new List<FormulasConcepto>();
            try
            {
                var query = _context.FormulasConcepto.AsQueryable();
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



        // GET: api/FormulasConcepto
        [HttpGet("[action]")]
        public async Task<IActionResult> GetFormulasConcepto()
        {
            List<FormulasConcepto> Items = new List<FormulasConcepto>();
            try
            {
                Items = await _context.FormulasConcepto.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        // api/FormulasConceptoGetFormulasConceptoById
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetFormulasConceptoById(int Id)
        {
            FormulasConcepto Items = new FormulasConcepto();
            try
            {
                Items = await _context.FormulasConcepto.Where(q => q.IdformulaConcepto.Equals(Id)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<FormulasConcepto>> Insert([FromBody]FormulasConcepto payload)
        {
            FormulasConcepto FormulasConcepto = payload;

            try
            {
                _context.FormulasConcepto.Add(FormulasConcepto);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(FormulasConcepto));
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<FormulasConcepto>> Update([FromBody]FormulasConcepto _FormulasConcepto)
        {

            try
            {
                FormulasConcepto FormulasConceptoq = (from c in _context.FormulasConcepto
                   .Where(q => q.IdConcepto == _FormulasConcepto.IdConcepto)
                                                select c
                     ).FirstOrDefault();

                _FormulasConcepto.FechaCreacion = FormulasConceptoq.FechaCreacion;
                _FormulasConcepto.UsuarioCreacion = FormulasConceptoq.UsuarioCreacion;

                _context.Entry(FormulasConceptoq).CurrentValues.SetValues((_FormulasConcepto));
                // _context.FormulasConcepto.Update(_FormulasConcepto);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_FormulasConcepto));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]FormulasConcepto payload)
        {
            FormulasConcepto FormulasConcepto = new FormulasConcepto();
            try
            {
                FormulasConcepto = _context.FormulasConcepto
                .Where(x => x.IdConcepto == (int)payload.IdConcepto)
                .FirstOrDefault();
                _context.FormulasConcepto.Remove(FormulasConcepto);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(FormulasConcepto));

        }
    }
}
