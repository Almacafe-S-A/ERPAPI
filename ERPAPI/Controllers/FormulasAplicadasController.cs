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
    public class FormulasAplicadasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public FormulasAplicadasController(ILogger<FormulasAplicadasController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de FormulasAplicadass paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetFormulasAplicadassPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<FormulasAplicadas> Items = new List<FormulasAplicadas>();
            try
            {
                var query = _context.FormulasAplicadas.AsQueryable();
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



        // GET: api/FormulasAplicadas
        [HttpGet("[action]")]
        public async Task<IActionResult> GetFormulasAplicadas()
        {
            List<FormulasAplicadas> Items = new List<FormulasAplicadas>();
            try
            {
                Items = await _context.FormulasAplicadas.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{EmployeeId}")]
        public async Task<IActionResult> GetFormulasAplicadasByEmployeeId(Int64 EmployeeId)
        {
            List<FormulasAplicadas> Items = new List<FormulasAplicadas>();
            try
            {
                Items = await _context.FormulasAplicadas.Where(q => q.IdEmpleado == EmployeeId).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return Ok(Items);
        }
        // api/FormulasAplicadasGetFormulasAplicadasById
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetFormulasAplicadasById(int Id)
        {
            FormulasAplicadas Items = new FormulasAplicadas();
            try
            {
                Items = await _context.FormulasAplicadas.Where(q => q.IdFormulaAplicada.Equals(Id)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<FormulasAplicadas>> Insert([FromBody]FormulasAplicadas payload)
        {
            FormulasAplicadas FormulasAplicadas = payload;

            try
            {
                _context.FormulasAplicadas.Add(FormulasAplicadas);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(FormulasAplicadas));
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<FormulasAplicadas>> Update([FromBody]FormulasAplicadas _FormulasAplicadas)
        {

            try
            {
                FormulasAplicadas FormulasAplicadasq = (from c in _context.FormulasAplicadas
                   .Where(q => q.IdFormulaAplicada == _FormulasAplicadas.IdFormulaAplicada)
                                                      select c
                     ).FirstOrDefault();

                _FormulasAplicadas.FechaCreacion = FormulasAplicadasq.FechaCreacion;
                _FormulasAplicadas.UsuarioCreacion = FormulasAplicadasq.UsuarioCreacion;

                _context.Entry(FormulasAplicadasq).CurrentValues.SetValues((_FormulasAplicadas));
                // _context.FormulasAplicadas.Update(_FormulasAplicadas);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_FormulasAplicadas));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]FormulasAplicadas payload)
        {
            FormulasAplicadas FormulasAplicadas = new FormulasAplicadas();
            try
            {
                FormulasAplicadas = _context.FormulasAplicadas
                .Where(x => x.IdFormulaAplicada == (int)payload.IdFormulaAplicada)
                .FirstOrDefault();
                _context.FormulasAplicadas.Remove(FormulasAplicadas);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(FormulasAplicadas));

        }
    }
}
